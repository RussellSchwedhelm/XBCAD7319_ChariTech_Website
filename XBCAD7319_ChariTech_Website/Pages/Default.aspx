<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Default" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Welcome to ChariTech</title>
    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Welcome to ChariTech</h1>
            <p>
                This is the default landing page for our web application. Here you can find 
                links to various sections of our site, such as Exhortations, Bible Courses, 
                and much more.
            </p>
            <p>
                Feel free to explore the site using the navigation bar at the top of the page.
            </p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/About">Learn more about us</asp:HyperLink>
        </div>
    </form>
</body>
</html>
