using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PurchaseInvoices
{
    internal sealed class CustomerRecord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    internal sealed class ItemRecord
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal? Cost { get; set; }
    }

    internal sealed class InvoiceLine
    {
        public decimal? Slno { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
    }

    internal sealed class InvoiceRecord
    {
        public int TransId { get; set; }
        public string TransNo { get; set; }
        public DateTime? Date { get; set; }
        public string CustomerId { get; set; }
        public decimal? Total { get; set; }
        public List<InvoiceLine> Lines { get; } = new List<InvoiceLine>();
    }

    internal sealed class SavedInvoice
    {
        public int TransId { get; set; }
        public string TransNo { get; set; }
        public DateTime? Date { get; set; }
    }

    internal sealed class InvoiceRepository
    {
        private readonly string connectionString;

        public InvoiceRepository(string connectionString)
        {
            this.connectionString = new SqlConnectionStringBuilder(connectionString).ConnectionString;
        }

        private SqlConnection Open()
        {
            SqlConnection con; //--- first step...
            con = new SqlConnection(connectionString); //-- combination of second or third step...
            con.Open();  //--fourth step...
            return con;
        }

        public List<string> GetInvoiceNumbers()
        {
            List<string> numbers = new List<string>();
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand("SELECT TRANS_NO FROM PURCH_HEADER ORDER BY TRANS_ID", con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    numbers.Add(reader["TRANS_NO"].ToString());
                }
            }
            return numbers;
        }

        public string GetNextInvoiceNumber()
        {
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TRANS_NO), 0) + 1 FROM PURCH_HEADER", con))
            {
                return cmd.ExecuteScalar().ToString();
            }
        }

        public InvoiceRecord FindInvoice(string transNo)
        {
            using (SqlConnection con = Open())
            {
                InvoiceRecord invoice = null;
                using (SqlCommand cmd = new SqlCommand("SELECT TOP (1) TRANS_ID, TRANS_NO, TRANS_DATE, CUST_ID, TOTAL_AMOUNT FROM PURCH_HEADER WHERE TRANS_NO = @TRANS_NO ORDER BY TRANS_ID", con))
                {
                    cmd.Parameters.AddWithValue("@TRANS_NO", transNo);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            invoice = new InvoiceRecord
                            {
                                TransId = Convert.ToInt32(reader["TRANS_ID"]),
                                TransNo = reader["TRANS_NO"].ToString(),
                                Date = reader["TRANS_DATE"] as DateTime?,
                                CustomerId = Convert.ToString(reader["CUST_ID"]),
                                Total = reader["TOTAL_AMOUNT"] as decimal?,
                            };
                        }
                    }
                }
                if (invoice == null)
                {
                    return null;
                }

                string linesSql = "SELECT d.SLNO, d.ITEM_CODE, i.ITEM_DESCRIPTION, d.QTY, d.RATE, d.AMOUNT " +
                                  "FROM PURCH_DETAIL d LEFT JOIN ITEM_MASTER i ON i.ITEM_CODE = d.ITEM_CODE " +
                                  "WHERE d.TRANS_ID = @TRANS_ID ORDER BY d.SLNO";
                using (SqlCommand cmd = new SqlCommand(linesSql, con))
                {
                    cmd.Parameters.AddWithValue("@TRANS_ID", invoice.TransId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            invoice.Lines.Add(new InvoiceLine
                            {
                                Slno = reader["SLNO"] as decimal?,
                                ItemCode = Convert.ToString(reader["ITEM_CODE"]),
                                Description = Convert.ToString(reader["ITEM_DESCRIPTION"]),
                                Quantity = reader["QTY"] as decimal?,
                                Rate = reader["RATE"] as decimal?,
                                Amount = reader["AMOUNT"] as decimal?,
                            });
                        }
                    }
                }
                return invoice;
            }
        }

        public SavedInvoice SaveInvoice(int? transId, string customerId, decimal total, IList<InvoiceLine> lines)
        {
            string headerSql = transId.HasValue
                ? "UPDATE PURCH_HEADER SET CUST_ID = @CUST_ID, TOTAL_AMOUNT = @TOTAL_AMOUNT WHERE TRANS_ID = @TRANS_ID; " +
                  "SELECT TRANS_ID, TRANS_NO, TRANS_DATE FROM PURCH_HEADER WHERE TRANS_ID = @TRANS_ID;"
                : "INSERT INTO PURCH_HEADER (TRANS_DATE, TRANS_NO, CUST_ID, TOTAL_AMOUNT) " +
                  "SELECT GETDATE(), ISNULL(MAX(TRANS_NO), 0) + 1, @CUST_ID, @TOTAL_AMOUNT FROM PURCH_HEADER WITH (UPDLOCK, HOLDLOCK); " +
                  "SELECT TRANS_ID, TRANS_NO, TRANS_DATE FROM PURCH_HEADER WHERE TRANS_ID = SCOPE_IDENTITY();";

            using (SqlConnection con = Open())
            using (SqlTransaction transaction = con.BeginTransaction())
            {
                SavedInvoice saved = null;
                using (SqlCommand cmd = new SqlCommand(headerSql, con, transaction))
                {
                    cmd.Parameters.AddWithValue("@CUST_ID", Value(customerId));
                    cmd.Parameters.AddWithValue("@TOTAL_AMOUNT", total);
                    if (transId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@TRANS_ID", transId.Value);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            saved = new SavedInvoice
                            {
                                TransId = Convert.ToInt32(reader["TRANS_ID"]),
                                TransNo = reader["TRANS_NO"].ToString(),
                                Date = reader["TRANS_DATE"] as DateTime?,
                            };
                        }
                    }
                }
                if (saved == null)
                {
                    return null;
                }

                if (transId.HasValue)
                {
                    Execute(con, transaction, "DELETE FROM PURCH_DETAIL WHERE TRANS_ID = @TRANS_ID", transId.Value);
                }

                if (lines.Count > 0)
                {
                    StringBuilder sql = new StringBuilder("INSERT INTO PURCH_DETAIL (TRANS_ID, SLNO, ITEM_CODE, QTY, RATE, AMOUNT) VALUES ");
                    using (SqlCommand cmd = new SqlCommand(string.Empty, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TRANS_ID", saved.TransId);
                        for (int i = 0; i < lines.Count; i++)
                        {
                            sql.Append(i == 0 ? string.Empty : ", ").Append($"(@TRANS_ID, @SLNO{i}, @ITEM_CODE{i}, @QTY{i}, @RATE{i}, @AMOUNT{i})");
                            cmd.Parameters.AddWithValue($"@SLNO{i}", i + 1);
                            cmd.Parameters.AddWithValue($"@ITEM_CODE{i}", Value(lines[i].ItemCode));
                            cmd.Parameters.AddWithValue($"@QTY{i}", Value(lines[i].Quantity));
                            cmd.Parameters.AddWithValue($"@RATE{i}", Value(lines[i].Rate));
                            cmd.Parameters.AddWithValue($"@AMOUNT{i}", Value(lines[i].Amount));
                        }
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                return saved;
            }
        }

        public bool DeleteInvoice(int transId)
        {
            using (SqlConnection con = Open())
            using (SqlTransaction transaction = con.BeginTransaction())
            {
                Execute(con, transaction, "DELETE FROM PURCH_DETAIL WHERE TRANS_ID = @TRANS_ID", transId);
                bool deleted = Execute(con, transaction, "DELETE FROM PURCH_HEADER WHERE TRANS_ID = @TRANS_ID", transId) > 0;
                transaction.Commit();
                return deleted;
            }
        }

        public DataTable GetInvoiceList()
        {
            string sql = "SELECT h.TRANS_ID, h.TRANS_NO, h.TRANS_DATE, h.CUST_ID, c.CUST_NAME, h.TOTAL_AMOUNT, " +
                         "(SELECT COUNT(*) FROM PURCH_DETAIL d WHERE d.TRANS_ID = h.TRANS_ID) AS LINES " +
                         "FROM PURCH_HEADER h LEFT JOIN CUSTOMER_MASTER c ON c.CUSTID = h.CUST_ID " +
                         "ORDER BY h.TRANS_ID DESC";
            DataTable data = new DataTable();
            using (SqlConnection con = Open())
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, con))
            {
                adapter.Fill(data);
            }
            return data;
        }

        public List<CustomerRecord> GetCustomers() => ReadCustomers(null);

        public CustomerRecord FindCustomer(string id)
        {
            List<CustomerRecord> found = ReadCustomers(id);
            return found.Count > 0 ? found[0] : null;
        }

        public List<ItemRecord> GetItems() => ReadItems(null);

        public ItemRecord FindItem(string code)
        {
            List<ItemRecord> found = ReadItems(code);
            return found.Count > 0 ? found[0] : null;
        }

        private List<CustomerRecord> ReadCustomers(string id)
        {
            List<CustomerRecord> customers = new List<CustomerRecord>();
            string sql = "SELECT CUSTID, CUST_NAME, CUST_ADDRESS FROM CUSTOMER_MASTER" + (id == null ? string.Empty : " WHERE CUSTID = @CUSTID") + " ORDER BY CUSTID";
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (id != null)
                {
                    cmd.Parameters.AddWithValue("@CUSTID", id);
                }
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new CustomerRecord
                        {
                            Id = reader.GetString(0),
                            Name = Convert.ToString(reader["CUST_NAME"]),
                            Address = Convert.ToString(reader["CUST_ADDRESS"]),
                        });
                    }
                }
            }
            return customers;
        }

        private List<ItemRecord> ReadItems(string code)
        {
            List<ItemRecord> items = new List<ItemRecord>();
            string sql = "SELECT ITEM_CODE, ITEM_DESCRIPTION, ITEM_COST FROM ITEM_MASTER" + (code == null ? string.Empty : " WHERE ITEM_CODE = @ITEM_CODE") + " ORDER BY ITEM_CODE";
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (code != null)
                {
                    cmd.Parameters.AddWithValue("@ITEM_CODE", code);
                }
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new ItemRecord
                        {
                            Code = reader.GetString(0),
                            Description = Convert.ToString(reader["ITEM_DESCRIPTION"]),
                            Cost = reader["ITEM_COST"] as decimal?,
                        });
                    }
                }
            }
            return items;
        }

        private static int Execute(SqlConnection con, SqlTransaction transaction, string sql, int transId)
        {
            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.Parameters.AddWithValue("@TRANS_ID", transId);
                return cmd.ExecuteNonQuery();
            }
        }

        private static object Value(object value) => value ?? DBNull.Value;
    }
}
