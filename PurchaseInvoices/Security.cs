using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace PurchaseInvoices
{
    internal enum UserRole
    {
        Admin,
        Staff
    }

    internal sealed class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public bool MustChangePassword { get; set; }
        public DateTime? LockedUntil { get; set; }
        public DateTime? LastSignIn { get; set; }
        public DateTime? CreatedAt { get; set; }

        public bool IsAdmin => Role == UserRole.Admin;
        public string DisplayName => string.IsNullOrWhiteSpace(FullName) ? UserName : FullName.Trim();
        public string RoleName => Role == UserRole.Admin ? "Admin" : "Staff";

        public string Initials
        {
            get
            {
                string[] words = DisplayName.Split(new[] { ' ', '.', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
                string initials = string.Concat(words.Take(2).Select(w => char.ToUpper(w[0], CultureInfo.CurrentCulture)));
                return initials.Length > 0 ? initials : "?";
            }
        }

        public static UserRole ParseRole(string value) =>
            string.Equals(value?.Trim(), "Admin", StringComparison.OrdinalIgnoreCase) ? UserRole.Admin : UserRole.Staff;
    }

    internal static class Session
    {
        public static AppUser Current { get; set; }
        public static bool IsAdmin => Current != null && Current.IsAdmin;
        public static string UserName => Current?.UserName;

        public static string RememberedUserName
        {
            get
            {
                try
                {
                    string path = RememberPath;
                    return File.Exists(path) ? File.ReadAllText(path).Trim() : string.Empty;
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    string path = RememberPath;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        return;
                    }
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.WriteAllText(path, value.Trim());
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                {
                }
            }
        }

        private static string RememberPath => Path.Combine(Path.GetDirectoryName(AppConnection.SettingsPath), "signin.txt");
    }

    internal static class AccountRules
    {
        public static string CheckUserName(string userName)
        {
            userName = (userName ?? string.Empty).Trim();
            if (userName.Length < 3 || userName.Length > 30)
            {
                return "The user name must be 3 to 30 characters long.";
            }
            if (!userName.All(c => (c < 128 && char.IsLetterOrDigit(c)) || c == '.' || c == '_' || c == '-'))
            {
                return "Use only letters, numbers, dots, dashes and underscores in the user name.";
            }
            return null;
        }

        public static string CheckNewPassword(string password, string confirm, string userName)
        {
            string weak = PasswordHasher.CheckStrength(password, userName);
            if (weak != null)
            {
                return weak;
            }
            return password == confirm ? null : "The two passwords don't match.";
        }
    }

    internal enum SignInStatus
    {
        Success,
        WrongPassword,
        Locked,
        Inactive
    }

    internal sealed class SignInResult
    {
        public SignInStatus Status { get; set; }
        public AppUser User { get; set; }
        public DateTime? LockedUntil { get; set; }
    }

    internal static class PasswordHasher
    {
        public const int MinimumLength = 8;
        private const string Scheme = "PBKDF2-SHA256";
        private const int Iterations = 100000;
        private const int SaltBytes = 16;
        private const int HashBytes = 32;

        private static readonly Lazy<string> DummyHash = new Lazy<string>(() => Hash(Guid.NewGuid().ToString("N") + "x1"));

        public static string Hash(string password)
        {
            byte[] salt = new byte[SaltBytes];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }
            byte[] hash = Derive(password, salt, Iterations, HashBytes);
            return string.Join("$", Scheme, Iterations.ToString(CultureInfo.InvariantCulture), Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool Verify(string password, string stored)
        {
            string[] parts = (stored ?? string.Empty).Split('$');
            int iterations;
            if (parts.Length != 4 || parts[0] != Scheme || !int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out iterations) || iterations < 1)
            {
                return false;
            }
            byte[] salt;
            byte[] expected;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }
            if (salt.Length == 0 || expected.Length == 0)
            {
                return false;
            }
            byte[] actual = Derive(password ?? string.Empty, salt, iterations, expected.Length);
            int difference = 0;
            for (int i = 0; i < expected.Length; i++)
            {
                difference |= actual[i] ^ expected[i];
            }
            return difference == 0;
        }

        public static void SpendTime(string password) => Verify(password, DummyHash.Value);

        public static string CheckStrength(string password, string userName)
        {
            password = password ?? string.Empty;
            if (password.Length < MinimumLength || !password.Any(char.IsLetter) || !password.Any(char.IsDigit))
            {
                return $"Use at least {MinimumLength} characters, with both letters and numbers.";
            }
            if (!string.IsNullOrWhiteSpace(userName) && password.IndexOf(userName.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "The password can't contain the user name.";
            }
            return null;
        }

        private static byte[] Derive(string password, byte[] salt, int iterations, int length)
        {
            using (Rfc2898DeriveBytes kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return kdf.GetBytes(length);
            }
        }
    }
}
