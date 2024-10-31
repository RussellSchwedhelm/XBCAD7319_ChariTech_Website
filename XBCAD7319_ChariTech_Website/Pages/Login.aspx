<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>

    <!-- CSS Styles -->
    <style>
        /* Basic Reset */
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: Arial, sans-serif;
        }

        body {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            background-color: #f0f0f0;
        }

        /* Form Container */
        .form-container {
            width: 100%;
            max-width: 400px;
            padding: 2rem;
            background-color: white;
            border-radius: 0.625rem;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.1);
        }

        /* Logo Section */
        .logo-container {
            display: flex;
            justify-content: center;
            margin-bottom: 1.5rem;
        }

        .form-logo {
            width: 80px;
            height: 80px;
        }

        /* Form Styling */
        .registration-form {
            display: flex;
            flex-direction: column;
        }

        .registration-form label {
            font-weight: 200;
            margin-bottom: 0.5rem;
            margin-top: 0.5rem;
        }

        .registration-form input,
        .registration-form select {
            width: 100%;
            padding: 0.625rem;
            margin-bottom: 1rem;
            border: 1px solid #ccc;
            border-radius: 0.3125rem;
            font-size: 1rem;
            border-radius: 0.5rem;
        }

        /* Button Styling */
        .btn {
            background-color: #333;
            color: white;
            padding: 0.75rem;
            width: 100%;
            border: none;
            border-radius: 0.5rem;
            cursor: pointer;
            font-size: 1rem;
            text-align: center;
            transition: background-color 0.3s ease;
            margin-top: 0.625rem; /* Consistent margin for all buttons */
        }

        .btn:hover {
            background-color: #555;
        }

        /* Register Button Styling */
        .register-btn {
            background-color: #333;
            margin: 0px 0px 0px 0px;
        }

        /* Social Login Buttons */
        .google-btn {
            background-color: #db4437;
            color: white;
        }

        .facebook-btn {
            background-color: #3b5998;
            color: white;
        }

        .social-login {
            margin-top: 1.5rem;
            display: flex;
            flex-direction: column;
        }

        .social-login button i {
            margin-right: 0.5rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <!-- Logo Section -->
            <div class="logo-container">
                <img src="~/Images/ChurchLogo.png" alt="Church Logo" class="form-logo" runat="server" />
            </div>

            <!-- Form Section -->
            <div class="registration-form">
                <label for="email">Email</label>
                <input type="email" id="email" name="email" required runat="server" />

                <div class="password-container">
                    <label for="password">Password</label>
                    <input type="password" id="password" name="password" required runat="server" />
                </div>

                <!-- Social Login Options -->
                <div class="social-login">
                    <!-- Add the OnClick attribute to LoginButton -->
                    <asp:Button ID="LoginButton" style="padding: 0.75rem; margin-bottom: 0.625rem;" runat="server" CssClass="btn register-btn" Text="Login" OnClick="LoginButton_Click" />

                    <!-- Register button -->
                    <button type="button" class="btn register-btn" onclick="window.location.href='/Pages/Register.aspx'; return false;">Register</button>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
