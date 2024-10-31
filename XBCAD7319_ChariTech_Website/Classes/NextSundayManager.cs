using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using XBCAD7319_ChariTech_Website.Pages;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class NextSundayManager
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

        //---------------------------------------------------------------------------------------------------------------------//
        // Method to get next Sunday information from the database
        public SundayInfo GetSundayInfoByDate(DateTime sundayDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT Presiding, Exhortation, OnTheDoor
                FROM dbo.NextSunday
                WHERE NextSundayDate = @sundayDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sundayDate", sundayDate);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new SundayInfo
                        {
                            Presiding = reader["Presiding"].ToString(),
                            Exhortation = reader["Exhortation"].ToString(),
                            OnTheDoor = reader["OnTheDoor"].ToString()
                        };
                    }
                }
            }
            return null; // No entry found for the selected date
        }
        //---------------------------------------------------------------------------------------------------------------------//
        // Method to save or update next Sunday information in the database
        public void SaveNextSundayInfo(int churchId, DateTime nextSundayDate, string presiding, string exhortation, string onTheDoor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    IF EXISTS (SELECT 1 FROM dbo.NextSunday WHERE NextSundayDate = @NextSundayDate)
                    BEGIN
                        UPDATE dbo.NextSunday
                        SET Presiding = @Presiding, Exhortation = @Exhortation, OnTheDoor = @OnTheDoor
                        WHERE NextSundayDate = @NextSundayDate
                    END
                    ELSE
                    BEGIN
                        INSERT INTO dbo.NextSunday (ChurchID, NextSundayDate, Presiding, Exhortation, OnTheDoor)
                        VALUES (@ChurchID, @NextSundayDate, @Presiding, @Exhortation, @OnTheDoor)
                    END";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ChurchID", churchId);
                    command.Parameters.AddWithValue("@NextSundayDate", nextSundayDate);
                    command.Parameters.AddWithValue("@Presiding", presiding);
                    command.Parameters.AddWithValue("@Exhortation", exhortation);
                    command.Parameters.AddWithValue("@OnTheDoor", onTheDoor);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        public List<(DateTime Date, string Presiding, string Exhortation, string OnTheDoor)> GetFutureSundays()
        {
            var futureSundays = new List<(DateTime Date, string Presiding, string Exhortation, string OnTheDoor)>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT NextSundayDate, Presiding, Exhortation, OnTheDoor
                    FROM dbo.NextSunday
                    WHERE NextSundayDate >= @Today
                    ORDER BY NextSundayDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Today", DateTime.Today);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        futureSundays.Add((
                            Date: reader.GetDateTime(0),
                            Presiding: reader["Presiding"].ToString(),
                            Exhortation: reader["Exhortation"].ToString(),
                            OnTheDoor: reader["OnTheDoor"].ToString()
                        ));
                    }
                }
            }
            return futureSundays;

        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//