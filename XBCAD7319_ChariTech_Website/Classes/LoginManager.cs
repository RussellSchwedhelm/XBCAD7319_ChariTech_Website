using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class LoginManager
    {
        // This method will authenticate the user by checking credentials in the database.
        public bool AuthenticateUser(string email, string password)
        {
            // Placeholder for actual Azure SQL Database connection and authentication logic
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT PasswordHash FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        // Get the stored hashed password from the database
                        string storedPasswordHash = Convert.ToString(cmd.ExecuteScalar());

                        if (!string.IsNullOrEmpty(storedPasswordHash))
                        {
                            // Hash the provided password
                            string hashedPassword = HashPassword(password);

                            // Compare the provided password (hashed) with the stored password (hashed)
                            return storedPasswordHash == hashedPassword;
                        }
                        else
                        {
                            // Email not found or no password stored
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error (optional) and handle connection exceptions
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        // Hash the password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
