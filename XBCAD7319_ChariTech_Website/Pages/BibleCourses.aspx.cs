using System;
using System.Collections.Generic;

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



    }


    //temporary testing class

    public class ItemOption
    {
        public string IconUrl { get; set; }  // URL to the icon image
        public string Text { get; set; }      // Display text for the option
    }



}