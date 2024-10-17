﻿<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Main Container -->
    <div class="main-container home">
        <!-- Left Section: Exhortations -->
        <div class="section">
            <h3 class="headings">Exhortations</h3>
            <div>
                <!-- Dynamic list of Exhortations -->
                <div class="exhortation-item">
                    <div class="exhortation-info">
                        <p>Talk Title | 01-01-2024</p>
                        <p>Brief Talk Description</p>
                    </div>
                        <a class="details-link" href="#">Details ></a>
                        <button class="play-button">▶</button>
                </div>
            </div>
        </div>

        <!-- Middle Section: Ecclesial News -->
        <div class="section">
            <h3 class="headings">Ecclesial News</h3>
            <div class="news-list">
                <!-- Dynamic list of News Letters -->
                <div class="news-item">
                    <div class="news-info">
                        <p>News Letter Title | 01-01-2024</p>
                        <p>Brief Letter Description</p>
                    </div>
                    <button class="news-button">🕮</button>
                </div>
            </div>
        </div>

        <!-- Right Section: Next Sunday, Prayer Requests, and Donations -->
        <div class="right-section">
            <!-- Next Sunday Section -->
            <div class="section next-sunday">
                <h5 class="headings">Next Sunday - 01-01-2024</h5>
                <div class="content">
                    <p><strong>Presiding:</strong> James Dean</p>
                    <p><strong>Exhortation:</strong> Phil Dunphy</p>
                    <p><strong>On The Door:</strong> John Doe</p>
                    <button>View Schedule</button>
                </div>
            </div>

            <!-- Prayer Requests Section -->
            <div class="section prayer-requests">
                <h5 class="headings">Prayer Requests - 01-01-2024</h5>
                <div class="prayer-requests-home">
                    <asp:Repeater ID="PrayerRequestsRepeater" runat="server">
                        <ItemTemplate>
                            <div><%# Container.DataItem.ToString() %></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Online Donations Section -->
            <div class="section donations">
                <h5 class="headings">Donations</h5>
                <div class="donation-grid">
                    <div>Blue Bag - General</div>
                    <div>Red Bag - Welfare</div>
                    <div>Rondebosch Food Drive</div>
                    <div>
                        <div class="progress-bar">
                            <div class="progress" style="width: 60%;"></div>
                        </div>
                    </div>
                    <div>New Hymn Books</div>
                    <div>
                        <div class="progress-bar">
                            <div class="progress" style="width: 45%;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
