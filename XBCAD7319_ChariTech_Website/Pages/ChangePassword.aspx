<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Page Layout */
        .change-password-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 80vh;
            padding: 20px;
        }

        /* Card Style */
        .change-password-card {
            width: 100%;
            max-width: 400px;
            padding: 2rem;
            background-color: #ffffff;
            border-radius: 15px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        /* Title */
        .change-password-card h2 {
            font-size: 1.8rem;
            color: #333;
            margin-bottom: 1.5rem;
        }

        /* Input Fields */
        .change-password-card input[type="password"] {
            width: 100%;
            padding: 0.8rem;
            margin-bottom: 1rem;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 1rem;
        }

        /* Error Message */
        .error-message {
            color: red;
            font-size: 0.9rem;
            margin-top: -0.5rem;
            margin-bottom: 1rem;
            display: none; /* Hide initially */
        }

        /* Button */
        .change-password-card button {
            background-color: #333;
            color: white;
            padding: 0.8rem;
            width: 100%;
            border: none;
            border-radius: 8px;
            font-size: 1rem;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .change-password-card button:hover {
            background-color: #555;
        }
    </style>

    <!-- Change Password Layout -->
    <div class="change-password-container">
        <div class="change-password-card">
            <h2>Change Password</h2>

            <!-- New Password Field -->
            <asp:Label ID="lblNewPassword" runat="server" Text="New Password" AssociatedControlID="txtNewPassword" />
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="input-password" />

            <!-- Confirm Password Field -->
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" AssociatedControlID="txtConfirmPassword" />
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="input-password" />

            <!-- Error Message -->
            <span id="passwordError" runat="server" class="error-message">Passwords don't match</span>


            <!-- Submit Button -->
            <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" 
            OnClick="btnChangePassword_Click" 
            CssClass="btn-change-password" />


        </div>
    </div>

    <!-- JavaScript for Client-Side Validation -->
    <script>
        document.getElementById('<%= btnChangePassword.ClientID %>').addEventListener('click', function(event) {
            // Prevent form submission to validate client-side
            event.preventDefault();

            // Get password fields and error message element
            var newPassword = document.getElementById('<%= txtNewPassword.ClientID %>');
            var confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>');
            var errorMessage = document.getElementById('passwordError');

            // Check if passwords match
            if (newPassword.value !== confirmPassword.value) {
                errorMessage.style.display = 'block'; // Show error message
            } else {
                errorMessage.style.display = 'none'; // Hide error message
                // Submit form programmatically if passwords match
                document.getElementById('<%= btnChangePassword.ClientID %>').removeEventListener('click', arguments.callee);
                document.forms[0].submit();
            }
        });
    </script>
</asp:Content>

