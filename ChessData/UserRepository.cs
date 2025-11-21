using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ChessData
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> RegisterUserAsync(string username, string password, string email, string fullName, string birthday)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // 1. Check tồn tại
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = @u OR Email = @e";
                    using (SqlCommand cmd = new SqlCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@e", email);
                        int count = (int)await cmd.ExecuteScalarAsync();
                        if (count > 0) return "ERROR|Tên đăng nhập hoặc Email đã tồn tại.";
                    }

                    // 2. Lưu user mới
                    string hash = BCrypt.Net.BCrypt.HashPassword(password);
                    string sql = "INSERT INTO Users (Username, PasswordHash, Email, FullName, Birthday) VALUES (@u, @p, @e, @f, @b)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", hash);
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.Parameters.AddWithValue("@f", fullName);
                        cmd.Parameters.AddWithValue("@b", DateTime.Parse(birthday));
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return "REGISTER_SUCCESS|Đăng ký thành công!";
            }
            catch (Exception ex) { return $"ERROR|Lỗi DB: {ex.Message}"; }
        }

        public async Task<string> LoginUserAsync(string username, string password)
        {
            try
            {
                string storedHash = "";
                string fullName = "";
                string email = "";
                string dob = "";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string sql = "SELECT PasswordHash, FullName, Email, Birthday FROM Users WHERE Username = @u";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                storedHash = reader["PasswordHash"].ToString();
                                fullName = reader["FullName"].ToString();
                                email = reader["Email"].ToString();
                                dob = Convert.ToDateTime(reader["Birthday"]).ToString("yyyy-MM-dd");
                            }
                            else return "ERROR|Sai tài khoản.";
                        }
                    }
                }

                if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                {
                    return $"LOGIN_SUCCESS|{fullName}|{email}|{dob}";
                }
                return "ERROR|Sai mật khẩu.";
            }
            catch (Exception ex) { return $"ERROR|Lỗi DB: {ex.Message}"; }
        }
    }
}