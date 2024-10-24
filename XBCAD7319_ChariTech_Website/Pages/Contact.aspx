<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Contact" %>

<asp:Content ID="ContactUsPage" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .contact-card {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
        }

        .profile-pic {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            object-fit: cover;
            margin-right: 20px;
        }

        .contact-info {
            display: flex;
            flex-direction: column;
        }
    </style>
        <div class="main-container" style="display: flex; justify-content: center; align-items: center; min-height: 100%; grid-template-columns: 1fr;">
            <div class="section" style="max-width: 40rem;" id="contactSection" runat="server">
                <!-- Contact cards will be dynamically generated here -->
            </div>
        </div>


</asp:Content>
