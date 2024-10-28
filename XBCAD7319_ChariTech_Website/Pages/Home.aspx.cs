using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        // Managers for different sections
        private readonly DonationManager donationManager = new DonationManager();
        private readonly NewsletterManager newsletterManager = new NewsletterManager();
        private readonly ExhortationManager exhortationManager = new ExhortationManager();
        private readonly NextSundayManager nextSundayManager = new NextSundayManager();
        private readonly PrayerRequestManager prayerRequestManager = new PrayerRequestManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to login if user is not authenticated
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadDonations();
                LoadNewsletters();
                LoadExhortations();
                LoadNextSundayDetails();
                LoadPrayerRequests();

                TodayDateLabel.Text = DateTime.Now.ToString("MM-dd-yyyy");
            }
        }

        // Load donation campaigns from the database and dynamically set display values
        private void LoadDonations()
        {
            int churchId = exhortationManager.GetChurchIdByEmail(Session["UserEmail"].ToString());
            DataTable donations = donationManager.GetDonationCampaigns(churchId);

            // Reset fields
            BlueBagLabel.Text = "Blue Bag - General";
            RedBagLabel.Text = "Red Bag - Welfare";
            Drive1Label.Text = string.Empty;
            Drive2Label.Text = string.Empty;
            int cnt = 1;

            // Dynamically assign values based on titles from the database
            foreach (DataRow row in donations.Rows)
            {
                string title = row["Title"].ToString();
                decimal donatedAmount = row.Field<decimal>("DonatedAmount");
                decimal goalAmount = row.Field<decimal>("DonationGoal");

                switch (cnt)
                {
                    case 1:
                        cnt++;
                        BlueBagLabel.Text = "Blue Bag -" + title;
                        break;
                    case 2:
                        cnt++;
                        RedBagLabel.Text = "Red Bag - " + title;
                        break;
                    case 3:
                        cnt++;
                        Drive1Label.Text = title;
                        SetProgress("Drive1ProgressBar", donatedAmount, goalAmount);
                        break;
                    case 4:
                        cnt++;
                        Drive2Label.Text = title;
                        SetProgress("Drive2ProgressBar", donatedAmount, goalAmount);
                        break;
                    default:
                        break;
                }
            }
        }

        // Set progress bar width based on donation and goal amount
        private void SetProgress(string progressBarId, decimal donatedAmount, decimal goalAmount)
        {
            int progress = goalAmount > 0 ? (int)((donatedAmount / goalAmount) * 100) : 0;
            string script = $"document.getElementById('{progressBarId}').style.width='{progress}%';";
            ClientScript.RegisterStartupScript(this.GetType(), progressBarId, script, true);
        }

        // Load newsletters from the database and bind to the repeater
        private void LoadNewsletters()
        {
            DataTable newsletters = newsletterManager.GetNewsletters();
            if (newsletters.Rows.Count > 0)
            {
                newsListRepeater.DataSource = newsletters;
                newsListRepeater.DataBind();
            }
        }

        // Load exhortations by church ID from the database and bind to the repeater
        private void LoadExhortations()
        {
            int churchId = exhortationManager.GetChurchIdByEmail(Session["UserEmail"].ToString());
            DataTable exhortations = exhortationManager.GetExhortationsByChurchID(churchId);

            if (exhortations.Rows.Count > 0)
            {
                ExhortationListRepeater.DataSource = exhortations;
                ExhortationListRepeater.DataBind();
            }
        }

        // Handle the download of a newsletter PDF when clicked
        protected void DownloadNewsletter(object sender, EventArgs e)
        {
            int newsletterId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            byte[] pdfContent = newsletterManager.GetNewsletterPdf(newsletterId);

            if (pdfContent != null)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename=newsletter_{newsletterId}.pdf");
                Response.OutputStream.Write(pdfContent, 0, pdfContent.Length);
                Response.Flush();
                Response.End();
            }
        }
        // Load details for the upcoming Sunday or current Sunday if today is Sunday
        private void LoadNextSundayDetails()
        {
            DateTime today = DateTime.Today;
            DateTime nextSunday = today.DayOfWeek == DayOfWeek.Sunday
                ? today.AddDays(7) // If today is Sunday, get the next Sunday (7 days later)
                : today.AddDays(7 - (int)today.DayOfWeek); // Otherwise, calculate the upcoming Sunday


            var sundayInfo = nextSundayManager.GetSundayInfoByDate(nextSunday);
            if (sundayInfo != null)
            {
                PresidingLabel.Text = sundayInfo.Presiding ?? "TBD";
                ExhortationLabel.Text = sundayInfo.Exhortation ?? "TBD";
                OnTheDoorLabel.Text = sundayInfo.OnTheDoor ?? "TBD";
                nextSundayTitle.Text = $"Next Sunday - {nextSunday:yyyy-MM-dd}";
            }
            else
            {
                // Clear fields if no data is available for the upcoming Sunday
                PresidingLabel.Text = "TBD";
                ExhortationLabel.Text = "TBD";
                OnTheDoorLabel.Text = "TBD";
                nextSundayTitle.Text = $"Next Sunday - {nextSunday:yyyy-MM-dd}";
            }
        }

        // Handle the click event for View Schedule button to generate PDF
        protected void ViewScheduleButton_Click(object sender, EventArgs e)
        {
            // Get future Sundays with data
            List<(DateTime Date, string Presiding, string Exhortation, string OnTheDoor)> futureSundays = nextSundayManager.GetFutureSundays();

            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(ms))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document document = new Document(pdf))
                {
                    // Add title to the PDF
                    document.Add(new Paragraph("Future Sundays Schedule")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(16)
                        .SetBold());

                    // Create a table with headers
                    iText.Layout.Element.Table table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2 })).UseAllAvailableWidth();

                    // Define header cells and style
                    Cell headerCell1 = new Cell().Add(new Paragraph("Date")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetBold();
                    Cell headerCell2 = new Cell().Add(new Paragraph("Presiding")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetBold();
                    Cell headerCell3 = new Cell().Add(new Paragraph("Exhortation")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetBold();
                    Cell headerCell4 = new Cell().Add(new Paragraph("On The Door")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetBold();

                    // Add headers to the table
                    table.AddCell(headerCell1);
                    table.AddCell(headerCell2);
                    table.AddCell(headerCell3);
                    table.AddCell(headerCell4);

                    // Add each future Sunday’s data to the table
                    foreach (var sunday in futureSundays)
                    {
                        table.AddCell(sunday.Date.ToString("yyyy-MM-dd"));
                        table.AddCell(sunday.Presiding);
                        table.AddCell(sunday.Exhortation);
                        table.AddCell(sunday.OnTheDoor);
                    }

                    // Add table to document
                    document.Add(table);
                }

                // Send the PDF to the client
                byte[] pdfBytes = ms.ToArray();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=Future_Sundays_Schedule.pdf");
                Response.BinaryWrite(pdfBytes);
                Response.Flush();
                Response.End();
            }
        }

        // Method to load prayer requests using PrayerRequestManager
        private void LoadPrayerRequests()
        {
            List<PrayerRequest> prayerRequests = prayerRequestManager.GetApprovedPrayerRequests();
            PrayerRequestsRepeater.DataSource = prayerRequests;
            PrayerRequestsRepeater.DataBind();
        }

        // Method to handle submission of a new prayer request
        protected void SubmitPrayerRequestButton_Click(object sender, EventArgs e)
        {
            UserManager userManager = new UserManager();
            int userId = userManager.GetUserIdByEmail(Session["UserEmail"].ToString());
            string prayerTarget = PrayerTargetTextBox.Text;

            // Submit the prayer request using PrayerRequestManager
            prayerRequestManager.SubmitPrayerRequest(userId, prayerTarget);

            // Refresh the prayer request list
            LoadPrayerRequests();
        }

        // Web method to fetch exhortation audio in memory
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static byte[] GetExhortationAudio(int exhortationId)
        {
            ExhortationManager exhortationManager = new ExhortationManager();
            byte[] audioData = exhortationManager.GetExhortationAudio(exhortationId);
            return audioData;
        }

        protected void btnExhortationSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtExhortationSearch.Text.Trim();
            int churchId = exhortationManager.GetChurchIdByEmail(Session["UserEmail"].ToString());

            // If search query is empty, load default exhortations
            if (string.IsNullOrEmpty(searchQuery))
            {
                LoadExhortations();
            }
            else
            {
                // Perform search with prioritization on title, then summary
                DataTable searchResults = exhortationManager.SearchExhortations(churchId, searchQuery);
                ExhortationListRepeater.DataSource = searchResults;
                ExhortationListRepeater.DataBind();
            }
        }

        protected void btnNewsSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtNewsSearch.Text.Trim();
            int churchId = newsletterManager.GetUserIdByEmail(Session["UserEmail"].ToString());

            // If search query is empty, load the default list of newsletters
            if (string.IsNullOrEmpty(searchQuery))
            {
                LoadNewsletters();
            }
            else
            {
                // Perform search using the title filter
                DataTable searchResults = newsletterManager.SearchNewsletters(churchId, searchQuery);
                newsListRepeater.DataSource = searchResults;
                newsListRepeater.DataBind();
            }
        }

        protected void PlayExhortation_Click(object sender, EventArgs e)
        {
            Button playButton = (Button)sender;
            int exhortationId = int.Parse(playButton.CommandArgument);

            // Redirect to Exhortations.aspx with autoplay=true
            Response.Redirect($"Exhortations.aspx?exhortationId={exhortationId}&autoplay=true");
        }

    }
}