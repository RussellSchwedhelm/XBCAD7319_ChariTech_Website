using System;
using System.Collections.Generic;
using System.Data;
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

        // Temporary list for prayer requests
        private List<string> temp;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to login if user is not authenticated
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                PopulateNames();
                LoadDonations();
                LoadNewsletters();
                LoadExhortations();
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

        // Populate names for the prayer requests section with a temporary list
        private void PopulateNames()
        {
            temp = new List<string>
            {
                "Emily Johnson",
                "Michael Smith",
                "Olivia Brown",
                "Benjamin Davis",
                "Sophia Martinez",
                "Jacob Wilson",
                "Isabella Thompson",
                "Ethan Garcia"
            };

            PrayerRequestsRepeater.DataSource = temp;
            PrayerRequestsRepeater.DataBind();
        }
    }
}
