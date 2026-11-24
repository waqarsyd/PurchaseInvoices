using System;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppSession());
        }
    }

    internal sealed class AppSession : ApplicationContext
    {
        public AppSession()
        {
            ShowSignIn();
        }

        private void ShowSignIn()
        {
            SignInForm signIn = new SignInForm();
            signIn.FormClosed += (s, e) =>
            {
                if (signIn.DialogResult == DialogResult.OK && Session.Current != null)
                {
                    ShowMain();
                }
                else
                {
                    ExitThread();
                }
            };
            MainForm = signIn;
            signIn.Show();
        }

        private void ShowMain()
        {
            InvoiceForm main = new InvoiceForm();
            main.FormClosed += (s, e) =>
            {
                ActivityLog.Record("Signed out");
                Session.Current = null;
                if (main.SignOutRequested)
                {
                    ShowSignIn();
                }
                else
                {
                    ExitThread();
                }
            };
            MainForm = main;
            main.Show();
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
        }
    }

    internal static class AppInfo
    {
        public const string ProductName = "Purchase Invoices";

        public static string Version
        {
            get
            {
                Version version = typeof(AppInfo).Assembly.GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
    }
}
