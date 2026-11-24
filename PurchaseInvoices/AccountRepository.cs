using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PurchaseInvoices
{
    internal sealed class CompanyProfile
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;

        public bool IsEmpty => string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Address) && string.IsNullOrWhiteSpace(Phone)
                               && string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(TaxNumber);
    }

    internal sealed class AccountRepository
    {
        public const int MaxFailedSignIns = 5;
        public const int LockoutMinutes = 5;

        private const string UserColumns = "USERID, USERNAME, FULL_NAME, USER_ROLE, IS_ACTIVE, MUST_CHANGE_PASSWORD, LOCKED_UNTIL, LAST_SIGN_IN, CREATED_AT";

        private readonly string connectionString;

        public AccountRepository(string connectionString)
        {
            this.connectionString = new SqlConnectionStringBuilder(connectionString).ConnectionString;
        }

        private SqlConnection Open()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public int CountUsers()
        {
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM APP_USERS", con))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<AppUser> GetUsers()
        {
            List<AppUser> users = new List<AppUser>();
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand($"SELECT {UserColumns} FROM APP_USERS ORDER BY USERNAME", con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(ReadUser(reader));
                }
            }
            return users;
        }

        public AppUser FindUser(int id)
        {
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand($"SELECT {UserColumns} FROM APP_USERS WHERE USERID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? ReadUser(reader) : null;
                }
            }
        }

        public AppUser CreateFirstAdmin(string companyName, string fullName, string userName, string password)
        {
            string hash = PasswordHasher.Hash(password);
            using (SqlConnection con = Open())
            using (SqlTransaction transaction = con.BeginTransaction(IsolationLevel.Serializable))
            {
                using (SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM APP_USERS WITH (UPDLOCK, HOLDLOCK)", con, transaction))
                {
                    if ((int)check.ExecuteScalar() > 0)
                    {
                        throw new InvalidOperationException("An account already exists. Sign in instead.");
                    }
                }
                int id = InsertUser(con, transaction, userName, fullName, UserRole.Admin, hash, false);
                if (!string.IsNullOrWhiteSpace(companyName))
                {
                    SaveCompanyName(con, transaction, companyName.Trim());
                }
                transaction.Commit();
                return new AppUser { Id = id, UserName = userName.Trim(), FullName = Clean(fullName), Role = UserRole.Admin };
            }
        }

        public AppUser CreateUser(string userName, string fullName, UserRole role, string password, bool mustChangePassword)
        {
            string hash = PasswordHasher.Hash(password);
            using (SqlConnection con = Open())
            {
                int id = InsertUser(con, null, userName, fullName, role, hash, mustChangePassword);
                return new AppUser { Id = id, UserName = userName.Trim(), FullName = Clean(fullName), Role = role, MustChangePassword = mustChangePassword };
            }
        }

        public void UpdateUser(int id, string fullName, UserRole role, bool isActive)
        {
            using (SqlConnection con = Open())
            using (SqlTransaction transaction = con.BeginTransaction(IsolationLevel.Serializable))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE APP_USERS SET FULL_NAME = @name, USER_ROLE = @role, IS_ACTIVE = @active WHERE USERID = @id", con, transaction))
                {
                    cmd.Parameters.AddWithValue("@name", (object)Clean(fullName) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@role", role.ToString());
                    cmd.Parameters.AddWithValue("@active", isActive);
                    cmd.Parameters.AddWithValue("@id", id);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException("This user no longer exists.");
                    }
                }
                using (SqlCommand admins = new SqlCommand("SELECT COUNT(*) FROM APP_USERS WITH (UPDLOCK, HOLDLOCK) WHERE USER_ROLE = 'Admin' AND IS_ACTIVE = 1", con, transaction))
                {
                    if ((int)admins.ExecuteScalar() == 0)
                    {
                        throw new InvalidOperationException("There must always be at least one active Admin.");
                    }
                }
                transaction.Commit();
            }
        }

        public void SetPassword(int id, string password, bool mustChangePassword)
        {
            string hash = PasswordHasher.Hash(password);
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand("UPDATE APP_USERS SET PASSWORD_HASH = @hash, MUST_CHANGE_PASSWORD = @must, FAILED_SIGN_INS = 0, LOCKED_UNTIL = NULL WHERE USERID = @id", con))
            {
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@must", mustChangePassword);
                cmd.Parameters.AddWithValue("@id", id);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException("This user no longer exists.");
                }
            }
        }

        public bool CheckPassword(int id, string password)
        {
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand("SELECT PASSWORD_HASH FROM APP_USERS WHERE USERID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return PasswordHasher.Verify(password, cmd.ExecuteScalar() as string);
            }
        }

        public SignInResult SignIn(string userName, string password)
        {
            using (SqlConnection con = Open())
            {
                AppUser user = null;
                string hash = null;
                DateTime now;
                using (SqlCommand cmd = new SqlCommand($"SELECT {UserColumns}, PASSWORD_HASH FROM APP_USERS WHERE USERNAME = @name; SELECT GETDATE();", con))
                {
                    cmd.Parameters.AddWithValue("@name", (userName ?? string.Empty).Trim());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = ReadUser(reader);
                            hash = reader["PASSWORD_HASH"] as string;
                        }
                        reader.NextResult();
                        reader.Read();
                        now = reader.GetDateTime(0);
                    }
                }

                if (user == null)
                {
                    PasswordHasher.SpendTime(password);
                    return new SignInResult { Status = SignInStatus.WrongPassword };
                }
                if (user.LockedUntil.HasValue && user.LockedUntil.Value > now)
                {
                    return new SignInResult { Status = SignInStatus.Locked, LockedUntil = user.LockedUntil, User = user };
                }
                if (!PasswordHasher.Verify(password, hash))
                {
                    string sql = "UPDATE APP_USERS SET " +
                                 "LOCKED_UNTIL = CASE WHEN FAILED_SIGN_INS + 1 >= @max THEN DATEADD(MINUTE, @minutes, GETDATE()) ELSE NULL END, " +
                                 "FAILED_SIGN_INS = CASE WHEN FAILED_SIGN_INS + 1 >= @max THEN 0 ELSE FAILED_SIGN_INS + 1 END " +
                                 "OUTPUT INSERTED.LOCKED_UNTIL WHERE USERID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@max", MaxFailedSignIns);
                        cmd.Parameters.AddWithValue("@minutes", LockoutMinutes);
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        DateTime? lockedUntil = cmd.ExecuteScalar() as DateTime?;
                        return lockedUntil.HasValue
                            ? new SignInResult { Status = SignInStatus.Locked, LockedUntil = lockedUntil, User = user }
                            : new SignInResult { Status = SignInStatus.WrongPassword, User = user };
                    }
                }
                if (!user.IsActive)
                {
                    return new SignInResult { Status = SignInStatus.Inactive, User = user };
                }
                using (SqlCommand cmd = new SqlCommand("UPDATE APP_USERS SET FAILED_SIGN_INS = 0, LOCKED_UNTIL = NULL, LAST_SIGN_IN = GETDATE() WHERE USERID = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.ExecuteNonQuery();
                }
                user.LockedUntil = null;
                user.LastSignIn = now;
                return new SignInResult { Status = SignInStatus.Success, User = user };
            }
        }

        public CompanyProfile GetCompanyProfile()
        {
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand("SELECT COMPANY_NAME, COMPANY_ADDRESS, PHONE, EMAIL, TAX_NUMBER FROM COMPANY_PROFILE WHERE PROFILE_ID = 1", con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return new CompanyProfile();
                }
                return new CompanyProfile
                {
                    Name = Convert.ToString(reader["COMPANY_NAME"]),
                    Address = Convert.ToString(reader["COMPANY_ADDRESS"]),
                    Phone = Convert.ToString(reader["PHONE"]),
                    Email = Convert.ToString(reader["EMAIL"]),
                    TaxNumber = Convert.ToString(reader["TAX_NUMBER"]),
                };
            }
        }

        public void SaveCompanyProfile(CompanyProfile profile)
        {
            string sql = "UPDATE COMPANY_PROFILE SET COMPANY_NAME = @name, COMPANY_ADDRESS = @address, PHONE = @phone, EMAIL = @email, TAX_NUMBER = @tax WHERE PROFILE_ID = 1; " +
                         "IF @@ROWCOUNT = 0 INSERT INTO COMPANY_PROFILE (PROFILE_ID, COMPANY_NAME, COMPANY_ADDRESS, PHONE, EMAIL, TAX_NUMBER) VALUES (1, @name, @address, @phone, @email, @tax);";
            using (SqlConnection con = Open())
            using (SqlTransaction transaction = con.BeginTransaction(IsolationLevel.Serializable))
            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.Parameters.AddWithValue("@name", Value(profile.Name));
                cmd.Parameters.AddWithValue("@address", Value(profile.Address));
                cmd.Parameters.AddWithValue("@phone", Value(profile.Phone));
                cmd.Parameters.AddWithValue("@email", Value(profile.Email));
                cmd.Parameters.AddWithValue("@tax", Value(profile.TaxNumber));
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
        }

        public void LogActivity(string userName, string activity, string details)
        {
            using (SqlConnection con = Open())
            using (SqlCommand cmd = new SqlCommand("INSERT INTO APP_ACTIVITY (USERNAME, ACTIVITY, DETAILS) VALUES (@user, @activity, @details)", con))
            {
                cmd.Parameters.AddWithValue("@user", Value(Limit(userName, 30)));
                cmd.Parameters.AddWithValue("@activity", Limit(activity, 40) ?? string.Empty);
                cmd.Parameters.AddWithValue("@details", Value(Limit(details, 400)));
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetActivity()
        {
            DataTable data = new DataTable();
            using (SqlConnection con = Open())
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT TOP (5000) ACTIVITY_ID, ACTIVITY_AT, USERNAME, ACTIVITY, DETAILS FROM APP_ACTIVITY ORDER BY ACTIVITY_ID DESC", con))
            {
                adapter.Fill(data);
            }
            return data;
        }

        private static int InsertUser(SqlConnection con, SqlTransaction transaction, string userName, string fullName, UserRole role, string hash, bool mustChangePassword)
        {
            string sql = "INSERT INTO APP_USERS (USERNAME, FULL_NAME, USER_ROLE, PASSWORD_HASH, MUST_CHANGE_PASSWORD) OUTPUT INSERTED.USERID VALUES (@user, @name, @role, @hash, @must)";
            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.Parameters.AddWithValue("@user", userName.Trim());
                cmd.Parameters.AddWithValue("@name", (object)Clean(fullName) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@role", role.ToString());
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@must", mustChangePassword);
                try
                {
                    return (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    throw new InvalidOperationException($"The user name '{userName.Trim()}' is already taken.", ex);
                }
            }
        }

        private static void SaveCompanyName(SqlConnection con, SqlTransaction transaction, string name)
        {
            string sql = "UPDATE COMPANY_PROFILE SET COMPANY_NAME = @name WHERE PROFILE_ID = 1; " +
                         "IF @@ROWCOUNT = 0 INSERT INTO COMPANY_PROFILE (PROFILE_ID, COMPANY_NAME) VALUES (1, @name);";
            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
        }

        private static AppUser ReadUser(SqlDataReader reader)
        {
            return new AppUser
            {
                Id = Convert.ToInt32(reader["USERID"]),
                UserName = Convert.ToString(reader["USERNAME"]),
                FullName = Convert.ToString(reader["FULL_NAME"]),
                Role = AppUser.ParseRole(Convert.ToString(reader["USER_ROLE"])),
                IsActive = Convert.ToBoolean(reader["IS_ACTIVE"]),
                MustChangePassword = Convert.ToBoolean(reader["MUST_CHANGE_PASSWORD"]),
                LockedUntil = reader["LOCKED_UNTIL"] as DateTime?,
                LastSignIn = reader["LAST_SIGN_IN"] as DateTime?,
                CreatedAt = reader["CREATED_AT"] as DateTime?,
            };
        }

        private static string Clean(string value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        private static object Value(string value) => (object)Clean(value) ?? DBNull.Value;

        private static string Limit(string value, int length)
        {
            if (value == null)
            {
                return null;
            }
            value = value.Trim();
            return value.Length <= length ? value : value.Substring(0, length);
        }
    }

    internal static class ActivityLog
    {
        public static void Record(string activity, string details = null)
        {
            try
            {
                string connectionString = AppConnection.ConnectionString;
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    new AccountRepository(connectionString).LogActivity(Session.UserName, activity, details);
                }
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException || ex is ArgumentException)
            {
            }
        }

        public static void Record(string userName, string activity, string details)
        {
            try
            {
                string connectionString = AppConnection.ConnectionString;
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    new AccountRepository(connectionString).LogActivity(userName, activity, details);
                }
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException || ex is ArgumentException)
            {
            }
        }
    }
}
