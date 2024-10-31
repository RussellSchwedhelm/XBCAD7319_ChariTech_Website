<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
            <span id="passwordError" runat="server" class="error-message" style="display: none;">Passwords don't match</span>

            <!-- Submit Button -->
            <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" 
                        OnClick="btnChangePassword_Click" 
                        CssClass="btn-change-password" />

            <!-- Cancel Button -->
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                        OnClick="btnCancel_Click" 
                        CssClass="btn-cancel" />
        </div>
    </div>

    <!-- JavaScript for Client-Side Validation -->
    <script>
        document.getElementById('<%= btnChangePassword.ClientID %>').addEventListener('click', function (event) {
            // Prevent form submission to validate client-side
            event.preventDefault();

            // Get password fields and error message element
            var newPassword = document.getElementById('<%= txtNewPassword.ClientID %>');
            var confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>');
            var errorMessage = document.getElementById('<%= passwordError.ClientID %>');

            // Check if passwords match
            if (newPassword.value !== confirmPassword.value) {
                errorMessage.style.display = 'block'; // Show error message
            } else {
                errorMessage.style.display = 'none'; // Hide error message
                // Programmatically trigger the form submission if passwords match
                __doPostBack('<%= btnChangePassword.UniqueID %>', '');
            }
        });
    </script>

</asp:Content>
