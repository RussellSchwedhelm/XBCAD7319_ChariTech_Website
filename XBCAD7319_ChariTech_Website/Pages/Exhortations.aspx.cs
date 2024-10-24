using System;
using System.Data;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;


namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class EditExhortations : System.Web.UI.Page
    {

        private ExhortationManager exhortationManager = new ExhortationManager();
        private UserManager userManager = new UserManager();


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
                // User is authenticated, you can access their email
                string email = Session["UserEmail"].ToString();
                // Use the email for further logic if needed
                int userId = userManager.GetUserIdByEmail(email);
                int userChurchId = userManager.GetChurchIdByUserId(userId);
                LoadExhortations(userChurchId);

            }
        }


        private void LoadExhortations(int churchID)
        {
             DataTable exhortations = exhortationManager.GetExhortationsByChurchID(churchID);

            // Clear any existing items in the ListView or DropDownList
            // exhortationListView.DataSource = exhortations;
            // exhortationListView.DataBind();

            /*  // Create dummy data using a DataTable
              DataTable exhortations = new DataTable();
              exhortations.Columns.Add("ExhortationID", typeof(int));
              exhortations.Columns.Add("Title", typeof(string));
              exhortations.Columns.Add("Description", typeof(string));
              exhortations.Columns.Add("Speaker", typeof(string));
              exhortations.Columns.Add("Date", typeof(DateTime));
              exhortations.Columns.Add("AudioFilePath", typeof(string));

              // Add sample rows
              exhortations.Rows.Add(1, "The Power of Faith", "A powerful talk about faith.", "John Doe", DateTime.Now, "https://example.com/audio1.mp3");
              exhortations.Rows.Add(2, "Living with Purpose", "Insights on purposeful living.", "Jane Smith", DateTime.Now.AddDays(-1), "https://example.com/audio2.mp3");

              // Bind the data to the Repeater control
              exhortationListView.DataSource = exhortations;
              exhortationsListView.DataBind();*/
        }




    }
}

/*
 
    This is the Javascript code to get 'hypothetically' this pge's play audio component working    

< script >
    let isPlaying = false;

function togglePlayPause()
{
    const playButton = document.querySelector('.play-button');
    const audio = document.querySelector('audio');

    if (isPlaying)
    {
        audio.pause();
        playButton.textContent = '▶';
    }
    else
    {
        audio.play();
        playButton.textContent = '❚❚';
    }

    isPlaying = !isPlaying;
}

document.addEventListener('DOMContentLoaded', function() {
    const progressBar = document.querySelector('.progress');
    const currentTimeElement = document.querySelector('.current-time');
    const totalTimeElement = document.querySelector('.total-time');

    // Example values, replace with actual audio data
    const totalDuration = 1239; // Total duration in seconds (20:39)
    const currentTime = 611; // Current time in seconds (10:11)

    // Update progress bar width
    const progressPercentage = (currentTime / totalDuration) * 100;
    progressBar.style.width = progressPercentage + '%';

    // Update timestamps
    currentTimeElement.textContent = formatTime(currentTime);
    totalTimeElement.textContent = formatTime(totalDuration);

    function formatTime(seconds)
    {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;
        return `${ minutes}:${ remainingSeconds.toString().padStart(2, '0')}`;
}
    });
</ script >

*/
