using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class BackupForm : Form
    {
        private static readonly Color SuccessColor = Color.FromArgb(4, 120, 87);
        private static readonly Color WarningColor = Color.FromArgb(180, 83, 9);
        private static readonly Color ErrorColor = Color.FromArgb(185, 28, 28);

        private readonly string connectionString;
        private readonly string database;
        private readonly bool local;
        private BackupFileInfo fileInfo;
        private int fileCheck;
        private bool busy;

        public BackupForm(string connectionString)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 700, 640);
            this.connectionString = connectionString;
            database = DatabaseBackup.TargetDatabase(connectionString);
            local = DatabaseBackup.IsLocalServer(connectionString);
            lblTarget.Text = $"Database {database} on {DatabaseBackup.TargetServer(connectionString)}";
            lblBackupIntro.Text = $"Saves a full, checked copy of '{database}'. Everyone can keep working while it runs.";
            lblRestoreIntro.Text = $"Replaces everything in '{database}' with the backup you choose. A safety backup of the current data is taken first, so a restore can be undone.";
            lblConfirm.Text = $"Type {database} to confirm";
            btnBackup.BusyText = "Backing up…";
            btnRestore.BusyText = "Restoring…";
            btnBrowseFolder.Enabled = local;
            btnBrowseFile.Enabled = local;
            grid.AutoGenerateColumns = false;
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "File", HeaderText = "Backup", FillWeight = 52 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Taken", HeaderText = "Taken", FillWeight = 30 });
            DataGridViewTextBoxColumn size = new DataGridViewTextBoxColumn { Name = "Size", HeaderText = "Size", FillWeight = 18 };
            size.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grid.Columns.Add(size);
        }

        public bool Restored { get; private set; }

        internal string LastBackupPath { get; private set; }

        private string Folder => txtFolder.Text.Trim();

        private void BackupForm_Load(object sender, EventArgs e)
        {
            using (BusyIndicator.Start(this, "Opening backup and restore…"))
            {
                txtFolder.Text = DatabaseBackup.DefaultFolder(connectionString);
                RefreshList();
            }
            SetStatus(Theme.TextMuted, $"Saved as {DatabaseBackup.NewFileName(connectionString)}.");
        }

        internal void RefreshList()
        {
            grid.Rows.Clear();
            if (!local)
            {
                grid.EmptyTitle = "Backups are on the server";
                grid.EmptyHint = "Type the full path of a backup file below.";
                return;
            }
            grid.EmptyTitle = "No backups in this folder yet";
            grid.EmptyHint = "Backups you make appear here.";
            try
            {
                DirectoryInfo folder = new DirectoryInfo(Folder);
                if (folder.Exists)
                {
                    foreach (FileInfo file in folder.GetFiles("*.bak").OrderByDescending(f => f.LastWriteTime).Take(50))
                    {
                        int index = grid.Rows.Add(file.Name, file.LastWriteTime.ToString("dd MMM yyyy, HH:mm", CultureInfo.CurrentCulture), SizeText(file.Length));
                        grid.Rows[index].Tag = file.FullName;
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is ArgumentException || ex is NotSupportedException)
            {
                grid.EmptyTitle = "Can't list this folder";
                grid.EmptyHint = "Windows doesn't let you open it. Browse to a backup file or type its path below.";
            }
            grid.ClearSelection();
        }

        private static string SizeText(long bytes)
        {
            return bytes >= 1024 * 1024
                ? (bytes / 1048576.0).ToString("0.0", CultureInfo.CurrentCulture) + " MB"
                : Math.Max(1, bytes / 1024).ToString(CultureInfo.CurrentCulture) + " KB";
        }

        internal async Task<bool> BackupAsync()
        {
            string folder = Folder;
            if (folder.Length == 0)
            {
                SetStatus(ErrorColor, "Choose a folder for the backup.");
                return false;
            }
            if (local)
            {
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is ArgumentException || ex is NotSupportedException)
                {
                    SetStatus(ErrorColor, "✗  Can't use that folder. " + ex.Message);
                    return false;
                }
            }
            string path = Path.Combine(folder, DatabaseBackup.NewFileName(connectionString));
            SetBusy(true, btnBackup);
            SetStatus(Theme.TextMuted, "Backing up, then checking the copy…");
            try
            {
                await Task.Run(() => DatabaseBackup.Backup(connectionString, path));
            }
            catch (SqlException ex)
            {
                SetStatus(ErrorColor, "✗  " + DatabaseBackup.Explain(ex));
                return false;
            }
            finally
            {
                SetBusy(false, null);
            }
            string size = string.Empty;
            try
            {
                if (local && File.Exists(path))
                {
                    size = "  ·  " + SizeText(new FileInfo(path).Length);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            LastBackupPath = path;
            SetStatus(SuccessColor, $"✓  Saved and checked {Path.GetFileName(path)}{size}");
            ActivityLog.Record("Backup created", path);
            RefreshList();
            return true;
        }

        internal async Task CheckFileAsync()
        {
            int check = ++fileCheck;
            string path = txtFile.Text.Trim();
            fileInfo = null;
            UpdateRestoreButton();
            if (path.Length == 0)
            {
                lblFileInfo.Text = string.Empty;
                return;
            }
            lblFileInfo.ForeColor = Theme.TextMuted;
            lblFileInfo.Text = "Reading the backup…";
            BackupFileInfo info = null;
            string problem = null;
            try
            {
                info = await Task.Run(() => DatabaseBackup.ReadHeader(connectionString, path));
            }
            catch (SqlException ex)
            {
                problem = DatabaseBackup.Explain(ex);
            }
            if (check != fileCheck || IsDisposed)
            {
                return;
            }
            if (info == null)
            {
                lblFileInfo.ForeColor = ErrorColor;
                lblFileInfo.Text = "✗  " + (problem ?? "That isn't a SQL Server backup file.");
                return;
            }
            fileInfo = info;
            string when = info.Finished?.ToString("dd MMM yyyy, HH:mm", CultureInfo.CurrentCulture) ?? "an unknown date";
            string text = $"Backup of '{info.DatabaseName}' from {info.ServerName}, taken {when}" + (info.Size.HasValue ? "  ·  " + SizeText(info.Size.Value) : string.Empty);
            bool other = !string.Equals(info.DatabaseName, database, StringComparison.OrdinalIgnoreCase);
            lblFileInfo.ForeColor = other ? WarningColor : Theme.TextSecondary;
            lblFileInfo.Text = other ? $"{text}. It comes from a different database; restoring replaces '{database}' with its contents." : text;
            UpdateRestoreButton();
        }

        private void UpdateRestoreButton()
        {
            btnRestore.Enabled = busy ? btnRestore.Busy : fileInfo != null && string.Equals(txtConfirm.Text.Trim(), database, StringComparison.OrdinalIgnoreCase);
        }

        internal async Task<bool> RestoreAsync()
        {
            UpdateRestoreButton();
            if (!btnRestore.Enabled || busy)
            {
                return false;
            }
            string path = txtFile.Text.Trim();
            string folder = Folder.Length > 0 ? Folder : Path.GetDirectoryName(path);
            string safety = Path.Combine(folder, DatabaseBackup.NewFileName(connectionString, "before-restore"));
            bool restoring = false;
            SetBusy(true, btnRestore);
            lblFileInfo.ForeColor = Theme.TextMuted;
            lblFileInfo.Text = "Taking a safety backup of the current data…";
            try
            {
                await Task.Run(() => DatabaseBackup.Backup(connectionString, safety));
                restoring = true;
                lblFileInfo.Text = "Restoring… everyone else is disconnected until it finishes.";
                await Task.Run(() => DatabaseBackup.Restore(connectionString, path));
            }
            catch (SqlException ex)
            {
                SetBusy(false, null);
                lblFileInfo.ForeColor = ErrorColor;
                lblFileInfo.Text = "✗  " + DatabaseBackup.Explain(ex) + (restoring
                    ? $" If the database no longer opens, restore the safety backup {Path.GetFileName(safety)}."
                    : " Nothing was changed.");
                UpdateRestoreButton();
                return false;
            }
            SetBusy(false, null);
            Restored = true;
            ActivityLog.Record("Database restored", $"From {Path.GetFileName(path)}; safety backup {Path.GetFileName(safety)}");
            MessageBox.Show(this, $"'{database}' was restored from {Path.GetFileName(path)}.\n\nThe data you had before is saved in {safety}.\n\nYou'll now be signed out, so that the restored data and user accounts are loaded.",
                "Restore finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        private void SetStatus(Color color, string text)
        {
            lblBackupStatus.ForeColor = color;
            lblBackupStatus.Text = text;
        }

        private void SetBusy(bool value, ModernButton active)
        {
            busy = value;
            btnBackup.Busy = value && active == btnBackup;
            btnRestore.Busy = value && active == btnRestore;
            btnBackup.Enabled = !value || active == btnBackup;
            btnClose.Enabled = !value;
            btnBrowseFolder.Enabled = !value && local;
            btnBrowseFile.Enabled = !value && local;
            txtFolder.ReadOnly = value;
            txtFile.ReadOnly = value;
            txtConfirm.ReadOnly = value;
            grid.Enabled = !value;
            UpdateRestoreButton();
        }

        private async void btnBackup_Click(object sender, EventArgs e)
        {
            await BackupAsync();
        }

        private async void btnRestore_Click(object sender, EventArgs e)
        {
            await RestoreAsync();
        }

        private async void txtFile_TextChanged(object sender, EventArgs e)
        {
            await CheckFileAsync();
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {
            UpdateRestoreButton();
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            string path = grid.CurrentRow != null && grid.CurrentRow.Selected ? grid.CurrentRow.Tag as string : null;
            if (path != null && !busy)
            {
                txtFile.Text = path;
            }
        }

        private void txtFolder_Leave(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog { Description = "Choose where to save backups. SQL Server itself writes the file, so it must be allowed to use this folder.", SelectedPath = Folder })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder.Text = dialog.SelectedPath;
                    RefreshList();
                }
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog { Title = "Choose a backup to restore", Filter = "SQL Server backups (*.bak)|*.bak|All files (*.*)|*.*", CheckFileExists = true })
            {
                if (Directory.Exists(Folder))
                {
                    dialog.InitialDirectory = Folder;
                }
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtFile.Text = dialog.FileName;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BackupForm_FormClosing(object sender, FormClosingEventArgs e)
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
