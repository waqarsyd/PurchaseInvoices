using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class AboutForm : Form
    {
        private readonly List<KeyValuePair<string, string>> details;

        public AboutForm(CompanyProfile company)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 600, 620);
            Assembly assembly = typeof(AboutForm).Assembly;
            string copyright = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).OfType<AssemblyCopyrightAttribute>().FirstOrDefault()?.Copyright ?? string.Empty;
            headerPanel.Subtitle = $"Version {AppInfo.Version}  ·  {copyright}";

            ConnectionDetails connection = AppConnection.Current;
            AppUser user = Session.Current;
            string built;
            try
            {
                built = File.GetLastWriteTime(assembly.Location).ToString("dd MMM yyyy", CultureInfo.CurrentCulture);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is NotSupportedException)
            {
                built = "unknown";
            }
            details = new List<KeyValuePair<string, string>>
            {
                Row("Version", $"{AppInfo.Version}  ·  built {built}"),
                Row("Signed in as", user == null ? "Nobody" : $"{user.DisplayName} ({user.UserName})"),
                Row("Role", user?.RoleName ?? "None"),
                Row("Company", string.IsNullOrWhiteSpace(company?.Name) ? "Not set yet" : company.Name.Trim()),
                Row("Database", string.IsNullOrWhiteSpace(connection.Database) ? "Not set" : connection.Database),
                Row("Server", string.IsNullOrWhiteSpace(connection.Server) ? "Not set" : connection.Server),
                Row("Connection from", SourceText(AppConnection.Source)),
                Row("Windows", RuntimeInformation.OSDescription.Trim() + (Environment.Is64BitOperatingSystem ? ", 64-bit" : ", 32-bit")),
            };
            detailsList.SetRows(details);
            shortcutsList.SetRows(new[]
            {
                Row("Ctrl+N", "New invoice"),
                Row("Ctrl+S", "Save invoice"),
                Row("Ctrl+E", "Edit invoice"),
                Row("Ctrl+P", "Print"),
                Row("Ctrl+F", "Find an invoice"),
                Row("Ctrl+L", "All invoices"),
                Row("Ctrl+R", "Reports"),
                Row("Alt+← / →", "Older / newer invoice"),
            });
        }

        internal string DetailsText => AppInfo.ProductName + Environment.NewLine + string.Join(Environment.NewLine, details.Select(d => $"{d.Key}: {d.Value}"));

        private static KeyValuePair<string, string> Row(string label, string value) => new KeyValuePair<string, string>(label, value);

        private static string SourceText(ConnectionSource source)
        {
            switch (source)
            {
                case ConnectionSource.UserSettings: return "Saved settings (Database window)";
                case ConnectionSource.AppConfig: return "App.config";
                case ConnectionSource.Override: return "Test settings";
                default: return "Not set up";
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(DetailsText);
                lblCopied.Text = "Copied to the clipboard.";
            }
            catch (ExternalException)
            {
                lblCopied.Text = "The clipboard is busy. Try again.";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == Keys.F1)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
