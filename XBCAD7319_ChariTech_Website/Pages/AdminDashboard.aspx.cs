using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class AdminDashboard : Page
    {
        // Instance of NextSundayManager to handle Sunday-related data
        private NextSundayManager nextSundayManager = new NextSundayManager();
        // Readonly instance of PrayerRequestManager to manage prayer requests
        private readonly PrayerRequestManager prayerRequestManager = new PrayerRequestManager();

        // Called when the page loads; executes only on the first load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Ensures actions are taken only on the first load, not on postbacks
            {
                // Check if the user is logged in and has the proper role (UserRoleID == 2)
                if (Session["UserEmail"] == null || (int)Session["UserRoleID"] != 2)
                {
                    Response.Redirect("Login.aspx"); // Redirects unauthorized users to login
                }
                LoadDonations(); // Loads donation data into page fields
                TodayDateLabel.Text = DateTime.Now.ToString("MM-dd-yyyy"); // Sets today's date label
                LoadPrayerRequests(); // Loads all prayer requests for display

                // Determine the next upcoming Sunday based on today's date
                DateTime today = DateTime.Today;
                DateTime nextSunday = today.DayOfWeek == DayOfWeek.Sunday
                    ? today.AddDays(7) // If today is Sunday, the next Sunday is 7 days away
                    : today.AddDays(7 - (int)today.DayOfWeek); // Otherwise, calculates the nearest upcoming Sunday

                // Loads data associated with the next Sunday into page fields
                LoadNextSundayInfo(nextSunday);
            }
        }

        // Event handler triggered when uploading a newsletter
        protected void UploadNewsletterButton_Click(object sender, EventArgs e)
        {
            // Retrieve the user’s email from the session
            string email = Session["UserEmail"].ToString();
            ExhortationManager exhortationManager = new ExhortationManager();
            int churchId = exhortationManager.GetChurchIdByEmail(email); // Get ChurchID associated with email

            if (churchId != -1) // Check if ChurchID retrieval was successful
            {
                string title = NewsletterTitle.Text; // Get title input
                DateTime date;

                // Validate date format and ensure a file has been uploaded
                if (DateTime.TryParse(NewsletterDate.Text, out date) && NewsletterFileUpload.HasFile)
                {
                    HttpPostedFile newsletterFile = NewsletterFileUpload.PostedFile;

                    // Check if the file is not null and has a valid length
                    if (newsletterFile != null && newsletterFile.ContentLength > 0)
                    {
                        NewsletterManager newsletterManager = new NewsletterManager();
                        bool uploadSuccess = newsletterManager.UploadNewsletter(email, churchId, title, date, newsletterFile);

                        if (uploadSuccess) // If upload succeeds
                        {
                            // Clear input fields
                            NewsletterTitle.Text = string.Empty;
                            NewsletterDate.Text = string.Empty;
                            NewsletterFileUpload.Attributes.Clear(); // Clears file input control
                            Response.Write("<script>alert('Newsletter uploaded successfully!');</script>");
                        }
                        else // If upload fails
                        {
                            Response.Write("<script>alert('Failed to upload the newsletter.');</script>");
                        }
                    }
                }
                else // Alert user if fields are incomplete or file is missing
                {
                    Response.Write("<script>alert('Please fill all fields and upload a valid file.');</script>");
                }
            }
            else // Alert if Church ID could not be retrieved
            {
                Response.Write("<script>alert('Unable to retrieve Church ID.');</script>");
            }
        }

        // Event handler triggered when uploading an exhortation
        protected void UploadExhortationButton_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            ExhortationManager exhortationManager = new ExhortationManager();
            int churchId = exhortationManager.GetChurchIdByEmail(email);

            if (churchId != -1)
            {
                string title = ExhortationTitle.Text; // Get title
                string speaker = ExhortationSpeaker.Text; // Get speaker name
                DateTime date;

                // Check if date format is valid and file is uploaded
                if (DateTime.TryParse(ExhortationDate.Text, out date) && ExhortationFileUpload.HasFile)
                {
                    HttpPostedFile exhortationFile = ExhortationFileUpload.PostedFile;

                    // Ensure file is valid, has content, and is in MP3 format
                    if (exhortationFile != null && exhortationFile.ContentLength > 0 && exhortationFile.FileName.EndsWith(".mp3"))
                    {
                        bool uploadSuccess = exhortationManager.UploadExhortation(email, churchId, title, speaker, date, exhortationFile);

                        if (uploadSuccess) // If upload is successful, clear fields and notify user
                        {
                            ExhortationTitle.Text = string.Empty;
                            ExhortationSpeaker.Text = string.Empty;
                            ExhortationDate.Text = string.Empty;
                            ExhortationFileUpload.Attributes.Clear(); // Clears file input control
                            Response.Write("<script>alert('Exhortation uploaded successfully!');</script>");
                        }
                        else // Display error message on upload failure
                        {
                            Response.Write("<script>alert('Failed to upload the exhortation.');</script>");
                        }
                    }
                    else // Alert if file is invalid or in the wrong format
                    {
                        Response.Write("<script>alert('Please upload a valid MP3 file.');</script>");
                    }
                }
                else // Prompt user to complete all fields
                {
                    Response.Write("<script>alert('Please fill all fields.');</script>");
                }
            }
            else // Notify if Church ID is not found
            {
                Response.Write("<script>alert('Unable to retrieve Church ID.');</script>");
            }
        }

        // Loads donation data and dynamically populates donation-related fields
        private void LoadDonations()
        {
            int churchId = new ExhortationManager().GetChurchIdByEmail(Session["UserEmail"].ToString());
            DonationManager donationManager = new DonationManager();
            DataTable donations = donationManager.GetDonationCampaigns(churchId);

            // Reset fields
            Drive1Name.Text = string.Empty;
            Drive1Amount.Text = string.Empty;
            Drive1Goal.Text = string.Empty;
            Drive2Name.Text = string.Empty;
            Drive2Amount.Text = string.Empty;
            Drive2Goal.Text = string.Empty;
            int cnt = 1;

            // Populate fields dynamically with donation data
            foreach (DataRow row in donations.Rows)
            {
                string title = row["Title"].ToString();
                decimal donatedAmount = row.Field<decimal>("DonatedAmount");
                decimal goalAmount = row.Field<decimal>("DonationGoal");

                switch (cnt)
                {
                    case 1:
                        cnt++;
                        BlueBagCause.Text = title;
                        break;
                    case 2:
                        cnt++;
                        RedBagCause.Text = title;
                        break;
                    case 3:
                        cnt++;
                        Drive1Name.Text = title;
                        Drive1Amount.Text = donatedAmount.ToString("0.##");
                        Drive1Goal.Text = goalAmount.ToString("0.##");
                        break;
                    case 4:
                        cnt++;
                        Drive2Name.Text = title;
                        Drive2Amount.Text = donatedAmount.ToString("0.##");
                        Drive2Goal.Text = goalAmount.ToString("0.##");
                        break;
                    default:
                        break;
                }
            }
        }

        // Event handler for the Publish Changes button
        protected void PublishButton_Click(object sender, EventArgs e)
        {
            // Define DataTable to hold updated donation data
            DataTable updatedDonationData = new DataTable();
            updatedDonationData.Columns.Add("Title", typeof(string));
            updatedDonationData.Columns.Add("DonatedAmount", typeof(decimal));
            updatedDonationData.Columns.Add("DonationGoal", typeof(decimal));

            // Add donation rows based on current input
            AddDonationRow(updatedDonationData, BlueBagCause.Text, "", "");
            AddDonationRow(updatedDonationData, RedBagCause.Text, "", "");
            AddDonationRow(updatedDonationData, Drive1Name.Text, Drive1Amount.Text, Drive1Goal.Text);
            AddDonationRow(updatedDonationData, Drive2Name.Text, Drive2Amount.Text, Drive2Goal.Text);

            int churchId = new ExhortationManager().GetChurchIdByEmail(Session["UserEmail"].ToString());
            DonationManager donationManager = new DonationManager();
            bool success = donationManager.ReplaceDonationCampaigns(churchId, updatedDonationData);

            // Notify user of the result and reload donations
            if (success)
            {
                Response.Write("<script>alert('Donations updated successfully!');</script>");
            }
            else
            {
                Response.Write("<script>alert('Failed to update donations.');</script>");
            }

            LoadDonations();
        }

        // Helper function to add a donation row to the DataTable
        private void AddDonationRow(DataTable table, string title, string currentAmountText, string goalAmountText)
        {
            decimal currentAmount = 0;
            decimal goalAmount = 0;

            // Parse current and goal amounts from input
            if (!string.IsNullOrWhiteSpace(currentAmountText))
                decimal.TryParse(currentAmountText, out currentAmount);

            if (!string.IsNullOrWhiteSpace(goalAmountText))
                decimal.TryParse(goalAmountText, out goalAmount);

            // Add new row to the table with donation data
            DataRow row = table.NewRow();
            row["Title"] = title;
            row["DonatedAmount"] = currentAmount;
            row["DonationGoal"] = goalAmount;
            table.Rows.Add(row);
        }

        // Event handler for the Cancel button to reload donation data
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            LoadDonations();
        }

        // Event handler for publishing a notification
        protected void PublishNotificationButton_Click(object sender, EventArgs e)
        {
            string title = NotificationTitle.Text;
            string message = NotificationMessage.Text;
            DateTime sentAt;

            // Validate date and ensure required fields are populated
            if (DateTime.TryParse(NotificationDate.Text, out sentAt) && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(message))
            {
                int churchId = new ExhortationManager().GetChurchIdByEmail(Session["UserEmail"].ToString());

                NotificationManager notificationManager = new NotificationManager();
                bool success = notificationManager.SaveNotification(title, message, sentAt, churchId);

                if (success) // Clear fields and notify success
                {
                    NotificationTitle.Text = string.Empty;
                    NotificationMessage.Text = string.Empty;
                    NotificationDate.Text = string.Empty;
                    Response.Write("<script>alert('Notification posted successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Failed to post notification.');</script>");
                }
            }
            else // Alert user to complete all fields
            {
                Response.Write("<script>alert('Please fill all fields correctly.');</script>");
            }
        }

        // Event handler for the Cancel Notification button to clear input fields
        protected void CancelNotification_Click(object sender, EventArgs e)
        {
            NotificationTitle.Text = string.Empty;
            NotificationMessage.Text = string.Empty;
            NotificationDate.Text = string.Empty;
        }

        // Event handler for saving Next Sunday info
        protected void SaveSundayInfoButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate;

            // Try parsing selected date and save it if valid
            if (DateTime.TryParse(NextSundayDate.Text, out selectedDate))
            {
                // Save Next Sunday information with presiding, exhortation, and on-the-door info
                nextSundayManager.SaveNextSundayInfo(
                    churchId: new ExhortationManager().GetChurchIdByEmail(Session["UserEmail"].ToString()),
                    nextSundayDate: selectedDate,
                    presiding: PresidingName.Text,
                    exhortation: ExhortationName.Text,
                    onTheDoor: OnTheDoorName.Text
                );

                // Notify user of successful save
                Response.Write("<script>alert('Sunday information saved successfully.');</script>");
            }
            else
            {
                Response.Write("<script>alert('Please select a valid Sunday date.');</script>");
            }
        }

        // Event handler to view future Sundays schedule
        protected void ViewScheduleButton_Click(object sender, EventArgs e)
        {
            // Retrieve list of future Sundays with associated data
            List<(DateTime Date, string Presiding, string Exhortation, string OnTheDoor)> futureSundays = nextSundayManager.GetFutureSundays();

            // MemoryStream for creating the PDF file
            using (MemoryStream ms = new MemoryStream())
            {
                // Initialize PDF writer, document, and add title
                using (PdfWriter writer = new PdfWriter(ms))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document document = new Document(pdf))
                {
                    document.Add(new Paragraph("Future Sundays Schedule")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(16)
                        .SetBold());

                    // Define table structure and header cells
                    iText.Layout.Element.Table table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2 })).UseAllAvailableWidth();

                    // Define and add header cells to the table
                    table.AddCell(new Cell().Add(new Paragraph("Date")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold());
                    table.AddCell(new Cell().Add(new Paragraph("Presiding")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold());
                    table.AddCell(new Cell().Add(new Paragraph("Exhortation")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold());
                    table.AddCell(new Cell().Add(new Paragraph("On The Door")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold());

                    // Add data rows for each future Sunday
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

                // Send the created PDF to the client
                byte[] pdfBytes = ms.ToArray();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=Future_Sundays_Schedule.pdf");
                Response.BinaryWrite(pdfBytes);
                Response.Flush();
                Response.End();
            }
        }

        // WebMethod to get information for a specific Sunday based on the selected date
        [WebMethod]
        public static SundayInfo GetSundayInfo(string selectedDate)
        {
            DateTime date;
            if (DateTime.TryParse(selectedDate, out date))
            {
                var manager = new NextSundayManager();
                var sundayInfo = manager.GetSundayInfoByDate(date);
                return sundayInfo ?? new SundayInfo(); // Return empty if no data found
            }
            return new SundayInfo();
        }

        // Method to load data for the selected Sunday into page fields
        private void LoadNextSundayInfo(DateTime selectedDate)
        {
            var sundayInfo = nextSundayManager.GetSundayInfoByDate(selectedDate);
            PresidingName.Text = sundayInfo?.Presiding ?? "";
            ExhortationName.Text = sundayInfo?.Exhortation ?? "";
            OnTheDoorName.Text = sundayInfo?.OnTheDoor ?? "";
        }

        // Load all prayer requests into repeater for display
        private void LoadPrayerRequests()
        {
            var prayerRequests = prayerRequestManager.GetAllPrayerRequests();
            PrayerRequestsRepeater.DataSource = prayerRequests;
            PrayerRequestsRepeater.DataBind();
        }

        // Event handler to save prayer request changes for approval status
        protected void SavePrayerRequestChangesButton_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in PrayerRequestsRepeater.Items)
            {
                var approvalCheckBox = (CheckBox)item.FindControl("ApprovalCheckBox");
                var prayerRequestIdHiddenField = (HiddenField)item.FindControl("PrayerRequestId");

                // Retrieve the RequestID and the Approval status
                int prayerRequestId = int.Parse(prayerRequestIdHiddenField.Value);
                bool isApproved = approvalCheckBox.Checked;

                // Update approval status in the database
                prayerRequestManager.UpdatePrayerRequestApprovalStatus(prayerRequestId, isApproved);
            }

            // Reload updated prayer requests
            LoadPrayerRequests();
        }
    }

    // Simple data structure to hold Sunday information
    public class SundayInfo
    {
        public string Presiding { get; set; }
        public string Exhortation { get; set; }
        public string OnTheDoor { get; set; }
    }
}
