using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace PurchaseInvoices
{
    internal sealed class BackupFileInfo
    {
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public DateTime? Finished { get; set; }
        public long? Size { get; set; }
        public string Description { get; set; }
    }

    internal static class DatabaseBackup
    {
        public static string TargetDatabase(string connectionString) => new SqlConnectionStringBuilder(connectionString).InitialCatalog;

        public static string TargetServer(string connectionString) => new SqlConnectionStringBuilder(connectionString).DataSource;

        public static bool IsLocalServer(string connectionString)
        {
            string host = TargetServer(connectionString).Trim();
            if (host.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase) || host.StartsWith("np:", StringComparison.OrdinalIgnoreCase) || host.StartsWith("lpc:", StringComparison.OrdinalIgnoreCase))
            {
                host = host.Substring(host.IndexOf(':') + 1);
            }
            if (host.StartsWith("(localdb)", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            host = host.Split('\\', ',')[0].Trim();
            return host == "." || host == "(local)" || host == "127.0.0.1" || host.Equals("localhost", StringComparison.OrdinalIgnoreCase) || host.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase);
        }

        public static string DefaultFolder(string connectionString)
        {
            try
            {
                using (SqlConnection con = OpenMaster(connectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS nvarchar(4000))", con))
                {
                    string folder = cmd.ExecuteScalar() as string;
                    if (!string.IsNullOrWhiteSpace(folder))
                    {
                        return folder.TrimEnd('\\');
                    }
                }
            }
            catch (SqlException)
            {
            }
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Purchase Invoices backups");
        }

        public static string NewFileName(string connectionString, string label = null)
        {
            string database = TargetDatabase(connectionString);
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                database = database.Replace(c, '-');
            }
            return $"{database}_{(label == null ? string.Empty : label + "_")}{DateTime.Now:yyyy-MM-dd_HHmmss}.bak";
        }

        public static void Backup(string connectionString, string path)
        {
            string database = TargetDatabase(connectionString);
            using (SqlConnection con = OpenMaster(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("BACKUP DATABASE @db TO DISK = @path WITH COPY_ONLY, INIT, FORMAT, CHECKSUM, NAME = @name, DESCRIPTION = @description", con) { CommandTimeout = 0 })
                {
                    cmd.Parameters.Add("@db", SqlDbType.NVarChar, 128).Value = database;
                    cmd.Parameters.Add("@path", SqlDbType.NVarChar, 4000).Value = path;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 128).Value = $"{database} full backup";
                    cmd.Parameters.Add("@description", SqlDbType.NVarChar, 255).Value = $"Made by {AppInfo.ProductName} {AppInfo.Version}" + (Session.UserName != null ? " for " + Session.UserName : string.Empty);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand verify = new SqlCommand("RESTORE VERIFYONLY FROM DISK = @path WITH CHECKSUM", con) { CommandTimeout = 0 })
                {
                    verify.Parameters.Add("@path", SqlDbType.NVarChar, 4000).Value = path;
                    verify.ExecuteNonQuery();
                }
            }
        }

        public static BackupFileInfo ReadHeader(string connectionString, string path)
        {
            using (SqlConnection con = OpenMaster(connectionString))
            using (SqlCommand cmd = new SqlCommand("RESTORE HEADERONLY FROM DISK = @path", con) { CommandTimeout = 120 })
            {
                cmd.Parameters.Add("@path", SqlDbType.NVarChar, 4000).Value = path;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return new BackupFileInfo
                    {
                        DatabaseName = Convert.ToString(reader["DatabaseName"]),
                        ServerName = Convert.ToString(reader["ServerName"]),
                        Finished = reader["BackupFinishDate"] as DateTime?,
                        Size = reader["BackupSize"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["BackupSize"]),
                        Description = Convert.ToString(reader["BackupDescription"]),
                    };
                }
            }
        }

        public static void Restore(string connectionString, string path)
        {
            string database = TargetDatabase(connectionString);
            SqlConnection.ClearAllPools();
            using (SqlConnection con = OpenMaster(connectionString))
            {
                List<KeyValuePair<string, string>> backupFiles = new List<KeyValuePair<string, string>>();
                using (SqlCommand cmd = new SqlCommand("RESTORE FILELISTONLY FROM DISK = @path", con) { CommandTimeout = 120 })
                {
                    cmd.Parameters.Add("@path", SqlDbType.NVarChar, 4000).Value = path;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            backupFiles.Add(new KeyValuePair<string, string>(Convert.ToString(reader["LogicalName"]), Convert.ToString(reader["Type"])));
                        }
                    }
                }

                List<string> dataFiles = new List<string>();
                List<string> logFiles = new List<string>();
                using (SqlCommand cmd = new SqlCommand("SELECT physical_name, type FROM sys.master_files WHERE database_id = DB_ID(@db) ORDER BY file_id", con))
                {
                    cmd.Parameters.Add("@db", SqlDbType.NVarChar, 128).Value = database;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            (Convert.ToInt32(reader["type"]) == 1 ? logFiles : dataFiles).Add(reader.GetString(0));
                        }
                    }
                }
                bool exists = dataFiles.Count > 0;
                string dataFolder = exists ? Path.GetDirectoryName(dataFiles[0]) : ServerFolder(con, "InstanceDefaultDataPath");
                string logFolder = logFiles.Count > 0 ? Path.GetDirectoryName(logFiles[0]) : ServerFolder(con, "InstanceDefaultLogPath") ?? dataFolder;

                List<KeyValuePair<string, string>> moves = new List<KeyValuePair<string, string>>();
                int data = 0;
                int logs = 0;
                foreach (KeyValuePair<string, string> file in backupFiles)
                {
                    if (file.Value == "L")
                    {
                        string target = logs < logFiles.Count ? logFiles[logs] : Path.Combine(logFolder, $"{database}_log{(logs == 0 ? string.Empty : (logs + 1).ToString())}.ldf");
                        moves.Add(new KeyValuePair<string, string>(file.Key, target));
                        logs++;
                    }
                    else
                    {
                        string target = data < dataFiles.Count ? dataFiles[data] : Path.Combine(dataFolder, data == 0 ? database + ".mdf" : $"{database}_{data + 1}.ndf");
                        moves.Add(new KeyValuePair<string, string>(file.Key, target));
                        data++;
                    }
                }

                if (exists)
                {
                    SetUserMode(con, database, "SINGLE_USER WITH ROLLBACK IMMEDIATE");
                }
                try
                {
                    string sql = "RESTORE DATABASE @db FROM DISK = @path WITH REPLACE, RECOVERY" + string.Concat(moves.Select((m, i) => $", MOVE @logical{i} TO @physical{i}"));
                    using (SqlCommand cmd = new SqlCommand(sql, con) { CommandTimeout = 0 })
                    {
                        cmd.Parameters.Add("@db", SqlDbType.NVarChar, 128).Value = database;
                        cmd.Parameters.Add("@path", SqlDbType.NVarChar, 4000).Value = path;
                        for (int i = 0; i < moves.Count; i++)
                        {
                            cmd.Parameters.Add($"@logical{i}", SqlDbType.NVarChar, 128).Value = moves[i].Key;
                            cmd.Parameters.Add($"@physical{i}", SqlDbType.NVarChar, 4000).Value = moves[i].Value;
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    using (SqlCommand check = new SqlCommand("SELECT DB_ID(@db)", con))
                    {
                        check.Parameters.Add("@db", SqlDbType.NVarChar, 128).Value = database;
                        if (!(check.ExecuteScalar() is DBNull))
                        {
                            SetUserMode(con, database, "MULTI_USER");
                        }
                    }
                }
            }
            SqlConnection.ClearAllPools();
        }

        public static string Explain(SqlException ex)
        {
            string message = ex.Message;
            if (message.IndexOf("Operating system error 5", StringComparison.OrdinalIgnoreCase) >= 0 || message.IndexOf("Access is denied", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "SQL Server isn't allowed to use that folder. Choose a folder the SQL Server service can read and write, such as its own Backup folder.";
            }
            if (message.IndexOf("Operating system error 3", StringComparison.OrdinalIgnoreCase) >= 0 || message.IndexOf("Operating system error 2", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "SQL Server can't find that folder or file. Remember the path is on the SQL Server's computer.";
            }
            int end = message.IndexOf(". ", StringComparison.Ordinal);
            return end > 0 ? message.Substring(0, end + 1) : message;
        }

        private static void SetUserMode(SqlConnection con, string database, string mode)
        {
            using (SqlCommand cmd = new SqlCommand($"DECLARE @sql nvarchar(max) = N'ALTER DATABASE ' + QUOTENAME(@db) + N' SET {mode}'; EXEC (@sql);", con) { CommandTimeout = 0 })
            {
                cmd.Parameters.Add("@db", SqlDbType.NVarChar, 128).Value = database;
                cmd.ExecuteNonQuery();
            }
        }

        private static string ServerFolder(SqlConnection con, string property)
        {
            using (SqlCommand cmd = new SqlCommand($"SELECT CAST(SERVERPROPERTY('{property}') AS nvarchar(4000))", con))
            {
                string folder = cmd.ExecuteScalar() as string;
                return string.IsNullOrWhiteSpace(folder) ? null : folder.TrimEnd('\\');
            }
        }

        private static SqlConnection OpenMaster(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master", Pooling = false };
            SqlConnection con = new SqlConnection(builder.ConnectionString);
            con.Open();
            return con;
        }
    }
}
