using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class NextSundayManager
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

        // Method to get next Sunday information from the database
        public (string Presiding, string Exhortation, string OnTheDoor) GetNextSundayInfo(DateTime nextSundayDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT TOP 1 Presiding, Exhortation, OnTheDoor
                    FROM dbo.NextSunday
                    WHERE NextSundayDate = @NextSundayDate
                    ORDER BY NextSundayDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NextSundayDate", nextSundayDate);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return (
                            reader["Presiding"].ToString(),
                            reader["Exhortation"].ToString(),
                            reader["OnTheDoor"].ToString()
                        );
                    }
                }
            }
            return (null, null, null); // Return nulls if no data found for next Sunday
        }

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
    }
}
