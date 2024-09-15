<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Contact" %>
<asp:Content ID="ContactUsPage" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .main-content {
            display: flex;
            flex-direction: column;
            align-items: center;
            width: 100%;
            padding: 20px;
            box-sizing: border-box;
        }
        .contact-card {
            background-color: #e0e0e0;
            padding: 20px;
            margin-bottom: 10px;
            border-radius: 5px;
            width: 80%;
            border-left: none;
            border-right: none;
        }
        .contact-info {
            margin: 5px 0;
        }
        .social-links {
            margin-top: 20px;
            display: flex;
            justify-content: space-around;
            width: 50%;
        }
        .social-link {
            text-decoration: none;
            color: #007bff;
            font-size: 1.2em;
            display: flex;
            align-items: center;
        }
        .social-link i {
            margin-right: 8px;
        }
    </style>
    <asp:Panel ID="pnlContact" runat="server" CssClass="main-content">
        <asp:Panel ID="contactCard1" runat="server" CssClass="contact-card">
            <div class="contact-info"><strong>Name:</strong> John Doe</div>
            <div class="contact-info"><strong>Contact Number:</strong> (123) 456-7890</div>
            <div class="contact-info"><strong>Email:</strong> john.doe@example.com</div>
        </asp:Panel>
        <asp:Panel ID="contactCard2" runat="server" CssClass="contact-card">
            <div class="contact-info"><strong>Name:</strong> Jane Smith</div>
            <div class="contact-info"><strong>Contact Number:</strong> (987) 654-3210</div>
            <div class="contact-info"><strong>Email:</strong> jane.smith@example.com</div>
        </asp:Panel>
        <asp:Panel ID="contactCard3" runat="server" CssClass="contact-card">
            <div class="contact-info"><strong>Name:</strong> Bob Johnson</div>
            <div class="contact-info"><strong>Contact Number:</strong> (555) 123-4567</div>
            <div class="contact-info"><strong>Email:</strong> bob.johnson@example.com</div>
        </asp:Panel>
        <div class="social-links">
            <a href="https://www.facebook.com/ctchristadelphians/" target="_blank" class="social-link">
                <i class="fab fa-facebook"></i> https://www.facebook.com/ctchristadelphians/
            </a>
            <a href="https://wa.me/1234567890" target="_blank" class="social-link">
                <i class="fab fa-whatsapp"></i> https://wa.me/1234567890
            </a>
        </div>
    </asp:Panel>
</asp:Content>
