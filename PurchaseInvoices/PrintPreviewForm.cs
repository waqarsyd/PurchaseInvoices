using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal partial class PrintPreviewForm : Form
    {
        private readonly InvoicePrintDocument document;
        private BusyIndicator loading;

        public PrintPreviewForm(InvoicePrintDocument document)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 640, 520);
            this.document = document;
            document.EndPrint += Document_EndPrint;
            FormClosed += (s, e) =>
            {
                document.EndPrint -= Document_EndPrint;
                StopLoading();
            };
            headerPanel.Subtitle = document.DocumentName;
            preview.Document = document;
            UpdatePager();
        }

        public bool Printed { get; private set; }

        internal BusyIndicator Loading
        {
            set { loading = value; }
        }

        private void StopLoading()
        {
            loading?.Dispose();
            loading = null;
        }

        private void Document_EndPrint(object sender, PrintEventArgs e)
        {
            StopLoading();
            if (IsHandleCreated && !IsDisposed)
            {
                BeginInvoke((MethodInvoker)UpdatePager);
            }
        }

        private void UpdatePager()
        {
            if (IsDisposed)
            {
                return;
            }
            int pages = Math.Max(1, document.TotalPages);
            if (preview.StartPage > pages - 1)
            {
                preview.StartPage = pages - 1;
            }
            lblPage.Text = $"Page {preview.StartPage + 1} of {pages}";
            btnPreviousPage.Enabled = preview.StartPage > 0;
            btnNextPage.Enabled = preview.StartPage < pages - 1;
            headerPanel.Subtitle = $"{document.DocumentName}  ·  {pages} {(pages == 1 ? "page" : "pages")}";
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (preview.StartPage > 0)
            {
                preview.StartPage--;
            }
            UpdatePager();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (preview.StartPage < document.TotalPages - 1)
            {
                preview.StartPage++;
            }
            UpdatePager();
        }

        private void btnFit_Click(object sender, EventArgs e)
        {
            preview.AutoZoom = true;
        }

        private void btnActualSize_Click(object sender, EventArgs e)
        {
            preview.AutoZoom = false;
            preview.Zoom = 1.0;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog dialog = new PrintDialog { Document = document, UseEXDialog = true, AllowSomePages = false, AllowSelection = false })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
            }
            try
            {
                using (BusyIndicator.Start(this, "Sending to the printer…", $"{document.DocumentName}  ·  {document.TotalPages} {(document.TotalPages == 1 ? "page" : "pages")}"))
                {
                    document.PrintController = new StandardPrintController();
                    document.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not print the invoice.\n\n" + ex.Message, "Print", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Printed = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.P:
                    btnPrint.PerformClick();
                    return true;
                case Keys.PageUp:
                    btnPreviousPage_Click(this, EventArgs.Empty);
                    return true;
                case Keys.PageDown:
                    btnNextPage_Click(this, EventArgs.Empty);
                    return true;
                case Keys.Escape:
                    Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
