using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class SignInForm : Form
    {
        private string connectionString;
        private DatabaseStatus status;
        private bool busy;
        private Func<Task> primaryAction;
        private Func<Task> secondaryAction;

        public SignInForm()
        {
            InitializeComponent();
            Theme.StyleWindow(this, 860, 560);
            brandPanel.VersionText = "Version " + AppInfo.Version;
            string remembered = Session.RememberedUserName;
            txtUserName.Text = remembered;
            chkRemember.Checked = remembered.Length > 0;
            ShowMessage("Checking the database…", "Connecting to your database. This only takes a moment.");
        }

        internal Task Ready { get; private set; } = Task.CompletedTask;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Ready = CheckAsync();
        }

        internal async Task CheckAsync()
        {
            SetBusy(true);
            ShowMessage("Checking the database…", "Connecting to your database. This only takes a moment.");
            try
            {
                connectionString = AppConnection.ConnectionString;
                UpdateConnectionLabel();
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    ShowMessage("Connect your database", "Purchase Invoices keeps its data in SQL Server. Choose the server and the database to use.",
                        "Database settings", OpenDatabaseSettingsAsync);
                    return;
                }

                string quickConnection;
                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString) { ConnectTimeout = 8 };
                    quickConnection = builder.ConnectionString;
                }
                catch (ArgumentException ex)
                {
                    ShowMessage("The connection settings aren't valid", ex.Message, "Database settings", OpenDatabaseSettingsAsync);
                    return;
                }

                try
                {
                    status = await Task.Run(() => DatabaseSetup.GetStatus(quickConnection));
                }
                catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
                {
                    ShowMessage("Can't reach the database", $"Purchase Invoices couldn't connect to {Target()}.\n\n{FirstSentence(ex.Message)}",
                        "Try again", CheckAsync, "Database settings", OpenDatabaseSettingsAsync);
                    return;
                }

                if (!status.IsReady)
                {
                    if (status.Problems.Count > 0)
                    {
                        ShowMessage("The database needs attention", $"{Target(status)} has problems that must be fixed by hand first:\n\n• " + string.Join("\n• ", status.Problems),
                            "Try again", CheckAsync, "Database settings", OpenDatabaseSettingsAsync);
                    }
                    else
                    {
                        bool fresh = !status.DatabaseExists || status.MissingTables.Count == DatabaseSetup.TableNames.Count;
                        string intro = fresh
                            ? $"Purchase Invoices will set up {Target(status)}. It will:"
                            : $"This version of Purchase Invoices needs a few additions to {Target(status)}. It will:";
                        ShowMessage(fresh ? "Set up your database" : "Update your database",
                            $"{intro}\n\n• {string.Join("\n• ", status.GetPlannedSteps())}\n\nExisting tables and data are kept.",
                            fresh ? "Set up now" : "Update now", SetUpDatabaseAsync, "Database settings", OpenDatabaseSettingsAsync);
                    }
                    return;
                }

                AccountRepository accounts = new AccountRepository(connectionString);
                int users = 0;
                CompanyProfile company = null;
                try
                {
                    await Task.Run(() =>
                    {
                        users = accounts.CountUsers();
                        company = accounts.GetCompanyProfile();
                    });
                }
                catch (SqlException ex)
                {
                    ShowMessage("Can't read the user accounts", FirstSentence(ex.Message), "Try again", CheckAsync, "Database settings", OpenDatabaseSettingsAsync);
                    return;
                }
                brandPanel.CompanyText = company.Name;
                if (users == 0)
                {
                    ShowPage(pageSetup, btnCreateAccount);
                    txtCompany.Text = company.Name;
                    ActiveControl = txtCompany.Text.Length > 0 ? txtFullName : txtCompany;
                }
                else
                {
                    ShowPage(pageSignIn, btnSignIn);
                    ActiveControl = txtUserName.Text.Length > 0 ? txtPassword : txtUserName;
                }
            }
            finally
            {
                SetBusy(false);
            }
        }

        internal async Task SetUpDatabaseAsync()
        {
            DatabaseStatus plan = status;
            string target = connectionString;
            if (plan == null || target == null)
            {
                await CheckAsync();
                return;
            }
            SetBusy(true);
            ShowMessage("Setting up the database…", "Creating what's missing. This only takes a moment.");
            try
            {
                await Task.Run(() =>
                {
                    if (!plan.DatabaseExists)
                    {
                        DatabaseSetup.CreateDatabase(target);
                    }
                    DatabaseSetup.ApplyTableChanges(target, plan);
                });
            }
            catch (SqlException ex)
            {
                SetBusy(false);
                ShowMessage("Setup didn't finish", $"No tables were created or changed.\n\n{FirstSentence(ex.Message)}", "Try again", CheckAsync, "Database settings", OpenDatabaseSettingsAsync);
                return;
            }
            SetBusy(false);
            ActivityLog.Record(null, "Database set up", string.Join("; ", plan.GetPlannedSteps()));
            await CheckAsync();
        }

        private async Task OpenDatabaseSettingsAsync()
        {
            using (DatabaseSettingsForm settings = new DatabaseSettingsForm())
            {
                settings.ShowDialog(this);
            }
            await CheckAsync();
        }

        internal async Task<bool> SignInAsync()
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text;
            if (userName.Length == 0)
            {
                return SignInError("Enter your user name.", txtUserName);
            }
            if (password.Length == 0)
            {
                return SignInError("Enter your password.", txtPassword);
            }

            AccountRepository accounts = new AccountRepository(connectionString);
            SignInResult result;
            SetBusy(true, btnSignIn);
            lblSignInError.Text = string.Empty;
            try
            {
                result = await Task.Run(() => accounts.SignIn(userName, password));
            }
            catch (SqlException ex)
            {
                return SignInError("Couldn't sign in. " + FirstSentence(ex.Message), txtPassword);
            }
            finally
            {
                SetBusy(false);
            }

            switch (result.Status)
            {
                case SignInStatus.WrongPassword:
                    ActivityLog.Record(userName, "Sign-in failed", "Wrong user name or password");
                    return SignInError("The user name or password is incorrect.", txtPassword);
                case SignInStatus.Locked:
                    ActivityLog.Record(userName, "Sign-in failed", "Account locked");
                    return SignInError($"Too many failed attempts. This account is locked until {result.LockedUntil.Value.ToString("t", CultureInfo.CurrentCulture)}. Try again later, or ask an Admin to reset the password.", txtPassword);
                case SignInStatus.Inactive:
                    ActivityLog.Record(userName, "Sign-in failed", "Account turned off");
                    return SignInError("This account is turned off. Ask an Admin to turn it back on.", txtPassword);
            }

            AppUser user = result.User;
            if (user.MustChangePassword)
            {
                using (ChangePasswordForm change = new ChangePasswordForm(connectionString, user, ChangePasswordMode.Forced))
                {
                    if (change.ShowDialog(this) != DialogResult.OK)
                    {
                        return SignInError("Choose a new password to continue.", txtPassword);
                    }
                }
                user.MustChangePassword = false;
            }

            Session.Current = user;
            Session.RememberedUserName = chkRemember.Checked ? user.UserName : null;
            ActivityLog.Record("Signed in");
            DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        internal async Task<bool> CreateAccountAsync()
        {
            string company = txtCompany.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string userName = txtNewUserName.Text.Trim();
            if (company.Length == 0)
            {
                return SetupError("Enter your company name.", txtCompany);
            }
            if (fullName.Length == 0)
            {
                return SetupError("Enter your full name.", txtFullName);
            }
            string problem = AccountRules.CheckUserName(userName);
            if (problem != null)
            {
                return SetupError(problem, txtNewUserName);
            }
            problem = AccountRules.CheckNewPassword(txtNewPassword.Text, txtConfirmPassword.Text, userName);
            if (problem != null)
            {
                return SetupError(problem, problem.Contains("match") ? txtConfirmPassword : txtNewPassword);
            }

            AccountRepository accounts = new AccountRepository(connectionString);
            string password = txtNewPassword.Text;
            AppUser user;
            SetBusy(true, btnCreateAccount);
            lblSetupError.Text = string.Empty;
            try
            {
                user = await Task.Run(() => accounts.CreateFirstAdmin(company, fullName, userName, password));
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
            {
                return SetupError(FirstSentence(ex.Message), txtNewUserName);
            }
            finally
            {
                SetBusy(false);
            }

            Session.Current = user;
            Session.RememberedUserName = user.UserName;
            ActivityLog.Record("Account created", $"First Admin for {company}");
            ActivityLog.Record("Signed in");
            DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        private bool SignInError(string message, Control focus)
        {
            lblSignInError.Text = message;
            focus.Focus();
            (focus as TextBox)?.SelectAll();
            return false;
        }

        private bool SetupError(string message, Control focus)
        {
            lblSetupError.Text = message;
            focus.Focus();
            (focus as TextBox)?.SelectAll();
            return false;
        }

        private void ShowMessage(string title, string text, string primaryText = null, Func<Task> primary = null, string secondaryText = null, Func<Task> secondary = null)
        {
            lblMessageTitle.Text = title;
            lblMessage.Text = text;
            primaryAction = primary;
            secondaryAction = secondary;
            btnMessagePrimary.Text = primaryText ?? string.Empty;
            btnMessagePrimary.Visible = primary != null;
            btnMessageSecondary.Text = secondaryText ?? string.Empty;
            btnMessageSecondary.Visible = secondary != null;
            ShowPage(pageMessage, primary != null ? btnMessagePrimary : null);
        }

        private void ShowPage(Panel page, IButtonControl accept)
        {
            foreach (Panel candidate in new[] { pageSignIn, pageSetup, pageMessage })
            {
                candidate.Visible = candidate == page;
            }
            AcceptButton = accept;
        }

        private void SetBusy(bool value, ModernButton active = null)
        {
            busy = value;
            foreach (ModernButton button in new[] { btnSignIn, btnCreateAccount, btnMessagePrimary, btnMessageSecondary, btnDatabaseSettings })
            {
                button.Busy = value && button == active;
                button.Enabled = !value || button == active;
            }
            spinner.Visible = value && active == null;
        }

        private void UpdateConnectionLabel()
        {
            ConnectionDetails details = AppConnection.Current;
            lblConnection.Text = string.IsNullOrWhiteSpace(details.Database)
                ? "No database connected yet"
                : $"Database: {details.Database} on {details.Server}";
        }

        private string Target()
        {
            ConnectionDetails details = AppConnection.Current;
            return $"database '{details.Database}' on '{details.Server}'";
        }

        private static string Target(DatabaseStatus status) => $"database '{status.Database}' on '{status.Server}'";

        private static string FirstSentence(string message)
        {
            int end = message.IndexOf(". ", StringComparison.Ordinal);
            return end > 0 ? message.Substring(0, end + 1) : message;
        }

        private void SignInField_TextChanged(object sender, EventArgs e)
        {
            lblSignInError.Text = string.Empty;
        }

        private void SetupField_TextChanged(object sender, EventArgs e)
        {
            lblSetupError.Text = string.Empty;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private async void btnSignIn_Click(object sender, EventArgs e)
        {
            await SignInAsync();
        }

        private async void btnCreateAccount_Click(object sender, EventArgs e)
        {
            await CreateAccountAsync();
        }

        private async void btnDatabaseSettings_Click(object sender, EventArgs e)
        {
            await OpenDatabaseSettingsAsync();
        }

        private async void btnMessagePrimary_Click(object sender, EventArgs e)
        {
            if (primaryAction != null)
            {
                await primaryAction();
            }
        }

        private async void btnMessageSecondary_Click(object sender, EventArgs e)
        {
            if (secondaryAction != null)
            {
                await secondaryAction();
            }
        }

        private void SignInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
