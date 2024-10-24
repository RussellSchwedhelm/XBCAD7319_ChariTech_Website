﻿<%@ Page Title="EditExhortations" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="EditExhortations.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Exhortations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-container">
    <!-- Exhortations Section -->
    <div class="section exhortationSelect">
        <div style="text-align: center;">
            <h3 class="heading">Exhortations</h3>

            <div class="search-container" >
                <asp:TextBox ID="txtSearchQuery2" runat="server" CssClass="search-box" placeholder="Search..."></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="search-button" Text="Search" OnClick="btnSearch_Click" />
            </div>
        </div>

        <div class="exhortations-list">
            <!-- Repeat this block for each exhortation dynamically -->
            <div class="exhortation-item">
                <img src='<%= ResolveUrl("~/Images/Trash 13.png") %>' alt="Trash Bin" style="width: 20px; height: 20px; margin-right: 10px;" />

                <div class="exhortation-info">
                    <p class="title">Talk Title | 01-01-2024</p>
                    <p class="description">Brief Talk Description</p>
                </div>
                <div class="exhortation-actions">
                    <a href="#" class="exhortation-details-link">Details ></a>
                    <button class="play-button">▶</button>
                </div>
            </div>
        </div>
    </div>



        <div class="section exhortationDisplay" >

                <asp:TextBox ID="txtExhortationTitle" class="editExhortationTitle" ReadOnly="False" Text="*Talk Title*" runat="server" ></asp:TextBox>
                <asp:TextBox ID="txtExhortationSummary" class="editExhortationSummary" ReadOnly="False" Text="*A brief descriptive summary*" runat="server" ></asp:TextBox>
            
            
    <div class="audio-player" >
        <div class="audio-header">
            <span class="speaker">Marshal</span>
            <span class="date">01-01-2024</span>
        </div>
        <div class="audio-controls">
            <button class="play-button2" onclick="togglePlayPause()">▶</button>
            <div class="progress-container">
                <div class="exh-progress-bar">
                    <div class="exh-progress"></div>
                </div>
                <div class="timestamps">
                    <span class="current-time">10:11</span>
                    <span class="total-time">20:39</span>
                </div>
            </div>
        </div>
    </div>
         
            

            <h5 class="section-heading"> </h5>

   
                <asp:TextBox ID="txtExhortationTranscript" 
                    class="editExhortationTranscript" 
                    ReadOnly="False" 
                    TextMode="MultiLine"
                    Text="This is an Autogenerated transcript, that descripes the words and ideas that were covered in an exhortation..." 
                    runat="server" 
                    style="width:auto"></asp:TextBox>

            
            

            <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-secondary" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" />
            
        </div>


    </div>


</asp:Content>
