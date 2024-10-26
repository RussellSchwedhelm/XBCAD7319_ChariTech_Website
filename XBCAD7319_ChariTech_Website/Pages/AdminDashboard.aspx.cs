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
        private NextSundayManager nextSundayManager = new NextSundayManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ensure user is logged in and has correct role
                if (Session["UserEmail"] == null || (int)Session["UserRoleID"] != 2)
                {
                    Response.Redirect("Login.aspx");
                }
                LoadDonations();

                // Calculate the upcoming Sunday
                DateTime today = DateTime.Today;
                DateTime nextSunday = today.DayOfWeek == DayOfWeek.Sunday
                    ? today.AddDays(7) // If today is Sunday, get the next Sunday (7 days later)
                    : today.AddDays(7 - (int)today.DayOfWeek); // Otherwise, calculate the upcoming Sunday

                // Load next Sunday information
                LoadNextSundayInfo(nextSunday);
            }
        }

        // Event handler for uploading newsletter
        protected void UploadNewsletterButton_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            ExhortationManager exhortationManager = new ExhortationManager();
            int churchId = exhortationManager.GetChurchIdByEmail(email); // Get ChurchID by email

            if (churchId != -1) // Ensure ChurchID was retrieved successfully
            {
                string title = NewsletterTitle.Text;
                DateTime date;

                if (DateTime.TryParse(NewsletterDate.Text, out date) && NewsletterFileUpload.HasFile)
                {
                    HttpPostedFile newsletterFile = NewsletterFileUpload.PostedFile;

                    if (newsletterFile != null && newsletterFile.ContentLength > 0)
                    {
                        NewsletterManager newsletterManager = new NewsletterManager();
                        bool uploadSuccess = newsletterManager.UploadNewsletter(email, churchId, title, date, newsletterFile);

                        if (uploadSuccess)
                        {
                            // Clear fields after successful upload
                            NewsletterTitle.Text = string.Empty;
                            NewsletterDate.Text = string.Empty;
                            NewsletterFileUpload.Attributes.Clear(); // Clears the file input

                            // Provide a clear message that the fields have been cleared
                            Response.Write("<script>alert('Newsletter uploaded successfully!');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed to upload the newsletter.');</script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please fill all fields and upload a valid file.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Unable to retrieve Church ID.');</script>");
            }
        }

        // Event handler for uploading exhortation
        protected void UploadExhortationButton_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            ExhortationManager exhortationManager = new ExhortationManager();
            int churchId = exhortationManager.GetChurchIdByEmail(email);

            if (churchId != -1)
            {
                string title = ExhortationTitle.Text;
                string speaker = ExhortationSpeaker.Text;
                DateTime date;
                if (DateTime.TryParse(ExhortationDate.Text, out date) && ExhortationFileUpload.HasFile)
                {
                    HttpPostedFile exhortationFile = ExhortationFileUpload.PostedFile;

                    if (exhortationFile != null && exhortationFile.ContentLength > 0 && exhortationFile.FileName.EndsWith(".mp3"))
                    {
                        bool uploadSuccess = exhortationManager.UploadExhortation(email, churchId, title, speaker, date, exhortationFile);

                        if (uploadSuccess)
                        {
                            // Clear fields after successful upload
                            ExhortationTitle.Text = string.Empty;
                            ExhortationSpeaker.Text = string.Empty;
                            ExhortationDate.Text = string.Empty;
                            ExhortationFileUpload.Attributes.Clear(); // Clears the file input
                            Response.Write("<script>alert('Exhortation uploaded successfully!');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed to upload the exhortation.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Please upload a valid MP3 file.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please fill all fields.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Unable to retrieve Church ID.');</script>");
            }
        }
        // Load donations from the database and dynamically display them in the appropriate fields
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

        // Handle the Publish Changes button click to update donation names and amounts
        protected void PublishButton_Click(object sender, EventArgs e)
        {
            DataTable updatedDonationData = new DataTable();
            updatedDonationData.Columns.Add("Title", typeof(string));
            updatedDonationData.Columns.Add("DonatedAmount", typeof(decimal));
            updatedDonationData.Columns.Add("DonationGoal", typeof(decimal));

            // Add rows for each donation drive with the updated names and amounts
            AddDonationRow(updatedDonationData, BlueBagCause.Text, "", "");
            AddDonationRow(updatedDonationData, RedBagCause.Text, "", "");
            AddDonationRow(updatedDonationData, Drive1Name.Text, Drive1Amount.Text, Drive1Goal.Text);
            AddDonationRow(updatedDonationData, Drive2Name.Text, Drive2Amount.Text, Drive2Goal.Text);

            int churchId = new ExhortationManager().GetChurchIdByEmail(Session["UserEmail"].ToString());
            DonationManager donationManager = new DonationManager();
            bool success = donationManager.ReplaceDonationCampaigns(churchId, updatedDonationData);

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

        // Helper method to add a donation row to the DataTable
        private void AddDonationRow(DataTable table, string title, string currentAmountText, string goalAmountText)
        {
            decimal currentAmount = 0;
            decimal goalAmount = 0;

            if (!string.IsNullOrWhiteSpace(currentAmountText))
                decimal.TryParse(currentAmountText, out currentAmount);

            if (!string.IsNullOrWhiteSpace(goalAmountText))
                decimal.TryParse(goalAmountText, out goalAmount);

            DataRow row = table.NewRow();
            row["Title"] = title;
            row["DonatedAmount"] = currentAmount;
            row["DonationGoal"] = goalAmount;
            table.Rows.Add(row);
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            LoadDonations();
        }

        protected void PublishNotificationButton_Click(object sender, EventArgs e)
        {
            string title = NotificationTitle.Text;
            string message = NotificationMessage.Text;
            DateTime sentAt;

            if (DateTime.TryParse(NotificationDate.Text, out sentAt) && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(message))
            {
                int churchId = new ExhortationManager().GetChurchIdByEmail(Session["UserEmail"].ToString());

                NotificationManager notificationManager = new NotificationManager();
                bool success = notificationManager.SaveNotification(title, message, sentAt, churchId);

                if (success)
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
            else
            {
                Response.Write("<script>alert('Please fill all fields correctly.');</script>");
            }
        }

        protected void CancelNotification_Click(object sender, EventArgs e)
        {
            NotificationTitle.Text = string.Empty;
            NotificationMessage.Text = string.Empty;
            NotificationDate.Text = string.Empty;
        }

        protected void SaveSundayInfoButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate;

            // Try parsing the selected date from the NextSundayDate TextBox
            if (DateTime.TryParse(NextSundayDate.Text, out selectedDate))
            {
                // Save the information for the calculated Sunday
                nextSundayManager.SaveNextSundayInfo(
                    churchId: new ExhortationManager().GetChurchIdByEmail(Session["UserEmail"].ToString()),
                    nextSundayDate: selectedDate,
                    presiding: PresidingName.Text,
                    exhortation: ExhortationName.Text,
                    onTheDoor: OnTheDoorName.Text
                );

                // Provide user feedback
                Response.Write("<script>alert('Sunday information saved successfully.');</script>");
            }
            else
            {
                // Handle case where the date is invalid
                Response.Write("<script>alert('Please select a valid Sunday date.');</script>");
            }
        }


        protected void ViewScheduleButton_Click(object sender, EventArgs e)
        {
            // Get future Sundays with data
            List<(DateTime Date, string Presiding, string Exhortation, string OnTheDoor)> futureSundays = nextSundayManager.GetFutureSundays();

            // Create a MemoryStream for the PDF
            using (MemoryStream ms = new MemoryStream())
            {
                // Initialize the PDF document
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
                    Cell headerCell1 = new Cell().Add(new Paragraph("Date")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold();
                    Cell headerCell2 = new Cell().Add(new Paragraph("Presiding")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold();
                    Cell headerCell3 = new Cell().Add(new Paragraph("Exhortation")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold();
                    Cell headerCell4 = new Cell().Add(new Paragraph("On The Door")).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold();

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

        [WebMethod]
        public static SundayInfo GetSundayInfo(string selectedDate)
        {
            DateTime date;
            if (DateTime.TryParse(selectedDate, out date))
            {
                var manager = new NextSundayManager();
                var sundayInfo = manager.GetSundayInfoByDate(date);
                return sundayInfo ?? new SundayInfo(); // Return empty if no data
            }
            return new SundayInfo();
        }

        // Loads data for a specific Sunday (used by the date picker and initial load)
        private void LoadNextSundayInfo(DateTime selectedDate)
        {
            var sundayInfo = nextSundayManager.GetSundayInfoByDate(selectedDate);
            PresidingName.Text = sundayInfo?.Presiding ?? "";
            ExhortationName.Text = sundayInfo?.Exhortation ?? "";
            OnTheDoorName.Text = sundayInfo?.OnTheDoor ?? "";
        }


    }
    public class SundayInfo
    {
        public string Presiding { get; set; }
        public string Exhortation { get; set; }
        public string OnTheDoor { get; set; }
    }
}
