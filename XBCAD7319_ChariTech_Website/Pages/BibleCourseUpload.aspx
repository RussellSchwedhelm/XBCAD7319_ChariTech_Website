<%@ Page Title="Upload Bible Course" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="BibleCourseUpload.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.BibleCourseUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="add-course-container">
        <!-- Image Upload Section -->
        <div class="image-placeholder">
            <asp:FileUpload ID="FileUploadCourse" runat="server" CssClass="upload-input" />
            <img style="margin: 0 auto;width: 150px; height: 150px;" src='<%= ResolveUrl("~/Images/ChurchLogo.png") %>' alt="Icon" class="item-icon" />
            
        </div>

        <!-- Course Form Section -->
        <div class="course-form">
            <h2>Add New Course</h2>
            
            <asp:Label AssociatedControlID="TextBoxCourseTitle" runat="server" Text="Course Title"></asp:Label>
            <asp:TextBox ID="TextBoxCourseTitle" runat="server" CssClass="form-input" Placeholder="Value"></asp:TextBox>

            <asp:Label AssociatedControlID="TextBoxDescription" runat="server" Text="Brief Description"></asp:Label>
            <asp:TextBox ID="TextBoxDescription" runat="server" CssClass="form-input" Placeholder="Value"></asp:TextBox>

            <asp:Label AssociatedControlID="ddlTheme" runat="server" Text="Theme/Topic"></asp:Label>
            
            <asp:DropDownList ID="ddlTheme" runat="server" CssClass="form-input">
                <asp:ListItem Value="New Believers">New Believers</asp:ListItem>
                <asp:ListItem Value="Old Testiment">Old Testament</asp:ListItem>
                <asp:ListItem Value="New Testament">New Testament</asp:ListItem>
                <asp:ListItem Value="Biblical Theology">Biblical Theology</asp:ListItem>
                <asp:ListItem Value="Christian Living">Christian Living</asp:ListItem>
                <asp:ListItem Value="Spiritual Growth">Spiritual Growth</asp:ListItem>
                <asp:ListItem Value="Ministry Training">Ministry Training</asp:ListItem>
                <asp:ListItem Value="Special Topics">Special Topics</asp:ListItem>
            </asp:DropDownList>


            <!-- File Upload Field -->
            <asp:Label AssociatedControlID="FileUpload1" runat="server" Text="Upload PDF"></asp:Label>
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-input" style="background-color: #ffffff" 
                accept="application/pdf"/>

            


            <asp:Label AssociatedControlID="TextBoxCompletionTime" runat="server" Text="Estimated Completion Time"></asp:Label>
            <asp:TextBox ID="TextBoxCompletionTime" runat="server" CssClass="form-input" 
                Placeholder="10 - 11 Weeks"></asp:TextBox>

            <!-- Buttons -->
            <div class="button-group">
                <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="btn-reset" OnClick="ButtonReset_Click" />
                <asp:Button ID="ButtonConfirm" runat="server" Text="Confirm" CssClass="btn-confirm" OnClick="ButtonConfirm_Click" />
            </div>
        </div>
    </div>
</asp:Content>
