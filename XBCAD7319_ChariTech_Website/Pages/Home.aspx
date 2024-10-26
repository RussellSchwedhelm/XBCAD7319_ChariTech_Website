<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Main Container -->
    <div class="main-container" style="column-count: 2;">
        <!-- Left Section: Exhortations -->
        <div class="section">
            <h3 class="headings">Exhortations</h3>
            <div class="exhortation-list">
                <!-- Dynamic list of Exhortations -->
                <asp:Repeater ID="ExhortationListRepeater" runat="server">
                    <ItemTemplate>
                        <div class="exhortation-item">
                            <div class="exhortation-info">
                                <p><%# Eval("Title") %> | <%# Eval("Date", "{0:MM-dd-yyyy}") %></p>
                                <p>Speaker: <%# Eval("Speaker") %></p>
                            </div>
                            <a class="details-link" href="#">Details ></a>
                            <!-- Play/Pause button -->
                            <button id="playButton_<%# Eval("ExhortationID") %>" class="play-button" onclick="togglePlayPause('<%# Eval("ExhortationID") %>')">▶</button>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- Middle Section: Ecclesial News -->
        <div class="section">
            <h3 class="headings">Ecclesial News</h3>
            <div class="news-list">
                <!-- Dynamic list of News Letters -->
                <asp:Repeater ID="newsListRepeater" runat="server">
                    <ItemTemplate>
                        <div class="news-item">
                            <div class="news-info">
                                <p><%# Eval("Title") %></p>
                                <p><%# Eval("IssueDate", "{0:MM-dd-yyyy}") %></p>
                            </div>
                            <asp:LinkButton ID="openPdfLinkButton" runat="server" CommandArgument='<%# Eval("NewsletterID") %>'
                                Text="🕮" CssClass="news-button" OnClientClick='<%# "openPdf(" + Eval("NewsletterID") + "); return false;" %>'></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- Right Section: Next Sunday, Prayer Requests, and Donations -->
        <div class="right-section">
            <!-- Next Sunday Section -->
            <div class="section">
                <asp:Label ID="nextSundayTitle" runat="server" CssClass="headings" Text="Next Sunday"></asp:Label>
                <div class="content next-sunday">
                    <p><strong>Presiding:</strong> <asp:Label ID="PresidingLabel" runat="server" Text="TBD"></asp:Label></p>
                    <p><strong>Exhortation:</strong> <asp:Label ID="ExhortationLabel" runat="server" Text="TBD"></asp:Label></p>
                    <p><strong>On The Door:</strong> <asp:Label ID="OnTheDoorLabel" runat="server" Text="TBD"></asp:Label></p>
                    <asp:Button ID="ViewScheduleButton" runat="server" Text="View Schedule" OnClick="ViewScheduleButton_Click" />
                </div>
            </div>

            <!-- Prayer Requests Section -->
            <div class="section">
                <h5 class="headings">Prayer Requests - <asp:Label ID="TodayDateLabel" runat="server"></asp:Label></h5>
                <div class="prayer-requests-home prayer-requests">
                    <asp:Repeater ID="PrayerRequestsRepeater" runat="server">
                        <ItemTemplate>
                            <div><%# Eval("PrayerTarget") %></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <button id="submitPrayerRequestBtn" type="button" onclick="togglePrayerRequestPopup()">Submit Prayer Request</button>
            </div>


            <!-- Online Donations Section -->
            <div class="section">
                <h5 class="headings">Donations</h5>
                <div class="donations">
                    <div class="donation-grid" style="width:100%;">
                        <!-- Blue Bag -->
                        <div>
                            <asp:Label ID="BlueBagLabel" runat="server" Text="Blue Bag - General"></asp:Label>
                        </div>

                        <!-- Red Bag -->
                        <div>
                            <asp:Label ID="RedBagLabel" runat="server" Text="Red Bag - Welfare"></asp:Label>
                        </div>

                        <!-- Rondebosch Food Drive with Progress Bar -->
                        <div>
                            <asp:Label ID="Drive1Label" runat="server" Text="Rondebosch Food Drive"></asp:Label>
                        </div>
                        <div>
                            <div class="progress-bar">
                                <div id="Drive1ProgressBar" class="progress"></div>
                            </div>
                        </div>

                        <!-- New Hymn Books with Progress Bar -->
                        <div>
                            <asp:Label ID="Drive2Label" runat="server" Text="New Hymn Books"></asp:Label>
                        </div>
                        <div>
                            <div class="progress-bar">
                                <div id="Drive2ProgressBar" class="progress"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <!-- Script to open PDF in a new window -->
    <script type="text/javascript">
        function openPdf(newsletterId) {
            var url = "DownloadNewsletter.aspx?id=" + newsletterId;
            window.open(url, '_blank');
        }
    </script>

    <script type="text/javascript">
        // Global variables to track the currently playing audio and its ID
        var currentAudio = null;
        var currentExhortationId = null;

        // Function to toggle between play and pause
        function togglePlayPause(exhortationId) {
            var playButton = document.getElementById("playButton_" + exhortationId);

            // If the same exhortation is playing, toggle between play and pause
            if (currentExhortationId === exhortationId && currentAudio !== null) {
                if (currentAudio.paused) {
                    currentAudio.play();
                    playButton.innerHTML = "❚❚";  // Change to pause icon
                } else {
                    currentAudio.pause();
                    playButton.innerHTML = "▶";  // Change to play icon
                }
            } else {
                // Pause any currently playing audio
                if (currentAudio !== null) {
                    currentAudio.pause();
                    var previousButton = document.getElementById("playButton_" + currentExhortationId);
                    if (previousButton) {
                        previousButton.innerHTML = "▶";  // Reset the previous button to play icon
                    }
                }

                // Play the new exhortation
                currentExhortationId = exhortationId;
                currentAudio = new Audio("DownloadExhortation.aspx?id=" + exhortationId);

                // Add an event listener to start playing only when the audio is ready
                currentAudio.addEventListener('canplaythrough', function () {
                    currentAudio.play();
                    playButton.innerHTML = "❚❚";  // Change to pause icon
                });

                // Handle the case when the audio ends
                currentAudio.onended = function () {
                    playButton.innerHTML = "▶";  // Reset button to play icon when audio ends
                    currentAudio = null;
                    currentExhortationId = null;
                };
            }
        }
    </script>

    <!-- Popup for submitting prayer request -->
    <div id="prayerRequestPopup" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); background-color:white; padding:20px; border:1px solid #ccc;">
        <h4>Submit Prayer Request</h4>
        <asp:TextBox ID="PrayerTargetTextBox" runat="server" Placeholder="Prayer Target" />
        <asp:Button ID="SubmitPrayerRequestButton" runat="server" Text="Submit" OnClick="SubmitPrayerRequestButton_Click" />
        <button type="button" onclick="togglePrayerRequestPopup()">Cancel</button>
    </div>

    <script>
        function togglePrayerRequestPopup() {
            const popup = document.getElementById("prayerRequestPopup");
            popup.style.display = popup.style.display === "none" ? "block" : "none";
        }
    </script>

</asp:Content>
