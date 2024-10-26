<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="BibleCourses.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.BibleCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="section" style="width:100%; padding:10px; margin-top: 10px; margin-bottom: 10px;">
        <div class="main-container">

            <div class=" courseSearchDisplay">
                <h3 style="margin: 0 auto;">Courses</h3>

                <img style="margin: 0 auto;width: 60px; height: 60px;" src='<%= ResolveUrl("~/Images/ChurchLogo.png") %>' alt="Icon" class="item-icon" />
                
                <h4 style="margin: 0 auto;">Deepen Your Understanding and Strengthen Your Faith</h4>
                <br />
                <div class="search-container" style="width: 40%; margin: 0 auto;" >
                    <asp:TextBox ID="txtSearchQuery2" runat="server" CssClass="search-box" placeholder="Search..."></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="search-button" Text="Search" OnClick="btnSearch_Click" />
                </div>
                 </div>

            <div class="section courseDisplay" >
<asp:DataList ID="dlCourses" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" CellPadding="10">
    <ItemTemplate>
        <div class="course-card">
            <div class="course-header">
                <asp:Label ID="lblCourseTitle" runat="server" Text='<%# Eval("CourseTitle") %>' CssClass="course-title"></asp:Label>
                <br />
                <asp:Label ID="lblTheme" runat="server" Text='<%# Eval("Theme") %>' CssClass="course-theme"></asp:Label>
            </div>
            <div class="course-body">
                <asp:Image ID="imgThumbnail" runat="server" CssClass="course-image" ImageUrl='<%# Eval("ImageUrl") %>' />
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
                    CommandArgument='<%# Eval("PdfFileUrl") %>' 
                    OnClientClick='<%# "openPdfInNewTab(\"" + ResolveUrl(Eval("PdfFileUrl").ToString()) + "\"); return false;" %>' />

    </div>
            </div>
        </div>
    </ItemTemplate>
</asp:DataList>
            </div>

            <div class="courseFilterSelect">
                <div class="filter-header">Filter</div> <!-- Added header to match the image -->
                <asp:Panel ID="PanelScrollableList" runat="server" CssClass="scrollable-list">
                    
                    <asp:Repeater ID="RepeaterOptions" runat="server">
                        <ItemTemplate>
                            <div class="filter-list-item">
                                <div class="icon-wrapper">
                                    <div class="icon-letter">A</div>
                                    <!--<img src='<%# Eval("IconUrl") %>' alt="Icon" class="item-icon" />-->
                                </div>
                                <span class="item-text"><%# Eval("Text") %></span>
                                <asp:CheckBox ID="CheckBoxItem" runat="server" CssClass="custom-checkbox" />
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

