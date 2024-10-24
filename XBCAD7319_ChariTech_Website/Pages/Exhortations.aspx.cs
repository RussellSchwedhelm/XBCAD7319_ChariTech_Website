using System;
using System.Data;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;


namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Exhortations : System.Web.UI.Page
    {
        private ExhortationManager exhortationManager = new ExhortationManager();
        private UserManager userManager = new UserManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string email = Session["UserEmail"].ToString();
                    int userId = userManager.GetUserIdByEmail(email);
                    int userChurchId = userManager.GetChurchIdByUserId(userId);
                    LoadExhortations(userChurchId);
                }
            }
        }

        private void LoadExhortations(int churchId)
        {
            DataTable exhortations = exhortationManager.GetExhortationsByChurchID(churchId);

            if (exhortations.Rows.Count > 0)
            {
                ExhortationListRepeater.DataSource = exhortations;
                ExhortationListRepeater.DataBind();
            }
        }
    }

    //the logic behind the search function
    /*protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchQuery = txtSearchQuery2.Text.Trim();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            //List<string> results = PerformSearch(searchQuery);  // Simulate the search logic
            //DisplayResults(results);
        }
        else
        {
            //lblSearchResults.Text = "Please enter a search term.";
        }
    }*/




}
