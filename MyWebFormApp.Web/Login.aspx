<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="MyWebFormApp.Web.Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login</title>
    <style>
        /* Center the form horizontally */
        #form1 {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #181818;
            color: #fff;
            margin: 0;
            padding: 0;
        }

        /* Style the login container */
        .login-container {
            width: 50%;
            margin: auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #282828;
            text-align: center; /* Center the contents horizontally */
        }

            /* Style the input fields */
            .login-container input[type=text],
            .login-container input[type=password],
            .login-container input[type=email],
            .login-container input[type=date] {
                width: calc(100% - 16px); /* Adjust for padding and border */
                padding: 8px;
                font-size: 16px;
                border-radius: 5px;
                border: 1px solid #ccc;
                margin-bottom: 10px;
                background-color: #333;
                color: #fff;
                box-sizing: border-box; /* Include padding and border in width calculation */
            }

            /* Style the login button */
            .login-container input[type=submit],
            .login-container #btnRegister {
                width: calc(100% - 40px); /* Reduce the width */
                padding: 6px 16px; /* Adjust padding */
                font-size: 14px; /* Adjust font size */
                border-radius: 5px;
                border: none;
                background-color: #4CAF50; /* Change the background color */
                color: white;
                cursor: pointer;
                margin-top: 10px; /* Adjust margin */
            }

            /* Style the registration button */
            .login-container #btnRegister {
                background-color: #3498db; /* Change the background color */
                margin-top: 10px; /* Adjust margin */
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Login</h2>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
            </div>
            <div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            </div>
            <!-- Add the "Create a New User" button -->
            <div>
                <asp:Button ID="btnRegister" runat="server" Text="Create a New User" OnClick="btnRegister_Click" />
            </div>
        </div>
    </form>
</body>
</html>
