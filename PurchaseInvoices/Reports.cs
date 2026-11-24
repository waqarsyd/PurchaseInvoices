using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace PurchaseInvoices
{
    internal enum ReportKind
    {
        ByCustomer,
        ByItem,
        ByMonth,
        Register
    }

    internal sealed class ReportColumn
    {
        public ReportColumn(string name, string header, float weight, string format = null, bool sum = false, bool right = false)
        {
            Name = name;
            Header = header;
            Weight = weight;
            Format = format;
            Sum = sum;
            RightAligned = right || sum;
        }

        public string Name { get; }
        public string Header { get; }
        public float Weight { get; }
        public string Format { get; }
        public bool Sum { get; }
        public bool RightAligned { get; }
    }

    internal sealed class ReportFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string CustomerId { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    internal sealed class ReportResult
    {
        public ReportKind Kind { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ReportColumn> Columns { get; set; }
        public DataTable Table { get; set; }
        public int Invoices { get; set; }
        public decimal Total { get; set; }
        public int Customers { get; set; }
        public string HighlightLabel { get; set; }
        public string HighlightValue { get; set; }
        public string HighlightHint { get; set; }
        public DateTime GeneratedAt { get; set; }

        public decimal Average => Invoices > 0 ? Math.Round(Total / Invoices, 2, MidpointRounding.AwayFromZero) : 0m;
        public bool HasTotals => Columns.Any(c => c.Sum);

        public List<string[]> FormattedRows()
        {
            List<string[]> rows = new List<string[]>();
            foreach (DataRow row in Table.Rows)
            {
                rows.Add(Columns.Select(c => Format(row[c.Name], c.Format)).ToArray());
            }
            return rows;
        }

        public string[] FormattedTotals()
        {
            if (!HasTotals)
            {
                return null;
            }
            string[] totals = new string[Columns.Count];
            for (int i = 0; i < Columns.Count; i++)
            {
                ReportColumn column = Columns[i];
                if (column.Sum)
                {
                    decimal sum = 0;
                    foreach (DataRow row in Table.Rows)
                    {
                        if (row[column.Name] != DBNull.Value)
                        {
                            sum += Convert.ToDecimal(row[column.Name], CultureInfo.InvariantCulture);
                        }
                    }
                    totals[i] = Format(sum, column.Format);
                }
                else
                {
                    totals[i] = string.Empty;
                }
            }
            totals[0] = "Total";
            return totals;
        }

        public static string Format(object value, string format)
        {
            if (value == null || value == DBNull.Value)
            {
                return string.Empty;
            }
            IFormattable formattable = value as IFormattable;
            return formattable != null && format != null ? formattable.ToString(format, CultureInfo.CurrentCulture) : Convert.ToString(value, CultureInfo.CurrentCulture);
        }
    }

    internal static class ReportCatalog
    {
        public static readonly ReportKind[] All = { ReportKind.ByCustomer, ReportKind.ByItem, ReportKind.ByMonth, ReportKind.Register };

        public static string Title(ReportKind kind)
        {
            switch (kind)
            {
                case ReportKind.ByCustomer: return "Purchases by customer";
                case ReportKind.ByItem: return "Purchases by item";
                case ReportKind.ByMonth: return "Purchases by month";
                default: return "Invoice register";
            }
        }

        public static string Hint(ReportKind kind)
        {
            switch (kind)
            {
                case ReportKind.ByCustomer: return "Who you buy from the most, with each customer's share";
                case ReportKind.ByItem: return "Quantities and amounts for every item, with the average rate";
                case ReportKind.ByMonth: return "How purchases change from month to month";
                default: return "Every invoice in the period, oldest first";
            }
        }

        public static List<ReportColumn> Columns(ReportKind kind)
        {
            switch (kind)
            {
                case ReportKind.ByCustomer:
                    return new List<ReportColumn>
                    {
                        new ReportColumn("CUST_ID", "ID", 8),
                        new ReportColumn("CUST_NAME", "Customer", 30),
                        new ReportColumn("INVOICES", "Invoices", 10, "N0", true),
                        new ReportColumn("AVERAGE", "Avg. invoice", 14, "N2", right: true),
                        new ReportColumn("LAST_DATE", "Last invoice", 14, "dd MMM yyyy"),
                        new ReportColumn("TOTAL", "Total", 15, "N2", true),
                        new ReportColumn("SHARE", "Share", 9, "P1", right: true),
                    };
                case ReportKind.ByItem:
                    return new List<ReportColumn>
                    {
                        new ReportColumn("ITEM_CODE", "Code", 9),
                        new ReportColumn("ITEM_DESCRIPTION", "Description", 31),
                        new ReportColumn("INVOICES", "Invoices", 10, "N0", right: true),
                        new ReportColumn("QUANTITY", "Quantity", 10, "N0", true),
                        new ReportColumn("AVERAGE_RATE", "Avg. rate", 12, "N2", right: true),
                        new ReportColumn("TOTAL", "Amount", 15, "N2", true),
                        new ReportColumn("SHARE", "Share", 9, "P1", right: true),
                    };
                case ReportKind.ByMonth:
                    return new List<ReportColumn>
                    {
                        new ReportColumn("MONTH_START", "Month", 22, "MMMM yyyy"),
                        new ReportColumn("INVOICES", "Invoices", 13, "N0", true),
                        new ReportColumn("CUSTOMERS", "Customers", 13, "N0", right: true),
                        new ReportColumn("AVERAGE", "Avg. invoice", 17, "N2", right: true),
                        new ReportColumn("TOTAL", "Total", 20, "N2", true),
                        new ReportColumn("SHARE", "Share", 11, "P1", right: true),
                    };
                default:
                    return new List<ReportColumn>
                    {
                        new ReportColumn("TRANS_NO", "Invoice", 10, "0"),
                        new ReportColumn("TRANS_DATE", "Date", 13, "dd MMM yyyy"),
                        new ReportColumn("CUST_ID", "ID", 8),
                        new ReportColumn("CUST_NAME", "Customer", 32),
                        new ReportColumn("LINES", "Lines", 8, "N0", true),
                        new ReportColumn("CREATED_BY", "Created by", 13),
                        new ReportColumn("TOTAL", "Total", 15, "N2", true),
                    };
            }
        }

        public static string Sql(ReportKind kind)
        {
            const string where = "WHERE (@from IS NULL OR h.TRANS_DATE >= @from) AND (@to IS NULL OR h.TRANS_DATE < @to) AND (@cust IS NULL OR h.CUST_ID = @cust)";
            switch (kind)
            {
                case ReportKind.ByCustomer:
                    return "SELECT h.CUST_ID, ISNULL(c.CUST_NAME, '') AS CUST_NAME, COUNT(*) AS INVOICES, ISNULL(SUM(h.TOTAL_AMOUNT), 0) AS TOTAL, " +
                           "ISNULL(AVG(h.TOTAL_AMOUNT), 0) AS AVERAGE, MAX(h.TRANS_DATE) AS LAST_DATE " +
                           "FROM PURCH_HEADER h LEFT JOIN CUSTOMER_MASTER c ON c.CUSTID = h.CUST_ID " + where +
                           " GROUP BY h.CUST_ID, c.CUST_NAME ORDER BY TOTAL DESC, h.CUST_ID";
                case ReportKind.ByItem:
                    return "SELECT d.ITEM_CODE, ISNULL(i.ITEM_DESCRIPTION, '') AS ITEM_DESCRIPTION, COUNT(DISTINCT d.TRANS_ID) AS INVOICES, " +
                           "ISNULL(SUM(d.QTY), 0) AS QUANTITY, ISNULL(SUM(d.AMOUNT), 0) AS TOTAL, " +
                           "CASE WHEN SUM(d.QTY) > 0 THEN SUM(d.AMOUNT) / SUM(d.QTY) ELSE 0 END AS AVERAGE_RATE " +
                           "FROM PURCH_DETAIL d JOIN PURCH_HEADER h ON h.TRANS_ID = d.TRANS_ID LEFT JOIN ITEM_MASTER i ON i.ITEM_CODE = d.ITEM_CODE " + where +
                           " GROUP BY d.ITEM_CODE, i.ITEM_DESCRIPTION ORDER BY TOTAL DESC, d.ITEM_CODE";
                case ReportKind.ByMonth:
                    return "SELECT DATEFROMPARTS(YEAR(h.TRANS_DATE), MONTH(h.TRANS_DATE), 1) AS MONTH_START, COUNT(*) AS INVOICES, COUNT(DISTINCT h.CUST_ID) AS CUSTOMERS, " +
                           "ISNULL(SUM(h.TOTAL_AMOUNT), 0) AS TOTAL, ISNULL(AVG(h.TOTAL_AMOUNT), 0) AS AVERAGE " +
                           "FROM PURCH_HEADER h " + where + " AND h.TRANS_DATE IS NOT NULL" +
                           " GROUP BY DATEFROMPARTS(YEAR(h.TRANS_DATE), MONTH(h.TRANS_DATE), 1) ORDER BY MONTH_START";
                default:
                    return "SELECT h.TRANS_NO, h.TRANS_DATE, h.CUST_ID, ISNULL(c.CUST_NAME, '') AS CUST_NAME, " +
                           "(SELECT COUNT(*) FROM PURCH_DETAIL d WHERE d.TRANS_ID = h.TRANS_ID) AS LINES, ISNULL(h.CREATED_BY, '') AS CREATED_BY, ISNULL(h.TOTAL_AMOUNT, 0) AS TOTAL " +
                           "FROM PURCH_HEADER h LEFT JOIN CUSTOMER_MASTER c ON c.CUSTID = h.CUST_ID " + where +
                           " ORDER BY h.TRANS_DATE, h.TRANS_NO";
            }
        }

        public const string StatsSql =
            "SELECT COUNT(*) AS INVOICES, ISNULL(SUM(h.TOTAL_AMOUNT), 0) AS TOTAL, COUNT(DISTINCT h.CUST_ID) AS CUSTOMERS FROM PURCH_HEADER h " +
            "WHERE (@from IS NULL OR h.TRANS_DATE >= @from) AND (@to IS NULL OR h.TRANS_DATE < @to) AND (@cust IS NULL OR h.CUST_ID = @cust)";
    }

    internal sealed class ReportRepository
    {
        private readonly string connectionString;

        public ReportRepository(string connectionString)
        {
            this.connectionString = new SqlConnectionStringBuilder(connectionString).ConnectionString;
        }

        public ReportResult Run(ReportKind kind, ReportFilter filter)
        {
            ReportResult result = new ReportResult
            {
                Kind = kind,
                Title = ReportCatalog.Title(kind),
                Description = filter.Description,
                Columns = ReportCatalog.Columns(kind),
                Table = new DataTable(),
                GeneratedAt = DateTime.Now,
            };
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = Command(con, ReportCatalog.Sql(kind), filter))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(result.Table);
                }
                using (SqlCommand cmd = Command(con, ReportCatalog.StatsSql, filter))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    result.Invoices = Convert.ToInt32(reader["INVOICES"]);
                    result.Total = Convert.ToDecimal(reader["TOTAL"]);
                    result.Customers = Convert.ToInt32(reader["CUSTOMERS"]);
                }
            }

            if (result.Columns.Any(c => c.Name == "SHARE"))
            {
                decimal grand = 0;
                foreach (DataRow row in result.Table.Rows)
                {
                    grand += Convert.ToDecimal(row["TOTAL"]);
                }
                DataColumn share = result.Table.Columns.Add("SHARE", typeof(decimal));
                foreach (DataRow row in result.Table.Rows)
                {
                    row[share] = grand == 0 ? 0m : Convert.ToDecimal(row["TOTAL"]) / grand;
                }
            }
            Highlight(result);
            return result;
        }

        private static void Highlight(ReportResult result)
        {
            DataRow top = null;
            foreach (DataRow row in result.Table.Rows)
            {
                if (top == null || Convert.ToDecimal(row["TOTAL"]) > Convert.ToDecimal(top["TOTAL"]))
                {
                    top = row;
                }
            }
            switch (result.Kind)
            {
                case ReportKind.ByCustomer:
                    result.HighlightLabel = "TOP CUSTOMER";
                    result.HighlightValue = top == null ? "—" : Name(top["CUST_NAME"], top["CUST_ID"]);
                    result.HighlightHint = top == null ? "No purchases in this period" : ShareHint(top);
                    break;
                case ReportKind.ByItem:
                    result.HighlightLabel = "TOP ITEM";
                    result.HighlightValue = top == null ? "—" : Name(top["ITEM_DESCRIPTION"], top["ITEM_CODE"]);
                    result.HighlightHint = top == null ? "No purchases in this period" : ShareHint(top);
                    break;
                case ReportKind.ByMonth:
                    result.HighlightLabel = "BUSIEST MONTH";
                    result.HighlightValue = top == null ? "—" : ReportResult.Format(top["MONTH_START"], "MMMM yyyy");
                    result.HighlightHint = top == null ? "No purchases in this period" : ShareHint(top);
                    break;
                default:
                    result.HighlightLabel = "CUSTOMERS";
                    result.HighlightValue = result.Customers.ToString("N0", CultureInfo.CurrentCulture);
                    result.HighlightHint = result.Customers == 1 ? "bought from in this period" : "bought from in this period";
                    break;
            }
        }

        private static string Name(object name, object code)
        {
            string text = Convert.ToString(name).Trim();
            return text.Length > 0 ? text : Convert.ToString(code);
        }

        private static string ShareHint(DataRow row)
        {
            return $"{ReportResult.Format(row["TOTAL"], "N2")}  ·  {ReportResult.Format(row["SHARE"], "P1")} of the total";
        }

        private static SqlCommand Command(SqlConnection con, string sql, ReportFilter filter)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = (object)filter.From ?? DBNull.Value;
            cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = (object)filter.To ?? DBNull.Value;
            cmd.Parameters.Add("@cust", SqlDbType.VarChar, 6).Value = string.IsNullOrEmpty(filter.CustomerId) ? (object)DBNull.Value : filter.CustomerId;
            return cmd;
        }
    }
}
