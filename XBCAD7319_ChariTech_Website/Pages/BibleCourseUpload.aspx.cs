using System;
using System.IO;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class BibleCourseUpload : System.Web.UI.Page
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


        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            TextBoxCourseTitle.Text = string.Empty;
            TextBoxDescription.Text = string.Empty;
            
            ddlTheme.SelectedValue= string.Empty;
            TextBoxCompletionTime.Text = string.Empty;
            FileUpload1.Attributes.Clear();
        }

        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {


            if (FileUpload1.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();

                // Check if the uploaded file is a PDF
                if (fileExtension == ".pdf")
                {
                    // Gather input values
                    string courseTitle = TextBoxCourseTitle.Text.Trim();
                    string description = TextBoxDescription.Text.Trim();
                    string theme = ddlTheme.SelectedValue;
                    string completionTime = TextBoxCompletionTime.Text.Trim();

                    // Check for empty fields
                    if (string.IsNullOrEmpty(courseTitle) ||
                        string.IsNullOrEmpty(description) ||
                        string.IsNullOrEmpty(theme) ||
                        string.IsNullOrEmpty(completionTime) ||
                        !FileUpload1.HasFile)
                    {
                        // Display an error message 
                        Response.Write("<script>alert('Please fill in all fields and upload a PDF.');</script>");
                        return; // Exit the method
                    }

                    // Convert PDF file to byte array
                    byte[] pdfBytes;
                    using (var binaryReader = new BinaryReader(FileUpload1.PostedFile.InputStream))
                    {
                        pdfBytes = binaryReader.ReadBytes(FileUpload1.PostedFile.ContentLength);
                    }

                    UserManager userManager= new UserManager();


                    var email = Session["UserEmail"].ToString();
                    var userID = userManager.GetUserIdByEmail(email);
                    var churchID = userManager.GetChurchIdByUserId(userID);


                    // Create a CourseClass object
                    CourseClass newCourse = new CourseClass
                    {
                        CourseTitle = courseTitle,
                        Theme = theme,
                        Duration = completionTime,
                        DateUploaded = DateTime.Now.ToString("yyyy-MM-dd"), // Or use a DateTime property if necessary
                        Description = description,
                        //PdfFileUrl = null
                        PdfFileContent = pdfBytes,
                        ChurchID = churchID
                    };

                    // Save the course to the database
                    CourseManager courseManagerHere = new CourseManager();



                    bool isSuccess = courseManagerHere.SaveCourse(newCourse);

                    if (isSuccess)
                    {
                        // Display a success message
                        Response.Write("<script>alert('Course sucessfully uploaded!');</script>");
                    }
                    else
                    {
                        // Display an error message
                        Response.Write("<script>alert('Error Uploading course.');</script>");
                    }
                }
                else
                {
                    Response.Write("Please upload a valid PDF file.");
                }
            }          
        }
    }
}