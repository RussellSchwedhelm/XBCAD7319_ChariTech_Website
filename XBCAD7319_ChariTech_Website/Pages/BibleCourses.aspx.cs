using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class BibleCourses : System.Web.UI.Page
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
                string email = Session["UserEmail"].ToString();
            }

            if (!IsPostBack)
            {
                BindRepeater();
                BindCourses();
            }
        }

        private void BindRepeater()
        {
            RepeaterOptions.DataSource = GetItemOptions();
            RepeaterOptions.DataBind();
        }

        public List<ItemOption> GetItemOptions()
        {
            string imagePath = ResolveUrl("~/Images/Trash 13.png");

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
            CourseManager courseManager = new CourseManager();
            string selectedTheme = GetSelectedTheme();
            string searchQuery = txtSearchQuery2.Text?.ToLower() ?? string.Empty;

            try
            {
                List<CourseClass> allCourses = courseManager.GetAllCourses();

                // Filter courses based on search query and selected theme
                var filteredCourses = allCourses
                    .Where(course => 
                        (course.CourseTitle?.ToLower().Contains(searchQuery) ?? false) || 
                        (course.Description?.ToLower().Contains(searchQuery) ?? false))
                    .ToList();

                if (!string.IsNullOrWhiteSpace(selectedTheme))
                {
                    filteredCourses = filteredCourses
                        .Where(course => course.Theme?.Equals(selectedTheme, StringComparison.OrdinalIgnoreCase) ?? false)
                        .ToList();
                }

                // Bind the filtered list to dlCourses
                dlCourses.DataSource = filteredCourses;
                dlCourses.DataBind();
            }
            catch (Exception ex)
            {
                // Display error message
                Response.Write("<script>alert('An error occurred while filtering courses.');</script>");
                
            }
        }

        private string GetSelectedTheme()
        {
            foreach (RepeaterItem item in RepeaterOptions.Items)
            {
                var checkBox = item.FindControl("CheckBoxItem") as CheckBox;
                var themeText = item.FindControl("item-text") as Label;

                if (checkBox != null && checkBox.Checked && themeText != null)
                {
                    return themeText.Text;
                }
            }
            return string.Empty;
        }
    }

    public class ItemOption
    {
        public string IconUrl { get; set; }
        public string Text { get; set; }
    }
}
