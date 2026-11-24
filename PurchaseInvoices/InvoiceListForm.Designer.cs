namespace PurchaseInvoices
{
    partial class InvoiceListForm
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
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.btnOpen = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.listCard = new PurchaseInvoices.CardPanel();
            this.grid = new PurchaseInvoices.ModernGrid();
            this.filterCard = new PurchaseInvoices.CardPanel();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.comboCustomer = new PurchaseInvoices.ModernComboBox();
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
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(980, 64);
            this.headerPanel.Subtitle = "Search, filter and open any invoice";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "All invoices";
            //
            // hostFilter
            //
            this.hostFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostFilter.Controls.Add(this.txtFilter);
            this.hostFilter.Glyph = "";
            this.hostFilter.Location = new System.Drawing.Point(640, 14);
            this.hostFilter.Name = "hostFilter";
            this.hostFilter.Placeholder = "Search invoice no. or customer";
            this.hostFilter.Size = new System.Drawing.Size(320, 36);
            this.hostFilter.TabIndex = 0;
            //
            // txtFilter
            //
            this.txtFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilter.Location = new System.Drawing.Point(36, 8);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(272, 18);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.FilterChanged);
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.lblSummary);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Controls.Add(this.btnOpen);
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
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(680, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // btnOpen
            //
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Enabled = false;
            this.btnOpen.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnOpen.Glyph = "";
            this.btnOpen.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnOpen.Location = new System.Drawing.Point(800, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(160, 36);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open invoice";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
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
            this.listCard.Glyph = "";
            this.listCard.Location = new System.Drawing.Point(14, 104);
            this.listCard.Name = "listCard";
            this.listCard.Size = new System.Drawing.Size(952, 384);
            this.listCard.TabIndex = 1;
            this.listCard.Title = "Invoices";
            //
            // grid
            //
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AlwaysShowSelection = true;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.Location = new System.Drawing.Point(20, 46);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(912, 320);
            this.grid.TabIndex = 0;
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            //
            // filterCard
            //
            this.filterCard.Controls.Add(this.lblCustomer);
            this.filterCard.Controls.Add(this.comboCustomer);
            this.filterCard.Controls.Add(this.lblPeriod);
            this.filterCard.Controls.Add(this.comboPeriod);
            this.filterCard.Controls.Add(this.btnClearFilters);
            this.filterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterCard.Location = new System.Drawing.Point(14, 8);
            this.filterCard.Name = "filterCard";
            this.filterCard.Size = new System.Drawing.Size(952, 96);
            this.filterCard.TabIndex = 0;
            //
            // lblCustomer
            //
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCustomer.Location = new System.Drawing.Point(24, 16);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(60, 15);
            this.lblCustomer.TabIndex = 10;
            this.lblCustomer.Text = "Customer";
            //
            // comboCustomer
            //
            this.comboCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboCustomer.Location = new System.Drawing.Point(24, 36);
            this.comboCustomer.Name = "comboCustomer";
            this.comboCustomer.Placeholder = "All customers";
            this.comboCustomer.Size = new System.Drawing.Size(300, 36);
            this.comboCustomer.TabIndex = 0;
            this.comboCustomer.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // lblPeriod
            //
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPeriod.Location = new System.Drawing.Point(340, 16);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(41, 15);
            this.lblPeriod.TabIndex = 11;
            this.lblPeriod.Text = "Period";
            //
            // comboPeriod
            //
            this.comboPeriod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboPeriod.Location = new System.Drawing.Point(340, 36);
            this.comboPeriod.Name = "comboPeriod";
            this.comboPeriod.Size = new System.Drawing.Size(220, 36);
            this.comboPeriod.TabIndex = 1;
            this.comboPeriod.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // btnClearFilters
            //
            this.btnClearFilters.Enabled = false;
            this.btnClearFilters.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClearFilters.Glyph = "";
            this.btnClearFilters.Location = new System.Drawing.Point(576, 36);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(150, 36);
            this.btnClearFilters.TabIndex = 2;
            this.btnClearFilters.Text = "Clear filters";
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            //
            // InvoiceListForm
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
            this.Name = "InvoiceListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "All invoices";
            this.Load += new System.EventHandler(this.InvoiceListForm_Load);
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
        private PurchaseInvoices.ModernButton btnClose;
        private PurchaseInvoices.ModernButton btnOpen;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel listCard;
        private PurchaseInvoices.ModernGrid grid;
        private PurchaseInvoices.CardPanel filterCard;
        private System.Windows.Forms.Label lblCustomer;
        private PurchaseInvoices.ModernComboBox comboCustomer;
        private System.Windows.Forms.Label lblPeriod;
        private PurchaseInvoices.ModernComboBox comboPeriod;
        private PurchaseInvoices.ModernButton btnClearFilters;
    }
}
