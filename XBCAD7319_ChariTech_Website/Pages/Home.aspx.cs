using System;
using System.Collections.Generic;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        // List to temporarily hold the prayer requests
        private List<string> temp;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the session exists
            if (Session["UserEmail"] == null)
            {
                // If no session, redirect to login page
                Response.Redirect("Login.aspx");
            }
            else
            {
                // User is authenticated, you can access their email
                string email = Session["UserEmail"].ToString();
                // Use the email for further logic if needed
            }

            // Only populate names if this is not a postback
            if (!IsPostBack)
            {
                // Call the PopulateNames method
                PopulateNames();

                // Bind the temp list to the Repeater
                PrayerRequestsRepeater.DataSource = temp;
                PrayerRequestsRepeater.DataBind();
            }
        }

        // Method to populate the list of names
        private void PopulateNames()
        {
            // Populate temp with a list of names
            temp = new List<string>
            {
                "Emily Johnson",
                "Michael Smith",
                "Olivia Brown",
                "Benjamin Davis",
                "Sophia Martinez",
                "Jacob Wilson",
                "Isabella Thompson",
                "Ethan Garcia"
            };
        }
    }
}
