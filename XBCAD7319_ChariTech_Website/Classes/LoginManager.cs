using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class LoginManager
    {
        // This method will authenticate the user by checking credentials in the database.
        public bool AuthenticateUser(string email, string password)
        {
            // Placeholder for actual Azure SQL Database connection and authentication logic
            // Once connected to Azure SQL, replace the hardcoded logic with actual DB queries.

            // Example query - replace with your actual DB connection and query
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // If count is greater than 0, the user exists and is authenticated
                    return count > 0;
                }
            }
        }
    }
}
