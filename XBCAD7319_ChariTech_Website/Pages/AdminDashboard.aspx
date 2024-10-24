<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="main-container">
                <!-- Ecclesial Newsletter Upload Section -->
        <form id="newsletterForm" runat="server" method="post" enctype="multipart/form-data">
            <div class="dashboard-stack">
                <div class="section item-wrap">
                    <h3 class="headings">Ecclesial Newsletter Upload</h3>
                    <div class="exhortation-upload-input-layout">
                        <!-- Date Input -->
                        <div class="upload-form">
                            <label for="newsletter-date">Date</label>
                            <input type="date" id="newsletter-date" name="newsletter-date" required>
                        </div>

                        <!-- Title Input -->
                        <div class="upload-form">
                            <label for="newsletter-title">Title</label>
                            <input type="text" id="newsletter-title" name="newsletter-title" placeholder="Enter the title" required>
                        </div>
                    </div>
                    <asp:FileUpload ID="fileUploadControl" runat="server" CssClass="file-upload" />
                    <!-- Submit Button -->
                    <asp:Button ID="uploadNewsletterButton" runat="server" CssClass="btn" Text="Upload Newsletter" OnClick="UploadNewsletterButton_Click" />
                </div>
            </div>
        </form>
            <!-- Prayer Request Review Section -->
            <div class="section prayer-requests-review item-wrap">
                <h3 class="headings">Prayer Request Review<br>
                    01-01-2024</h3>
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

        <div class="dashboard-stack item-wrap">

                    <!-- Exhortation Upload Section -->
        <form id="exhortationForm" runat="server" method="post" enctype="multipart/form-data">
            <div class="dashboard-stack item-wrap">
                <div class="section item-wrap">
                    <h3 class="headings">Exhortation Upload</h3>
                    <div class="exhortation-upload-input-layout">
                        <!-- Date Input -->
                        <div class="upload-form">
                            <label for="exhortation-date">Date</label>
                            <input type="date" id="exhortation-date" name="exhortation-date" required>
                        </div>
                        <!-- Title Input -->
                        <div class="upload-form">
                            <label for="exhortation-title">Title</label>
                            <input type="text" id="exhortation-title" name="exhortation-title" placeholder="Enter the title" required>
                        </div>
                        <!-- Speaker Input -->
                        <div class="upload-form">
                            <label for="exhortation-speaker">Speaker</label>
                            <input type="text" id="exhortation-speaker" name="exhortation-speaker" placeholder="Enter the speaker's name" required>
                        </div>
                        <!-- File Upload Input -->
                        <asp:FileUpload ID="exhortationFileUpload" runat="server" CssClass="file-upload" />
                    </div>
                    <!-- Submit Button -->
                    <asp:Button ID="uploadExhortationButton" runat="server" CssClass="btn" Text="Upload Exhortation" OnClick="UploadExhortationButton_Click" />
                </div>
            </div>
        </form>

            <!-- Online Donations Section -->
            <div class="section item-wrap">
                <h3 class="headings">Online Donations</h3>
                <div class="donation-editable">
                    <div>
                        Blue Bag - <span>General</span> <i class="fa fa-edit"></i>
                        Red Bag - <span>Welfare</span> <i class="fa fa-edit"></i>
                    </div>
                    <div>
                        Rondebosch Food Drive: <span>2156 / 5200</span> <i class="fa fa-edit"></i>
                    </div>
                    <div>
                        New Hymn Books: <span>400 / 800</span> <i class="fa fa-edit"></i>
                    </div>
                </div>
                <div style="align-self: center">
                    <button class="btn cancel-btn">Cancel</button>
                    <button class="btn publish-btn">Publish Changes</button>
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

        </div>
        <div class="dashboard-stack">
            <div class="section bible-course-nav item-wrap">
                <h3 class="headings">Next Sunday<br>
                    01-01-2024</h3>
                <p><strong>Presiding:</strong> James Dean <i class="fa fa-edit"></i></p>
                <p><strong>Exhortation:</strong> Phil Dunphy <i class="fa fa-edit"></i></p>
                <p><strong>On The Door:</strong> John Doe <i class="fa fa-edit"></i></p>
                <div class="next-sunday calendar">
                    <!-- Flatpickr calendar is always visible and only Sundays are selectable -->
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

    <!-- Include Flatpickr CSS and JS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const nextSundayCalendar = document.getElementById('next-sunday-calendar');

            // Initialize Flatpickr with Sundays only and always open
            flatpickr(nextSundayCalendar, {
                inline: true, // Calendar is always visible
                minDate: "today", // Start from today
                enable: [
                    function (date) {
                        // Return true only for Sundays
                        return date.getDay() === 0;
                    }
                ],
                onReady: function (selectedDates, dateStr, instance) {
                    // Adjust the calendar container's size to be full
                    instance.calendarContainer.style.height = "auto"; // Automatic height
                }
            });
        });
    </script>

<script>
    document.addEventListener('DOMContentLoaded', function () {
        // Newsletter section elements
        const newsletterDateInput = document.getElementById('newsletter-date');
        const newsletterTitleInput = document.getElementById('newsletter-title');
        const newsletterFileInput = document.getElementById('<%= fileUploadControl.ClientID %>');
        const newsletterButton = document.getElementById('<%= uploadNewsletterButton.ClientID %>'); // Fix: Use correct ID

        // Exhortation section elements
        const exhortationDateInput = document.getElementById('exhortation-date');
        const exhortationTitleInput = document.getElementById('exhortation-title');
        const exhortationSpeakerInput = document.getElementById('exhortation-speaker');
        const exhortationFileInput = document.getElementById('<%= exhortationFileUpload.ClientID %>');
        const exhortationButton = document.getElementById('<%= uploadExhortationButton.ClientID %>'); // Fix: Use correct ID

        // Validate Newsletter Fields
        newsletterButton.addEventListener('click', function (event) {
            if (!newsletterDateInput.value || !newsletterTitleInput.value || !newsletterFileInput.value) {
                event.preventDefault();
                alert("Please fill out all fields in the Newsletter Upload section.");
            }
        });

        // Validate Exhortation Fields
        exhortationButton.addEventListener('click', function (event) {
            if (!exhortationDateInput.value || !exhortationTitleInput.value || !exhortationSpeakerInput.value || !exhortationFileInput.value.endsWith('.mp3')) {
                event.preventDefault();
                alert("Please fill out all fields in the Exhortation Upload section and upload a valid .mp3 file.");
            }
        });
    });
</script>



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
    </style>
</asp:Content>
