using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class DonationManager
    {
        private readonly string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

        // Method to clear existing campaigns and insert new ones
        public bool ReplaceDonationCampaigns(int churchId, DataTable newDonationData)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Clear existing records for the church
                        string deleteQuery = "DELETE FROM DonationCampaign WHERE ChurchID = @ChurchID";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@ChurchID", churchId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        // Step 2: Insert new records
                        string insertQuery = @"INSERT INTO DonationCampaign (ChurchID, Title, DonatedAmount, DonationGoal)
                                               VALUES (@ChurchID, @Title, @DonatedAmount, @DonationGoal)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            foreach (DataRow row in newDonationData.Rows)
                            {
                                insertCmd.Parameters.Clear();
                                insertCmd.Parameters.AddWithValue("@ChurchID", churchId);
                                insertCmd.Parameters.AddWithValue("@Title", row["Title"]);
                                insertCmd.Parameters.AddWithValue("@DonatedAmount", row["DonatedAmount"]);
                                insertCmd.Parameters.AddWithValue("@DonationGoal", row["DonationGoal"]);
                                insertCmd.ExecuteNonQuery();
                            }
                        }

                        // Commit the transaction if all operations succeed
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        // Roll back the transaction if any error occurs
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        // Method to get donation campaigns for a specific church
        public DataTable GetDonationCampaigns(int churchId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, DonatedAmount, DonationGoal FROM DonationCampaign WHERE ChurchID = @ChurchID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChurchID", churchId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable donationCampaigns = new DataTable();
                    da.Fill(donationCampaigns);
                    return donationCampaigns;
                }
            }
        }
    }
}
