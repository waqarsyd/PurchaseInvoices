using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class ActivityForm : Form
    {
        private readonly string connectionString;
        private DataTable table = new DataTable();
        private bool loading;

        public ActivityForm(string connectionString)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 760, 520);
            this.connectionString = connectionString;
            comboPeriod.Items.AddRange(InvoiceListForm.Periods);
            comboPeriod.SelectedIndex = 0;
            grid.AutoGenerateColumns = false;
            AddColumn("ACTIVITY_AT", "When", 19, "dd MMM yyyy, HH:mm:ss");
            AddColumn("USERNAME", "User", 13, null);
            AddColumn("ACTIVITY", "Activity", 19, null);
            AddColumn("DETAILS", "Details", 49, null);
        }

        private void AddColumn(string column, string header, float weight, string format)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn { DataPropertyName = column, HeaderText = header, Name = column, FillWeight = weight };
            if (format != null)
            {
                col.DefaultCellStyle.Format = format;
            }
            grid.Columns.Add(col);
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (BusyIndicator.Start(this, "Loading the activity log…"))
                {
                    table = new AccountRepository(connectionString).GetActivity();
                }
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException || ex is ArgumentException)
            {
                MessageBox.Show(this, "Could not load the activity log.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnExport.Enabled = false;
                return;
            }

            loading = true;
            try
            {
                SortedSet<string> users = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow row in table.Rows)
                {
                    string user = row["USERNAME"] as string;
                    if (!string.IsNullOrEmpty(user))
                    {
                        users.Add(user);
                    }
                }
                comboUser.Items.AddRange(users.Cast<object>().ToArray());
                grid.DataSource = new DataView(table);
                ApplyFilter();
            }
            finally
            {
                loading = false;
            }
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
                parts.Add($"(ACTIVITY LIKE '%{like}%' OR DETAILS LIKE '%{like}%' OR USERNAME LIKE '%{like}%')");
            }
            if (comboUser.SelectedIndex >= 0)
            {
                parts.Add($"USERNAME = '{comboUser.SelectedItem.ToString().Replace("'", "''")}'");
            }
            DateTime from, to;
            if (InvoiceListForm.TryGetRange(comboPeriod.SelectedIndex, DateTime.Today, out from, out to))
            {
                parts.Add(string.Format(CultureInfo.InvariantCulture, "ACTIVITY_AT >= #{0:MM/dd/yyyy}# AND ACTIVITY_AT < #{1:MM/dd/yyyy}#", from, to));
            }
            view.RowFilter = string.Join(" AND ", parts);

            bool filtered = parts.Count > 0;
            grid.EmptyTitle = filtered ? "Nothing matches" : "No activity yet";
            grid.EmptyHint = filtered ? "Try a different search, user or period, or click Clear filters." : "Sign-ins, invoices, customers, items and user changes appear here.";
            btnClearFilters.Enabled = filtered;
            btnExport.Enabled = view.Count > 0;
            string latest = table.Rows.Count >= 5000 ? " (the latest 5,000)" : string.Empty;
            lblSummary.Text = filtered
                ? $"{view.Count} of {table.Rows.Count} entries{latest}"
                : $"{view.Count} {(view.Count == 1 ? "entry" : "entries")}{latest}";
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                ApplyFilter();
            }
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            loading = true;
            try
            {
                txtFilter.Text = string.Empty;
                comboUser.SelectedIndex = -1;
                comboPeriod.SelectedIndex = 0;
            }
            finally
            {
                loading = false;
            }
            ApplyFilter();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            CsvExport.SaveWithDialog(this, $"Activity log {DateTime.Today:yyyy-MM-dd}.csv", CsvExport.FromGrid(grid));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            if (keyData == (Keys.Control | Keys.F))
            {
                txtFilter.Focus();
                txtFilter.SelectAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
