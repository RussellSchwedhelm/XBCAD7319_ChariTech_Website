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
        // Handles the exhortation upload
        protected void UploadExhortationButton_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            int churchId = 1; // Replace with actual church ID logic if needed
            string title = Request.Form["exhortation-title"];
            string speaker = Request.Form["exhortation-speaker"];
            DateTime issueDate = DateTime.Parse(Request.Form["exhortation-date"]);

            if (exhortationFileUpload.HasFile && exhortationFileUpload.PostedFile.ContentLength > 0 && exhortationFileUpload.PostedFile.FileName.EndsWith(".mp3"))
            {
                HttpPostedFile uploadedFile = exhortationFileUpload.PostedFile;
                ExhortationManager exhortationManager = new ExhortationManager();
                bool uploadSuccess = exhortationManager.UploadExhortation(email, churchId, title, speaker, issueDate, uploadedFile);

                if (uploadSuccess)
                {
                    Response.Write("<script>alert('Exhortation uploaded successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Failed to upload the exhortation.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please upload a valid MP3 file.');</script>");
            }
        }

        // Handles the newsletter upload
        protected void UploadNewsletterButton_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            int churchId = 1; // Replace with actual church ID logic if needed
            string title = Request.Form["newsletter-title"];
            DateTime issueDate = DateTime.Parse(Request.Form["newsletter-date"]);

            if (fileUploadControl.HasFile && fileUploadControl.PostedFile.ContentLength > 0)
            {
                HttpPostedFile uploadedFile = fileUploadControl.PostedFile;
                NewsletterManager newsletterManager = new NewsletterManager();
                bool uploadSuccess = newsletterManager.UploadNewsletter(email, churchId, title, issueDate, uploadedFile);

                if (uploadSuccess)
                {
                    Response.Write("<script>alert('Newsletter uploaded successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Failed to upload the newsletter.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please upload a valid file.');</script>");
            }
        }
    }
}
