using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class NotificationManager
    {
        //---------------------------------------------------------------------------------------------------------------------//

        public bool SaveNotification(string title, string message, DateTime sentAt, int churchId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Insert the notification into the Notification table
                string query = @"
                    INSERT INTO Notification (Title, Message, SentAt, ChurchID) 
                    VALUES (@Title, @Message, @SentAt, @ChurchID);
                    SELECT SCOPE_IDENTITY();";

                int notificationId;

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Message", message);
                    cmd.Parameters.AddWithValue("@SentAt", sentAt);
                    cmd.Parameters.AddWithValue("@ChurchID", churchId);

                    // Execute the insert and retrieve the new notification ID
                    notificationId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insert into UserNotification for each user ID in the provided list
                query = @"
                    INSERT INTO UserNotification (NotificationID) 
                    VALUES (@NotificationID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NotificationID", notificationId);
                    cmd.ExecuteScalar();
                }

                return notificationId > 0;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        public bool HasUnreadNotifications(int userId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Select notifications that do not have a corresponding entry for the user in UserNotification
                string query = @"
                SELECT COUNT(1) 
                FROM Notification N
                WHERE N.NotificationID NOT IN (
                SELECT UN.NotificationID 
                FROM UserNotification UN 
                WHERE UN.UserID = @UserID
            )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    int unreadCount = (int)cmd.ExecuteScalar();
                    return unreadCount > 0;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        public List<Notification> GetUnreadNotifications(int userId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;
            List<Notification> notifications = new List<Notification>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT N.NotificationID, N.Title, N.Message, N.SentAt 
            FROM Notification N
            WHERE NOT EXISTS (
                SELECT 1 
                FROM UserNotification UN 
                WHERE UN.NotificationID = N.NotificationID 
                AND UN.UserID = @UserID
            )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new Notification
                            {
                                NotificationID = (int)reader["NotificationID"],
                                Title = reader["Title"].ToString(),
                                Message = reader["Message"].ToString(),
                                SentAt = (DateTime)reader["SentAt"]
                            });
                        }
                    }
                }
            }
            return notifications;
        }
        //---------------------------------------------------------------------------------------------------------------------//

        public bool MarkNotificationAsRead(int userId, int notificationId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Insert into UserNotification for a specific notification and user
                string query = @"
            INSERT INTO UserNotification (NotificationID, UserID)
            SELECT @NotificationID, @UserID
            WHERE NOT EXISTS (
                SELECT 1 
                FROM UserNotification 
                WHERE NotificationID = @NotificationID 
                AND UserID = @UserID
            );";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Set the parameters for user ID and notification ID
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@NotificationID", notificationId);
                    conn.Open();

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//