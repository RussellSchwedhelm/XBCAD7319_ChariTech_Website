<%@ Page Title="Exhortations" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Exhortations.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Exhortations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container">
        <!-- Exhortations Section -->
        <div class="section">
            <div style="text-align: center;">
            <h3 class="headings">Exhortations</h3>
                <div class="search-container" >
                    <asp:TextBox ID="txtSearchQuery2" runat="server" CssClass="search-box" placeholder="Search..."></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="search-button" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>

            <div class="exhortation-list">
                <asp:Repeater ID="ExhortationListRepeater" runat="server" OnItemCommand="ExhortationListRepeater_ItemCommand">
                    <ItemTemplate>
                        <div class="exhortation-item">
                            <div class="exhortation-info">
                                <p><%# Eval("Title") %> | <%# Eval("Date", "{0:MM-dd-yyyy}") %></p>
                                <p>Speaker: <%# Eval("Speaker") %></p>
                            </div>
                            <asp:LinkButton ID="DetailsLink" runat="server"
                                CommandName="LoadAudio"
                                CommandArgument='<%# Eval("ExhortationID") %>'
                                Text="Details >" CssClass="details-link" />


                            <asp:Button ID="PlayButton" runat="server"
                                CommandName="PlayAudio"
                                CommandArgument='<%# Eval("ExhortationID") %>'
                                Text="▶" CssClass="play-button"
                                data-exhortationid='<%# Eval("ExhortationID") %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="section exhortationDisplay">
            <div style="text-align: center;">
                <h3>
                    <asp:Label ID="TitleLabel" runat="server" Text="*Talk Title*" /></h3>
            </div>
            <h5 class="section-heading" style="margin-left: 10%;">Speaker:
                <asp:Label ID="SpeakerLabel" runat="server" /></h5>
            <h5 class="section-heading" style="margin-left: 10%;">Date:
                <asp:Label ID="DateLabel" runat="server" /></h5>

            <div class="audio-player" style="display: flex; justify-content: center; align-items: center; margin-top: 20px; height: 50px;">
                <audio id="AudioPlayer" controls>
                    <source id="AudioSource" type="audio/mpeg" src="null" />
                </audio>
            </div>


            <br />

            <div style="text-align: center;">
                <h4 class="section-heading">Summary</h4>
                <div style="max-height: 10rem; overflow-y: auto; padding: 0.5rem; border: 1px solid #ddd;">
                    <p>
                        <asp:Label ID="SummaryLabel" runat="server" Text="Summary Not Available..." />
                    </p>
                </div>
            </div>

            <div style="text-align: center; margin-top: 1rem;">
                <h4 class="section-heading">Transcription</h4>
                <div style="max-height: 15rem; overflow-y: auto; padding: 0.5rem; border: 1px solid #ddd;">
                    <p>
                        <asp:Label ID="TranscriptionLabel" runat="server" Text="Transcription Not Available..." />
                    </p>
                </div>
            </div>

        </div>
    </div>

<script type="text/javascript">
    var currentExhortationId = null;

    function setAudioSource(base64Audio, exhortationId, autoplay = true) {
        const audioPlayer = document.getElementById("AudioPlayer");
        const audioSource = document.getElementById("AudioSource");

        // Set loading spinner and log for debugging
        togglePlayButtonSpinner(exhortationId, true);
        console.log("Loading audio for Exhortation ID:", exhortationId);

        // Set the audio source and reload when ready
        audioSource.src = base64Audio;
        audioPlayer.load();

        // Set the currentExhortationId to the one being played
        currentExhortationId = exhortationId;

        audioPlayer.oncanplaythrough = () => {
            // Remove loading spinner when audio is ready to play
            togglePlayButtonSpinner(exhortationId, false);
            updatePlayButton(autoplay ? "❚❚" : "▶", exhortationId); // Set to appropriate icon based on autoplay
            if (autoplay) {
                audioPlayer.play(); // Start playback only if autoplay is true
            }
        };

        // Update button icons during playback and pause events
        audioPlayer.onplay = () => {
            console.log("Playing audio for Exhortation ID:", exhortationId);
            updatePlayButton("❚❚", exhortationId); // Show pause icon
        };

        audioPlayer.onpause = () => {
            console.log("Paused audio for Exhortation ID:", exhortationId);
            updatePlayButton("▶", exhortationId); // Show play icon
        };

        audioPlayer.onended = () => {
            console.log("Audio ended for Exhortation ID:", exhortationId);
            updatePlayButton("▶", exhortationId); // Reset to play icon
        };
    }

    function updatePlayButton(icon, exhortationId) {
        const playButton = document.querySelector(`button[data-exhortationid="${exhortationId}"]`);
        if (playButton) {
            playButton.innerHTML = icon; // Set icon as innerHTML
            playButton.classList.remove("loading"); // Ensure loading state is removed
            console.log("Updated play button to:", icon);
        } else {
            console.error("Play button not found for Exhortation ID:", exhortationId);
        }
    }

    function togglePlayButtonSpinner(exhortationId, showSpinner) {
        const playButton = document.querySelector(`button[data-exhortationid="${exhortationId}"]`);
        if (playButton) {
            if (showSpinner) {
                playButton.classList.add("loading"); // Add loading class to show overlay
                console.log("Showing loading spinner for Exhortation ID:", exhortationId);
            } else {
                playButton.classList.remove("loading"); // Remove loading class
                console.log("Hiding loading spinner for Exhortation ID:", exhortationId);
            }
        } else {
            console.error("Play button not found for Exhortation ID:", exhortationId);
        }
    }
</script>

</asp:Content>
