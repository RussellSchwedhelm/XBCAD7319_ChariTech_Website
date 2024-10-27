using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;
using System.Text;

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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchQuery2.Text.Trim();
            string email = Session["UserEmail"].ToString();
            int userId = userManager.GetUserIdByEmail(email);
            int userChurchId = userManager.GetChurchIdByUserId(userId);
            int churchId = userManager.GetChurchIdByUserId(userId);

            DataTable exhortationsTable = exhortationManager.GetExhortationsByChurchID(churchId);

            PerformSearch(exhortationsTable, searchTerm);
        }

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
    }
}
