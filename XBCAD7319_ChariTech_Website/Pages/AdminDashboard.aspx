<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main Container -->
    <div class="main-container">
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

                <!-- File Upload Section with Drag and Drop support -->
                <div class="file-upload" id="file-drop-area">
                    <i class="fa fa-upload" id="upload-icon"></i>
                    <p id="upload-text">Drag and Drop Here Or Browse For a File</p>
                    <input type="file" id="file-upload" name="fileUpload" style="display: none">
                </div>

                <!-- Disable the button by default -->
                <asp:Button ID="uploadButton" runat="server" CssClass="btn" Text="Upload Newsletter" OnClick="UploadNewsletterButton_Click" Enabled="false" />
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

            <!-- Exhortation Upload Section -->
            <div class="section item-wrap">
                <h3 class="headings">Exhortation Upload</h3>
                <div class="exhoration-upload-input-layout">
                    <div class="upload-form">
                        <label for="exhortation-date">Date</label>
                        <input type="date" id="exhortation-date">
                    </div>
                    <div class="upload-form">
                        <label for="exhortation-title">Title</label>
                        <input type="text" id="exhortation-title" placeholder="-----------------------">
                    </div>
                </div>
                <div class="file-upload">
                    <i class="fa fa-upload"></i>
                    <p>Drag and Drop Here Or Browse For a File</p>
                </div>
            </div>

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
                  const dateInput = document.getElementById('date');
                  const titleInput = document.getElementById('title');
                  const fileInput = document.getElementById('file-upload');
                  const uploadButton = document.getElementById('<%= uploadButton.ClientID %>'); // Ensures correct button reference with ClientID in ASP.NET
            const dropArea = document.getElementById('file-drop-area');
            const uploadIcon = document.getElementById('upload-icon');
            const uploadText = document.getElementById('upload-text');

            // Function to check if all fields are filled
            function checkFields() {
                if (dateInput.value && titleInput.value && fileInput.files.length > 0) {
                    const file = fileInput.files[0];
                    if (file.type === "application/pdf") {
                        uploadButton.disabled = false;
                        uploadButton.style.backgroundColor = ""; // Reset background color
                        uploadButton.style.cursor = "pointer"; // Change cursor to pointer
                    } else {
                        alert("Only PDF files are allowed.");
                        uploadButton.disabled = true;
                        uploadButton.style.backgroundColor = "grey"; // Grey out the button
                        uploadButton.style.cursor = "not-allowed"; // Change cursor to not-allowed
                    }
                } else {
                    uploadButton.disabled = true;
                    uploadButton.style.backgroundColor = "grey"; // Grey out the button
                    uploadButton.style.cursor = "not-allowed"; // Change cursor to not-allowed
                }
            }

            // Add event listeners to inputs to trigger field check
            dateInput.addEventListener('input', checkFields);
            titleInput.addEventListener('input', checkFields);
            fileInput.addEventListener('change', function () {
                if (fileInput.files.length > 0) {
                    const file = fileInput.files[0];
                    const fileName = file.name;
                    uploadText.textContent = `Selected file: ${fileName}`; // Change text to file name
                    uploadIcon.style.display = 'none'; // Hide the icon

                    // Check if the selected file is a PDF
                    if (file.type !== "application/pdf") {
                        alert("Only PDF files are allowed.");
                        fileInput.value = ""; // Clear the input
                        uploadText.textContent = "Drag and Drop Here Or Browse For a File"; // Reset the text
                    }
                }
                checkFields();
            });

            // Handle click to trigger file selection
            dropArea.addEventListener('click', function () {
                fileInput.click(); // Trigger the hidden file input click
            });

            // Add drag and drop functionality
            ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
                dropArea.addEventListener(eventName, preventDefaults, false);
            });

            function preventDefaults(e) {
                e.preventDefault();
                e.stopPropagation();
            }

            // Handle dropped files
            dropArea.addEventListener('drop', handleDrop, false);

            function handleDrop(e) {
                const dt = e.dataTransfer;
                const files = dt.files;

                fileInput.files = files; // Assign the dropped file to the hidden input
                const fileName = fileInput.files[0].name;
                const file = fileInput.files[0];
                uploadText.textContent = `Selected file: ${fileName}`; // Change text to file name
                uploadIcon.style.display = 'none'; // Hide the icon

                // Check if the dropped file is a PDF
                if (file.type !== "application/pdf") {
                    alert("Only PDF files are allowed.");
                    fileInput.value = ""; // Clear the input
                    uploadText.textContent = "Drag and Drop Here Or Browse For a File"; // Reset the text
                }

                checkFields();
            }
        });
    </script>
    <script>
        function submitNewsletterForm() {
            // Manually submit the form to trigger the server-side event
            const form = document.createElement('form');
            form.method = 'POST';
            form.action = window.location.href;  // Submit to the current page

            // Add the title input
            const titleInput = document.createElement('input');
            titleInput.type = 'hidden';
            titleInput.name = 'title';
            titleInput.value = document.getElementById('title').value;
            form.appendChild(titleInput);

            // Add the date input
            const dateInput = document.createElement('input');
            dateInput.type = 'hidden';
            dateInput.name = 'date';
            dateInput.value = document.getElementById('date').value;
            form.appendChild(dateInput);

            // Add the file input
            const fileInput = document.createElement('input');
            fileInput.type = 'hidden';
            fileInput.name = 'fileUpload';
            fileInput.value = document.getElementById('file-upload').files[0]; // This will only pass the file name

            form.appendChild(fileInput);

            document.body.appendChild(form); // Append the form to the body
            form.submit();  // Submit the form
        }
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
