using System;
using System.Data;
using XBCAD7319_ChariTech_Website.Classes;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Contact : System.Web.UI.Page
    {
        private ContactManager contactManager = new ContactManager(); // Instantiate ContactManager

        //---------------------------------------------------------------------------------------------------------------------//
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
                // User is authenticated, fetch their email and church ID
                string email = Session["UserEmail"].ToString();
                int userChurchID = contactManager.GetChurchIDByEmail(email); // Use ContactManager to get ChurchID

                // Fetch users for the same ChurchID with RoleID = 2
                DataTable users = contactManager.GetUsersByChurchID(userChurchID); // Use ContactManager to get users

                // Generate the contact cards
                GenerateContactCards(users);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
        // Method to generate contact cards dynamically
        private void GenerateContactCards(DataTable users)
        {
            foreach (DataRow row in users.Rows)
            {
                string firstName = row["FirstName"].ToString();
                string surname = row["Surname"].ToString();
                string email = row["Email"].ToString();
                string contactNumber = row["ContactNumber"].ToString();

                // Format contact number with spaces (000 000 0000)
                if (!string.IsNullOrEmpty(contactNumber) && contactNumber.Length == 10)
                {
                    contactNumber = string.Format("{0} {1} {2}", contactNumber.Substring(0, 3), contactNumber.Substring(3, 3), contactNumber.Substring(6));
                }

                byte[] profilePicBytes = row["ProfilePicture"] as byte[];  // Profile picture is stored as byte array

                // Convert profile picture to Base64 string if it exists
                string profilePicBase64 = "";
                if (profilePicBytes != null)
                {
                    profilePicBase64 = "data:image/png;base64," + Convert.ToBase64String(profilePicBytes);
                }

                // Create HTML for each contact card with profile picture on the left and info on the right
                string contactCardHtml = $@"
                    <div class='contact-card'>
                        <img src='{profilePicBase64}' alt='Profile Picture' class='profile-pic' />
                        <div class='contact-info'>
                            <h3>{firstName} {surname}</h3>
                            <p>Email: {email}</p>";

                // Only display contact number if it's not null or empty
                if (!string.IsNullOrEmpty(contactNumber))
                {
                    contactCardHtml += $"<p>Contact: {contactNumber}</p>";
                }

                contactCardHtml += "</div></div>";

                // Append the generated HTML to the section
                contactSection.InnerHtml += contactCardHtml;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//