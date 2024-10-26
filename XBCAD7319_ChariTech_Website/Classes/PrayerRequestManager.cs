using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class PrayerRequestManager
    {
        private readonly string connectionString;

        public PrayerRequestManager()
        {
            connectionString = ConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
        }

        // Method to fetch approved prayer requests only
        public List<PrayerRequest> GetApprovedPrayerRequests()
        {
            var prayerRequests = new List<PrayerRequest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PrayerTarget FROM dbo.PrayerRequest WHERE Approved = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prayerRequests.Add(new PrayerRequest
                            {
                                PrayerTarget = reader["PrayerTarget"] != DBNull.Value ? reader["PrayerTarget"].ToString() : string.Empty
                            });
                        }
                    }
                }
            }

            // Add a blank PrayerRequest object if no records were found
            if (prayerRequests.Count == 0)
            {
                prayerRequests.Add(new PrayerRequest { PrayerTarget = string.Empty });
            }

            return prayerRequests;
        }

        // Method to fetch all prayer requests with approval status
        public List<PrayerRequest> GetAllPrayerRequests()
        {
            var prayerRequests = new List<PrayerRequest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestID, PrayerTarget, Approved FROM dbo.PrayerRequest";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prayerRequests.Add(new PrayerRequest
                            {
                                RequestID = (int)reader["RequestID"],
                                PrayerTarget = reader["PrayerTarget"] != DBNull.Value ? reader["PrayerTarget"].ToString() : string.Empty,
                                Approved = reader["Approved"] != DBNull.Value && (bool)reader["Approved"]
                            });
                        }
                    }
                }
            }

            return prayerRequests;
        }

        // Method to submit a new prayer request
        public void SubmitPrayerRequest(int userId, string prayerTarget)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.PrayerRequest (UserID, PrayerTarget) VALUES (@UserID, @PrayerTarget)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@PrayerTarget", prayerTarget);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to update the approval status of a prayer request
        public void UpdatePrayerRequestApprovalStatus(int requestId, bool isApproved)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE dbo.PrayerRequest SET Approved = @Approved WHERE RequestID = @RequestID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Approved", isApproved);
                    command.Parameters.AddWithValue("@RequestID", requestId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
