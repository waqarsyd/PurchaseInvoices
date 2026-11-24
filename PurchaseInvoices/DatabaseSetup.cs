using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PurchaseInvoices
{
    internal sealed class TableDefinition
    {
        public TableDefinition(string name, params string[] columnDefinitions)
        {
            Name = name;
            Definitions = columnDefinitions;
            Columns = columnDefinitions.Select(d => d.Split(' ')[0]).ToArray();
            CreateSql = $"CREATE TABLE dbo.{name} ({string.Join(", ", columnDefinitions)})";
        }

        public string Name { get; }
        public string[] Columns { get; }
        public string[] Definitions { get; }
        public string CreateSql { get; }

        public string Definition(string column) => Definitions[Array.FindIndex(Columns, c => string.Equals(c, column, StringComparison.OrdinalIgnoreCase))];

        public bool CanAdd(string column)
        {
            string definition = " " + Definition(column).ToUpperInvariant() + " ";
            bool required = definition.Contains(" NOT NULL ");
            bool keyOrIdentity = definition.Contains(" PRIMARY KEY ") || definition.Contains(" IDENTITY");
            return !keyOrIdentity && (!required || definition.Contains(" DEFAULT "));
        }
    }

    internal sealed class MissingColumn
    {
        public MissingColumn(string table, string column)
        {
            Table = table;
            Column = column;
        }

        public string Table { get; }
        public string Column { get; }
        public override string ToString() => Table + "." + Column;
    }

    internal sealed class DatabaseStatus
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public bool DatabaseExists { get; set; }
        public bool RenameOldTransId { get; set; }
        public List<string> MissingTables { get; } = new List<string>();
        public List<MissingColumn> MissingColumns { get; } = new List<MissingColumn>();
        public List<string> Problems { get; } = new List<string>();

        public bool IsReady => DatabaseExists && !RenameOldTransId && MissingTables.Count == 0 && MissingColumns.Count == 0 && Problems.Count == 0;

        public List<string> GetPlannedSteps()
        {
            List<string> steps = new List<string>();
            if (!DatabaseExists)
            {
                steps.Add($"Create database '{Database}'");
            }
            if (RenameOldTransId)
            {
                steps.Add("Rename column PURCH_HEADER.TRASN_ID to TRANS_ID");
            }
            foreach (string table in MissingTables)
            {
                steps.Add($"Create table {table}");
            }
            foreach (MissingColumn column in MissingColumns)
            {
                steps.Add($"Add column {column}");
            }
            return steps;
        }
    }

    internal static class DatabaseSetup
    {
        private static readonly string[] SystemDatabases = { "master", "model", "msdb", "tempdb" };

        private static readonly TableDefinition[] Tables =
        {
            new TableDefinition("CUSTOMER_MASTER",
                "CUSTID VARCHAR(6) NOT NULL PRIMARY KEY",
                "CUST_NAME VARCHAR(50) NULL",
                "CUST_ADDRESS VARCHAR(500) NULL"),
            new TableDefinition("ITEM_MASTER",
                "ITEM_CODE VARCHAR(6) NOT NULL PRIMARY KEY",
                "ITEM_DESCRIPTION VARCHAR(50) NULL",
                "ITEM_COST NUMERIC(18,2) NULL"),
            new TableDefinition("PURCH_HEADER",
                "TRANS_ID NUMERIC(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY",
                "TRANS_DATE DATETIME NULL",
                "TRANS_NO NUMERIC(18,0) NULL",
                "CUST_ID VARCHAR(6) NULL",
                "TOTAL_AMOUNT NUMERIC(18,2) NULL",
                "CREATED_BY VARCHAR(30) NULL",
                "CREATED_AT DATETIME NULL",
                "MODIFIED_BY VARCHAR(30) NULL",
                "MODIFIED_AT DATETIME NULL"),
            new TableDefinition("PURCH_DETAIL",
                "TRANS_ID NUMERIC(18,0) NULL REFERENCES dbo.PURCH_HEADER (TRANS_ID)",
                "SLNO NUMERIC(6,0) NULL",
                "ITEM_CODE VARCHAR(6) NULL",
                "QTY NUMERIC(6,0) NULL",
                "RATE NUMERIC(18,2) NULL",
                "AMOUNT NUMERIC(18,2) NULL"),
            new TableDefinition("APP_USERS",
                "USERID INT IDENTITY(1,1) NOT NULL PRIMARY KEY",
                "USERNAME VARCHAR(30) NOT NULL UNIQUE",
                "FULL_NAME VARCHAR(60) NULL",
                "USER_ROLE VARCHAR(10) NOT NULL",
                "PASSWORD_HASH VARCHAR(200) NOT NULL",
                "IS_ACTIVE BIT NOT NULL DEFAULT 1",
                "MUST_CHANGE_PASSWORD BIT NOT NULL DEFAULT 0",
                "FAILED_SIGN_INS INT NOT NULL DEFAULT 0",
                "LOCKED_UNTIL DATETIME NULL",
                "LAST_SIGN_IN DATETIME NULL",
                "CREATED_AT DATETIME NOT NULL DEFAULT GETDATE()"),
            new TableDefinition("APP_ACTIVITY",
                "ACTIVITY_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY",
                "ACTIVITY_AT DATETIME NOT NULL DEFAULT GETDATE()",
                "USERNAME VARCHAR(30) NULL",
                "ACTIVITY VARCHAR(40) NOT NULL",
                "DETAILS VARCHAR(400) NULL"),
            new TableDefinition("COMPANY_PROFILE",
                "PROFILE_ID INT NOT NULL PRIMARY KEY",
                "COMPANY_NAME VARCHAR(100) NULL",
                "COMPANY_ADDRESS VARCHAR(500) NULL",
                "PHONE VARCHAR(30) NULL",
                "EMAIL VARCHAR(100) NULL",
                "TAX_NUMBER VARCHAR(30) NULL"),
        };

        public static IReadOnlyList<string> TableNames => Tables.Select(t => t.Name).ToList();

        public static DatabaseStatus GetStatus(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            string database = builder.InitialCatalog;

            if (string.IsNullOrWhiteSpace(database))
            {
                throw new InvalidOperationException("The connection string in App.config has no database name (Initial Catalog).");
            }
            if (SystemDatabases.Contains(database, StringComparer.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"The connection string in App.config points to the system database '{database}'. Set Initial Catalog to an application database.");
            }

            DatabaseStatus status = new DatabaseStatus { Server = builder.DataSource, Database = database };

            using (SqlConnection master = new SqlConnection(MasterConnectionString(connectionString)))
            using (SqlCommand cmd = new SqlCommand("SELECT DB_ID(@name)", master))
            {
                cmd.Parameters.AddWithValue("@name", database);
                master.Open();
                status.DatabaseExists = !(cmd.ExecuteScalar() is DBNull);
            }

            if (!status.DatabaseExists)
            {
                status.MissingTables.AddRange(Tables.Select(t => t.Name));
                return status;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                foreach (TableDefinition table in Tables)
                {
                    string fullName = "dbo." + table.Name;
                    if (Scalar(con, "SELECT OBJECT_ID(@name)", fullName) is DBNull)
                    {
                        status.MissingTables.Add(table.Name);
                        continue;
                    }

                    List<string> existing = GetColumns(con, fullName);
                    if (existing.Count == 0)
                    {
                        status.Problems.Add($"{fullName} already exists but is not a table.");
                        continue;
                    }

                    if (table.Name == "PURCH_HEADER" && !Contains(existing, "TRANS_ID") && Contains(existing, "TRASN_ID"))
                    {
                        status.RenameOldTransId = true;
                        existing.Add("TRANS_ID");
                    }

                    foreach (string column in table.Columns.Where(c => !Contains(existing, c)))
                    {
                        if (table.CanAdd(column))
                        {
                            status.MissingColumns.Add(new MissingColumn(table.Name, column));
                        }
                        else
                        {
                            status.Problems.Add($"{fullName} is missing column {column}.");
                        }
                    }
                }
            }

            return status;
        }

        public static void CreateDatabase(string connectionString)
        {
            string database = new SqlConnectionStringBuilder(connectionString).InitialCatalog;
            using (SqlConnection master = new SqlConnection(MasterConnectionString(connectionString)))
            using (SqlCommand cmd = new SqlCommand("CREATE DATABASE [" + database.Replace("]", "]]") + "]", master))
            {
                master.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ApplyTableChanges(string connectionString, DatabaseStatus status)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    if (status.RenameOldTransId)
                    {
                        Execute(con, transaction, "EXEC sp_rename 'dbo.PURCH_HEADER.TRASN_ID', 'TRANS_ID', 'COLUMN'");
                    }
                    foreach (TableDefinition table in Tables.Where(t => status.MissingTables.Contains(t.Name)))
                    {
                        Execute(con, transaction, table.CreateSql);
                    }
                    foreach (MissingColumn column in status.MissingColumns)
                    {
                        TableDefinition table = Tables.First(t => t.Name == column.Table);
                        Execute(con, transaction, $"ALTER TABLE dbo.{table.Name} ADD {table.Definition(column.Column)}");
                    }
                    transaction.Commit();
                }
            }
        }

        private static string MasterConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "master";
            return builder.ConnectionString;
        }

        private static object Scalar(SqlConnection con, string sql, string name)
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@name", name);
                return cmd.ExecuteScalar();
            }
        }

        private static List<string> GetColumns(SqlConnection con, string fullName)
        {
            List<string> columns = new List<string>();
            using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID(@name, 'U')", con))
            {
                cmd.Parameters.AddWithValue("@name", fullName);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(reader.GetString(0));
                    }
                }
            }
            return columns;
        }

        private static bool Contains(List<string> columns, string column)
        {
            return columns.Contains(column, StringComparer.OrdinalIgnoreCase);
        }

        private static void Execute(SqlConnection con, SqlTransaction transaction, string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
