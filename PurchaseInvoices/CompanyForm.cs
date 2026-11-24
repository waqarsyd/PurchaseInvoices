using System;
using System.Data.Common;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class CompanyForm : Form
    {
        private static readonly Regex EmailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private readonly AccountRepository accounts;
        private bool loading;
        private bool dirty;
        private bool busy;

        public CompanyForm(string connectionString)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 600, 520);
            accounts = new AccountRepository(connectionString);
            btnSave.BusyText = "Saving…";
        }

        private void CompanyForm_Load(object sender, EventArgs e)
        {
            CompanyProfile profile;
            try
            {
                using (BusyIndicator.Start(this, "Loading the company profile…"))
                {
                    profile = accounts.GetCompanyProfile();
                }
            }
            catch (Exception ex) when (ex is DbException || ex is InvalidOperationException)
            {
                MessageBox.Show(this, "Could not load the company profile.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                profile = new CompanyProfile();
            }
            loading = true;
            try
            {
                txtName.Text = profile.Name;
                txtAddress.Text = profile.Address;
                txtPhone.Text = profile.Phone;
                txtEmail.Text = profile.Email;
                txtTax.Text = profile.TaxNumber;
            }
            finally
            {
                loading = false;
            }
            ActiveControl = txtName;
        }

        internal CompanyProfile ReadProfile()
        {
            return new CompanyProfile
            {
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                TaxNumber = txtTax.Text.Trim(),
            };
        }

        private string Check(CompanyProfile profile, out Control focus)
        {
            focus = null;
            if (profile.Name.Length == 0)
            {
                focus = txtName;
                return "Enter the company name.";
            }
            if (profile.Email.Length > 0 && !EmailPattern.IsMatch(profile.Email))
            {
                focus = txtEmail;
                return "That email address doesn't look right.";
            }
            return null;
        }

        internal async Task<bool> SaveAsync()
        {
            CompanyProfile profile = ReadProfile();
            Control focus;
            string problem = Check(profile, out focus);
            if (problem != null)
            {
                lblError.Text = problem;
                focus.Focus();
                return false;
            }
            SetBusy(true);
            try
            {
                await Task.Run(() => accounts.SaveCompanyProfile(profile));
            }
            catch (Exception ex) when (ex is DbException || ex is InvalidOperationException)
            {
                lblError.Text = "Could not save. " + ex.Message;
                return false;
            }
            finally
            {
                SetBusy(false);
            }
            ActivityLog.Record("Company profile updated", profile.Name);
            dirty = false;
            DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        internal static InvoicePrintData SampleInvoice(CompanyProfile company)
        {
            InvoicePrintData sample = new InvoicePrintData
            {
                InvoiceNo = "1001",
                Date = DateTime.Today,
                CustomerId = "C001",
                CustomerName = "Sample customer",
                CustomerAddress = "12 Example Street, Sample City",
                Company = company,
                PrintedBy = Session.Current?.DisplayName ?? string.Empty,
            };
            sample.Lines.Add(new InvoicePrintLine("1", "I001", "Sample item", 2, 250m, 500m));
            sample.Lines.Add(new InvoicePrintLine("2", "I002", "Another sample item", 1, 1200m, 1200m));
            sample.Total = 1700m;
            return sample;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show(this, "No printer is installed on this PC. Install a printer, or \"Microsoft Print to PDF\", to preview invoices.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BusyIndicator loading = BusyIndicator.Start(this, "Preparing print preview…", "A sample invoice with these details");
            try
            {
                using (InvoicePrintDocument document = new InvoicePrintDocument(SampleInvoice(ReadProfile())))
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

        private void SetBusy(bool value)
        {
            busy = value;
            btnSave.Busy = value;
            btnCancel.Enabled = !value;
            btnPreview.Enabled = !value;
        }

        private void Field_TextChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                dirty = true;
                lblError.Text = string.Empty;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CompanyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy)
            {
                e.Cancel = true;
                return;
            }
            if (e.CloseReason == CloseReason.UserClosing && dirty && DialogResult != DialogResult.OK
                && MessageBox.Show(this, "Discard your changes to the company profile?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && !busy)
            {
                Close();
                return true;
            }
            if (keyData == (Keys.Control | Keys.S) && !busy)
            {
                btnSave.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
