using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class RegistrationManager
    {
        //---------------------------------------------------------------------------------------------------------------------//
        // Method to check if the email is already registered
        public bool IsEmailRegistered(string email)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0; // Return true if the email is already in use
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // In case of error, assume the email is not registered
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
        // Method to register a new user
        public bool RegisterUser(string firstName, string surname, string email, int churchID, string password, byte[] profilePicture)
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

                    string query = "INSERT INTO Users (FirstName, Surname, Email, ChurchID, PasswordHash, ProfilePicture, DateRegistered, RoleID) " +
                                   "VALUES (@FirstName, @Surname, @Email, @ChurchID, @PasswordHash, @ProfilePicture, @DateRegistered, @RoleID)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@Surname", surname);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@ChurchID", churchID);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@ProfilePicture", (object)profilePicture ?? DBNull.Value); // Handle null for profile picture
                        cmd.Parameters.AddWithValue("@DateRegistered", DateTime.Now); // Automatically set registration date
                        cmd.Parameters.AddWithValue("@RoleID", 1);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Return true if the insert was successful
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
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
        //---------------------------------------------------------------------------------------------------------------------//
        // Method to fetch and return the list of churches sorted by ChurchID
        public DataTable GetSortedChurches()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ChurchID, ChurchName FROM Church ORDER BY ChurchID ASC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable churches = new DataTable();
                    da.Fill(churches);  // Fill the DataTable with church data
                    return churches;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//