<%@ Page Title="Upload Bible Course" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="BibleCourseUpload.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.BibleCourseUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="add-course-container">
        <!-- Image Upload Section -->
        <div class="image-placeholder">
            <asp:FileUpload ID="FileUploadCoverImage" runat="server" CssClass="upload-input" />
            <span class="upload-text">Upload File</span>
        </div>

        <!-- Course Form Section -->
        <div class="course-form">
            <h2>Add New Course</h2>
            
            <asp:Label AssociatedControlID="TextBoxCourseTitle" runat="server" Text="Course Title"></asp:Label>
            <asp:TextBox ID="TextBoxCourseTitle" runat="server" CssClass="form-input" Placeholder="Value"></asp:TextBox>

            <asp:Label AssociatedControlID="TextBoxDescription" runat="server" Text="Brief Description"></asp:Label>
            <asp:TextBox ID="TextBoxDescription" runat="server" CssClass="form-input" Placeholder="Value"></asp:TextBox>

            <asp:Label AssociatedControlID="TextBoxTheme" runat="server" Text="Theme/Topic"></asp:Label>
            <asp:TextBox ID="TextBoxTheme" runat="server" CssClass="form-input" Placeholder="Value"></asp:TextBox>

            <asp:Label AssociatedControlID="TextBoxCompletionTime" runat="server" Text="Estimated Completion Time"></asp:Label>
            <asp:TextBox ID="TextBoxCompletionTime" runat="server" CssClass="form-input" Placeholder="10 - 11 Weeks"></asp:TextBox>

            <!-- Buttons -->
            <div class="button-group">
                <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="btn-reset" OnClick="ButtonReset_Click" />
                <asp:Button ID="ButtonConfirm" runat="server" Text="Confirm" CssClass="btn-confirm" OnClick="ButtonConfirm_Click" />
            </div>
        </div>
    </div>
</asp:Content>
