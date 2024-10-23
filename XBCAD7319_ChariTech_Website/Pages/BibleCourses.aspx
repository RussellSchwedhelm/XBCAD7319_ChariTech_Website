<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="BibleCourses.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.BibleCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="section" style="flex:1; padding:10px; margin-top: 10px; margin-bottom: 10px;">
        <div class="main-container">

            <div class="section courseSearchDisplay">

            </div>

            <div class="section courseDisplay" style="background-color: aquamarine">
                <h3 class="heading">Sentence</h3>
            </div>

            <div class="courseFilterSelect">
                <asp:Panel ID="PanelScrollableList" runat="server" CssClass="section scrollable-list">
                    <asp:Repeater ID="RepeaterOptions" runat="server">
                        <ItemTemplate>
                            <div class="list-item">
                                <img src='<%# Eval("IconUrl") %>' alt="Icon" class="item-icon" />
                                <span class="item-text"><%# Eval("Text") %></span>
                                <asp:CheckBox ID="CheckBoxItem" runat="server" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>

        </div>
    </div>
</asp:Content>
