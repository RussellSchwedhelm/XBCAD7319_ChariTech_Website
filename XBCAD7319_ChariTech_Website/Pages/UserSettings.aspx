    <%@ Page Title="Settings" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.UserSettings" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <link rel="stylesheet" type="text/css" href="~/Content/Site.css" />

        <!-- Settings Layout -->
        <div class="settings-page">
            <div class="settings-container" style="width: 60vw; border-color: transparent;">
                <!-- Left Settings Panel -->
                <div class="settings-left">
                    <div style="text-align: center;">
                        <h3> Settings </h3>
                    </div>

                    <div class="setting-option" style="display: flex; align-items: center;">
                        <asp:Label ID="lblDarkModeDesc" runat="server" Text="Turn on/off dark mode"></asp:Label>
                        <!-- Hidden input to store the value for the ASP.NET checkbox functionality -->
                        <asp:HiddenField ID="chkDarkModeHidden" runat="server" />

                        <!-- Custom switch design -->
                        <label class="switch" style="margin-left:auto">
                            <input type="checkbox" id="chkDarkModeCustom" runat="server" />
                            <span class="slider"></span>
                        </label>
                    </div>

                    <div style="text-align: center;">
                        <h3>Notifications</h3>
                    </div>

                    <!-- Bible Basics -->
                    <asp:Label ID="Label1" runat="server" Text="Bible Basics" style="color: #888;"></asp:Label>
                    <div class="setting-option" style="display: flex; align-items: center;">
                        <small style="color: #888;">Switch on/off Bible Basic Notifications</small>
                        <asp:HiddenField ID="chkBibleBasicsHidden" runat="server" />
                        <label class="switch" style="margin-left:auto">
                            <input type="checkbox" id="chkBibleBasicsCustom" runat="server" disabled />
                            <span class="slider" style="background-color: #ccc;"></span>
                        </label>
                    </div>

                    <!-- Responsibility Updates -->
                    <asp:Label ID="lblResponsibilityUpdates" runat="server" Text="Responsibility Updates" style="color: #888;"></asp:Label>
                    <div class="setting-option" style="display: flex; align-items: center;">
                        <small style="color: #888;">Switch on/off Responsibility Update Notifications</small>
                        <asp:HiddenField ID="chkResponsibilityHidden" runat="server" />
                        <label class="switch" style="margin-left:auto">
                            <input type="checkbox" id="chkResponsibiltyUpdatesCustom" runat="server" disabled />
                            <span class="slider" style="background-color: #ccc;"></span>
                        </label>
                    </div>

                    <!-- Email Updates -->
                    <asp:Label ID="lblEmailUpdates" runat="server" Text="Email Updates" style="color: #888;"></asp:Label>
                    <div class="setting-option" style="display: flex; align-items: center;">
                        <small style="color: #888;">Switch on/off Email Update Notifications</small>
                        <asp:HiddenField ID="chkEmailNotification" runat="server" />
                        <label class="switch" style="margin-left:auto">
                            <input type="checkbox" id="chkEmailUpdates" runat="server" disabled />
                            <span class="slider" style="background-color: #ccc;"></span>
                        </label>
                    </div>
            
                    <div style="text-align: center; margin-top: 20px; margin-left: 15px;">
                        <asp:Button ID="btnSaveSettings" runat="server" Text="Save Settings" CssClass="btn btn-secondary" OnClick="btnSaveSettings_Click" />
                    </div>
                </div>
                <!-- ----------------------------------------------------------------------------- -->

                <!-- Right Account Panel -->
                <div class="settings-right">
                    <div class="account-info">
                        <img id="userProfilePic" runat="server" alt="Profile Picture" class="profile-pic" src="" />
                        
                        <br/>

                        <div class="profile-picture-container" onclick="document.getElementById('profilePictureUpload').click();">
                            <div class="edit-icon">
                                <i class="fas fa-pen"></i>
                                <span>Upload Profile Picture</span>
                            </div>
                            <asp:FileUpload ID="profilePictureUpload" class="hidden-upload" runat="server" accept="image/*" OnChange="previewProfilePicture();" />
                        </div>

                        

                    <h3>My Account</h3>
                    <div class="account-fields">
                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" style="padding:10px;"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" style="background-color: transparent; border: none; color: #BBBBBD; padding: 5px; border-bottom: 1px solid #BBBBBD; width: 75%; margin-left: auto "></asp:TextBox>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblName" runat="server" Text="Name" style="padding:10px;"></asp:Label>
                            <asp:TextBox ID="txtName" runat="server" style="background-color: white; border: 1px solid #BBBBBD; color: #000; padding: 5px; width: 75%;  margin-left: auto; "></asp:TextBox>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblSurname" runat="server" Text="Surname" style="padding:10px;"></asp:Label>
                            <asp:TextBox ID="txtSurname" runat="server" style="background-color: white; border: 1px solid #BBBBBD; color: #000; padding: 5px;  width: 75%; margin-left: auto;  "></asp:TextBox>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblEcclesia" runat="server" Text="Ecclesia" style="padding:10px;"></asp:Label>
                            <asp:DropDownList ID="ddlEcclesia" runat="server" style="background-color: white; border: 1px solid #BBBBBD; color: #000; padding: 5px;  width: 75%;  margin-left: auto ; ">
                                <asp:ListItem Value="" Text="Select" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-secondary" OnClick="btnSaveChanges_Click" />
                    <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn btn-primary" OnClick="btnLogout_Click" />
                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-secondary" OnClick="btnChangePassword_Click" />
                </div>
            </div>
        </div>
        <script>
            function previewProfilePicture() {
                const fileInput = document.getElementById('profilePictureUpload');
                const profilePicture = document.getElementById('<%= userProfilePic.ClientID %>');

                if (fileInput.files && fileInput.files[0]) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        profilePicture.src = e.target.result;
                    };
                    reader.readAsDataURL(fileInput.files[0]);
                }
            }
        </script>

</asp:Content>
