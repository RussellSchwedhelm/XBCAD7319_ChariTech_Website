<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="main-container dashboard-container">
        <!-- Ecclesial Newsletter Upload Section -->
        <div class="section ecclesial-news-upload">
            <h3>Ecclesial Newsletter Upload</h3>
            <div class="upload-form">                
                <label for="date">Date</label>
                <input type="date" id="date">
            </div>
            <div class="upload-form">                
                <label for="title">Title</label>
                <input type="text" id="title" placeholder="-----------------------">
            </div>
            <div class="file-upload">
                <i class="fa fa-upload"></i>
                <p>Drag and Drop Here Or Browse For a File</p>
            </div>
        </div>

        <!-- Online Donations Section -->
        <div class="section online-donations">
            <h3>Online Donations</h3>
            <div class="donation-editable">
                <div class="donation-item">
                    Blue Bag <span>General</span> <i class="fa fa-edit"></i>
                </div>
                <div class="donation-item">
                    Red Bag <span>Welfare</span> <i class="fa fa-edit"></i>
                </div>
                <div class="donation-item">
                    Rondebosch Food Drive <span>2156 / 5200</span> <i class="fa fa-edit"></i>
                </div>
                <div class="donation-item">
                    New Hymn Books <span>400 / 800</span> <i class="fa fa-edit"></i>
                </div>
            </div>
            <div class="buttons-group">
                <button class="btn cancel-btn">Cancel</button>
                <button class="btn publish-btn">Publish Changes</button>
            </div>
        </div>

        <!-- Next Sunday Section -->
        <div class="section next-sunday-modi">
            <h3>Next Sunday - 01-01-2024</h3>
            <p><strong>Presiding:</strong> James Dean</p>
            <p><strong>Exhortation:</strong> Phil Dunphy</p>
            <p><strong>On The Door:</strong> John Doe</p>
            <div class="next-sunday calendar">
                <input type="date" id="next-sunday-date">
                <button class="black-button">Save Sunday Information</button>
            </div>
        </div>

        <!-- Prayer Request Review Section -->
        <div class="section prayer-requests-review">
            <h3>Prayer Request Review - 01-01-2024</h3>
            <ul class="prayer-requests-list">
                <li>Emily Johnson <i class="fa fa-times-circle"></i></li>
                <li>Michael Smith <i class="fa fa-check-circle"></i></li>
                <li>Olivia Brown <i class="fa fa-times-circle"></i></li>
                <li>Benjamin Davis <i class="fa fa-check-circle"></i></li>
                <li>Sophia Martinez <i class="fa fa-check-circle"></i></li>
                <li>Jacob Wilson <i class="fa fa-check-circle"></i></li>
                <li>Isabella Thompson <i class="fa fa-check-circle"></i></li>
                <li>Ethan Garcia <i class="fa fa-times-circle"></i></li>
            </ul>
            <div class="buttons-group">
                <button class="btn accept-btn">Accept All</button>
                <button class="btn deny-btn">Deny All</button>
            </div>
        </div>

        <!-- Notifications Section -->
        <div class="section notifications">
            <h3>Create Notification</h3>
            <div class="notification-form">
                <label for="notification-date">Date</label>
                <input type="text" id="notification-date" placeholder="--/--/----">
                <label for="notification-title">Title</label>
                <input type="text" id="notification-title" placeholder="-----------------------">
                <textarea placeholder="Enter notification text"></textarea>
                <div class="dropdown">
                    <label for="destination-filters">Destination Filters</label>
                    <select id="destination-filters">
                        <option>None</option>
                    </select>
                </div>
            </div>
            <div class="buttons-group">
                <button class="btn cancel-btn">Cancel</button>
                <button class="btn publish-btn">Publish Notification</button>
            </div>
        </div>

        <!-- Upload Bible Course Section -->
        <div class="section">
            <div style="align-content: center">
                <button class="black-button">Upload Bible Course</button>
            </div>
        </div>
    </div>
</asp:Content>
