using System;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
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
        }
    }
}