using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class UsersForm : Form
    {
        private readonly string connectionString;
        private readonly AccountRepository accounts;
        private List<AppUser> users = new List<AppUser>();
        private AppUser editing;
        private bool loading;
        private bool dirty;
        private bool busy;
        private bool selectionPending;

        public UsersForm(string connectionString)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 900, 580);
            this.connectionString = connectionString;
            accounts = new AccountRepository(connectionString);
            cboRole.Items.AddRange(new object[] { "Admin", "Staff" });
            cboRole.Descriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Admin", "Everything, including users, backups and settings" },
                { "Staff", "Invoices, customers, items and reports" },
            };
            cboRole.DropDownWidth = Theme.S(420);
            btnSave.BusyText = "Saving…";
            grid.AutoGenerateColumns = false;
            AddColumn("UserName", "User name", 19);
            AddColumn("FullName", "Full name", 26);
            AddColumn("Role", "Role", 11);
            AddColumn("Status", "Status", 21);
            AddColumn("LastSignIn", "Last sign-in", 23);
        }

        public bool CurrentUserChanged { get; private set; }

        private bool EditingSelf => editing != null && Session.Current != null && editing.Id == Session.Current.Id;

        private void AddColumn(string name, string header, float weight)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = name, HeaderText = header, FillWeight = weight });
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            LoadUsers(Session.Current?.Id);
        }

        private void LoadUsers(int? selectId)
        {
            try
            {
                using (BusyIndicator.Start(this, "Loading users…"))
                {
                    users = accounts.GetUsers();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, "Could not load the users.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            loading = true;
            try
            {
                grid.Rows.Clear();
                foreach (AppUser user in users)
                {
                    int index = grid.Rows.Add(user.UserName, user.FullName, user.RoleName, StatusText(user),
                        user.LastSignIn?.ToString("dd MMM yyyy, HH:mm", CultureInfo.CurrentCulture) ?? "Never");
                    grid.Rows[index].Tag = user;
                }
                DataGridViewRow row = grid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => ((AppUser)r.Tag).Id == selectId) ?? grid.Rows.Cast<DataGridViewRow>().FirstOrDefault();
                if (row != null)
                {
                    grid.CurrentCell = row.Cells[0];
                }
            }
            finally
            {
                loading = false;
            }
            int admins = users.Count(u => u.IsAdmin && u.IsActive);
            lblStatus.Text = $"{users.Count} {(users.Count == 1 ? "user" : "users")}  ·  {admins} active {(admins == 1 ? "Admin" : "Admins")}";
            ShowUser(grid.CurrentRow?.Tag as AppUser);
        }

        private static string StatusText(AppUser user)
        {
            if (!user.IsActive)
            {
                return "Turned off";
            }
            if (user.LockedUntil.HasValue && user.LockedUntil.Value > DateTime.Now)
            {
                return "Locked";
            }
            return user.MustChangePassword ? "New password due" : "Active";
        }

        private void ShowUser(AppUser user)
        {
            loading = true;
            try
            {
                editing = user;
                bool isNew = user == null;
                editorCard.Title = isNew ? "New user" : "User details";
                txtUserName.Text = user?.UserName ?? string.Empty;
                txtUserName.ReadOnly = !isNew;
                txtFullName.Text = user?.FullName ?? string.Empty;
                cboRole.SelectedIndex = user != null && user.IsAdmin ? 0 : 1;
                chkActive.Checked = isNew || user.IsActive;
                cboRole.Enabled = !EditingSelf;
                chkActive.Enabled = !EditingSelf;

                foreach (Control control in new Control[] { lblPassword, hostPassword, lblConfirm, hostConfirm, chkMustChange })
                {
                    control.Visible = isNew;
                }
                txtPassword.Text = string.Empty;
                txtConfirm.Text = string.Empty;
                chkMustChange.Checked = true;
                lblInfo.Visible = !isNew;
                lblInfo.Text = isNew ? string.Empty : InfoText(user);
                btnResetPassword.Visible = !isNew;
                lblError.Text = string.Empty;
            }
            finally
            {
                loading = false;
            }
            dirty = false;
            UpdateButtons();
        }

        private string InfoText(AppUser user)
        {
            List<string> lines = new List<string>
            {
                user.LastSignIn.HasValue ? "Last signed in " + user.LastSignIn.Value.ToString("dd MMM yyyy, HH:mm", CultureInfo.CurrentCulture) : "Hasn't signed in yet.",
            };
            if (user.CreatedAt.HasValue)
            {
                lines.Add("Added on " + user.CreatedAt.Value.ToString("dd MMM yyyy", CultureInfo.CurrentCulture));
            }
            if (user.LockedUntil.HasValue && user.LockedUntil.Value > DateTime.Now)
            {
                lines.Add($"Locked after too many failed sign-ins, until {user.LockedUntil.Value.ToString("t", CultureInfo.CurrentCulture)}. Reset the password to unlock it now.");
            }
            if (EditingSelf)
            {
                lines.Add("This is you. You can't change your own role or turn yourself off. Use Change password in your account menu for your password.");
            }
            return string.Join("\n", lines);
        }

        private void UpdateButtons()
        {
            btnSave.Enabled = dirty || busy;
            btnSave.Busy = busy;
            btnResetPassword.Enabled = !busy && editing != null && !EditingSelf;
            btnNewUser.Enabled = !busy;
        }

        private void Field_Changed(object sender, EventArgs e)
        {
            if (loading)
            {
                return;
            }
            dirty = true;
            lblError.Text = string.Empty;
            UpdateButtons();
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (loading || selectionPending)
            {
                return;
            }
            selectionPending = true;
            BeginInvoke((Action)HandleSelection);
        }

        private void HandleSelection()
        {
            selectionPending = false;
            AppUser selected = grid.CurrentRow?.Tag as AppUser;
            if (selected == null || (editing != null && selected.Id == editing.Id))
            {
                return;
            }
            if (dirty && !ConfirmDiscard())
            {
                SelectRow(editing?.Id);
                return;
            }
            ShowUser(selected);
        }

        private void SelectRow(int? id)
        {
            loading = true;
            try
            {
                DataGridViewRow row = grid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => ((AppUser)r.Tag).Id == id);
                if (row != null)
                {
                    grid.CurrentCell = row.Cells[0];
                }
                else
                {
                    grid.CurrentCell = null;
                    grid.ClearSelection();
                }
            }
            finally
            {
                loading = false;
            }
        }

        private bool ConfirmDiscard()
        {
            string who = editing == null ? "this new user" : editing.UserName;
            return MessageBox.Show(this, $"Discard your changes to {who}?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        internal void StartNewUser()
        {
            if (dirty && !ConfirmDiscard())
            {
                return;
            }
            SelectRow(null);
            ShowUser(null);
            ActiveControl = txtUserName;
        }

        internal async Task<bool> SaveAsync()
        {
            string fullName = txtFullName.Text.Trim();
            UserRole role = cboRole.SelectedIndex == 0 ? UserRole.Admin : UserRole.Staff;
            bool active = chkActive.Checked;
            if (editing == null)
            {
                string userName = txtUserName.Text.Trim();
                string problem = AccountRules.CheckUserName(userName);
                if (problem != null)
                {
                    return Error(problem, txtUserName);
                }
                if (fullName.Length == 0)
                {
                    return Error("Enter the person's full name.", txtFullName);
                }
                problem = AccountRules.CheckNewPassword(txtPassword.Text, txtConfirm.Text, userName);
                if (problem != null)
                {
                    return Error(problem, problem.Contains("match") ? txtConfirm : txtPassword);
                }
                string password = txtPassword.Text;
                bool mustChange = chkMustChange.Checked;
                AppUser created;
                SetBusy(true);
                try
                {
                    created = await Task.Run(() =>
                    {
                        AppUser user = accounts.CreateUser(userName, fullName, role, password, mustChange);
                        if (!active)
                        {
                            accounts.UpdateUser(user.Id, fullName, role, false);
                        }
                        return user;
                    });
                }
                catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
                {
                    return Error(ex.Message, txtUserName);
                }
                finally
                {
                    SetBusy(false);
                }
                ActivityLog.Record("User added", $"{created.UserName} ({created.RoleName})");
                dirty = false;
                LoadUsers(created.Id);
                return true;
            }

            if (fullName.Length == 0)
            {
                return Error("Enter the person's full name.", txtFullName);
            }
            AppUser before = editing;
            SetBusy(true);
            try
            {
                await Task.Run(() => accounts.UpdateUser(before.Id, fullName, role, active));
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
            {
                return Error(ex.Message, cboRole);
            }
            finally
            {
                SetBusy(false);
            }

            List<string> changes = new List<string>();
            if (!string.Equals(before.FullName ?? string.Empty, fullName, StringComparison.Ordinal))
            {
                changes.Add($"name set to {fullName}");
            }
            if (before.Role != role)
            {
                changes.Add($"role set to {(role == UserRole.Admin ? "Admin" : "Staff")}");
            }
            if (before.IsActive != active)
            {
                changes.Add(active ? "turned on" : "turned off");
            }
            if (changes.Count > 0)
            {
                ActivityLog.Record("User changed", $"{before.UserName}: {string.Join(", ", changes)}");
            }
            if (Session.Current != null && Session.Current.Id == before.Id)
            {
                Session.Current.FullName = fullName;
                CurrentUserChanged = true;
            }
            dirty = false;
            LoadUsers(before.Id);
            return true;
        }

        private bool Error(string message, Control focus)
        {
            lblError.Text = message;
            focus.Focus();
            return false;
        }

        private void SetBusy(bool value)
        {
            busy = value;
            UpdateButtons();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveAsync();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            StartNewUser();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (editing == null || EditingSelf)
            {
                return;
            }
            if (dirty && !ConfirmDiscard())
            {
                return;
            }
            int id = editing.Id;
            using (ChangePasswordForm reset = new ChangePasswordForm(connectionString, editing, ChangePasswordMode.Reset))
            {
                if (reset.ShowDialog(this) == DialogResult.OK)
                {
                    LoadUsers(id);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy || (e.CloseReason == CloseReason.UserClosing && dirty && !ConfirmDiscard()))
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
            if (keyData == (Keys.Control | Keys.S) && btnSave.Enabled)
            {
                btnSave.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.N) && btnNewUser.Enabled)
            {
                btnNewUser.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
