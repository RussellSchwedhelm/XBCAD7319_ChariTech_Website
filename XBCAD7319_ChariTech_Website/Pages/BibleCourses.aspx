﻿<%@ Page Title="Bible Courses" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="BibleCourses.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.BibleCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha384-k6RqeWeci5ZR/Lv4MR0sA0FfDOM5MTh+g4j06Z5pTf9Xd6vl2Pii2B1PiVVF2/Kf" crossorigin="anonymous">

    <!-- Main Container -->
    <div class="section" style="width: 90%; padding: 10px; margin-top: 20px; margin-bottom: 20px;">
        <div class="main-container">

            <div class=" courseSearchDisplay">
                <h3 style="margin: 0 auto;">Courses</h3>

                <img style="margin: 0 auto; width: 60px; height: 60px;" src='<%= ResolveUrl("~/Images/ChurchLogo.png") %>' alt="Icon" class="item-icon" />

                <h4 style="margin: 0 auto;">Deepen Your Understanding and Strengthen Your Faith</h4>
                <br />
                <div class="search-container" style="width: 40%; margin: 0 auto;">
                    <asp:TextBox ID="txtSearchQuery2" runat="server" CssClass="search-box" placeholder="Search..."></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="search-button" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>

            <div class="section courseDisplay">
                <asp:DataList ID="dlCourses" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" CellPadding="10">
                    <ItemTemplate>
                        <div class="course-card">
                            <div class="course-header">
                                <asp:Label ID="lblCourseTitle" runat="server" Text='<%# Eval("CourseTitle") %>' CssClass="course-title"></asp:Label>
                                <br />
                                <asp:Label ID="lblTheme" runat="server" Text='<%# Eval("Theme") %>' CssClass="course-theme"></asp:Label>
                            </div>
                            <div class="course-body">
                                <asp:Image ID="imgThumbnail" runat="server" CssClass="course-image" Height="60px" Width="60px" ImageAlign="Middle" ImageUrl='<%# ResolveUrl("~/Images/ChurchLogo.png") %>' />
                            </div>
                            <div class="course-footer">
                                <asp:Label ID="lblTimeToComplete" runat="server" Text='<%# Eval("Duration") %>' CssClass="course-duration"></asp:Label>
                                <br />
                                <asp:Label ID="lblDateUploaded" runat="server" Text='<%# Eval("DateUploaded") %>' CssClass="course-date"></asp:Label>
                                <br />
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="course-description"></asp:Label>

                                <asp:Button ID="btnOpen"
                                    runat="server"
                                    Text="Open"
                                    CssClass="course-open-btn"
                                    CommandArgument='<%# Container.ItemIndex %>'
                                    OnClick="btnOpen_Click" />
                            </div>
                        </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>



            <div class="courseFilterSelect">
                <div class="filter-header">Filter</div>
                <!-- Added header to match the image -->
                <asp:Panel ID="Panel1" runat="server" CssClass="scrollable-filter-list">

                    <asp:Repeater ID="RepeaterOptions" runat="server">
                        <ItemTemplate>
                            <div class="filter-list-item">
                                <div class="icon-wrapper">
                                    <i class="fas fa-search"></i>
                                    <!-- Font Awesome magnifying glass icon -->
                                </div>
                                <span class="item-text"><%# Eval("Text") %></span>
                                <asp:CheckBox ID="CheckBoxItem" runat="server" CssClass="custom-checkbox"
                                    AutoPostBack="True"
                                    OnCheckedChanged="ThemeCheckbox_CheckedChanged"/>
                                            <asp:HiddenField ID="ThemeHiddenField" runat="server" Value='<%# Eval("Text") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function openPdfInNewTab(pdfUrl) {
            if (pdfUrl) {
                window.open(pdfUrl, '_blank');
            } else {
                alert("No PDF file available for this course.");
            }
        }

    </script>





</asp:Content>

