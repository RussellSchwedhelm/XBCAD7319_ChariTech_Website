using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;


namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class BibleCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the session exists
            /*if (Session["UserEmail"] == null)
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

            


            if (!IsPostBack)
            {
                BindRepeater();
                BindCourses();
            }
            
        }


        private void BindRepeater()
        {
            // Bind the repeater to the list of item options
            RepeaterOptions.DataSource = GetItemOptions();
            RepeaterOptions.DataBind();
        }


        //Temp proof of concept method
        public List<ItemOption> GetItemOptions()
        {
            string imagePath = ResolveUrl("~/Images/Trash 13.png");//This currently doesnt have a use but is good as a parameter placeholder

            return new List<ItemOption>
            {
                new ItemOption { IconUrl = imagePath, Text = "New Believers" },
                new ItemOption { IconUrl = imagePath, Text = "Old Testament" },
                new ItemOption { IconUrl = imagePath, Text = "New Testament" },
                new ItemOption { IconUrl = imagePath, Text = "Biblical Theology" },
                new ItemOption { IconUrl = imagePath, Text = "Christian Living" },
                new ItemOption { IconUrl = imagePath, Text = "Spiritual Growth" },
                new ItemOption { IconUrl = imagePath, Text = "Ministry Training" },
                new ItemOption { IconUrl = imagePath, Text = "Special Topics" }
            };
        }



        private void BindCourses()
        {
            CourseManager courseManager = new CourseManager();
            List<CourseClass> courses = courseManager.GetAllCourses();

            foreach (var course in courses)
            {
                // Save PDF and get URL
                course.PdfFileUrl = courseManager.SavePdfFromByteArray(course.PdfFileContent, course.CourseTitle.Replace(" ", "_"));
            }


            dlCourses.DataSource = courses;
            dlCourses.DataBind();
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //To be finished later
        }


   }

    //For Filter UI
    public class ItemOption
    {
        public string IconUrl { get; set; }  // URL to the icon image
        public string Text { get; set; }      // Display text for the option
    }
}