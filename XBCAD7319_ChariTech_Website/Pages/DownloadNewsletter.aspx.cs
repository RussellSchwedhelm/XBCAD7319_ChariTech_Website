using System;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class DownloadNewsletter : System.Web.UI.Page
    {
        private NewsletterManager newsletterManager = new NewsletterManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int newsletterId;
                if (int.TryParse(Request.QueryString["id"], out newsletterId))
                {
                    // Retrieve the PDF content from the database
                    byte[] pdfContent = newsletterManager.GetNewsletterPdf(newsletterId);

                    if (pdfContent != null)
                    {
                        // Set response to return the PDF
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", $"inline; filename=newsletter_{newsletterId}.pdf");
                        Response.OutputStream.Write(pdfContent, 0, pdfContent.Length);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }
}
