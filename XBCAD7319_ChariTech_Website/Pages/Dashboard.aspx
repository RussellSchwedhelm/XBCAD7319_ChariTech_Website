<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="main-container" style="align-items: center">
        <!-- Ecclesial Newsletter Upload Section -->
        <div class="dashboard-stack">
            <div class="section item-wrap">
                <h3 class="headings">Ecclesial Newsletter Upload</h3>
                <div class="exhoration-upload-input-layout">
                    <!-- Fixed class name -->
                    <div class="upload-form">
                        <label for="date">Date</label>
                        <input type="date" id="date">
                    </div>
                    <div class="upload-form">
                        <label for="title">Title</label>
                        <input type="text" id="title" placeholder="-----------------------">
                    </div>
                </div>
                <div class="file-upload">
                    <i class="fa fa-upload"></i>
                    <p>Drag and Drop Here Or Browse For a File</p>
                </div>
            </div>
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

        </div>
        <div class="dashboard-stack item-wrap">

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
            <!-- Next Sunday Section -->
            <div class="section bible-course-nav item-wrap">
                <h3 class="headings">Next Sunday<br>
                    01-01-2024</h3>
                <p><strong>Presiding:</strong> James Dean <i class="fa fa-edit"></i></p>
                <p><strong>Exhortation:</strong> Phil Dunphy <i class="fa fa-edit"></i></p>
                <p><strong>On The Door:</strong> John Doe <i class="fa fa-edit"></i></p>
                <div class="next-sunday calendar">
                    <!-- Flatpickr calendar is always visible here -->
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

    <!-- Initialize Flatpickr for always-visible calendar, Sundays only -->
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            flatpickr("#next-sunday-calendar", {
                inline: true, // Keep the calendar always open
                defaultDate: "2024-01-01", // Set the default date for Next Sunday
                enable: [
                    function (date) {
                        // Return true to allow Sundays only (0 is Sunday in JS Date)
                        return date.getDay() === 0;
                    }
                ],
                onChange: function (selectedDates, dateStr, instance) {
                    console.log("Selected date: ", dateStr);
                }
            });
        });
    </script>

</asp:Content>
