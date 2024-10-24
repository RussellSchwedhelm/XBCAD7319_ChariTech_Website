using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private NewsletterManager newsletterManager = new NewsletterManager();
        private ExhortationManager exhortationManager = new ExhortationManager();

        // List to temporarily hold the prayer requests
        private List<string> temp;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                PopulateNames();
                LoadNewsletters(); // Load newsletters on page load
                LoadExhortations(); // Load exhortations on page load
                PrayerRequestsRepeater.DataSource = temp;
                PrayerRequestsRepeater.DataBind();
            }
        }

        private void LoadNewsletters()
        {
            DataTable newsletters = newsletterManager.GetNewsletters();
            if (newsletters.Rows.Count > 0)
            {
                newsListRepeater.DataSource = newsletters;
                newsListRepeater.DataBind();
            }
        }

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

        // Download newsletter PDF when clicked
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
        }
    }
}