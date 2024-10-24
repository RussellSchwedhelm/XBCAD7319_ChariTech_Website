<%@ Page Title="" Language="C#" MasterPageFile="~/Site Masters/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.About" %>

<asp:Content ID="AboutUsPage" ContentPlaceHolderID="MainContent" runat="server">
    <div class="about-us">
        <div class="right-section centered-content" style="max-width: 40rem; min-height: 100%;">
            <div class="section" style="height: fit-content;">
                <h2 class="headings">Our Mission</h2>
                Our mission as Christadelphians is to fulfill the charge given by our Lord, as stated in Mark 16:15-16: "Go into all the world and preach the gospel to every creature. He who believes and is baptized will be saved, but he who does not believe will be condemned."
                We are dedicated to preaching the Truth of God's Word and encouraging and strengthening the lives of our brothers and sisters in Christ with pastoral and welfare assistance.
                We believe in the centrality of Jesus Christ's mission, which includes his divine son-ship, his call to repentance, and the proclamation of God's kingdom. We acknowledge that Jesus' sacrifice provides the basis for the remission of sins, and we seek salvation through belief in the gospel, repentance, baptism, and obedience to Christ's teachings.
            </div>
            <div class="section" style="height: fit-content">
                <h2 class="headings">Our Vision</h2>
                Our vision for our website is to create a resourceful and supportive online community for our members. This includes providing a digital archive for accessing exhortations and newsletters, implementing AI technology to transcribe and summarize content, and offering interactive Bible study courses. We aim to facilitate communication and organization within our community through features like announcements and scheduling tools. Additionally, we seek to support our members through online prayer request submissions and secure donation options.
            </div>
        </div>
        <div class="section" style="max-width: 40rem;">
            <h2 class="headings">Code Of Conduct</h2>
            <p>The Christadelphians are a global Christian community dedicated to understanding and living according to the Bible. We believe in Jesus Christ as the Son of God and strive to follow his example through worship, Bible study, and service. Our members come from diverse backgrounds, united by a commitment to biblical teachings and the hope of eternal life through Jesus.</p>

            <p>We provide a supportive environment where individuals can grow spiritually, form meaningful relationships, and deepen their relationship with God. As a community, we focus on sharing the gospel message and fostering mutual encouragement, personal responsibility, and love for others.</p>
            <ul>
                <li><strong>Respect and Kindness:</strong> Treat everyone with politeness, respect, and kindness.</li>
                <li><strong>Honesty:</strong> Uphold honesty in all actions to build trust within the community.</li>
                <li><strong>Safety and Well-Being:</strong> Ensure the safety and well-being of all, especially children and vulnerable individuals.</li>
                <li><strong>Healthy Boundaries:</strong> Maintain clear, appropriate boundaries in relationships to avoid favoritism.</li>
                <li><strong>Teamwork in Ministry:</strong> Always work with another adult when serving in sensitive roles, particularly with children.</li>
                <li><strong>Conflict Resolution:</strong> Resolve conflicts in a biblical, Christ-centered manner with a focus on reconciliation.</li>
                <li><strong>Reporting Obligations:</strong> Report any instances of abuse or criminal behavior responsibly and in accordance with the law.</li>
            </ul>
        </div>
    </div>
    <style>
        .about-us {
            flex-grow: 1;
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 1.25rem;
            padding: 1.25rem;
            box-sizing: border-box;
            min-height: fit-content;
            max-width: 80rem;
            align-content: center;
        }

        .right-section {
            display: flex;
            flex-direction: column;
            justify-content: center; /* Center vertically */
            align-items: center; /* Center horizontally */
        }

        /* Responsive Grid for smaller screens */
        @media (max-width: 992px) {
            .about-us {
                grid-template-columns: 1fr;
                grid-template-rows: auto;
                height: auto;
            }
        }
    </style>
</asp:Content>
