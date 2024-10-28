<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Main Container -->
    <div class="main-container">
        <!-- Left Section: Exhortations -->
        <div class="section">
            <h3 class="headings">Exhortations</h3>
            <!-- Search Bar for Exhortations -->
            <div class="search-container">
                <asp:TextBox ID="txtExhortationSearch" runat="server" CssClass="search-box" placeholder="Search Exhortations..."></asp:TextBox>
                <asp:Button ID="btnExhortationSearch" runat="server" CssClass="search-button" Text="Search" OnClick="btnExhortationSearch_Click" />
            </div>
            <div class="exhortation-list">
                <!-- Dynamic list of Exhortations -->
                <asp:Repeater ID="ExhortationListRepeater" runat="server">
                    <ItemTemplate>
                        <div class="exhortation-item">
                            <div class="exhortation-info">
                                <p><%# Eval("Title") %> | <%# Eval("Date", "{0:MM-dd-yyyy}") %></p>
                                <p>Speaker: <%# Eval("Speaker") %></p>
                            </div>
                            <!-- Details button: Navigate to Exhortations.aspx with autoplay=false -->
                            <a class="details-link" href='<%# "Exhortations.aspx?exhortationId=" + Eval("ExhortationID") + "&autoplay=false" %>'>Details ></a>

                            <!-- Play button: Navigate to Exhortations.aspx with autoplay=true -->
                            <asp:Button ID="PlayButton" runat="server" Text="▶" CommandArgument='<%# Eval("ExhortationID") %>' CssClass="play-button"
                                OnClick="PlayExhortation_Click" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>

        <!-- Middle Section: Ecclesial News -->
        <div class="section">
            <h3 class="headings">Ecclesial News</h3>
            <!-- Search Bar for Ecclesial News -->
            <div class="search-container">
                <asp:TextBox ID="txtNewsSearch" runat="server" CssClass="search-box" placeholder="Search News..."></asp:TextBox>
                <asp:Button ID="btnNewsSearch" runat="server" CssClass="search-button" Text="Search" OnClick="btnNewsSearch_Click" />
            </div>
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
                    <p>
                        <strong>Presiding:</strong>
                        <asp:Label ID="PresidingLabel" runat="server" Text="TBD"></asp:Label>
                    </p>
                    <p>
                        <strong>Exhortation:</strong>
                        <asp:Label ID="ExhortationLabel" runat="server" Text="TBD"></asp:Label>
                    </p>
                    <p>
                        <strong>On The Door:</strong>
                        <asp:Label ID="OnTheDoorLabel" runat="server" Text="TBD"></asp:Label>
                    </p>
                    <asp:Button ID="ViewScheduleButton" runat="server" Text="View Schedule" OnClick="ViewScheduleButton_Click" />
                </div>
            </div>

            <!-- Prayer Requests Section -->
            <div class="section">
                <h5 class="headings">Prayer Requests -
                    <asp:Label ID="TodayDateLabel" runat="server"></asp:Label></h5>
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
                    <div class="donation-grid" style="width: 100%;">
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
        var currentPlayer = null;
        var currentExhortationId = null;

        function togglePlayPause(exhortationId) {
            const playButton = document.getElementById("playButton_" + exhortationId);

            if (currentExhortationId === exhortationId && currentPlayer) {
                // Toggle play/pause if the same exhortation is clicked
                if (currentPlayer.paused) {
                    currentPlayer.play();
                    playButton.innerHTML = "❚❚";
                } else {
                    currentPlayer.pause();
                    playButton.innerHTML = "▶";
                }
            } else {
                // Stop the current player if a new exhortation is clicked
                if (currentPlayer) {
                    currentPlayer.pause();
                    document.getElementById("playButton_" + currentExhortationId).innerHTML = "▶";
                }

                // Fetch audio data through AJAX
                fetch(`/Pages/Home.aspx/GetExhortationAudio?exhortationId=${exhortationId}`, {
                    method: 'GET'
                })
                .then(response => response.arrayBuffer())
                .then(audioBuffer => {
                    const audioBlob = new Blob([audioBuffer], { type: "audio/mpeg" });
                    const audioUrl = URL.createObjectURL(audioBlob);
                    currentPlayer = new Audio(audioUrl);

                    currentExhortationId = exhortationId;
                    
                    // Start playing the new audio file and update UI
                    currentPlayer.play().then(() => {
                        playButton.innerHTML = "❚❚";
                    }).catch(error => {
                        alert("Audio playback failed. Please try again.");
                        playButton.innerHTML = "▶";
                    });
                })
                .catch(error => {
                    alert("There was an error loading the audio. Please try again later.");
                });
            }
        }
    </script>

    <!-- Popup for submitting prayer request -->
    <div id="prayerRequestPopup" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: white; padding: 20px; border: 1px solid #ccc;">
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
