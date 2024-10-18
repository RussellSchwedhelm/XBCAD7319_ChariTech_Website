<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="XBCAD7319_ChariTech_Website.Pages.Register" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login/Register - Charitech</title>

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

        /* Password Container Styling */
        .password-container {
            display: flex;
            flex-direction: column;
        }

        /* Password Toggle Styling */
        .password-toggle {
            display: flex;
            align-items: center;
            gap: 0.5rem; /* Adds space between the icon and the text */
            cursor: pointer;
            margin-top: 0.5rem; /* Adds spacing between the second input and the toggle */
            margin-bottom: 0.5rem; /* Adds spacing between the second input and the toggle */
        }

            .password-toggle i {
                font-size: 1.5rem; /* Adjust icon size */
                color: #555;
                transition: color 0.3s ease;
            }

                .password-toggle i:hover {
                    color: #000;
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

        /* Register and Login Buttons */
        .register-btn {
            margin-bottom: 0.625rem;
        }
    </style>
</head>
<body>
    <div class="form-container">
        <!-- Logo Section -->
        <div class="logo-container">
            <img src="~/Images/ChurchLogo.png" alt="Church Logo" class="form-logo" runat="server" />
        </div>

        <!-- Form Section -->
        <div class="registration-form">
            <label for="firstName">First Name</label>
            <input type="text" id="firstName" name="firstName" placeholder="Value" required />

            <label for="surname">Surname</label>
            <input type="text" id="surname" name="surname" placeholder="Value" required />

            <label for="email">Email</label>
            <input type="email" id="email" name="email" placeholder="Value" required />

            <label for="ecclesia">Ecclesia</label>
            <select id="ecclesia" name="ecclesia" required>
                <option value="">Select</option>
                <option value="Ecclesia1">Ecclesia 1</option>
                <option value="Ecclesia2">Ecclesia 2</option>
            </select>

            <div class="password-container">
                <label for="password">Password</label>
                <input type="password" id="password" name="password" placeholder="Value" required />

                <label for="rePassword">Re-Enter Password</label>
                <input type="password" id="rePassword" name="rePassword" placeholder="Value" required />

                <!-- Eye Icon for View Passwords -->
                <div class="password-toggle">
                    <i class="fa fa-eye" id="togglePassword" aria-hidden="true"></i>
                    <span>View Passwords</span>
                </div>
            </div>

            <button type="submit" class="btn register-btn">Register</button>
            <button type="button" class="btn login-btn">Login</button>
        </div>
    </div>

    <!-- Optional JavaScript for Password Toggle -->
    <script>
        const togglePassword = document.getElementById('togglePassword');
        const passwordFields = document.querySelectorAll('#password, #rePassword');

        togglePassword.addEventListener('click', () => {
            passwordFields.forEach(field => {
                const type = field.getAttribute('type') === 'password' ? 'text' : 'password';
                field.setAttribute('type', type);
            });

            // Toggle the icon between 'fa-eye' and 'fa-eye-slash'
            togglePassword.classList.toggle('fa-eye-slash');
        });


    </script>
</body>
</html>
