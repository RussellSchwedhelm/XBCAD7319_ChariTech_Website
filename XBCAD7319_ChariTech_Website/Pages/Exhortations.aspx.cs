using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Exhortations : System.Web.UI.Page
    {
        private ExhortationManager exhortationManager = new ExhortationManager();
        private UserManager userManager = new UserManager();

        //---------------------------------------------------------------------------------------------------------------------//

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

                    // Check for query parameters
                    if (Request.QueryString["exhortationId"] != null && int.TryParse(Request.QueryString["exhortationId"], out int exhortationId))
                    {
                        // Set autoplay based on query parameter; default to false if not present or invalid
                        bool autoplay = Request.QueryString["autoplay"] != null && bool.TryParse(Request.QueryString["autoplay"], out bool auto) && auto;

                        // Load the specific exhortation with autoplay setting
                        LoadExhortationById(exhortationId, autoplay);

                        LoadExhortations(userChurchId);
                    }
                    else
                    {
                        // If no query parameters, load the list of exhortations for the church
                        LoadExhortations(userChurchId);
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        private void LoadExhortationById(int exhortationId, bool autoplay)
        {
            // Load exhortation details
            DisplayExhortationDetails(exhortationId);

            // Load and set audio based on autoplay parameter
            var audioData = exhortationManager.GetExhortationAudio(exhortationId);
            if (audioData != null)
            {
                string base64Audio = "data:audio/mp3;base64," + Convert.ToBase64String(audioData);
                ClientScript.RegisterStartupScript(this.GetType(), "playAudio",
                    $"setAudioSource('{base64Audio}', {exhortationId}, {autoplay.ToString().ToLower()});", true);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        private void LoadExhortations(int churchId)
        {
            DataTable exhortations = exhortationManager.GetExhortationsByChurchID(churchId);

            if (exhortations.Rows.Count > 0)
            {
                ExhortationListRepeater.DataSource = exhortations;
                ExhortationListRepeater.DataBind();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void ExhortationListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ShowDetails")
            {
                int exhortationId = Convert.ToInt32(e.CommandArgument);
                DisplayExhortationDetails(exhortationId);
            }
            else if (e.CommandName == "PlayAudio")
            {
                int exhortationId = Convert.ToInt32(e.CommandArgument);
                PlayExhortation(exhortationId, autoplay: true); // Play immediately
            }
            else if (e.CommandName == "LoadAudio")
            {
                int exhortationId = Convert.ToInt32(e.CommandArgument);
                PlayExhortation(exhortationId, autoplay: false); // Load but do not autoplay
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        private void PlayExhortation(int exhortationId, bool autoplay)
        {
            var audioData = exhortationManager.GetExhortationAudio(exhortationId);

            if (audioData != null)
            {
                string base64Audio = "data:audio/mp3;base64," + Convert.ToBase64String(audioData);

                // Display the exhortation details
                DisplayExhortationDetails(exhortationId);

                // Set audio source with autoplay parameter
                ClientScript.RegisterStartupScript(this.GetType(), "playAudio",
                    $"setAudioSource('{base64Audio}', {exhortationId}, {autoplay.ToString().ToLower()});", true);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        private void DisplayExhortationDetails(int exhortationId)
        {
            var exhortation = exhortationManager.GetExhortationById(exhortationId);

            if (exhortation != null)
            {
                // Populate the details section with data from the Exhortation object
                TitleLabel.Text = exhortation.Title;
                SpeakerLabel.Text = exhortation.Speaker;
                DateLabel.Text = exhortation.Date.ToString("dd-MM-yyyy");
                TranscriptionLabel.Text = !string.IsNullOrEmpty(exhortation.AITranscriptionText)
                    ? exhortation.AITranscriptionText
                    : "No transcription available.";
                SummaryLabel.Text = !string.IsNullOrEmpty(exhortation.AISummaryText)
                    ? exhortation.AISummaryText
                    : "No summary available.";
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchQuery2.Text.Trim();
            string email = Session["UserEmail"].ToString();
            int userId = userManager.GetUserIdByEmail(email);
            int userChurchId = userManager.GetChurchIdByUserId(userId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Fetch prioritized search results
                DataTable searchResults = exhortationManager.SearchExhortations(userChurchId, searchTerm);
                ExhortationListRepeater.DataSource = searchResults;
            }
            else
            {
                // Load default exhortations list if search term is empty
                DataTable exhortations = exhortationManager.GetExhortationsByChurchID(userChurchId);
                ExhortationListRepeater.DataSource = exhortations;
            }
            ExhortationListRepeater.DataBind();
        }
        //---------------------------------------------------------------------------------------------------------------------//

        private void PerformSearch(DataTable exhortationsTable, string searchTerm)
        {
            var results = exhortationsTable.AsEnumerable()
                .Where(row => (row.Field<string>("Title") ?? "").IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                              (row.Field<string>("SummaryText") ?? "").IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);

            if (results.Any())
            {
                DataTable filteredTable = results.CopyToDataTable();
                ExhortationListRepeater.DataSource = filteredTable;
                ExhortationListRepeater.DataBind();
            }
            else
            {
                ExhortationListRepeater.DataSource = null;
                ExhortationListRepeater.DataBind();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//