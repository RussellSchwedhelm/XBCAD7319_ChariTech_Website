<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Main Container -->
    <div class="main-container">
    <!-- Exhortations Section -->
    <div class="section exhortations">
        <h3 class="section-heading">Exhortations</h3>
        <div class="exhortations-list">
            <!-- Repeat this block for each exhortation dynamically -->
            <div class="exhortation-item">
                <div class="exhortation-info">
                    <p class="title">Talk Title | 01-01-2024</p>
                    <p class="description">Brief Talk Description</p>
                </div>
                <div class="exhortation-actions">
                    <a href="#" class="details-link">Details ></a>
                    <button class="play-button">▶</button>
                </div>
            </div>
        </div>
    </div>

        <!-- Ecclesial News Section -->
        <div class="section ecclesial-news">
            <h3 class="section-heading">Ecclesial News</h3>
            <div class="news-list">
                <!-- Repeat this block for each newsletter dynamically -->
                <div class="news-item">
                    <div class="news-info">
                        <p class="title">News Letter Title | 01-01-2024</p>
                        <p class="description">Brief Letter Description</p>
                    </div>
                    <button class="news-play-button">🕮</button>
                </div>
            </div>
        </div>

        <!-- Next Sunday Section -->
        <div class="section next-sunday">
            <h4>Next Sunday - 01-01-2024</h4>
            <div class="content">
                <p><strong>Presiding:</strong> James Dean</p>
                <p><strong>Exhortation:</strong> Phil Dunphy</p>
                <p><strong>On The Door:</strong> John Doe</p>
                <button class="view-schedule">View Schedule</button>
            </div>
        </div>

        <!-- Prayer Requests Section -->
        <div class="section prayer-requests">
            <h4>Prayer Requests - 01-01-2024</h4>
            <ul>
                <li>Emily Johnson</li>
                <li>Michael Smith</li>
                <li>Olivia Brown</li>
                <li>Benjamin Davis</li>
                <li>Sophia Martinez</li>
                <li>Jacob Wilson</li>
                <li>Isabella Thompson</li>
                <li>Ethan Garcia</li>
            </ul>
        </div>

        <!-- Online Donations Section -->
        <div class="section donations">
            <h4>Donations</h4>
            <div class="donation-grid">
                <!-- Row 1: Blue Bag and Red Bag -->
                <div>Blue Bag - General</div>
                <div>Red Bag - Welfare</div>

                <!-- Row 2: Rondebosch Food Drive and progress bar -->
                <div>Rondebosch Food Drive</div>
                <div>
                    <div class="progress-bar">
                        <div class="progress" style="width: 60%;"></div> <!-- Adjust width for progress -->
                    </div>
                </div>

                <!-- Row 3: New Hymn Books and progress bar -->
                <div>New Hymn Books</div>
                <div>
                    <div class="progress-bar">
                        <div class="progress" style="width: 45%;"></div> <!-- Adjust width for progress -->
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

