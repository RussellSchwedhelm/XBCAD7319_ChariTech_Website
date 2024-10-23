using System;
using System.Web;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if session exists
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (Session["UserRoleID"] == null || (int)Session["UserRoleID"] != 2)
            {
                // Redirect to home if the user is not an admin
                Response.Redirect("/Pages/Home.aspx");
            }
        }

        protected void UploadNewsletterButton_Click(object sender, EventArgs e)
        {
            // Retrieve form data
            string email = Session["UserEmail"].ToString(); // User email from session
            int churchId = 0; // Assuming churchId is 1 for now, modify as needed
            string title = Request.Form["title"]; // Newsletter title from input
            DateTime issueDate = DateTime.Parse(Request.Form["date"]); // Date from input

            // Access the FileUpload control
            if (fileUploadControl.HasFile && fileUploadControl.PostedFile.ContentLength > 0)
            {
                // Get the uploaded file
                HttpPostedFile uploadedFile = fileUploadControl.PostedFile;

                // Instantiate the NewsletterManager class
                NewsletterManager newsletterManager = new NewsletterManager();

                // Call the UploadNewsletter method
                bool uploadSuccess = newsletterManager.UploadNewsletter(email, churchId, title, issueDate, uploadedFile);

                if (uploadSuccess)
                {
                    // Display success message
                    Response.Write("<script>alert('Newsletter uploaded successfully!');</script>");
                }
                else
                {
                    // Display error message
                    Response.Write("<script>alert('Failed to upload the newsletter.');</script>");
                }
            }
            else
            {
                // Display error message for invalid file
                Response.Write("<script>alert('Please upload a valid PDF file.');</script>");
            }
        }

    }
}
