<%@ Page Title="Settings" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Settings Layout -->
    <div class="settings-page">
        <div class="settings-container">
            <!-- Left Settings Panel -->
            <div class="settings-left" >
                <h3>Settings</h3>
                <div class="setting-option" style="display: flex; align-items: center;">
                    <asp:Label ID="lblDarkModeDesc" runat="server" Text="Turn on/off dark mode"></asp:Label>
                    <!-- Hidden input to store the value for the ASP.NET checkbox functionality -->
                    <asp:HiddenField ID="chkDarkModeHidden" runat="server" />

                    <!-- Custom switch design -->
                    <label class="switch" style="margin-left:auto">
                        <input type="checkbox" id="chkDarkModeCustom" />
                        <span class="slider"></span>
                    </label>


                </div>

                <div class="setting-option">
                    <asp:Label ID="lblVolume" runat="server" Text="Volume"></asp:Label>
                    <br />
                    <input type="range" id="volume" name="volume" min="0" max="100" runat="server" />
                    <small>Volume Intensity</small>
                </div>
                <asp:Label ID="lblButtonClickSound" runat="server" Text="Button Click Sound"></asp:Label>
                <div class="setting-option" style="display: flex; align-items: center;">
                    
                    <small>Make Clicking Sounds When Buttons Are Clicked</small>
                    <!--<asp:CheckBox ID="chkButtonClickSound" Width="40" runat="server" />-->

                    <!-- Hidden input to store the value for the ASP.NET checkbox functionality -->
                    <asp:HiddenField ID="chcButtonSoundHidden" runat="server" />

                    <!-- Custom switch design -->
                    <label class="switch" style="margin-left:auto">
                        <input type="checkbox" id="chkButtonClickSoundCustom" />
                        <span class="slider"></span>
                    </label>


                </div>

                <h3>Notifications</h3>

                <asp:Label ID="Label1" runat="server" Text="Bible Basics"></asp:Label>
                <div class="setting-option" style="display: flex; align-items: center;">
                    <small>Switch on/off Bible Basic Notifications</small>
                    

                    <!-- Hidden input to store the value for the ASP.NET checkbox functionality -->
                    <asp:HiddenField ID="chkBibleBasicsHidden" runat="server" />

                    <!-- Custom switch design -->
                    <label class="switch" style="margin-left:auto">
                        <input type="checkbox" id="chkBibleBasicsCustom" />
                        <span class="slider"></span>
                    </label>                                


                </div>

                <asp:Label ID="lblResponsibilityUpdates" runat="server" Text="Responsibility Updates"></asp:Label>
                <div class="setting-option" style="display: flex; align-items: center;">
                    
                    
                    <small>Switch on/off Responsibility Update Notifications</small>
                    
                    
                    <!-- Hidden input to store the value for the ASP.NET checkbox functionality -->
                    <asp:HiddenField ID="chkResponsibilityHidden" runat="server" />

                    <!-- Custom switch design -->
                    <label class="switch" style="margin-left:auto">
                        <input type="checkbox" id="chkResponsibiltyUpdatesCustom" />
                        <span class="slider"></span>
                    </label>


                </div>
            </div>

            <!-- Right Account Panel -->
            <div class="settings-right">
                <div class="account-info">
                    <img src="~/Images/ProfilePic.png" alt="Profile Picture" class="profile-pic" />
                    <h3>My Account</h3>
                    <div class="account-fields">
                        <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        
                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                        <asp:TextBox ID="txtName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="lblSurname" runat="server" Text="Surname"></asp:Label>
                        <asp:TextBox ID="txtSurname" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                        <asp:Label ID="lblEcclesia" runat="server" Text="Ecclesia"></asp:Label>
                        <asp:DropDownList ID="ddlEcclesia" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Cape Town">Cape Town</asp:ListItem>
                            <asp:ListItem Value="Johannesburg">Johannesburg</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn btn-primary" />

                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnUpdateEmail" runat="server" Text="Update Your Email" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnDeleteAccount" runat="server" Text="Delete Your Account" CssClass="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
