using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace PurchaseInvoices
{
    internal enum ConnectionSource
    {
        None,
        AppConfig,
        UserSettings,
        Override
    }

    internal sealed class ConnectionDetails
    {
        public string Server { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public bool WindowsAuthentication { get; set; } = true;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string ToConnectionString(int connectTimeoutSeconds = 15)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = Server.Trim(),
                InitialCatalog = Database.Trim(),
                IntegratedSecurity = WindowsAuthentication,
                MultipleActiveResultSets = true,
                ConnectTimeout = connectTimeoutSeconds
            };
            if (!WindowsAuthentication)
            {
                builder.UserID = UserName.Trim();
                builder.Password = Password;
            }
            return builder.ConnectionString;
        }

        public static ConnectionDetails FromConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            return new ConnectionDetails
            {
                Server = builder.DataSource,
                Database = builder.InitialCatalog,
                WindowsAuthentication = builder.IntegratedSecurity,
                UserName = builder.UserID,
                Password = builder.Password
            };
        }
    }

    internal static class AppConnection
    {
        internal static string Override { get; set; }

        internal static string SettingsPath { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PurchaseInvoices", "connection.xml");

        public static string AppConfigConnectionString
        {
            get
            {
                string value = ConfigurationManager.ConnectionStrings["MYCON"]?.ConnectionString;
                return string.IsNullOrWhiteSpace(value) ? null : value;
            }
        }

        public static ConnectionSource Source
        {
            get
            {
                if (!string.IsNullOrEmpty(Override))
                {
                    return ConnectionSource.Override;
                }
                if (LoadSaved() != null)
                {
                    return ConnectionSource.UserSettings;
                }
                return AppConfigConnectionString != null ? ConnectionSource.AppConfig : ConnectionSource.None;
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(Override))
                {
                    return Override;
                }
                ConnectionDetails saved = LoadSaved();
                return saved != null ? saved.ToConnectionString() : AppConfigConnectionString;
            }
        }

        public static ConnectionDetails Current
        {
            get
            {
                string connectionString = ConnectionString;
                if (connectionString == null)
                {
                    return new ConnectionDetails { Server = @".\SQLEXPRESS" };
                }
                try
                {
                    return ConnectionDetails.FromConnectionString(connectionString);
                }
                catch (ArgumentException)
                {
                    return new ConnectionDetails { Server = @".\SQLEXPRESS" };
                }
            }
        }

        public static ConnectionDetails LoadSaved()
        {
            if (!File.Exists(SettingsPath))
            {
                return null;
            }
            try
            {
                XElement root = XElement.Load(SettingsPath);
                return new ConnectionDetails
                {
                    Server = (string)root.Element("Server") ?? string.Empty,
                    Database = (string)root.Element("Database") ?? string.Empty,
                    WindowsAuthentication = (bool?)root.Element("WindowsAuthentication") ?? true,
                    UserName = (string)root.Element("UserName") ?? string.Empty,
                    Password = Unprotect((string)root.Element("Password"))
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void Save(ConnectionDetails details)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
            bool sqlLogin = !details.WindowsAuthentication;
            new XElement("Connection",
                new XElement("Server", details.Server.Trim()),
                new XElement("Database", details.Database.Trim()),
                new XElement("WindowsAuthentication", details.WindowsAuthentication),
                new XElement("UserName", sqlLogin ? details.UserName.Trim() : string.Empty),
                new XElement("Password", sqlLogin ? Protect(details.Password) : string.Empty)).Save(SettingsPath);
        }

        public static void ClearSaved()
        {
            if (File.Exists(SettingsPath))
            {
                File.Delete(SettingsPath);
            }
        }

        private static string Protect(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            byte[] encrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(value), null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        private static string Unprotect(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            byte[] decrypted = ProtectedData.Unprotect(Convert.FromBase64String(value), null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
