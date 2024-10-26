using System;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class Notification
    {
        public int NotificationID { get; set; }  // Unique ID for each notification
        public string Title { get; set; }        // Title of the notification
        public string Message { get; set; }      // Main content/message of the notification
        public DateTime SentAt { get; set; }     // Timestamp when the notification was sent

        // Optional: Constructor for easy instantiation
        public Notification(int notificationID, string title, string message, DateTime sentAt)
        {
            NotificationID = notificationID;
            Title = title;
            Message = message;
            SentAt = sentAt;
        }

        // Parameterless constructor (required for data-binding or serialization)
        public Notification()
        {
        }
    }
}
