<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.UserSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="~/Content/Site.css" />

    <!-- Settings Layout -->
    <div class="settings-page">
        <div class="settings-container" style="width: 70vw; border-color: transparent;">
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

                <asp:Label ID="Label1" runat="server" Text="Bible Basics"></asp:Label>
                <div class="setting-option" style="display: flex; align-items: center;">
                    <small>Switch on/off Bible Basic Notifications</small>
                    <asp:HiddenField ID="chkBibleBasicsHidden" runat="server" />
                    <label class="switch" style="margin-left:auto">
                        <input type="checkbox" id="chkBibleBasicsCustom" runat="server" />
                        <span class="slider"></span>
                    </label>
                </div>

                <asp:Label ID="lblResponsibilityUpdates" runat="server" Text="Responsibility Updates"></asp:Label>
                <div class="setting-option" style="display: flex; align-items: center;">
                    <small>Switch on/off Responsibility Update Notifications</small>
                    <asp:HiddenField ID="chkResponsibilityHidden" runat="server" />
                    <label class="switch" style="margin-left:auto">
                        <input type="checkbox" id="chkResponsibiltyUpdatesCustom" runat="server" />
                        <span class="slider"></span>
                    </label>
                </div>

              <asp:Label ID="lblEmailUpdates" runat="server" Text="Email Updates"></asp:Label>
            <div class="setting-option" style="display: flex; align-items: center;">
                <small>Switch on/off Email Update Notifications</small>
                <asp:HiddenField ID="chkEmailNotification" runat="server" />
                <label class="switch" style="margin-left:auto">
                    <input type="checkbox" id="chkEmailUpdates" runat="server" />
                    <span class="slider"></span>
                </label>
            </div>

                
                <div style="text-align: center; margin-top: 20px;">
                    <asp:Button ID="btnSaveSettings" runat="server" Text="Save Settings" CssClass="btn btn-success" OnClick="btnSaveSettings_Click" />
                </div>
            </div>
            <!-- ----------------------------------------------------------------------------- -->

            <!-- Right Account Panel -->
            <div class="settings-right">
                <div class="account-info">
                    <img src="~/Images/ProfilePic.png" alt="Profile Picture" class="profile-pic" />
                    <h3>My Account</h3>
                    <div class="account-fields">
                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" style="padding:10px;"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" style="background-color: transparent; border: none; color: #BBBBBD; padding: 5px; border-bottom: 1px solid #BBBBBD;"></asp:TextBox>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblName" runat="server" Text="Name" style="padding:10px;"></asp:Label>
                            <asp:TextBox ID="txtName" runat="server" ReadOnly="true" style="background-color: transparent; border: none; color: #BBBBBD; padding: 5px; border-bottom: 1px solid #BBBBBD;"></asp:TextBox>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblSurname" runat="server" Text="Surname" style="padding:10px;"></asp:Label>
                            <asp:TextBox ID="txtSurname" runat="server" ReadOnly="true" style="background-color: transparent; border: none; color: #BBBBBD; padding: 5px; border-bottom: 1px solid #BBBBBD;"></asp:TextBox>
                        </div>


                        <div style="display: flex; align-items: center;">
                            <asp:Label ID="lblEcclesia" runat="server" Text="Ecclesia" style="padding:10px;"></asp:Label>
                            <asp:DropDownList ID="ddlEcclesia" runat="server" style="background-color: transparent; border: none; color: #BBBBBD; padding: 5px; border-bottom: 1px solid #BBBBBD;">
                                <asp:ListItem Value="Cape Town">Cape Town</asp:ListItem>
                                <asp:ListItem Value="Johannesburg">Johannesburg</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn btn-primary" />
                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnUpdateEmail" runat="server" Text="Update Your Email" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnDeleteAccount" runat="server" Text="Delete Your Account" CssClass="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
