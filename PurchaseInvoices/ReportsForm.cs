using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class ReportsForm : Form
    {
        private readonly string connectionString;
        private readonly int customRange;
        private ReportKind kind = ReportKind.ByCustomer;
        private bool loading = true;

        public ReportsForm(string connectionString)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 980, 640);
            this.connectionString = connectionString;
            comboPeriod.Items.AddRange(InvoiceListForm.Periods);
            customRange = comboPeriod.Items.Add("Custom range");
            comboPeriod.SelectedIndex = 6;
            comboCustomer.DropDownWidth = Theme.S(380);
            DateTime today = DateTime.Today;
            dtFrom.Value = new DateTime(today.Year, today.Month, 1);
            dtTo.Value = today;
            grid.AutoGenerateColumns = false;
            HighlightNav();
        }

        internal ReportResult Result { get; private set; }

        internal ReportKind Kind => kind;

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerRecord> customers;
                using (BusyIndicator.Start(this, "Opening reports…"))
                {
                    customers = new InvoiceRepository(connectionString).GetCustomers();
                }
                Dictionary<string, string> names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (CustomerRecord customer in customers)
                {
                    comboCustomer.Items.Add(customer.Id);
                    names[customer.Id] = customer.Name;
                }
                comboCustomer.Descriptions = names;
            }
            catch (Exception ex) when (ex is DbException || ex is InvalidOperationException)
            {
                MessageBox.Show(this, "Could not load the customers.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loading = false;
            RunReport();
        }

        internal void SelectReport(ReportKind report)
        {
            kind = report;
            HighlightNav();
            RunReport();
        }

        private void HighlightNav()
        {
            ModernButton[] buttons = { btnByCustomer, btnByItem, btnByMonth, btnRegister };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Kind = ReportCatalog.All[i] == kind ? ButtonKind.Soft : ButtonKind.Secondary;
            }
            lblReportHint.Text = ReportCatalog.Hint(kind);
        }

        internal ReportFilter BuildFilter()
        {
            ReportFilter filter = new ReportFilter();
            bool custom = comboPeriod.SelectedIndex == customRange;
            string period = comboPeriod.SelectedItem?.ToString() ?? InvoiceListForm.Periods[0];
            DateTime from, to;
            if (custom)
            {
                DateTime first = dtFrom.Value.Date <= dtTo.Value.Date ? dtFrom.Value.Date : dtTo.Value.Date;
                DateTime last = dtFrom.Value.Date <= dtTo.Value.Date ? dtTo.Value.Date : dtFrom.Value.Date;
                filter.From = first;
                filter.To = last.AddDays(1);
                filter.Description = $"{first.ToString("dd MMM yyyy", CultureInfo.CurrentCulture)} to {last.ToString("dd MMM yyyy", CultureInfo.CurrentCulture)}";
            }
            else if (InvoiceListForm.TryGetRange(comboPeriod.SelectedIndex, DateTime.Today, out from, out to))
            {
                filter.From = from;
                filter.To = to;
                filter.Description = $"{period} ({from.ToString("dd MMM yyyy", CultureInfo.CurrentCulture)} to {to.AddDays(-1).ToString("dd MMM yyyy", CultureInfo.CurrentCulture)})";
            }
            else
            {
                filter.Description = period;
            }
            if (comboCustomer.SelectedIndex >= 0)
            {
                filter.CustomerId = comboCustomer.SelectedItem.ToString();
                string name;
                IDictionary<string, string> names = comboCustomer.Descriptions;
                filter.Description += "  ·  " + filter.CustomerId + (names != null && names.TryGetValue(filter.CustomerId, out name) && name.Length > 0 ? " " + name : string.Empty);
            }
            return filter;
        }

        internal void RunReport()
        {
            if (loading)
            {
                return;
            }
            ReportFilter filter = BuildFilter();
            ReportResult result;
            try
            {
                using (BusyIndicator.Start(this, $"Running {ReportCatalog.Title(kind).ToLower(CultureInfo.CurrentCulture)}…", filter.Description))
                {
                    result = new ReportRepository(connectionString).Run(kind, filter);
                }
            }
            catch (Exception ex) when (ex is DbException || ex is InvalidOperationException)
            {
                MessageBox.Show(this, "Could not run the report.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Result = result;
            ShowResult();
        }

        private void ShowResult()
        {
            ReportResult result = Result;
            gridCard.Title = result.Title;
            grid.Columns.Clear();
            foreach (ReportColumn column in result.Columns)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn { Name = column.Name, HeaderText = column.Header, FillWeight = column.Weight };
                if (column.RightAligned)
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (column.Name == "TOTAL")
                {
                    col.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.75f);
                }
                grid.Columns.Add(col);
            }
            foreach (string[] row in result.FormattedRows())
            {
                grid.Rows.Add(row);
            }
            string[] totals = result.FormattedTotals();
            if (totals != null && grid.Rows.Count > 0)
            {
                int index = grid.Rows.Add(totals);
                DataGridViewCellStyle style = grid.Rows[index].DefaultCellStyle;
                style.Font = new Font("Segoe UI Semibold", 9.75f);
                style.BackColor = Theme.AccentSoft;
                style.ForeColor = Theme.AccentText;
                style.SelectionBackColor = Theme.AccentSoftHover;
                style.SelectionForeColor = Theme.AccentText;
            }
            grid.ClearSelection();

            tileTotal.Value = result.Total.ToString("N2", CultureInfo.CurrentCulture);
            tileTotal.Hint = $"{result.Invoices} {(result.Invoices == 1 ? "invoice" : "invoices")}";
            tileInvoices.Value = result.Invoices.ToString("N0", CultureInfo.CurrentCulture);
            tileInvoices.Hint = $"from {result.Customers} {(result.Customers == 1 ? "customer" : "customers")}";
            tileAverage.Value = result.Average.ToString("N2", CultureInfo.CurrentCulture);
            tileAverage.Hint = "per invoice";
            tileHighlight.Caption = result.HighlightLabel;
            tileHighlight.Value = result.HighlightValue;
            tileHighlight.Hint = result.HighlightHint;

            int count = result.Table.Rows.Count;
            string noun = result.Kind == ReportKind.ByCustomer ? "customer" : result.Kind == ReportKind.ByItem ? "item" : result.Kind == ReportKind.ByMonth ? "month" : "invoice";
            lblSummary.Text = $"{count} {noun}{(count == 1 ? string.Empty : "s")}  ·  {result.Description}";
            btnExport.Enabled = count > 0;
            btnPrint.Enabled = true;
        }

        private void statsPanel_Layout(object sender, LayoutEventArgs e)
        {
            StatTile[] tiles = { tileTotal, tileInvoices, tileAverage, tileHighlight };
            int width = statsPanel.ClientSize.Width / tiles.Length;
            for (int i = 0; i < tiles.Length; i++)
            {
                int right = i == tiles.Length - 1 ? statsPanel.ClientSize.Width : (i + 1) * width;
                tiles[i].SetBounds(i * width, 0, right - i * width, statsPanel.ClientSize.Height);
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            bool custom = comboPeriod.SelectedIndex == customRange;
            foreach (Control control in new Control[] { lblFrom, dtFrom, lblTo, dtTo })
            {
                control.Visible = custom;
            }
            RunReport();
        }

        private void btnByCustomer_Click(object sender, EventArgs e) => SelectReport(ReportKind.ByCustomer);
        private void btnByItem_Click(object sender, EventArgs e) => SelectReport(ReportKind.ByItem);
        private void btnByMonth_Click(object sender, EventArgs e) => SelectReport(ReportKind.ByMonth);
        private void btnRegister_Click(object sender, EventArgs e) => SelectReport(ReportKind.Register);

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (Result != null)
            {
                CsvExport.SaveWithDialog(this, $"{Result.Title} {DateTime.Today:yyyy-MM-dd}.csv", CsvExport.FromGrid(grid));
            }
        }

        internal ReportPrintDocument CreatePrintDocument()
        {
            CompanyProfile company;
            try
            {
                company = new AccountRepository(connectionString).GetCompanyProfile();
            }
            catch (Exception ex) when (ex is DbException || ex is InvalidOperationException)
            {
                company = new CompanyProfile();
            }
            return new ReportPrintDocument(Result, company, Session.Current?.DisplayName);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (Result == null)
            {
                return;
            }
            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show(this, "No printer is installed on this PC. Install a printer, or \"Microsoft Print to PDF\", to print reports.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BusyIndicator loading = BusyIndicator.Start(this, "Preparing print preview…", Result.Title);
            try
            {
                using (ReportPrintDocument document = CreatePrintDocument())
                using (PrintPreviewForm preview = new PrintPreviewForm(document) { Loading = loading })
                {
                    preview.ShowDialog(this);
                }
            }
            finally
            {
                loading.Dispose();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    return true;
                case Keys.Control | Keys.P:
                    btnPrint.PerformClick();
                    return true;
                case Keys.Control | Keys.D1:
                    SelectReport(ReportKind.ByCustomer);
                    return true;
                case Keys.Control | Keys.D2:
                    SelectReport(ReportKind.ByItem);
                    return true;
                case Keys.Control | Keys.D3:
                    SelectReport(ReportKind.ByMonth);
                    return true;
                case Keys.Control | Keys.D4:
                    SelectReport(ReportKind.Register);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
