using System;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class BibleCourseUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
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
            }*/
        }


        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            TextBoxCourseTitle.Text = string.Empty;
            TextBoxDescription.Text = string.Empty;
            TextBoxTheme.Text = string.Empty;
            TextBoxCompletionTime.Text = string.Empty;
            FileUploadCoverImage.Attributes.Clear();
        }

        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            // Code to handle form submission, e.g., saving to database
            string courseTitle = TextBoxCourseTitle.Text;
            string description = TextBoxDescription.Text;
            string theme = TextBoxTheme.Text;
            string completionTime = TextBoxCompletionTime.Text;

            if (FileUploadCoverImage.HasFile)
            {
                string fileName = FileUploadCoverImage.FileName;
                // Save the file, handle errors, etc.
            }

            // Perform additional operations like saving to database, showing success message, etc.
        }
    }
}