using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal enum ChangePasswordMode
    {
        Own,
        Forced,
        Reset
    }

    internal partial class ChangePasswordForm : Form
    {
        private readonly AccountRepository accounts;
        private readonly AppUser user;
        private readonly ChangePasswordMode mode;
        private bool busy;

        public ChangePasswordForm(string connectionString, AppUser user, ChangePasswordMode mode)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 480, 440);
            accounts = new AccountRepository(connectionString);
            this.user = user;
            this.mode = mode;

            switch (mode)
            {
                case ChangePasswordMode.Own:
                    lblIntro.Text = "Enter your current password, then choose a new one.";
                    break;
                case ChangePasswordMode.Forced:
                    headerPanel.Subtitle = "Choose a new password to continue";
                    lblIntro.Text = $"Welcome, {user.DisplayName}. Your password was set by an Admin, so please choose your own before you continue.";
                    break;
                default:
                    Text = "Reset password";
                    headerPanel.Title = "Reset password";
                    headerPanel.Subtitle = $"Set a temporary password for {user.DisplayName}";
                    lblIntro.Text = $"Choose a temporary password for {user.UserName} and tell it to them. Resetting also unlocks the account.";
                    lblNew.Text = "Temporary password";
                    lblConfirm.Text = "Confirm temporary password";
                    btnSave.Text = "Reset password";
                    chkMustChange.Visible = true;
                    break;
            }

            if (mode != ChangePasswordMode.Own)
            {
                int shift = lblNew.Top - lblCurrent.Top;
                lblCurrent.Visible = false;
                hostCurrent.Visible = false;
                foreach (Control control in new Control[] { lblNew, hostNew, lblConfirm, hostConfirm, lblRule, chkMustChange, lblError })
                {
                    control.Top -= shift;
                }
            }
            if (chkMustChange.Visible)
            {
                lblError.Top = chkMustChange.Bottom + (lblRule.Top - hostConfirm.Bottom);
            }
            ActiveControl = mode == ChangePasswordMode.Own ? txtCurrent : txtNew;
            btnSave.BusyText = "Saving…";
        }

        internal async Task<bool> SaveAsync()
        {
            string current = txtCurrent.Text;
            string password = txtNew.Text;
            if (mode == ChangePasswordMode.Own && current.Length == 0)
            {
                return Error("Enter your current password.", txtCurrent);
            }
            string problem = AccountRules.CheckNewPassword(password, txtConfirm.Text, user.UserName);
            if (problem != null)
            {
                return Error(problem, problem.Contains("match") ? txtConfirm : txtNew);
            }
            if (mode == ChangePasswordMode.Own && password == current)
            {
                return Error("Choose a password that's different from your current one.", txtNew);
            }

            bool mustChange = mode == ChangePasswordMode.Reset && chkMustChange.Checked;
            SetBusy(true);
            try
            {
                if (mode == ChangePasswordMode.Own && !await Task.Run(() => accounts.CheckPassword(user.Id, current)))
                {
                    return Error("Your current password is incorrect.", txtCurrent);
                }
                await Task.Run(() => accounts.SetPassword(user.Id, password, mustChange));
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
            {
                return Error(ex.Message, txtNew);
            }
            finally
            {
                SetBusy(false);
            }

            if (mode == ChangePasswordMode.Reset)
            {
                ActivityLog.Record("Password reset", $"For {user.UserName}" + (mustChange ? ", who must choose a new one at sign-in" : string.Empty));
            }
            else
            {
                ActivityLog.Record(user.UserName, "Password changed", mode == ChangePasswordMode.Forced ? "After an Admin reset" : null);
            }
            DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        private bool Error(string message, Control focus)
        {
            lblError.Text = message;
            focus.Focus();
            (focus as TextBox)?.SelectAll();
            return false;
        }

        private void SetBusy(bool value)
        {
            busy = value;
            btnSave.Busy = value;
            btnCancel.Enabled = !value;
        }

        private void Field_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
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
            if (keyData == Keys.Enter && !busy)
            {
                btnSave.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
