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
                string userEmail = Session["UserEmail"].ToString();  // Get the email from the session
                string title = Request.Form["title"];
                DateTime issueDate = DateTime.Parse(Request.Form["date"]);
                HttpPostedFile uploadedFile = Request.Files["fileUpload"];

                // Check if a file was uploaded
                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    // Create a new instance of the NewsletterManager class
                    NewsletterManager manager = new NewsletterManager();

                    // Call the UploadNewsletter method
                    bool isUploaded = manager.UploadNewsletter(
                        email: userEmail,  // Use email from session
                        churchId: 1,       // Example ChurchID (you can dynamically get this)
                        title: title,
                        issueDate: issueDate,
                        uploadedFile: uploadedFile
                    );

                    // Provide feedback to the user
                    if (isUploaded)
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
                    Response.Write("<script>alert('Please select a valid PDF file to upload.');</script>");
                }
            }
        }
    }

