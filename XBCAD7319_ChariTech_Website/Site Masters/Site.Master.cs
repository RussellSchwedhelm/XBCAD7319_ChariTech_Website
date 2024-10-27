using System;
using System.Web.UI;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website
{
    public partial class SiteMaster : MasterPage
    {
        private ContactManager contactManager = new ContactManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Register the Page_PreRender event handler to ensure controls are available before rendering
                Page.PreRender += Page_PreRender;
            }

            if (Session["UserEmail"] != null)
            {
                // Set the profile image for the user on each page load
                SetProfileImage();

                NewsletterManager newsletterManager = new NewsletterManager();
                int userId = newsletterManager.GetUserIdByEmail(Session["UserEmail"].ToString());
                NotificationManager notificationManager = new NotificationManager();

                // Check for unread notifications and update session variable
                bool hasUnreadNotifications = notificationManager.HasUnreadNotifications(userId);
                Session["HasUnreadNotifications"] = hasUnreadNotifications;

                // Update the notification icon based on unread notifications
                NotificationIcon.ImageUrl = hasUnreadNotifications
                    ? ResolveUrl("~/Images/notification_unread.png")
                    : ResolveUrl("~/Images/notification_read.png");
            }
        }

        private void SetProfileImage()
        {
            // Retrieve the user's email from the session
            string email = Session["UserEmail"].ToString();

            // Fetch the profile picture as a byte array using ContactManager
            byte[] profilePicBytes = contactManager.GetProfilePictureByEmail(email);

            // If a profile picture exists, convert it to Base64 string
            if (profilePicBytes != null)
            {
                string profilePicBase64 = "data:image/png;base64," + Convert.ToBase64String(profilePicBytes);
                ProfilePicture.ImageUrl = profilePicBase64;
            }
            else
            {
                // Set a default profile picture if no image is available
                ProfilePicture.ImageUrl = ResolveUrl("~/Images/default_profile_picture.png");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // This event handler is called after all controls are initialized and available
            if (Session["UserEmail"] != null)
            {
                NewsletterManager newsletterManager = new NewsletterManager();
                int userId = newsletterManager.GetUserIdByEmail(Session["UserEmail"].ToString());
                NotificationManager notificationManager = new NotificationManager();

                // Retrieve unread notifications only if there are any
                bool hasUnreadNotifications = (bool)Session["HasUnreadNotifications"];
                if (hasUnreadNotifications && rptNotifications != null)
                {
                    var notifications = notificationManager.GetUnreadNotifications(userId);

                    // Bind the unread notifications to the Repeater control
                    rptNotifications.DataSource = notifications;
                    rptNotifications.DataBind();
                }
            }
        }

        protected void ClearNotifications(object sender, EventArgs e)
        {
            // Initialize necessary managers
            NotificationManager notificationManager = new NotificationManager();
            NewsletterManager newsletterManager = new NewsletterManager();

            // Get the user ID based on the session email
            int userId = newsletterManager.GetUserIdByEmail(Session["UserEmail"].ToString());

            // Retrieve all unread notifications for the user
            var unreadNotifications = notificationManager.GetUnreadNotifications(userId);

            // Loop through each unread notification and mark it as read
            foreach (var notification in unreadNotifications)
            {
                // Mark each notification as read
                notificationManager.MarkNotificationAsRead(userId, notification.NotificationID);
            }

            // Optionally, update the UI or session to reflect that notifications are cleared
            Session["HasUnreadNotifications"] = false;
            NotificationIcon.ImageUrl = ResolveUrl("~/Images/notification_read.png");

            // Re-bind the Repeater control to refresh the UI if necessary
            rptNotifications.DataSource = null;
            rptNotifications.DataBind();
        }
    }
}
