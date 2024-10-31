using System;
using System.Data;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class EditExhortations : System.Web.UI.Page
    {
        private ExhortationManager exhortationManager = new ExhortationManager();

        //---------------------------------------------------------------------------------------------------------------------//

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user session exists
            if (Session["UserEmail"] == null)
            {
                // If no session, redirect to the login page
                Response.Redirect("Login.aspx");
            }
            else if (!IsPostBack)
            {
                // If session exists, load exhortations
                string email = Session["UserEmail"].ToString();
                int churchId = exhortationManager.GetChurchIdByEmail(email);
                LoadExhortations(churchId);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void LoadExhortations(int churchId)
        {
            // Retrieve the list of exhortations for the church
            DataTable exhortations = exhortationManager.GetExhortationsByChurchID(churchId);
            ExhortationListRepeater.DataSource = exhortations;
            ExhortationListRepeater.DataBind();
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void ExhortationListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Get the exhortation ID from the CommandArgument
            int exhortationId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "DeleteExhortation":
                    // Delete the selected exhortation
                    exhortationManager.DeleteExhortationById(exhortationId);
                    ReloadExhortations();
                    break;
                case "LoadDetails":
                    // Display the details of the selected exhortation
                    DisplayExhortationDetails(exhortationId);
                    ViewState["CurrentExhortationId"] = exhortationId;
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        private void ReloadExhortations()
        {
            // Reload the list of exhortations after changes
            int churchId = exhortationManager.GetChurchIdByEmail(Session["UserEmail"].ToString());
            LoadExhortations(churchId);
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void DisplayExhortationDetails(int exhortationId)
        {
            var exhortation = exhortationManager.GetExhortationById(exhortationId);
            if (exhortation != null)
            {
                txtExhortationTitle.Text = exhortation.Title;
                txtExhortationSpeaker.Text = exhortation.Speaker;
                txtExhortationDate.Text = exhortation.Date.ToString("yyyy-MM-dd"); // Format for date picker
                txtExhortationTranscript.Text = exhortation.AITranscriptionText;
                txtExhortationSummary.Text = exhortation.AISummaryText;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (ViewState["CurrentExhortationId"] != null)
            {
                int exhortationId = Convert.ToInt32(ViewState["CurrentExhortationId"]);
                string title = txtExhortationTitle.Text;
                string speaker = txtExhortationSpeaker.Text;
                DateTime date = DateTime.Parse(txtExhortationDate.Text);
                string transcriptText = txtExhortationTranscript.Text;
                string summaryText = txtExhortationSummary.Text;

                exhortationManager.UpdateExhortationDetails(exhortationId, title, speaker, date, transcriptText, summaryText);
                ReloadExhortations();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Clear the details view and reset the view state
            txtExhortationTitle.Text = "";
            txtExhortationTranscript.Text = "";
            txtExhortationSummary.Text = "";
            txtExhortationSpeaker.Text = "";
            txtExhortationDate.Text = "";
            ViewState["CurrentExhortationId"] = null;
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Perform search for exhortations based on user input
            string searchTerm = txtSearchQuery2.Text.Trim();
            string email = Session["UserEmail"].ToString();
            int userChurchId = exhortationManager.GetChurchIdByEmail(email);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                DataTable searchResults = exhortationManager.SearchExhortations(userChurchId, searchTerm);
                ExhortationListRepeater.DataSource = searchResults;
            }
            else
            {
                LoadExhortations(userChurchId);
            }
            ExhortationListRepeater.DataBind();
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//