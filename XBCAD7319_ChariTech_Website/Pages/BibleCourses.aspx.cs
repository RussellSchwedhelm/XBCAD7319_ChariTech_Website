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
            // Example data source: A list of courses (could come from a database)
            List<CourseClass> courses = new List<CourseClass>()
            {
                new CourseClass { CourseTitle = "Course 1", Theme = "New Believers", Duration = "10-11 weeks", DateUploaded = "Oct 2023", Description = "Course 1 description", ImageUrl = "~/Images/Media.png", PdfFileUrl="~/Content/CoursePdfs/FinanceTextbook.pdf" },
                new CourseClass { CourseTitle = "Course 2", Theme = "Old Testament", Duration = "6-8 weeks", DateUploaded = "Sept 2023", Description = "Course 2 description 100000000000000000000", ImageUrl = "~/Images/Media.png",PdfFileUrl="~/Content/CoursePdfs/FinanceTextbook.pdf" },
                new CourseClass { CourseTitle = "Course 3", Theme = "Special Topics", Duration = "10-11 weeks", DateUploaded = "Nov 2023", Description = "Course 1 description", ImageUrl = "~/Images/Media.png",PdfFileUrl="~/Content/CoursePdfs/FinanceTextbook.pdf" },
                new CourseClass { CourseTitle = "Course 4", Theme = "Biblical Theology", Duration = "10-11 weeks", DateUploaded = "Dec 2023", Description = "Course 1 description", ImageUrl = "~/Images/Media.png", PdfFileUrl="~/Content/CoursePdfs/FinanceTextbook.pdf" },
                
                
                // Add more courses here
            };

            dlCourses.DataSource = courses;
            dlCourses.DataBind();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }






   }


    //temporary testing class

    public class ItemOption
    {
        public string IconUrl { get; set; }  // URL to the icon image
        public string Text { get; set; }      // Display text for the option
    }







}