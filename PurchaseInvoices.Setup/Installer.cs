using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace PurchaseInvoices.Setup
{
    internal sealed class InstallOptions
    {
        public string Folder { get; set; } = Installer.DefaultFolder;
        public bool DesktopShortcut { get; set; } = true;
        public bool StartMenuShortcut { get; set; } = true;
        public bool Register { get; set; } = true;
    }

    internal static class Installer
    {
        public const string ProductName = "Purchase Invoices";
        public const string AppExe = "PurchaseInvoices.exe";
        public const string AppConfig = "PurchaseInvoices.exe.config";
        public const string SetupExe = "PurchaseInvoicesSetup.exe";
        public const string ShortcutName = "Purchase Invoices.lnk";
        public const string UninstallKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\PurchaseInvoices";
        private const string PayloadName = "PurchaseInvoices.Setup.payload.zip";
        private const string VersionEntry = "version.txt";

        private static string payloadVersion;

        public static string DefaultFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", ProductName);

        public static string DesktopShortcutPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ShortcutName);

        public static string StartMenuShortcutPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), ShortcutName);

        public static string PayloadVersion
        {
            get
            {
                if (payloadVersion == null)
                {
                    using (ZipArchive zip = OpenPayload())
                    {
                        ZipArchiveEntry entry = zip.GetEntry(VersionEntry);
                        using (StreamReader reader = new StreamReader(entry.Open()))
                        {
                            payloadVersion = reader.ReadToEnd().Trim();
                        }
                    }
                }
                return payloadVersion;
            }
        }

        public static string ShortVersion
        {
            get
            {
                Version version;
                return Version.TryParse(PayloadVersion, out version) ? $"{version.Major}.{version.Minor}.{Math.Max(0, version.Build)}" : PayloadVersion;
            }
        }

        public static string InstalledFolder()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(UninstallKey))
            {
                string folder = key?.GetValue("InstallLocation") as string;
                return string.IsNullOrWhiteSpace(folder) ? null : folder;
            }
        }

        public static string InstalledVersion()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(UninstallKey))
            {
                return key?.GetValue("DisplayVersion") as string;
            }
        }

        public static List<Process> RunningCopies(string folder)
        {
            string target = Path.GetFullPath(Path.Combine(folder, AppExe));
            List<Process> running = new List<Process>();
            foreach (Process process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(AppExe)))
            {
                try
                {
                    if (string.Equals(Path.GetFullPath(process.MainModule.FileName), target, StringComparison.OrdinalIgnoreCase))
                    {
                        running.Add(process);
                        continue;
                    }
                }
                catch (Exception ex) when (ex is System.ComponentModel.Win32Exception || ex is InvalidOperationException)
                {
                }
                process.Dispose();
            }
            return running;
        }

        public static void Install(InstallOptions options, Action<int, string> progress = null)
        {
            string folder = Path.GetFullPath(options.Folder);
            Report(progress, 5, "Creating the folder…");
            Directory.CreateDirectory(folder);

            using (ZipArchive zip = OpenPayload())
            {
                List<ZipArchiveEntry> entries = zip.Entries.Where(e => e.Name.Length > 0 && e.FullName != VersionEntry).ToList();
                for (int i = 0; i < entries.Count; i++)
                {
                    ZipArchiveEntry entry = entries[i];
                    string target = Path.GetFullPath(Path.Combine(folder, entry.FullName));
                    if (!target.StartsWith(folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    Report(progress, 10 + 60 * i / Math.Max(1, entries.Count), "Copying " + entry.Name + "…");
                    if (string.Equals(entry.Name, AppConfig, StringComparison.OrdinalIgnoreCase) && File.Exists(target))
                    {
                        continue;
                    }
                    Directory.CreateDirectory(Path.GetDirectoryName(target));
                    entry.ExtractToFile(target, true);
                }
            }

            Report(progress, 75, "Adding the uninstaller…");
            string self = typeof(Installer).Assembly.Location;
            string setupCopy = Path.Combine(folder, SetupExe);
            if (!string.Equals(Path.GetFullPath(self), setupCopy, StringComparison.OrdinalIgnoreCase))
            {
                File.Copy(self, setupCopy, true);
            }

            string app = Path.Combine(folder, AppExe);
            if (options.StartMenuShortcut)
            {
                Report(progress, 82, "Adding the Start menu shortcut…");
                CreateShortcut(StartMenuShortcutPath, app, folder);
            }
            if (options.DesktopShortcut)
            {
                Report(progress, 88, "Adding the desktop shortcut…");
                CreateShortcut(DesktopShortcutPath, app, folder);
            }
            if (options.Register)
            {
                Report(progress, 94, "Registering with Windows…");
                Register(folder);
            }
            Report(progress, 100, "Done.");
        }

        public static void Uninstall(string folder, bool unregister = true, bool removeShortcuts = true, Action<int, string> progress = null)
        {
            folder = Path.GetFullPath(folder);
            string app = Path.Combine(folder, AppExe);
            if (removeShortcuts)
            {
                Report(progress, 15, "Removing shortcuts…");
                foreach (string shortcut in new[] { DesktopShortcutPath, StartMenuShortcutPath })
                {
                    if (File.Exists(shortcut) && string.Equals(ShortcutTarget(shortcut), app, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Delete(shortcut);
                    }
                }
            }
            if (unregister)
            {
                Report(progress, 35, "Removing it from Installed apps…");
                Registry.CurrentUser.DeleteSubKeyTree(UninstallKey, false);
            }

            Report(progress, 60, "Removing files…");
            string running = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
            bool runningFromFolder = running.StartsWith(folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
            foreach (string name in new[] { AppExe, AppConfig, "PurchaseInvoices.pdb", SetupExe })
            {
                string path = Path.Combine(folder, name);
                if (File.Exists(path) && !string.Equals(path, running, StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(path);
                }
            }
            if (runningFromFolder)
            {
                Process.Start(new ProcessStartInfo("cmd.exe", $"/c ping 127.0.0.1 -n 3 > nul & del /f /q \"{running}\" & rmdir \"{folder}\"")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                });
            }
            else if (Directory.Exists(folder) && !Directory.EnumerateFileSystemEntries(folder).Any())
            {
                Directory.Delete(folder);
            }
            Report(progress, 100, "Done.");
        }

        private static void Register(string folder)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(UninstallKey))
            {
                string setup = Path.Combine(folder, SetupExe);
                key.SetValue("DisplayName", ProductName);
                key.SetValue("DisplayVersion", ShortVersion);
                key.SetValue("Publisher", ProductName);
                key.SetValue("DisplayIcon", Path.Combine(folder, AppExe) + ",0");
                key.SetValue("InstallLocation", folder);
                key.SetValue("InstallDate", DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
                key.SetValue("UninstallString", $"\"{setup}\" /uninstall");
                key.SetValue("QuietUninstallString", $"\"{setup}\" /uninstall /quiet");
                key.SetValue("NoModify", 1, RegistryValueKind.DWord);
                key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                long bytes = Directory.EnumerateFiles(folder).Sum(f => new FileInfo(f).Length);
                key.SetValue("EstimatedSize", (int)Math.Max(1, bytes / 1024), RegistryValueKind.DWord);
            }
        }

        public static void CreateShortcut(string path, string target, string workingFolder)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            Type shellType = Type.GetTypeFromProgID("WScript.Shell");
            object shell = Activator.CreateInstance(shellType);
            object link = null;
            try
            {
                link = shellType.InvokeMember("CreateShortcut", BindingFlags.InvokeMethod, null, shell, new object[] { path });
                Type linkType = link.GetType();
                linkType.InvokeMember("TargetPath", BindingFlags.SetProperty, null, link, new object[] { target });
                linkType.InvokeMember("WorkingDirectory", BindingFlags.SetProperty, null, link, new object[] { workingFolder });
                linkType.InvokeMember("IconLocation", BindingFlags.SetProperty, null, link, new object[] { target + ",0" });
                linkType.InvokeMember("Description", BindingFlags.SetProperty, null, link, new object[] { "Create, find and print purchase invoices" });
                linkType.InvokeMember("Save", BindingFlags.InvokeMethod, null, link, null);
            }
            finally
            {
                if (link != null)
                {
                    Marshal.FinalReleaseComObject(link);
                }
                Marshal.FinalReleaseComObject(shell);
            }
        }

        public static string ShortcutTarget(string path)
        {
            Type shellType = Type.GetTypeFromProgID("WScript.Shell");
            object shell = Activator.CreateInstance(shellType);
            object link = null;
            try
            {
                link = shellType.InvokeMember("CreateShortcut", BindingFlags.InvokeMethod, null, shell, new object[] { path });
                return link.GetType().InvokeMember("TargetPath", BindingFlags.GetProperty, null, link, null) as string;
            }
            finally
            {
                if (link != null)
                {
                    Marshal.FinalReleaseComObject(link);
                }
                Marshal.FinalReleaseComObject(shell);
            }
        }

        private static ZipArchive OpenPayload()
        {
            Stream stream = typeof(Installer).Assembly.GetManifestResourceStream(PayloadName);
            if (stream == null)
            {
                throw new InvalidOperationException("This setup file is damaged: the program files are missing from it.");
            }
            return new ZipArchive(stream, ZipArchiveMode.Read);
        }

        private static void Report(Action<int, string> progress, int percent, string text)
        {
            progress?.Invoke(percent, text);
        }
    }
}
