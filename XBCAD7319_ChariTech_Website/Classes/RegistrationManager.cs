using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class RegistrationManager
    {
        public bool RegisterUser(string firstName, string surname, string email, string ecclesia, string password)
        {
            // Hash the password before saving it
            string hashedPassword = HashPassword(password);

            // Get the connection string from Web.config
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (FirstName, Surname, Email, Ecclesia, PasswordHash) VALUES (@FirstName, @Surname, @Email, @Ecclesia, @PasswordHash)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@Surname", surname);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Ecclesia", ecclesia);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        int result = cmd.ExecuteNonQuery();

                        // If result > 0, it means the insert was successful
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as necessary
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        // Function to hash the password using SHA256
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
