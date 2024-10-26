﻿<%@ Page Title="Exhortations" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="Exhortations.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Exhortations" %>

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
                            <asp:Button ID="DetailsButton" runat="server"
                                CommandName="ShowDetails"
                                CommandArgument='<%# Eval("ExhortationID") %>'
                                Text="Details >" />
                            <button id="playButton_<%# Eval("ExhortationID") %>" class="play-button"
                                onclick="togglePlayPause('<%# Eval("ExhortationID") %>')">
                                ▶</button>
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


            <div class="audio-player">
                <audio id="AudioPlayer" controls runat="server">
                    <source id="AudioSource" type="audio/mpeg" />
                </audio>
            </div>

            <br />

            <div style="text-align: center;">
                <h4 class="section-heading">Summary</h4>

            <p>
                <asp:Label ID="SummaryLabel" runat="server" Text="This is an autogenerated summary..." />
            </p>
            </div>




            <div style="text-align: center;">
                <h4 class="section-heading">Transcription</h4>
                <p>
                    <asp:Label ID="TranscriptionLabel" runat="server" Text="This is an autogenerated transcript..." />
                </p>
            </div>


            
        </div>




    </div>




    <script type="text/javascript">
        var currentAudio = null;
        var currentExhortationId = null;

        function togglePlayPause(exhortationId) {
            var playButton = document.getElementById("playButton_" + exhortationId);

            if (currentExhortationId === exhortationId && currentAudio !== null) {
                if (currentAudio.paused) {
                    currentAudio.play();
                    playButton.innerHTML = "❚❚";
                } else {
                    currentAudio.pause();
                    playButton.innerHTML = "▶";
                }
            } else {
                if (currentAudio !== null) {
                    currentAudio.pause();
                    var previousButton = document.getElementById("playButton_" + currentExhortationId);
                    if (previousButton) {
                        previousButton.innerHTML = "▶";
                    }
                }

                currentExhortationId = exhortationId;
                currentAudio = new Audio("DownloadExhortation.aspx?id=" + exhortationId);
                currentAudio.addEventListener('canplaythrough', function () {
                    currentAudio.play();
                    playButton.innerHTML = "❚❚";
                });

                currentAudio.onended = function () {
                    playButton.innerHTML = "▶";
                    currentAudio = null;
                    currentExhortationId = null;
                };
            }
        }


        function loadExhortationDetails(exhortationId) {
            // Make an AJAX call to fetch the details from the server
            fetch(`GetExhortationDetails.aspx?id=${exhortationId}`)
                .then(response => response.json())
                .then(data => {
                    if (data) {
                        // Populate the details section with the retrieved data
                        document.querySelector(".exhortationDisplay h3").innerText = data.Title;
                        document.querySelector(".exhortationDisplay h4.section-heading").innerText = data.Description;
                        document.querySelector(".audio-header .speaker").innerText = data.Speaker;
                        document.querySelector(".audio-header .date").innerText = new Date(data.Date).toLocaleDateString();

                        // Set the audio player source (if needed)
                        const audioPlayer = document.querySelector(".audio-player audio");
                        if (audioPlayer) {
                            audioPlayer.src = `DownloadExhortation.aspx?id=${exhortationId}`;
                        }
                    }
                })
                .catch(error => {
                    console.error('Error fetching exhortation details:', error);
                });
        }
    </script>

</asp:Content>
