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
            <h3 class="headings">Prayer Request Review - <asp:Label ID="TodayDateLabel" runat="server"></asp:Label></h3>
            <asp:Repeater ID="PrayerRequestsRepeater" runat="server">
                <ItemTemplate>
                    <div class="prayer-request-item">
                        <asp:HiddenField ID="PrayerRequestId" runat="server" Value='<%# Eval("RequestID") %>' />
                        <label>
                            <%# Eval("PrayerTarget") %>
                            <asp:CheckBox ID="ApprovalCheckBox" runat="server" Checked='<%# Eval("Approved") %>' />
                        </label>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="buttons-group">
                <asp:Button ID="SavePrayerRequestChangesButton" runat="server" Text="Accept Changes" OnClick="SavePrayerRequestChangesButton_Click" />
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
            
            <!-- Blue Bag -->
            <div class="upload-form">
                <label>Blue Bag - </label>
                <asp:TextBox ID="BlueBagCause" runat="server" CssClass="form-control" placeholder="Cause" Text="General" />
            </div>

            <!-- Red Bag -->
            <div class="upload-form">
                <label>Red Bag - </label>
                <asp:TextBox ID="RedBagCause" runat="server" CssClass="form-control" placeholder="Cause" Text="Welfare" />
            </div>

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
                <div class="notification-form exhortation-upload-input-layout">
                    <div class="upload-form">
                        <label for="notification-date">Date</label>
                        <asp:TextBox ID="NotificationDate" runat="server" CssClass="form-control" TextMode="Date" placeholder="--/--/----"></asp:TextBox>
                    </div>
                    <div class="upload-form">
                        <label for="notification-title">Title</label>
                        <asp:TextBox ID="NotificationTitle" runat="server" CssClass="form-control" placeholder="Enter title"></asp:TextBox>
                    </div>
                </div>
                <asp:TextBox ID="NotificationMessage" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" placeholder="Enter notification text"></asp:TextBox>
                <div class="buttons-group">
                    <asp:Button ID="CancelNotificationButton" runat="server" CssClass="btn cancel-btn" Text="Cancel" OnClick="CancelNotification_Click" />
                    <asp:Button ID="PublishNotificationButton" runat="server" CssClass="btn publish-btn" Text="Publish Notification" OnClick="PublishNotificationButton_Click" />
                </div>
            </div>

            <!-- Next Sunday Section with Dynamic Title Date -->
            <div class="dashboard-stack">
                <div class="section bible-course-nav item-wrap">
                    <h3 class="headings" id="nextSundayTitle">Next Sunday</h3>

                    <div class="upload-form">
                        <label for="presiding">Presiding</label>
                        <asp:TextBox ID="PresidingName" runat="server" CssClass="form-control" placeholder="Enter presiding name"></asp:TextBox>
                    </div>

                    <div class="upload-form">
                        <label for="exhortation">Exhortation</label>
                        <asp:TextBox ID="ExhortationName" runat="server" CssClass="form-control" placeholder="Enter exhortation name"></asp:TextBox>
                    </div>

                    <div class="upload-form">
                        <label for="on-the-door">On The Door</label>
                        <asp:TextBox ID="OnTheDoorName" runat="server" CssClass="form-control" placeholder="Enter door person's name"></asp:TextBox>
                    </div>

                    <div class="next-sunday calendar">
                        <label for="next-sunday-date">Next Sunday Date</label>
                        <asp:TextBox ID="NextSundayDate" runat="server" CssClass="form-control" TextMode="Date" placeholder="Select Date" onchange="onDateChange()" />
                    </div>

                    <div class="buttons-group">
                        <asp:Button ID="SaveSundayInfoButton" runat="server" Text="Save Sunday Information" OnClick="SaveSundayInfoButton_Click" />
                        <asp:Button ID="ViewScheduleButton" runat="server" CssClass="btn" Text="View Schedule" OnClick="ViewScheduleButton_Click" />
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

    <!-- Initialize Flatpickr with Sunday-only selection -->
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            flatpickr("#<%= NextSundayDate.ClientID %>", {
                enableTime: false,
                dateFormat: "Y-m-d",
                defaultDate: new Date(),
                "disable": [
                    function (date) {
                        // Return true to disable all non-Sunday dates
                        return date.getDay() !== 0;
                    }
                ]
            });
        });
    </script>
    <script type="text/javascript">
        // JavaScript to handle date change and fetch data via AJAX
        function onDateChange() {
            var selectedDate = document.getElementById('<%= NextSundayDate.ClientID %>').value;

        // Set the title dynamically
        document.getElementById("nextSundayTitle").innerText = "Next Sunday - " + selectedDate;

        // Make AJAX call to fetch data for the selected date
        $.ajax({
            type: "POST",
            url: "AdminDashboard.aspx/GetSundayInfo",
            data: JSON.stringify({ selectedDate: selectedDate }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var data = response.d;
                document.getElementById('<%= PresidingName.ClientID %>').value = data.Presiding || "";
                document.getElementById('<%= ExhortationName.ClientID %>').value = data.Exhortation || "";
                document.getElementById('<%= OnTheDoorName.ClientID %>').value = data.OnTheDoor || "";
            },
            error: function () {
                alert("Error loading data for the selected Sunday.");
            }
        });
        }
</script>
</asp:Content>