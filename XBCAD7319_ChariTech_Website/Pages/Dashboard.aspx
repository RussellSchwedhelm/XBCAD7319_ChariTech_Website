<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="main-container">
        
        <!-- Ecclesial Newsletter Upload Section -->
        <div class="section ecclesial-news-upload">
            <h3>Ecclesial Newsletter Upload</h3>
            <form>
                <label for="newsletter-date">Date:</label>
                <input type="date" id="newsletter-date" name="newsletter-date">
                
                <label for="newsletter-title">Title:</label>
                <input type="text" id="newsletter-title" name="newsletter-title" placeholder="Enter title here">
                
                <label for="newsletter-file">Upload File:</label>
                <input type="file" id="newsletter-file" name="newsletter-file">
                
                <button type="submit">Upload</button>
            </form>
        </div>

        <!-- Online Donations Section -->
        <div class="section online-donations">
            <h3>Online Donations</h3>
            <div>
                <label>Blue Bag (General):</label>
                <input type="text" value="Blue Bag" readonly>
                
                <label>Red Bag (Welfare):</label>
                <input type="text" value="Red Bag" readonly>

                <label>Rondebosch Food Drive:</label>
                <input type="text" value="2156 / 5200" readonly>
                <div class="progress-bar">
                    <div class="progress" style="width: 40%;"></div>
                </div>

                <label>New Hymn Books:</label>
                <input type="text" value="400 / 800" readonly>
                <div class="progress-bar">
                    <div class="progress" style="width: 50%;"></div>
                </div>
            </div>
        </div>

        <!-- Next Sunday Section -->
        <div class="section next-sunday">
            <h3>Next Sunday - 01-01-2024</h3>
            <p><strong>Presiding:</strong> James Dean</p>
            <p><strong>Exhortation:</strong> Phil Dunphy</p>
            <p><strong>On The Door:</strong> John Doe</p>
        </div>

        <!-- Prayer Request Review Section -->
        <div class="section prayer-request-review">
            <h3>Prayer Request Review - 01-01-2024</h3>
            <ul>
                <li>Emily Johnson <span class="action-button">✘</span></li>
                <li>Michael Smith <span class="action-button">✔</span></li>
                <li>Olivia Brown <span class="action-button">✘</span></li>
                <li>Benjamin Davis <span class="action-button">✔</span></li>
                <li>Sophia Martinez <span class="action-button">✔</span></li>
                <li>Jacob Wilson <span class="action-button">✔</span></li>
                <li>Isabella Thompson <span class="action-button">✔</span></li>
                <li>Ethan Garcia <span class="action-button">✘</span></li>
            </ul>
            <button class="accept-all">Accept All</button>
            <button class="deny-all">Deny All</button>
        </div>

        <!-- Upload Bible Course Section -->
        <div class="section upload-bible-course">
            <h3>Upload Bible Course</h3>
            <form>
                <label for="bible-course-date">Date:</label>
                <input type="date" id="bible-course-date" name="bible-course-date">

                <label for="bible-course-title">Title:</label>
                <input type="text" id="bible-course-title" name="bible-course-title" placeholder="Enter course title">
                
                <button type="submit">Upload</button>
            </form>
        </div>

    </div>
</asp:Content>
