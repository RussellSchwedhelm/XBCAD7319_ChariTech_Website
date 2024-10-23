<%@ Page Title="Register" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Register" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Register</title>

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

        /* Logo and Profile Picture Section */
        .logo-container {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 1rem;
            margin-bottom: 1.5rem;
        }

        .form-logo {
            width: 80px;
            height: 80px;
        }

        .profile-picture-container {
            position: relative;
            width: 80px;
            height: 80px;
            border-radius: 50%;
            overflow: hidden;
            cursor: pointer;
        }

        .profile-picture-container img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            border-radius: 50%;
        }

        .edit-icon {
            position: absolute;
            bottom: 0;
            width: 100%;
            height: 20px;
            background-color: rgba(128, 128, 128, 0.8);
            color: white;
            text-align: center;
            font-size: 0.75rem;
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 0.25rem;
        }

        .edit-icon i {
            font-size: 1rem;
        }

        .hidden-upload {
            display: none;
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
        }

        /* Button Styling */
        .btn {
            margin-top: 0.625rem;
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
        }

        .btn:hover {
            background-color: #555;
        }

        /* Social Login Buttons */
        .google-btn {
            background-color: #db4437;
            color: white;
            margin-top: 0.625rem;
            padding: 0.75rem;
            width: 100%;
            border: none;
            border-radius: 0.5rem;
            cursor: pointer;
            font-size: 1rem;
            text-align: center;
        }

        .facebook-btn {
            background-color: #3b5998;
            color: white;
            margin-top: 0.625rem;
            padding: 0.75rem;
            width: 100%;
            border: none;
            border-radius: 0.5rem;
            cursor: pointer;
            font-size: 1rem;
            text-align: center;
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
    <form runat="server">
        <div class="form-container">
            <!-- Logo and Profile Picture Section -->
            <div class="logo-container">
                <img src="~/Images/ChurchLogo.png" alt="Church Logo" class="form-logo" runat="server" />

                <div class="profile-picture-container" onclick="document.getElementById('profilePictureUpload').click();">
                    <asp:Image ID="profilePicture" runat="server" ImageUrl="~/Images/default_profile.png" />
                    <div class="edit-icon">
                        <i class="fas fa-pen"></i>
                        <span>Edit</span>
                    </div>
                    <asp:FileUpload ID="profilePictureUpload" class="hidden-upload" runat="server" accept="image/*" OnChange="previewProfilePicture();" />
                </div>
            </div>

            <!-- Form Section -->
            <div class="registration-form">
                <label for="firstName">First Name</label>
                <asp:TextBox ID="firstNameInput" runat="server" />

                <label for="surname">Surname</label>
                <asp:TextBox ID="surnameInput" runat="server" />

                <label for="email">Email</label>
                <asp:TextBox ID="emailInput" runat="server" TextMode="Email" />

                <!-- Ecclesia selection -->
                <label for="ecclesia">Ecclesia</label>
                <asp:DropDownList ID="ecclesia" runat="server">
                    <asp:ListItem Value="" Text="Select" />
                </asp:DropDownList>


                <!-- Password fields (hidden for SSO users) -->
                <div id="passwordContainer" runat="server">
                    <label for="password">Password</label>
                    <asp:TextBox ID="passwordInput" runat="server" TextMode="Password" />
                </div>

                <div id="rePasswordContainer" runat="server">
                    <label for="rePassword">Re-Enter Password</label>
                    <asp:TextBox ID="rePasswordInput" runat="server" TextMode="Password" />
                </div>
            </div>

            <div class="social-login">
                <asp:Button ID="registerBtn" runat="server" CssClass="btn register-btn" Text="Register" OnClick="RegisterButton_Click" />
                <asp:Button ID="loginBtn" runat="server" CssClass="btn register-btn" Text="Login" OnClick="LoginButton_Click" />
                <!-- Add provider parameter to specify Google or Facebook login -->
                <button type="button" class="btn google-btn" onclick="location.href='/Pages/LoginExternal.aspx?provider=google'">
                    <i class="fab fa-google"></i> Sign up with Google
                </button>
            </div>
        </div>
    </form>

    <script>
        function previewProfilePicture() {
            const fileInput = document.getElementById('profilePictureUpload');
            const profilePicture = document.getElementById('<%= profilePicture.ClientID %>');

            if (fileInput.files && fileInput.files[0]) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    profilePicture.src = e.target.result;
                };
                reader.readAsDataURL(fileInput.files[0]);
            }
        }
</script>
</body>
</html>
