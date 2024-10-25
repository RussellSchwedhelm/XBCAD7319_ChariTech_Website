<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="main-container">
        <!-- Ecclesial Newsletter Upload Section -->
        <asp:Panel ID="newsletterPanel" runat="server" CssClass="dashboard-stack">
            <div class="section item-wrap">
                <h3 class="headings">Ecclesial Newsletter Upload</h3>
                <div class="upload-form">
                    <label for="newsletter-date">Date</label>
                    <asp:TextBox ID="NewsletterDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="upload-form">
                    <label for="newsletter-title">Title</label>
                    <asp:TextBox ID="NewsletterTitle" runat="server" CssClass="form-control" placeholder="Enter the title"></asp:TextBox>
                </div>

                <div class="upload-form">
                    <asp:FileUpload ID="NewsletterFileUpload" runat="server" CssClass="file-upload" />
                </div>

                <asp:Button ID="UploadNewsletterButton" runat="server" CssClass="btn" Text="Upload Newsletter" OnClick="UploadNewsletterButton_Click" />
            </div>
        </asp:Panel>

        <!-- Prayer Request Review Section -->
        <div class="section prayer-requests-review item-wrap">
            <h3 class="headings">Prayer Request Review<br> 01-01-2024</h3>
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
                <button>Accept Changes</button>
                <button>Revert Changes</button>
            </div>
        </div>

        <!-- Exhortation Upload Section -->
        <asp:Panel ID="exhortationPanel" runat="server" CssClass="dashboard-stack">
            <div class="section item-wrap">
                <h3 class="headings">Exhortation Upload</h3>
                <div class="upload-form">
                    <label for="exhortation-date">Date</label>
                    <asp:TextBox ID="ExhortationDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="upload-form">
                    <label for="exhortation-title">Title</label>
                    <asp:TextBox ID="ExhortationTitle" runat="server" CssClass="form-control" placeholder="Enter the title"></asp:TextBox>
                </div>

                <div class="upload-form">
                    <label for="exhortation-speaker">Speaker</label>
                    <asp:TextBox ID="ExhortationSpeaker" runat="server" CssClass="form-control" placeholder="Enter the speaker's name"></asp:TextBox>
                </div>

                <div class="upload-form">
                    <asp:FileUpload ID="ExhortationFileUpload" runat="server" CssClass="file-upload" />
                </div>

                <asp:Button ID="UploadExhortationButton" runat="server" CssClass="btn" Text="Upload Exhortation" OnClick="UploadExhortationButton_Click" />
            </div>
        </asp:Panel>

        <!-- Online Donations Section -->
        <div class="section item-wrap">
            <h3 class="headings">Online Donations</h3>
            
            <!-- Blue Bag - General -->
            <div class="upload-form">
                <label>Blue Bag - </label>
                <asp:TextBox ID="BlueBagCause" runat="server" CssClass="form-control" placeholder="Cause" Text="General" />
            </div>

            <!-- Red Bag - Welfare -->
            <div class="upload-form">
                <label>Red Bag - </label>
                <asp:TextBox ID="RedBagCause" runat="server" CssClass="form-control" placeholder="Cause" Text="Welfare" />
            </div>

            <!-- Rondebosch Food Drive -->
            <div class="upload-form">
                <label>Drive - </label>
                <asp:TextBox ID="Drive1Name" runat="server" CssClass="form-control" placeholder="Cause" />
            </div>
            <div class="upload-form">
                <label for="Drive1Amout">Current Amount</label>
                <asp:TextBox ID="Drive1Amount" runat="server" CssClass="form-control" placeholder="Enter Current Amount" />
            </div>
            <div class="upload-form">
                <label for="Drive1Goal">Goal Amount</label>
                <asp:TextBox ID="Drive1Goal" runat="server" CssClass="form-control" placeholder="Enter Goal Amount" />
            </div>

            <!-- New Hymn Books -->
            <div class="upload-form">
                <label>Drive - </label>
                <asp:TextBox ID="Drive2Name" runat="server" CssClass="form-control" placeholder="Cause" Text="Book Drive" />
            </div>
            <div class="upload-form">
                <label for="Drive2Amount">Current Amount</label>
                <asp:TextBox ID="Drive2Amount" runat="server" CssClass="form-control" placeholder="Enter Current Amount" />
            </div>
            <div class="upload-form">
                <label for="Drive2Goal">Goal Amount</label>
                <asp:TextBox ID="Drive2Goal" runat="server" CssClass="form-control" placeholder="Enter Goal Amount" />
            </div>

            <div style="align-self: center">
                <asp:Button ID="CancelButton" runat="server" CssClass="btn cancel-btn" Text="Cancel" OnClick="CancelButton_Click" />
                <asp:Button ID="PublishButton" runat="server" CssClass="btn publish-btn" Text="Publish Changes" OnClick="PublishButton_Click" />
            </div>
        </div>

        <!-- Notifications Section -->
        <div class="section item-wrap">
            <h3 class="headings">Create Notification</h3>
            <div class="notification-form exhoration-upload-input-layout">
                <div class="upload-form">
                    <label for="notification-date">Date</label>
                    <input type="text" id="notification-date" placeholder="--/--/----">
                </div>
                <div class="upload-form">
                    <label for="notification-title">Title</label>
                    <input type="text" id="notification-title" placeholder="-----------------------">
                </div>
            </div>
            <textarea placeholder="Enter notification text"></textarea>
            <div style="align-self: end" class="dropdown">
                <label for="destination-filters">Destination Filters</label>
                <select id="destination-filters">
                    <option>None</option>
                </select>
            </div>
            <div class="buttons-group">
                <button>Cancel</button>
                <button>Publish Notification</button>
            </div>
        </div>

        <!-- Bible Course and Next Sunday Information -->
        <div class="dashboard-stack">
            <div class="section bible-course-nav item-wrap">
                <h3 class="headings">Next Sunday<br>01-01-2024</h3>
                <p><strong>Presiding:</strong> James Dean <i class="fa fa-edit"></i></p>
                <p><strong>Exhortation:</strong> Phil Dunphy <i class="fa fa-edit"></i></p>
                <p><strong>On The Door:</strong> John Doe <i class="fa fa-edit"></i></p>
                <div class="next-sunday calendar">
                    <!-- Flatpickr calendar -->
                    <div id="next-sunday-calendar"></div>
                </div>
                <div class="buttons-group">
                    <button>Save Sunday Information</button>
                </div>
            </div>

            <!-- Upload Bible Course Section -->
            <div class="section bible-course-nav item-wrap">
                <div class="buttons-group">
                    <button>Upload Bible Course</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Flatpickr CSS and JS for the calendar -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

    <style>
        /* Button Styling */
        .btn {
            background-color: #333;
            color: white;
            padding: 0.75rem;
            width: fit-content;
            border: none;
            border-radius: 0.5rem;
            cursor: pointer;
            font-size: 1rem;
            text-align: center;
            transition: background-color 0.3s ease;
            margin-top: 0.625rem; /* Consistent margin for all buttons */
        }

        .btn:hover {
            background-color: #555; /* Add hover effect */
        }

        /* Cancel and Publish button styling */
        .cancel-btn {
            background-color: #f44336;
        }

        .publish-btn {
            background-color: #4CAF50;
        }
    </style>
</asp:Content>
