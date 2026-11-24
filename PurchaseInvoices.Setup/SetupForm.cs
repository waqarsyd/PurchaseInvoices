using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseInvoices.Setup
{
    internal sealed class SetupForm : Form
    {
        private static readonly Color Ink = Color.FromArgb(15, 23, 42);
        private static readonly Color Muted = Color.FromArgb(100, 116, 139);
        private static readonly Color Accent = Color.FromArgb(79, 70, 229);
        private static readonly Color Accent2 = Color.FromArgb(124, 58, 237);
        private static readonly Color Line = Color.FromArgb(226, 232, 240);
        private static readonly Color Success = Color.FromArgb(4, 120, 87);
        private static readonly Color ErrorColor = Color.FromArgb(185, 28, 28);

        private readonly bool uninstalling;
        private readonly InstallOptions options;
        private readonly Image logo;
        private Panel banner;
        private Panel pageOptions;
        private Panel pageProgress;
        private Panel pageDone;
        private Label lblIntro;
        private TextBox txtFolder;
        private Button btnBrowse;
        private CheckBox chkDesktop;
        private CheckBox chkStartMenu;
        private CheckBox chkLaunch;
        private Label lblNote;
        private Label lblProgress;
        private ProgressBar progressBar;
        private Label lblDoneTitle;
        private Label lblDoneText;
        private Button btnPrimary;
        private Button btnCancel;
        private bool busy;
        private bool finished;

        public SetupForm(bool uninstall, InstallOptions options)
        {
            uninstalling = uninstall;
            this.options = options;
            using (Stream stream = typeof(SetupForm).Assembly.GetManifestResourceStream("PurchaseInvoices.Setup.AppLogo.png"))
            {
                logo = stream == null ? null : new Bitmap(Image.FromStream(stream));
            }
            InitializeComponent();
            try
            {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            }
            catch (ArgumentException)
            {
            }
            ShowOptions();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(640, 470);
            Font = new Font("Segoe UI", 9.75F);
            ForeColor = Ink;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = uninstalling ? "Remove Purchase Invoices" : "Purchase Invoices Setup";

            banner = new Panel { Dock = DockStyle.Top, Size = new Size(640, 110) };
            banner.Paint += Banner_Paint;
            banner.Resize += (s, e) => banner.Invalidate();

            Panel footer = new Panel { Dock = DockStyle.Bottom, Size = new Size(640, 64), BackColor = Color.FromArgb(248, 250, 252) };
            footer.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Line))
                {
                    e.Graphics.DrawLine(pen, 0, 0, footer.Width, 0);
                }
            };
            btnPrimary = MakeButton(uninstalling ? "Remove" : "Install", true);
            btnPrimary.Location = new Point(500, 14);
            btnPrimary.Size = new Size(120, 36);
            btnPrimary.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPrimary.Click += btnPrimary_Click;
            btnCancel = MakeButton("Cancel", false);
            btnCancel.Location = new Point(390, 14);
            btnCancel.Size = new Size(100, 36);
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Click += (s, e) => Close();
            footer.Controls.Add(btnPrimary);
            footer.Controls.Add(btnCancel);

            Panel body = new Panel { Dock = DockStyle.Fill, Size = new Size(640, 296), BackColor = Color.White };

            pageOptions = new Panel { Dock = DockStyle.Fill, Size = new Size(640, 296) };
            lblIntro = new Label { Location = new Point(32, 24), Size = new Size(576, 40), ForeColor = Ink, Font = new Font("Segoe UI", 9.75F) };
            Label lblFolder = new Label { Text = "Install to", AutoSize = true, Location = new Point(32, 76), BackColor = Color.Transparent, Font = new Font("Segoe UI Semibold", 9F), ForeColor = Color.FromArgb(71, 85, 105) };
            txtFolder = new TextBox { Location = new Point(32, 102), Size = new Size(460, 26), Font = new Font("Segoe UI", 10F) };
            btnBrowse = MakeButton("Browse…", false);
            btnBrowse.Location = new Point(502, 100);
            btnBrowse.Size = new Size(106, 30);
            btnBrowse.Click += btnBrowse_Click;
            chkDesktop = new CheckBox { Text = "Add a desktop shortcut", AutoSize = true, Location = new Point(33, 146), Checked = options.DesktopShortcut };
            chkStartMenu = new CheckBox { Text = "Add it to the Start menu", AutoSize = true, Location = new Point(33, 174), Checked = options.StartMenuShortcut };
            chkLaunch = new CheckBox { Text = "Start Purchase Invoices when setup finishes", AutoSize = true, Location = new Point(33, 202), Checked = true };
            lblNote = new Label { Location = new Point(32, 240), Size = new Size(576, 52), ForeColor = Muted, Font = new Font("Segoe UI", 9F) };
            pageOptions.Controls.AddRange(new Control[] { lblIntro, lblFolder, txtFolder, btnBrowse, chkDesktop, chkStartMenu, chkLaunch, lblNote });

            pageProgress = new Panel { Dock = DockStyle.Fill, Size = new Size(640, 296), Visible = false };
            lblProgress = new Label { Location = new Point(32, 70), Size = new Size(576, 24), ForeColor = Ink, Font = new Font("Segoe UI", 9.75F) };
            progressBar = new ProgressBar { Location = new Point(32, 100), Size = new Size(576, 12), Style = ProgressBarStyle.Continuous };
            pageProgress.Controls.AddRange(new Control[] { lblProgress, progressBar });

            pageDone = new Panel { Dock = DockStyle.Fill, Size = new Size(640, 296), Visible = false };
            lblDoneTitle = new Label { Location = new Point(32, 40), Size = new Size(576, 34), Font = new Font("Segoe UI Semibold", 15F), ForeColor = Success };
            lblDoneText = new Label { Location = new Point(32, 82), Size = new Size(576, 160), ForeColor = Ink, Font = new Font("Segoe UI", 9.75F) };
            pageDone.Controls.AddRange(new Control[] { lblDoneTitle, lblDoneText });

            body.Controls.Add(pageOptions);
            body.Controls.Add(pageProgress);
            body.Controls.Add(pageDone);
            Controls.Add(body);
            Controls.Add(footer);
            Controls.Add(banner);
            ResumeLayout(false);
        }

        private static Button MakeButton(string text, bool primary)
        {
            Button button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9.75F),
                BackColor = primary ? Accent : Color.White,
                ForeColor = primary ? Color.White : Ink,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false,
            };
            button.FlatAppearance.BorderColor = primary ? Accent : Color.FromArgb(203, 213, 225);
            button.FlatAppearance.MouseOverBackColor = primary ? Color.FromArgb(99, 91, 240) : Color.FromArgb(241, 245, 249);
            return button;
        }

        private void Banner_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle area = banner.ClientRectangle;
            if (area.Width <= 0 || area.Height <= 0)
            {
                return;
            }
            float scale = DeviceDpi / 96f;
            using (LinearGradientBrush brush = new LinearGradientBrush(area, Color.FromArgb(15, 23, 42), Color.FromArgb(46, 38, 120), LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, area);
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = (int)(58 * scale);
            int left = (int)(30 * scale);
            if (logo != null)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(logo, new Rectangle(left, (area.Height - size) / 2, size, size));
            }
            int textLeft = left + size + (int)(18 * scale);
            using (Font title = new Font("Segoe UI Semibold", 18F))
            using (Font subtitle = new Font("Segoe UI", 10F))
            {
                TextRenderer.DrawText(g, Installer.ProductName, title, new Point(textLeft, (int)(26 * scale)), Color.White, TextFormatFlags.NoPadding);
                string line = uninstalling ? "Remove from this PC" : $"Version {Installer.ShortVersion} setup";
                TextRenderer.DrawText(g, line, subtitle, new Point(textLeft + (int)(2 * scale), (int)(66 * scale)), Color.FromArgb(199, 210, 254), TextFormatFlags.NoPadding);
            }
        }

        private void ShowOptions()
        {
            string installed = Installer.InstalledFolder();
            string installedVersion = Installer.InstalledVersion();
            if (uninstalling)
            {
                lblIntro.Text = $"This removes Purchase Invoices from {options.Folder}, along with its shortcuts.";
                foreach (Control control in new Control[] { txtFolder, btnBrowse, chkDesktop, chkStartMenu, chkLaunch })
                {
                    control.Visible = false;
                }
                pageOptions.Controls.OfType<Label>().First(l => l.Text == "Install to").Visible = false;
                lblNote.Location = new Point(lblNote.Left, lblIntro.Bottom + 12);
                lblNote.Text = "Your invoices, customers, items and users are kept: they live in your SQL Server database, not in the program folder. Your saved connection settings stay too, in case you install it again.";
                return;
            }
            txtFolder.Text = installed ?? options.Folder;
            lblIntro.Text = installed == null
                ? "This installs Purchase Invoices for your Windows account. No administrator rights are needed."
                : installedVersion == Installer.ShortVersion
                    ? $"Purchase Invoices {installedVersion} is already installed. Setup will repair it, keeping your settings."
                    : $"Purchase Invoices {installedVersion} is installed. Setup will update it to {Installer.ShortVersion}, keeping your settings.";
            btnPrimary.Text = installed == null ? "Install" : installedVersion == Installer.ShortVersion ? "Repair" : "Update";
            lblNote.Text = "Purchase Invoices keeps its data in a SQL Server database (SQL Server Express is free). The first time it starts, it helps you connect to your server and sets up the database for you.";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog { Description = "Choose where to install Purchase Invoices", SelectedPath = Directory.Exists(txtFolder.Text) ? txtFolder.Text : string.Empty })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder.Text = dialog.SelectedPath.EndsWith(Installer.ProductName, StringComparison.OrdinalIgnoreCase) ? dialog.SelectedPath : Path.Combine(dialog.SelectedPath, Installer.ProductName);
                }
            }
        }

        private async void btnPrimary_Click(object sender, EventArgs e)
        {
            if (finished)
            {
                if (!uninstalling && chkLaunch.Checked)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(Path.Combine(options.Folder, Installer.AppExe)) { WorkingDirectory = options.Folder, UseShellExecute = true });
                    }
                    catch (Exception ex) when (ex is System.ComponentModel.Win32Exception || ex is FileNotFoundException)
                    {
                        MessageBox.Show(this, "Purchase Invoices was installed, but it didn't start: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                Close();
                return;
            }

            if (!uninstalling)
            {
                string folder = txtFolder.Text.Trim();
                try
                {
                    folder = Path.GetFullPath(folder);
                }
                catch (Exception ex) when (ex is ArgumentException || ex is NotSupportedException || ex is PathTooLongException)
                {
                    MessageBox.Show(this, "That folder isn't valid. " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                options.Folder = folder;
                options.DesktopShortcut = chkDesktop.Checked;
                options.StartMenuShortcut = chkStartMenu.Checked;
            }
            if (!CloseRunningCopies())
            {
                return;
            }

            busy = true;
            pageOptions.Visible = false;
            pageProgress.Visible = true;
            btnPrimary.Enabled = false;
            btnCancel.Enabled = false;
            Action<int, string> progress = (percent, text) => BeginInvoke((MethodInvoker)(() =>
            {
                progressBar.Value = Math.Max(0, Math.Min(100, percent));
                lblProgress.Text = text;
            }));
            Exception failure = null;
            try
            {
                if (uninstalling)
                {
                    await Task.Run(() => Installer.Uninstall(options.Folder, options.Register, true, progress));
                }
                else
                {
                    await Task.Run(() => Installer.Install(options, progress));
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException || ex is System.Runtime.InteropServices.COMException)
            {
                failure = ex;
            }
            busy = false;
            finished = true;
            pageProgress.Visible = false;
            pageDone.Visible = true;
            btnCancel.Visible = false;
            btnPrimary.Enabled = true;
            btnPrimary.Text = "Finish";
            if (failure != null)
            {
                lblDoneTitle.ForeColor = ErrorColor;
                lblDoneTitle.Text = uninstalling ? "Couldn't remove everything" : "Setup didn't finish";
                lblDoneText.Text = failure.Message + "\n\nClose any open Purchase Invoices windows and run setup again.";
                chkLaunch.Checked = false;
                return;
            }
            lblDoneTitle.Text = uninstalling ? "Purchase Invoices was removed" : "Purchase Invoices is ready";
            lblDoneText.Text = uninstalling
                ? "Its program files and shortcuts are gone. Your data is still in your SQL Server database."
                : $"It's installed in {options.Folder}." + (options.StartMenuShortcut ? "\n\nYou'll find it in the Start menu" + (options.DesktopShortcut ? " and on your desktop." : ".") : string.Empty)
                  + "\n\nThe first time it starts, choose your SQL Server and database, then create the administrator account.";
        }

        private bool CloseRunningCopies()
        {
            while (true)
            {
                var running = Installer.RunningCopies(options.Folder);
                if (running.Count == 0)
                {
                    return true;
                }
                foreach (Process process in running)
                {
                    process.Dispose();
                }
                if (MessageBox.Show(this, "Purchase Invoices is open. Save your work and close it, then click Retry.", Text, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) != DialogResult.Retry)
                {
                    return false;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ActiveControl = btnPrimary;
            txtFolder.SelectionStart = txtFolder.TextLength;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (busy)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                logo?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
