using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class InvoiceListForm : Form
    {
        private static readonly string[] Periods =
        {
            "All time", "Today", "Last 7 days", "Last 30 days", "This month", "Last month", "This year", "Last year"
        };

        private readonly string connectionString;
        private readonly string preselectTransNo;
        private DataTable table = new DataTable();
        private bool loading;

        public InvoiceListForm(string connectionString, string preselectTransNo)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 760, 520);
            this.connectionString = connectionString;
            this.preselectTransNo = preselectTransNo;
            comboCustomer.DropDownWidth = Theme.S(380);
            comboPeriod.Items.AddRange(Periods);
            comboPeriod.SelectedIndex = 0;
            grid.AutoGenerateColumns = false;
            AddColumn("TRANS_NO", "Invoice no.", 13, DataGridViewContentAlignment.NotSet, null, false);
            AddColumn("TRANS_DATE", "Date", 16, DataGridViewContentAlignment.NotSet, "dd MMM yyyy", false);
            AddColumn("CUST_ID", "Customer ID", 14, DataGridViewContentAlignment.NotSet, null, false);
            AddColumn("CUST_NAME", "Customer", 31, DataGridViewContentAlignment.NotSet, null, false);
            AddColumn("LINES", "Lines", 9, DataGridViewContentAlignment.MiddleRight, null, false);
            AddColumn("TOTAL_AMOUNT", "Total", 17, DataGridViewContentAlignment.MiddleRight, "N2", true);
        }

        public string SelectedTransNo { get; private set; }

        private void AddColumn(string column, string header, float weight, DataGridViewContentAlignment alignment, string format, bool bold)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = column,
                HeaderText = header,
                Name = column,
                FillWeight = weight,
            };
            if (alignment != DataGridViewContentAlignment.NotSet)
            {
                col.DefaultCellStyle.Alignment = alignment;
            }
            if (format != null)
            {
                col.DefaultCellStyle.Format = format;
            }
            if (bold)
            {
                col.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f);
            }
            grid.Columns.Add(col);
        }

        private void InvoiceListForm_Load(object sender, EventArgs e)
        {
            try
            {
                InvoiceRepository db = new InvoiceRepository(connectionString);
                DataTable data = db.GetInvoiceList();
                Dictionary<string, string> customers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (CustomerRecord customer in db.GetCustomers())
                {
                    customers[customer.Id] = customer.Name;
                    comboCustomer.Items.Add(customer.Id);
                }
                comboCustomer.Descriptions = customers;
                table = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not load the invoices.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOpen.Enabled = false;
                return;
            }

            loading = true;
            try
            {
                grid.DataSource = new DataView(table);
                ApplyFilter();
                SelectTransNo(preselectTransNo);
            }
            finally
            {
                loading = false;
            }
            UpdateOpenButton();
            ActiveControl = txtFilter;
        }

        private void ApplyFilter()
        {
            DataView view = grid.DataSource as DataView;
            if (view == null)
            {
                return;
            }
            List<string> parts = new List<string>();
            string text = txtFilter.Text.Trim();
            if (text.Length > 0)
            {
                string like = MasterForm.EscapeLike(text);
                parts.Add($"(CONVERT(TRANS_NO, 'System.String') LIKE '%{like}%' OR CUST_ID LIKE '%{like}%' OR CUST_NAME LIKE '%{like}%')");
            }
            if (comboCustomer.SelectedIndex >= 0)
            {
                parts.Add($"CUST_ID = '{comboCustomer.SelectedItem.ToString().Replace("'", "''")}'");
            }
            DateTime from, to;
            if (TryGetRange(comboPeriod.SelectedIndex, DateTime.Today, out from, out to))
            {
                parts.Add(string.Format(CultureInfo.InvariantCulture, "TRANS_DATE >= #{0:MM/dd/yyyy}# AND TRANS_DATE < #{1:MM/dd/yyyy}#", from, to));
            }
            view.RowFilter = string.Join(" AND ", parts);

            bool filtered = parts.Count > 0;
            grid.EmptyTitle = filtered ? "No matching invoices" : "No invoices yet";
            grid.EmptyHint = filtered ? "Try a different search, customer or period, or click Clear filters." : "Saved invoices will appear here.";
            btnClearFilters.Enabled = filtered;

            decimal sum = 0;
            foreach (DataRowView row in view)
            {
                if (row["TOTAL_AMOUNT"] != DBNull.Value)
                {
                    sum += Convert.ToDecimal(row["TOTAL_AMOUNT"]);
                }
            }
            int count = view.Count;
            string noun = count == 1 ? "invoice" : "invoices";
            lblSummary.Text = filtered
                ? $"{count} of {table.Rows.Count} {(table.Rows.Count == 1 ? "invoice" : "invoices")}  ·  total {sum.ToString("N2", CultureInfo.CurrentCulture)}"
                : $"{count} {noun}  ·  total {sum.ToString("N2", CultureInfo.CurrentCulture)}";
        }

        internal static bool TryGetRange(int period, DateTime today, out DateTime from, out DateTime to)
        {
            DateTime monthStart = new DateTime(today.Year, today.Month, 1);
            DateTime yearStart = new DateTime(today.Year, 1, 1);
            switch (period)
            {
                case 1: from = today; to = today.AddDays(1); return true;
                case 2: from = today.AddDays(-6); to = today.AddDays(1); return true;
                case 3: from = today.AddDays(-29); to = today.AddDays(1); return true;
                case 4: from = monthStart; to = monthStart.AddMonths(1); return true;
                case 5: from = monthStart.AddMonths(-1); to = monthStart; return true;
                case 6: from = yearStart; to = yearStart.AddYears(1); return true;
                case 7: from = yearStart.AddYears(-1); to = yearStart; return true;
                default: from = DateTime.MinValue; to = DateTime.MaxValue; return false;
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (loading)
            {
                return;
            }
            string keep = SelectedRowTransNo();
            loading = true;
            try
            {
                ApplyFilter();
                SelectTransNo(keep);
            }
            finally
            {
                loading = false;
            }
            UpdateOpenButton();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            loading = true;
            try
            {
                txtFilter.Text = string.Empty;
                comboCustomer.SelectedIndex = -1;
                comboPeriod.SelectedIndex = 0;
            }
            finally
            {
                loading = false;
            }
            FilterChanged(sender, e);
            txtFilter.Focus();
        }

        private string SelectedRowTransNo()
        {
            DataRowView row = grid.CurrentRow?.DataBoundItem as DataRowView;
            return row?["TRANS_NO"].ToString();
        }

        private void SelectTransNo(string transNo)
        {
            grid.ClearSelection();
            if (grid.Rows.Count == 0)
            {
                grid.CurrentCell = null;
                return;
            }
            DataGridViewRow target = grid.Rows[0];
            if (transNo != null)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (string.Equals(row.Cells[0].Value?.ToString(), transNo, StringComparison.Ordinal))
                    {
                        target = row;
                        break;
                    }
                }
            }
            grid.CurrentCell = target.Cells[0];
            target.Selected = true;
        }

        private void UpdateOpenButton()
        {
            btnOpen.Enabled = grid.CurrentRow != null && grid.Rows.Count > 0;
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                UpdateOpenButton();
            }
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenSelected();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenSelected();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenSelected()
        {
            string transNo = SelectedRowTransNo();
            if (transNo == null)
            {
                return;
            }
            SelectedTransNo = transNo;
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (comboCustomer.DroppedDown || comboPeriod.DroppedDown)
                    {
                        break;
                    }
                    OpenSelected();
                    return true;
                case Keys.Escape:
                    Close();
                    return true;
                case Keys.Control | Keys.F:
                    txtFilter.Focus();
                    txtFilter.SelectAll();
                    return true;
                case Keys.Down:
                case Keys.Up:
                    if (ActiveControl == txtFilter && grid.Rows.Count > 0)
                    {
                        int index = grid.CurrentRow?.Index ?? -1;
                        index = keyData == Keys.Down ? Math.Min(index + 1, grid.Rows.Count - 1) : Math.Max(index - 1, 0);
                        grid.CurrentCell = grid.Rows[index].Cells[0];
                        return true;
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
