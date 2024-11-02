<%@ Page Title="EditExhortations" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="EditExhortations.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.EditExhortations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container" style="grid-template-columns: 1fr 1fr;">
        <!-- Exhortations Section -->
        <div class="section">
            <div style="text-align: center;">
                <h3 class="heading">Exhortations</h3>
                <div class="search-container">
                    <asp:TextBox ID="txtSearchQuery2" runat="server" CssClass="search-box" placeholder="Search..."></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="search-button" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>

            <!-- Dynamic Repeater for Exhortations List -->
            <asp:Repeater ID="ExhortationListRepeater" runat="server" OnItemCommand="ExhortationListRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="exhortation-item">
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" CommandName="DeleteExhortation" CommandArgument='<%# Eval("ExhortationID") %>' ToolTip="Delete">
                            <asp:Image ID="DeleteButton" runat="server" ImageUrl='<%= ResolveUrl("~/Images/Trash13.png") %>' CssClass="delete-icon" />
                        </asp:LinkButton>
            
                        <div class="exhortation-info">
                            <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' CssClass="title" />
                        </div>
            
                        <asp:LinkButton ID="DetailsLink" runat="server" CommandName="LoadDetails" CommandArgument='<%# Eval("ExhortationID") %>'
                                        Text="Details >" CssClass="details-link" />
                        <asp:Button ID="PlayButton1" runat="server" CommandName="PlayAudio" CommandArgument='<%# Eval("ExhortationID") %>'
                                     Text="▶" CssClass="play-button" data-exhortationid='<%# Eval("ExhortationID") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>

        <!-- Editable Fields for Exhortation Details -->
        <div class="section exhortationDisplay">
           <div class="section exhortationDisplay">
                <div class="editExhortationContainer" style="text-align: center;">
                    <h3>Title:
                        <asp:TextBox ID="txtExhortationTitle" runat="server" Text="*Talk Title*" CssClass="title-textbox" />
                    </h3>
        
                    <h5 class="section-heading">Speaker:
                        <asp:TextBox ID="txtExhortationSpeaker" runat="server" CssClass="speaker-textbox" />
                    </h5>
        
                    <h5 class="section-heading">Date:
                        <asp:TextBox ID="txtExhortationDate" runat="server" CssClass="date-textbox" TextMode="Date" />
                    </h5>

                <div class="audio-player" style="display: flex; justify-content: center; align-items: center; margin-top: 20px; height: 50px;">
                    <audio id="AudioPlayer" controls>
                        <source id="AudioSource" type="audio/mpeg" src="null" />
                    </audio>
                </div>

                <br />

                <div style="text-align: center;">
                    <h4 class="section-heading">Summary</h4>
                    <asp:TextBox ID="txtExhortationSummary" runat="server" TextMode="MultiLine" CssClass="summary-textbox"
                                 Text="Editable Summary..." style="max-height: 10rem; width: 80%; overflow-y: auto; padding: 0.5rem; border: 1px solid #ddd;">
                    </asp:TextBox>
                </div>

                <div style="text-align: center; margin-top: 1rem;">
                    <h4 class="section-heading">Transcription</h4>
                    <asp:TextBox ID="txtExhortationTranscript" runat="server" TextMode="MultiLine" CssClass="transcript-textbox"
                                 Text="Editable Transcript..." style="max-height: 15rem; width: 80%; overflow-y: auto; padding: 0.5rem; border: 1px solid #ddd;">
                    </asp:TextBox>
                </div>

                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-secondary"
                            style="width: 50%; margin-top: 1rem;" OnClick="btnSaveChanges_Click" />
                
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" style="width: 50%; margin-top: 1rem;" OnClick="btnCancel_Click" />
            </div>
        </div>
        </div>
        </div>

    <!-- JavaScript for handling audio play/pause functionality -->
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
