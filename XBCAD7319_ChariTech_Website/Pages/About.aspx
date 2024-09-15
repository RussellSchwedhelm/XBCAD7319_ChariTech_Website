<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.About" %>
<asp:Content ID="AboutUsPage" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .main-content {
            display: flex;
            justify-content: space-between;
            width: 100%;
            padding: 20px;
            box-sizing: border-box;
        }
        .left-panel {
            background-color: #e0e0e0;
            padding: 10px;
            border-radius: 5px;
            width: 40%;
        }
        .right-column {
            display: flex;
            flex-direction: column;
            width: 55%;
        }
        .panel {
            background-color: #e0e0e0;
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
        }
        .heading {
            font-size: 1.5em;
            margin-bottom: 10px;
        }
        .text {
            font-size: 1em;
        }
    </style>
    <asp:Panel ID="pnlMission" runat="server" CssClass="main-content">
        <asp:Panel ID="pnl0" runat="server" CssClass="left-panel">
            <div class="heading">Our Mission</div>
            <div class="text">
                <meta charset="utf-8" />
                <span style="white-space:pre-wrap;">Our mission as Christadelphians is to fulfill the charge given by our Lord, as stated in Mark 16:15-16: &quot;Go into all the world and preach the gospel to every creature. He who believes and is baptized will be saved, but he who does not believe will be condemned.&quot;<br /> We are dedicated to preaching the Truth of God&#39;s Word and encouraging and strengthening the lives of our brothers and sisters in Christ with pastoral and welfare assistance.<br /> We believe in the centrality of Jesus Christ&#39;s mission, which includes his divine son-ship, his call to repentance, and the proclamation of God&#39;s kingdom. We acknowledge that Jesus&#39; sacrifice provides the basis for the remission of sins, and we seek salvation through belief in the gospel, repentance, baptism, and obedience to Christ&#39;s teachings.</span></div>
        </asp:Panel>
        <asp:Panel ID="rightColumn" runat="server" CssClass="right-column">
            <asp:Panel ID="pnl1" runat="server" CssClass="panel">
                <div class="heading">Our Vision</div>
                <meta charset="utf-8" />
                <span style="white-space:pre-wrap;">Our vision for our website is to create a resourceful and supportive online community for our members. This includes providing a digital archive for accessing exhortations and newsletters, implementing AI technology to transcribe and summarize content, and offering interactive Bible study courses. We aim to facilitate communication and organization within our community through features like announcements and scheduling tools. Additionally, we seek to support our members through online prayer request submissions and secure donation options.</span></asp:Panel>
            <asp:Panel ID="pnl2" runat="server" CssClass="panel">
                <div class="heading">Code Of Conduct</div>
                <div class="text">
                    <meta charset="utf-8" />
                    <span style="white-space:pre-wrap;">Lorem ipsum odor amet, consectetuer adipiscing elit. Euismod egestas maximus tempor efficitur per, fringilla condimentum. Habitant conubia at magna enim quis, magnis praesent varius. Convallis venenatis hendrerit augue taciti conubia massa quisque. Fusce felis eleifend eget mattis justo neque. Suscipit condimentum ultrices nunc conubia metus quisque integer nisl. Id pulvinar metus dui lorem massa facilisis habitasse viverra.<br /> Tellus consectetur nisl interdum morbi facilisis. Habitant cursus blandit at habitasse vel. Proin massa bibendum litora placerat venenatis vulputate lacinia condimentum class. Vivamus mi per eu sit mattis tincidunt in. Risus commodo neque neque primis penatibus dis arcu donec. Mi vulputate bibendum duis est donec vestibulum justo montes.<br /> Habitant nascetur feugiat condimentum lobortis facilisi. Morbi turpis montes ultricies diam cubilia nam. Nulla dictumst rutrum nullam odio cursus. Curae pretium orci mattis dolor 
                    dolor tempor pellentesque. Scelerisque in cursus augue finibus; natoque inceptos finibus nunc. Eu vitae hac neque tortor magnis augue etiam rhoncus. Taciti donec dis convallis imperdiet; curae molestie. Tortor himenaeos volutpat erat venenatis; interdum accumsan gravida vehicula. Mattis est senectus magnis cursus efficitur.<br /> Pretium proin maximus inceptos mauris nisl tempor vitae. Potenti sed lacus ipsum nunc nostra aliquam. Sagittis felis placerat posuere accumsan mattis ultrices maximus suspendisse. Odio aliquet nunc placerat ac magna consequat; euismod tellus! Turpis tincidunt class augue molestie augue habitant ligula. Iaculis inceptos habitasse nunc taciti nunc dui ullamcorper. Scelerisque auctor parturient habitasse sit finibus malesuada pretium.<br /> Potenti sed lacus ipsum nunc nostra aliquam. Sagittis felis placerat posuere accumsan mattis ultrices maximus suspendisse. Odio aliquet nunc placerat ac magna consequat; euismod tellus! Turpis tincidunt class augue molestie 
                    augue habitant ligula. Iaculis inceptos habitasse nunc taciti nunc dui ullamcorper. Scelerisque auctor parturient habitasse sit finibus malesuada pretium.Potenti sed lacus ipsum nunc nostra aliquam. Sagittis felis placerat posuere accumsan mattis ultrices maximus suspendisse. Odio aliquet nunc placerat ac magna consequat; euismod tellus! Turpis tincidunt class augue molestie augue habitant ligula. Iaculis inceptos habitasse nunc taciti nunc dui ullamcorper. Scelerisque auctor parturient habitasse sit finibus malesuada pretium.<br /> </span></div>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>