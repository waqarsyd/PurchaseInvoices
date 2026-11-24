using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PurchaseInvoices.Setup
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            bool uninstall = Has(args, "/uninstall");
            bool quiet = Has(args, "/quiet");
            InstallOptions options = new InstallOptions
            {
                Folder = Value(args, "/dir=") ?? (uninstall ? Installer.InstalledFolder() ?? AppContext.BaseDirectory.TrimEnd('\\') : Installer.DefaultFolder),
                DesktopShortcut = !Has(args, "/noshortcuts"),
                StartMenuShortcut = !Has(args, "/noshortcuts"),
                Register = !Has(args, "/noregister"),
            };

            if (quiet)
            {
                try
                {
                    if (Installer.RunningCopies(options.Folder).Count > 0)
                    {
                        Console.Error.WriteLine("Purchase Invoices is running. Close it and try again.");
                        return 2;
                    }
                    if (uninstall)
                    {
                        Installer.Uninstall(options.Folder, options.Register, !Has(args, "/noshortcuts"));
                    }
                    else
                    {
                        Installer.Install(options);
                    }
                    return 0;
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException || ex is System.Runtime.InteropServices.COMException)
                {
                    Console.Error.WriteLine(ex.Message);
                    return 1;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SetupForm(uninstall, options));
            return 0;
        }

        private static bool Has(string[] args, string flag) => args.Any(a => string.Equals(a, flag, StringComparison.OrdinalIgnoreCase));

        private static string Value(string[] args, string prefix)
        {
            string arg = args.FirstOrDefault(a => a.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
            return arg == null ? null : arg.Substring(prefix.Length).Trim('"');
        }
    }
}
