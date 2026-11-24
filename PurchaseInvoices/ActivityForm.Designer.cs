namespace PurchaseInvoices
{
    partial class ActivityForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerPanel = new PurchaseInvoices.HeaderPanel();
            this.hostFilter = new PurchaseInvoices.FieldHost();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.footerPanel = new PurchaseInvoices.FooterPanel();
            this.lblSummary = new System.Windows.Forms.Label();
            this.btnExport = new PurchaseInvoices.ModernButton();
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.listCard = new PurchaseInvoices.CardPanel();
            this.grid = new PurchaseInvoices.ModernGrid();
            this.filterCard = new PurchaseInvoices.CardPanel();
            this.lblUser = new System.Windows.Forms.Label();
            this.comboUser = new PurchaseInvoices.ModernComboBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.comboPeriod = new PurchaseInvoices.ModernComboBox();
            this.btnClearFilters = new PurchaseInvoices.ModernButton();
            this.headerPanel.SuspendLayout();
            this.hostFilter.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.listCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.filterCard.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.hostFilter);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(980, 64);
            this.headerPanel.Subtitle = "Who did what, and when";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Activity log";
            //
            // hostFilter
            //
            this.hostFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostFilter.Controls.Add(this.txtFilter);
            this.hostFilter.Glyph = "";
            this.hostFilter.Location = new System.Drawing.Point(620, 14);
            this.hostFilter.Name = "hostFilter";
            this.hostFilter.Placeholder = "Search activity, details or user";
            this.hostFilter.Size = new System.Drawing.Size(340, 36);
            this.hostFilter.TabIndex = 0;
            //
            // txtFilter
            //
            this.txtFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilter.Location = new System.Drawing.Point(36, 8);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(292, 18);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.FilterChanged);
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.lblSummary);
            this.footerPanel.Controls.Add(this.btnExport);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 560);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(980, 60);
            this.footerPanel.TabIndex = 2;
            //
            // lblSummary
            //
            this.lblSummary.AutoSize = true;
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSummary.Location = new System.Drawing.Point(20, 21);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(0, 17);
            this.lblSummary.TabIndex = 2;
            //
            // btnExport
            //
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnExport.Glyph = "";
            this.btnExport.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnExport.Location = new System.Drawing.Point(690, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 36);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export CSV";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(850, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.listCard);
            this.contentPanel.Controls.Add(this.filterCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(980, 496);
            this.contentPanel.TabIndex = 1;
            //
            // listCard
            //
            this.listCard.Controls.Add(this.grid);
            this.listCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCard.Glyph = "";
            this.listCard.Location = new System.Drawing.Point(14, 104);
            this.listCard.Name = "listCard";
            this.listCard.Size = new System.Drawing.Size(952, 384);
            this.listCard.TabIndex = 1;
            this.listCard.Title = "Activity";
            //
            // grid
            //
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.Location = new System.Drawing.Point(20, 46);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(912, 320);
            this.grid.TabIndex = 0;
            //
            // filterCard
            //
            this.filterCard.Controls.Add(this.lblUser);
            this.filterCard.Controls.Add(this.comboUser);
            this.filterCard.Controls.Add(this.lblPeriod);
            this.filterCard.Controls.Add(this.comboPeriod);
            this.filterCard.Controls.Add(this.btnClearFilters);
            this.filterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterCard.Location = new System.Drawing.Point(14, 8);
            this.filterCard.Name = "filterCard";
            this.filterCard.Size = new System.Drawing.Size(952, 96);
            this.filterCard.TabIndex = 0;
            //
            // lblUser
            //
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUser.Location = new System.Drawing.Point(24, 16);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(31, 15);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "User";
            //
            // comboUser
            //
            this.comboUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboUser.Location = new System.Drawing.Point(24, 36);
            this.comboUser.Name = "comboUser";
            this.comboUser.Placeholder = "All users";
            this.comboUser.Size = new System.Drawing.Size(240, 36);
            this.comboUser.TabIndex = 0;
            this.comboUser.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // lblPeriod
            //
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPeriod.Location = new System.Drawing.Point(280, 16);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(41, 15);
            this.lblPeriod.TabIndex = 11;
            this.lblPeriod.Text = "Period";
            //
            // comboPeriod
            //
            this.comboPeriod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboPeriod.Location = new System.Drawing.Point(280, 36);
            this.comboPeriod.Name = "comboPeriod";
            this.comboPeriod.Size = new System.Drawing.Size(220, 36);
            this.comboPeriod.TabIndex = 1;
            this.comboPeriod.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // btnClearFilters
            //
            this.btnClearFilters.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClearFilters.Glyph = "";
            this.btnClearFilters.Location = new System.Drawing.Point(516, 36);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(150, 36);
            this.btnClearFilters.TabIndex = 2;
            this.btnClearFilters.Text = "Clear filters";
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            //
            // ActivityForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(980, 620);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MinimizeBox = false;
            this.Name = "ActivityForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Activity log";
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.hostFilter.ResumeLayout(false);
            this.hostFilter.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.listCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.filterCard.ResumeLayout(false);
            this.filterCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FieldHost hostFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private PurchaseInvoices.FooterPanel footerPanel;
        private System.Windows.Forms.Label lblSummary;
        private PurchaseInvoices.ModernButton btnExport;
        private PurchaseInvoices.ModernButton btnClose;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel listCard;
        private PurchaseInvoices.ModernGrid grid;
        private PurchaseInvoices.CardPanel filterCard;
        private System.Windows.Forms.Label lblUser;
        private PurchaseInvoices.ModernComboBox comboUser;
        private System.Windows.Forms.Label lblPeriod;
        private PurchaseInvoices.ModernComboBox comboPeriod;
        private PurchaseInvoices.ModernButton btnClearFilters;
    }
}
