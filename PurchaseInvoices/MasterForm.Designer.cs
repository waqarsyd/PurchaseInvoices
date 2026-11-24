namespace PurchaseInvoices
{
    partial class MasterForm
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
            this.btnNew = new PurchaseInvoices.ModernButton();
            this.footerPanel = new PurchaseInvoices.FooterPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.listCard = new PurchaseInvoices.CardPanel();
            this.grid = new PurchaseInvoices.ModernGrid();
            this.editorCard = new PurchaseInvoices.CardPanel();
            this.lblField1 = new System.Windows.Forms.Label();
            this.hostField1 = new PurchaseInvoices.FieldHost();
            this.txtField1 = new System.Windows.Forms.TextBox();
            this.lblField2 = new System.Windows.Forms.Label();
            this.hostField2 = new PurchaseInvoices.FieldHost();
            this.txtField2 = new System.Windows.Forms.TextBox();
            this.lblField3 = new System.Windows.Forms.Label();
            this.hostField3 = new PurchaseInvoices.FieldHost();
            this.txtField3 = new System.Windows.Forms.TextBox();
            this.btnSave = new PurchaseInvoices.ModernButton();
            this.btnDelete = new PurchaseInvoices.ModernButton();
            this.headerPanel.SuspendLayout();
            this.hostFilter.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.listCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.editorCard.SuspendLayout();
            this.hostField1.SuspendLayout();
            this.hostField2.SuspendLayout();
            this.hostField3.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.hostFilter);
            this.headerPanel.Controls.Add(this.btnNew);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(960, 64);
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Records";
            //
            // hostFilter
            //
            this.hostFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostFilter.Controls.Add(this.txtFilter);
            this.hostFilter.Glyph = "";
            this.hostFilter.Location = new System.Drawing.Point(540, 14);
            this.hostFilter.Name = "hostFilter";
            this.hostFilter.Size = new System.Drawing.Size(240, 36);
            this.hostFilter.TabIndex = 0;
            //
            // txtFilter
            //
            this.txtFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilter.Location = new System.Drawing.Point(36, 8);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(192, 18);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            //
            // btnNew
            //
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnNew.Glyph = "";
            this.btnNew.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnNew.Location = new System.Drawing.Point(790, 14);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(150, 36);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.lblStatus);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 540);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(960, 60);
            this.footerPanel.TabIndex = 2;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblStatus.Location = new System.Drawing.Point(20, 21);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 1;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(820, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.listCard);
            this.contentPanel.Controls.Add(this.editorCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(960, 476);
            this.contentPanel.TabIndex = 1;
            //
            // listCard
            //
            this.listCard.Controls.Add(this.grid);
            this.listCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCard.Glyph = "";
            this.listCard.Location = new System.Drawing.Point(14, 8);
            this.listCard.Name = "listCard";
            this.listCard.Size = new System.Drawing.Size(572, 460);
            this.listCard.TabIndex = 0;
            this.listCard.Title = "All records";
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
            this.grid.Size = new System.Drawing.Size(532, 396);
            this.grid.TabIndex = 0;
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            //
            // editorCard
            //
            this.editorCard.Controls.Add(this.lblField1);
            this.editorCard.Controls.Add(this.hostField1);
            this.editorCard.Controls.Add(this.lblField2);
            this.editorCard.Controls.Add(this.hostField2);
            this.editorCard.Controls.Add(this.lblField3);
            this.editorCard.Controls.Add(this.hostField3);
            this.editorCard.Controls.Add(this.btnSave);
            this.editorCard.Controls.Add(this.btnDelete);
            this.editorCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.editorCard.Glyph = "";
            this.editorCard.Location = new System.Drawing.Point(586, 8);
            this.editorCard.Name = "editorCard";
            this.editorCard.Size = new System.Drawing.Size(360, 460);
            this.editorCard.TabIndex = 1;
            this.editorCard.Title = "Details";
            //
            // lblField1
            //
            this.lblField1.AutoSize = true;
            this.lblField1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblField1.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblField1.Location = new System.Drawing.Point(24, 48);
            this.lblField1.Name = "lblField1";
            this.lblField1.Size = new System.Drawing.Size(30, 15);
            this.lblField1.TabIndex = 10;
            this.lblField1.Text = "Code";
            //
            // hostField1
            //
            this.hostField1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostField1.Controls.Add(this.txtField1);
            this.hostField1.Location = new System.Drawing.Point(24, 68);
            this.hostField1.Name = "hostField1";
            this.hostField1.Size = new System.Drawing.Size(312, 36);
            this.hostField1.TabIndex = 0;
            //
            // txtField1
            //
            this.txtField1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.txtField1.Location = new System.Drawing.Point(12, 8);
            this.txtField1.Name = "txtField1";
            this.txtField1.Size = new System.Drawing.Size(288, 18);
            this.txtField1.TabIndex = 0;
            this.txtField1.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblField2
            //
            this.lblField2.AutoSize = true;
            this.lblField2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblField2.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblField2.Location = new System.Drawing.Point(24, 114);
            this.lblField2.Name = "lblField2";
            this.lblField2.Size = new System.Drawing.Size(38, 15);
            this.lblField2.TabIndex = 11;
            this.lblField2.Text = "Name";
            //
            // hostField2
            //
            this.hostField2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostField2.Controls.Add(this.txtField2);
            this.hostField2.Location = new System.Drawing.Point(24, 134);
            this.hostField2.Name = "hostField2";
            this.hostField2.Size = new System.Drawing.Size(312, 36);
            this.hostField2.TabIndex = 1;
            //
            // txtField2
            //
            this.txtField2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtField2.Location = new System.Drawing.Point(12, 8);
            this.txtField2.Name = "txtField2";
            this.txtField2.Size = new System.Drawing.Size(288, 18);
            this.txtField2.TabIndex = 0;
            this.txtField2.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblField3
            //
            this.lblField3.AutoSize = true;
            this.lblField3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblField3.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblField3.Location = new System.Drawing.Point(24, 180);
            this.lblField3.Name = "lblField3";
            this.lblField3.Size = new System.Drawing.Size(50, 15);
            this.lblField3.TabIndex = 12;
            this.lblField3.Text = "Details";
            //
            // hostField3
            //
            this.hostField3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostField3.Controls.Add(this.txtField3);
            this.hostField3.Location = new System.Drawing.Point(24, 200);
            this.hostField3.Name = "hostField3";
            this.hostField3.Size = new System.Drawing.Size(312, 120);
            this.hostField3.TabIndex = 2;
            //
            // txtField3
            //
            this.txtField3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtField3.Location = new System.Drawing.Point(12, 8);
            this.txtField3.Multiline = true;
            this.txtField3.Name = "txtField3";
            this.txtField3.Size = new System.Drawing.Size(288, 104);
            this.txtField3.TabIndex = 0;
            this.txtField3.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSave.Glyph = "";
            this.btnSave.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(186, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 36);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnDelete
            //
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnDelete.Glyph = "";
            this.btnDelete.Kind = PurchaseInvoices.ButtonKind.Danger;
            this.btnDelete.Location = new System.Drawing.Point(24, 400);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 36);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //
            // MasterForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MinimizeBox = false;
            this.Name = "MasterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Records";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterForm_FormClosing);
            this.Load += new System.EventHandler(this.MasterForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.hostFilter.ResumeLayout(false);
            this.hostFilter.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.listCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.editorCard.ResumeLayout(false);
            this.editorCard.PerformLayout();
            this.hostField1.ResumeLayout(false);
            this.hostField1.PerformLayout();
            this.hostField2.ResumeLayout(false);
            this.hostField2.PerformLayout();
            this.hostField3.ResumeLayout(false);
            this.hostField3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FieldHost hostFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private PurchaseInvoices.ModernButton btnNew;
        private PurchaseInvoices.FooterPanel footerPanel;
        private System.Windows.Forms.Label lblStatus;
        private PurchaseInvoices.ModernButton btnClose;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel listCard;
        private PurchaseInvoices.ModernGrid grid;
        private PurchaseInvoices.CardPanel editorCard;
        private System.Windows.Forms.Label lblField1;
        private PurchaseInvoices.FieldHost hostField1;
        private System.Windows.Forms.TextBox txtField1;
        private System.Windows.Forms.Label lblField2;
        private PurchaseInvoices.FieldHost hostField2;
        private System.Windows.Forms.TextBox txtField2;
        private System.Windows.Forms.Label lblField3;
        private PurchaseInvoices.FieldHost hostField3;
        private System.Windows.Forms.TextBox txtField3;
        private PurchaseInvoices.ModernButton btnSave;
        private PurchaseInvoices.ModernButton btnDelete;
    }
}
