using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


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
                DisplayExhortationDetails(exhortationId);  // Call the method to display details
            }
        }

        private void DisplayExhortationDetails(int exhortationId)
        {
            var exhortation = exhortationManager.GetExhortationById(exhortationId);

            if (exhortation != null)
            {
                // Retrieve AI Transcription and Summary if IDs are available
                AITranscription transcription = exhortation.AITranscriptionID.HasValue
                    ? exhortationManager.GetAITranscriptionById(exhortation.AITranscriptionID.Value)
                    : null;

                AISummary summary = exhortation.AISummaryID.HasValue
                    ? exhortationManager.GetAISummaryById(exhortation.AISummaryID.Value)
                    : null;

                // Populate the details section
                TitleLabel.Text = exhortation.Title;
                SpeakerLabel.Text = exhortation.Speaker;
                DateLabel.Text = exhortation.Date.ToString("dd-MM-yyyy");
                TranscriptionLabel.Text = transcription?.TranscriptionText ?? "No transcription available.";
                SummaryLabel.Text = summary?.SummaryText ?? "No summary available.";

                // Set the audio player source
                AudioPlayer.Src = $"DownloadExhortation.aspx?id={exhortationId}";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the search query from the text box
            string searchTerm = txtSearchQuery2.Text.Trim();

            string email = Session["UserEmail"].ToString();
            int userId = userManager.GetUserIdByEmail(email);
            int userChurchId = userManager.GetChurchIdByUserId(userId);
            int churchId = userManager.GetChurchIdByUserId(userId);

            // Retrieve the filtered DataTable for the current church
            DataTable exhortationsTable = new DataTable();
            exhortationsTable = exhortationManager.GetExhortationsByChurchID(churchId);

            // Perform search on the DataTable using the search term
            PerformSearch(exhortationsTable, searchTerm);

        }


        private void PerformSearch(DataTable exhortationsTable, string searchTerm)
        {
            // Use LINQ to search for the term in Title or SummaryText, case-insensitively
            var results = exhortationsTable.AsEnumerable()
     .Where(row => (row.Field<string>("Title") ?? "").IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                   (row.Field<string>("SummaryText") ?? "").IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);

            if (results.Any())
            {
                // Convert to DataTable only if there are results
                DataTable filteredTable = results.CopyToDataTable();
                ExhortationListRepeater.DataSource = filteredTable;
                ExhortationListRepeater.DataBind();
            }
            else
            {
                // Handle no results
                ExhortationListRepeater.DataSource = null;
                ExhortationListRepeater.DataBind();
            }
        }



        private void PerformSearch(int churchID, string searchTerm)
        {
            DataTable searchResults = exhortationManager.SearchExhortations(churchID, searchTerm);

            if (searchResults.Rows.Count > 0)
            {
                ExhortationListRepeater.DataSource = searchResults;
                ExhortationListRepeater.DataBind();
            }
            else
            {
                // Handle the case when no results are found
                ExhortationListRepeater.DataSource = null;
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
