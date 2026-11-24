using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class DatabaseSettingsForm : Form
    {
        private static readonly Color SuccessColor = Color.FromArgb(4, 120, 87);
        private static readonly Color WarningColor = Color.FromArgb(180, 83, 9);
        private static readonly Color ErrorColor = Color.FromArgb(185, 28, 28);

        private DatabaseStatus lastStatus;
        private string lastTestedConnection;
        private bool loading;
        private bool busy;

        public DatabaseSettingsForm()
        {
            InitializeComponent();
            Theme.StyleWindow(this, 560, 540);
            cboAuth.Items.AddRange(new object[] { "Windows authentication", "SQL Server login" });
            LoadDetails(AppConnection.Current);
        }

        public bool NeedsReload { get; private set; }

        private void LoadDetails(ConnectionDetails details)
        {
            loading = true;
            try
            {
                txtServer.Text = details.Server;
                txtDatabase.Text = details.Database;
                cboAuth.SelectedIndex = details.WindowsAuthentication ? 0 : 1;
                txtUser.Text = details.UserName;
                txtPassword.Text = details.Password;
            }
            finally
            {
                loading = false;
            }
            UpdateAuthFields();
            ForgetTest("Click Test connection to check these settings.");
            UpdateSource();
        }

        private ConnectionDetails ReadDetails()
        {
            return new ConnectionDetails
            {
                Server = txtServer.Text.Trim(),
                Database = txtDatabase.Text.Trim(),
                WindowsAuthentication = cboAuth.SelectedIndex != 1,
                UserName = txtUser.Text.Trim(),
                Password = txtPassword.Text
            };
        }

        private void UpdateSource()
        {
            ConnectionSource source = AppConnection.Source;
            lblSource.Text = source == ConnectionSource.UserSettings ? "The app is using your saved connection settings."
                : source == ConnectionSource.AppConfig ? "The app is using the connection from App.config."
                : source == ConnectionSource.Override ? "The app is using a connection set for testing."
                : "No connection is set up yet.";
            btnUseDefault.Enabled = source == ConnectionSource.UserSettings && AppConnection.AppConfigConnectionString != null;
        }

        private void UpdateAuthFields()
        {
            bool sqlLogin = cboAuth.SelectedIndex == 1;
            txtUser.ReadOnly = !sqlLogin;
            txtPassword.ReadOnly = !sqlLogin;
            txtUser.TabStop = sqlLogin;
            txtPassword.TabStop = sqlLogin;
        }

        private void cboAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAuthFields();
            Field_TextChanged(sender, e);
        }

        private void Field_TextChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                ForgetTest("Settings changed. Click Test connection to check them.");
            }
        }

        private void ForgetTest(string message)
        {
            lastStatus = null;
            lastTestedConnection = null;
            btnCreateTables.Visible = false;
            SetStatus(Theme.TextMuted, message);
        }

        private void SetStatus(Color color, string message)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = message;
        }

        private string Validate(ConnectionDetails details)
        {
            if (details.Server.Length == 0)
            {
                return @"Enter the SQL Server name, for example .\SQLEXPRESS.";
            }
            if (details.Database.Length == 0)
            {
                return "Enter the database name.";
            }
            if (!details.WindowsAuthentication && details.UserName.Length == 0)
            {
                return "Enter the SQL Server login name.";
            }
            return null;
        }

        private void SetBusy(bool value)
        {
            busy = value;
            UseWaitCursor = value;
            btnTest.Enabled = !value;
            btnSave.Enabled = !value;
            btnCreateTables.Enabled = !value;
            btnUseDefault.Enabled = !value && AppConnection.Source == ConnectionSource.UserSettings && AppConnection.AppConfigConnectionString != null;
        }

        internal async Task<DatabaseStatus> TestAsync()
        {
            ConnectionDetails details = ReadDetails();
            string error = Validate(details);
            if (error != null)
            {
                ForgetTest(string.Empty);
                SetStatus(ErrorColor, error);
                return null;
            }
            string connectionString = details.ToConnectionString(5);
            SetBusy(true);
            SetStatus(Theme.TextMuted, "Connecting…");
            try
            {
                DatabaseStatus status = await Task.Run(() => DatabaseSetup.GetStatus(connectionString));
                lastStatus = status;
                lastTestedConnection = details.ToConnectionString();
                ShowStatus(status);
                return status;
            }
            catch (Exception ex)
            {
                lastStatus = null;
                lastTestedConnection = null;
                btnCreateTables.Visible = false;
                SetStatus(ErrorColor, "✗  Could not connect. " + FirstSentence(ex.Message));
                return null;
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void ShowStatus(DatabaseStatus status)
        {
            btnCreateTables.Visible = false;
            if (status.IsReady)
            {
                SetStatus(SuccessColor, $"✓  Connected. Database '{status.Database}' has all the tables this app needs.");
            }
            else if (status.Problems.Count > 0)
            {
                SetStatus(ErrorColor, "✗  Connected, but these must be fixed by hand first: " + string.Join(" ", status.Problems));
            }
            else
            {
                SetStatus(WarningColor, "!  Connected. Setup needed: " + string.Join(", ", status.GetPlannedSteps()).ToLowerInvariant() + ".");
                btnCreateTables.Visible = true;
            }
        }

        private static string FirstSentence(string message)
        {
            int end = message.IndexOf(". ", StringComparison.Ordinal);
            return end > 0 ? message.Substring(0, end + 1) : message;
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            await TestAsync();
        }

        internal async Task CreateTablesAsync()
        {
            DatabaseStatus status = await TestAsync();
            if (status == null || status.IsReady || status.Problems.Count > 0)
            {
                return;
            }
            string plan = "- " + string.Join("\n- ", status.GetPlannedSteps());
            string target = $"database '{status.Database}' on server '{status.Server}'";
            if (MessageBox.Show(this, $"This will make these changes in {target}:\n\n{plan}\n\nExisting tables and data are not changed. Continue?", "Create tables",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string connectionString = lastTestedConnection;
            bool databaseCreated = false;
            SetBusy(true);
            SetStatus(Theme.TextMuted, "Creating…");
            try
            {
                await Task.Run(() =>
                {
                    if (!status.DatabaseExists)
                    {
                        DatabaseSetup.CreateDatabase(connectionString);
                        databaseCreated = true;
                    }
                    DatabaseSetup.ApplyTableChanges(connectionString, status);
                });
            }
            catch (Exception ex)
            {
                SetBusy(false);
                string note = databaseCreated ? $" Database '{status.Database}' was created but is still empty." : string.Empty;
                SetStatus(ErrorColor, $"✗  Setup failed: {FirstSentence(ex.Message)} No tables were created or changed.{note}");
                return;
            }
            SetBusy(false);
            NeedsReload = true;
            await TestAsync();
        }

        private async void btnCreateTables_Click(object sender, EventArgs e)
        {
            await CreateTablesAsync();
        }

        internal async Task<bool> SaveAsync()
        {
            ConnectionDetails details = ReadDetails();
            string error = Validate(details);
            if (error != null)
            {
                SetStatus(ErrorColor, error);
                return false;
            }
            if (lastStatus == null || lastTestedConnection != details.ToConnectionString())
            {
                await TestAsync();
            }
            if (lastStatus == null && MessageBox.Show(this, "The app could not connect with these settings. Save them anyway?", "Database connection",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return false;
            }
            AppConnection.Save(details);
            NeedsReload = true;
            DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveAsync();
        }

        private void btnUseDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Stop using your saved connection settings and go back to the connection in App.config?", "Database connection",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            AppConnection.ClearSaved();
            NeedsReload = true;
            LoadDetails(AppConnection.Current);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DatabaseSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy)
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
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
