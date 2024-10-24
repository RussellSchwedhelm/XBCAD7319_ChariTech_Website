using System;
using System.Web;
using System.Web.UI;
using XBCAD7319_ChariTech_Website.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class AdminDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ensure user is logged in and has correct role
                if (Session["UserEmail"] == null || (int)Session["UserRoleID"] != 2)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        // Event handler for uploading newsletter
        protected void UploadNewsletterButton_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            ExhortationManager exhortationManager = new ExhortationManager();
            int churchId = exhortationManager.GetChurchIdByEmail(email); // Get ChurchID by email

            if (churchId != -1) // Ensure ChurchID was retrieved successfully
            {
                string title = NewsletterTitle.Text;
                DateTime date;

                if (DateTime.TryParse(NewsletterDate.Text, out date) && NewsletterFileUpload.HasFile)
                {
                    HttpPostedFile newsletterFile = NewsletterFileUpload.PostedFile;

                    if (newsletterFile != null && newsletterFile.ContentLength > 0)
                    {
                        NewsletterManager newsletterManager = new NewsletterManager();
                        bool uploadSuccess = newsletterManager.UploadNewsletter(email, churchId, title, date, newsletterFile);

                        if (uploadSuccess)
                        {
                            // Clear fields after successful upload
                            NewsletterTitle.Text = string.Empty;
                            NewsletterDate.Text = string.Empty;
                            NewsletterFileUpload.Attributes.Clear(); // Clears the file input

                            // Provide a clear message that the fields have been cleared
                            Response.Write("<script>alert('Newsletter uploaded successfully!');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed to upload the newsletter.');</script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please fill all fields and upload a valid file.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Unable to retrieve Church ID.');</script>");
            }
        }



        // Event handler for uploading exhortation
        protected void UploadExhortationButton_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            ExhortationManager exhortationManager = new ExhortationManager();
            int churchId = exhortationManager.GetChurchIdByEmail(email);

            if (churchId != -1)
            {
                string title = ExhortationTitle.Text;
                string speaker = ExhortationSpeaker.Text;
                DateTime date;
                if (DateTime.TryParse(ExhortationDate.Text, out date) && ExhortationFileUpload.HasFile)
                {
                    HttpPostedFile exhortationFile = ExhortationFileUpload.PostedFile;

                    if (exhortationFile != null && exhortationFile.ContentLength > 0 && exhortationFile.FileName.EndsWith(".mp3"))
                    {
                        bool uploadSuccess = exhortationManager.UploadExhortation(email, churchId, title, speaker, date, exhortationFile);

                        if (uploadSuccess)
                        {
                            // Clear fields after successful upload
                            ExhortationTitle.Text = string.Empty;
                            ExhortationSpeaker.Text = string.Empty;
                            ExhortationDate.Text = string.Empty;
                            ExhortationFileUpload.Attributes.Clear(); // Clears the file input
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
                else
                {
                    Response.Write("<script>alert('Please fill all fields.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Unable to retrieve Church ID.');</script>");
            }
        }
    }
}
