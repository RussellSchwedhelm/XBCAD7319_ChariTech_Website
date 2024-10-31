using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class BibleCourses : System.Web.UI.Page
    {

        public ExhortationManager exhortationManager = new ExhortationManager();


        //---------------------------------------------------------------------------------------------------------------------//
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
        //---------------------------------------------------------------------------------------------------------------------//
        private void BindRepeater()
        {
            RepeaterOptions.DataSource = GetItemOptions();
            RepeaterOptions.DataBind();
        }
        //---------------------------------------------------------------------------------------------------------------------//

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
        //---------------------------------------------------------------------------------------------------------------------//

        private void BindCourses(List<string> themes = null)
        {
            CourseManager courseManager = new CourseManager();
            int churchId = exhortationManager.GetChurchIdByEmail(Session["UserEmail"].ToString());

            // If no themes are provided, check session (used for initial load)
            if (themes == null)
            {
                themes = Session["SelectedThemes"] as List<string> ?? new List<string>();
            }

            // Fetch all courses for the given church, filtering by selected themes
            List<CourseClass> allCourses = courseManager.GetAllCourses(churchId);
            var filteredCourses = allCourses
                .Where(course => themes.Count == 0 || themes.Contains(course.Theme, StringComparer.OrdinalIgnoreCase))
                .ToList();

            foreach (var course in filteredCourses)
            {
                // Save PDF and get URL
                course.PdfFileUrl = courseManager.SavePdfFromByteArray(course.PdfFileContent, course.CourseTitle.Replace(" ", "_"));
            }

            dlCourses.DataSource = filteredCourses;
            dlCourses.DataBind();
        }


        //---------------------------------------------------------------------------------------------------------------------//

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CourseManager courseManager = new CourseManager();
            int churchId = exhortationManager.GetChurchIdByEmail(Session["UserEmail"].ToString());
            string searchQuery = txtSearchQuery2.Text?.ToLower() ?? string.Empty;

            try
            {
                // Retrieve selected themes from session
                List<string> themes = Session["SelectedThemes"] as List<string> ?? new List<string>();

                // Fetch all courses for the given church
                List<CourseClass> allCourses = courseManager.GetAllCourses(churchId);

                // Filter courses by search query and selected themes
                var filteredCourses = allCourses
                    .Where(course =>
                        (course.CourseTitle?.ToLower().Contains(searchQuery) ?? false) ||
                        (course.Description?.ToLower().Contains(searchQuery) ?? false))
                    .Where(course =>
                        themes.Count == 0 || themes.Contains(course.Theme, StringComparer.OrdinalIgnoreCase))
                    .ToList();

                // Bind the filtered list to dlCourses
                dlCourses.DataSource = filteredCourses;
                dlCourses.DataBind();
            }
            catch (Exception ex)
            {
                // Display error message if any exception occurs
                Response.Write("<script>alert('An error occurred while filtering courses.');</script>");
            }
        }




        //---------------------------------------------------------------------------------------------------------------------//

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            int churchId = exhortationManager.GetChurchIdByEmail(Session["UserEmail"].ToString());
            try
            {
                Button btn = (Button)sender;

                // Get the index from the CommandArgument
                int index = Convert.ToInt32(btn.CommandArgument);

                // Retrieve the list (filtered or total) of courses
                CourseManager courseManager = new CourseManager();
                List<CourseClass> courses = courseManager.GetAllCourses(churchId);

                // Retrieve the specific course using the index
                CourseClass selectedCourse = courses[index];

                // Save PDF and get the URL
                string pdfUrl = new CourseManager().SavePdfFromByteArray(selectedCourse.PdfFileContent, "CoursePDF");

                // Open the PDF URL in a new tab
                string script = $"window.open('{ResolveUrl(pdfUrl)}', '_blank');";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenPdf", script, true);
            }
            catch
            {
                // Alert if no PDF is available
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('PDF not available');", true);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------//


        protected void ThemeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var repeaterItem = (RepeaterItem)checkbox.NamingContainer; // Get the parent RepeaterItem
            var hiddenField = (HiddenField)repeaterItem.FindControl("ThemeHiddenField"); // Find the HiddenField with theme text

            string theme = hiddenField.Value; // Retrieve the theme from the HiddenField

            // Initialize selected themes list in session if it doesn't exist
            List<string> selectedThemes = Session["SelectedThemes"] as List<string> ?? new List<string>();

            if (checkbox.Checked)
            {
                // Add theme if not already in the list
                if (!selectedThemes.Contains(theme))
                {
                    selectedThemes.Add(theme);
                }
            }
            else
            {
                // Remove theme if it is in the list
                selectedThemes.Remove(theme);
            }

            // Update session variable
            Session["SelectedThemes"] = selectedThemes;

            // Bind filtered courses based on the updated list of selected themes
            BindCourses(selectedThemes);
        }


        //---------------------------------------------------------------------------------------------------------------------//
        public class ItemOption
        {
            public string IconUrl { get; set; }
            public string Text { get; set; }
        }
        //---------------------------------------------------------------------------------------------------------------------//




    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//