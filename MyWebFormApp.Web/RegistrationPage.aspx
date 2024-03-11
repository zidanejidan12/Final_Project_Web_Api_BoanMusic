<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegistrationPage.aspx.vb" Inherits="MyWebFormApp.Web.RegistrationPage" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Registration</title>
    <style>
        /* Center the form horizontally */
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

        /* Style the registration container */
        .registration-container {
            width: 50%;
            margin: auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #282828;
        }

            /* Style the input fields */
            .registration-container input[type=text],
            .registration-container input[type=password],
            .registration-container input[type=email],
            .registration-container input[type=date] {
                width: 100%;
                padding: 10px;
                margin: 5px 0;
                box-sizing: border-box;
                border-radius: 5px;
                border: 1px solid #ccc;
                font-size: 16px;
                background-color: #333;
                color: #fff;
            }

            /* Style the registration button */
            .registration-container input[type=submit] {
                width: 100%;
                padding: 10px;
                margin: 10px 0;
                box-sizing: border-box;
                border-radius: 5px;
                border: none;
                background-color: #4CAF50;
                color: white;
                font-size: 16px;
                cursor: pointer;
            }
    </style>
    <script>
        function showAlert(message) {
            alert(message);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="registration-container">
            <h2>Registration</h2>
            <!-- Add labels and textboxes for registration -->
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div>
                <!-- Add textboxes for user input -->
                <asp:TextBox ID="txtNewUserName" runat="server" placeholder="Username"></asp:TextBox>
            </div>
            <div>
                <asp:TextBox ID="txtNewUserEmail" runat="server" placeholder="Email"></asp:TextBox>
            </div>
            <div>
                <asp:TextBox ID="txtNewUserPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>
            <div>
                <asp:TextBox ID="txtNewUserDateOfBirth" runat="server" placeholder="Date of Birth (YYYY/MM/DD)"></asp:TextBox>
            </div>
            <div>
                <asp:FileUpload ID="fuProfileImage" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnAddUser" runat="server" Text="Register" OnClick="btnAddUser_Click" />
            </div>
            <!-- Add the "I Have an Account" button -->
            <div>
                <asp:Button ID="btnLoginRedirect" runat="server" Text="I Have an Account" OnClick="btnLoginRedirect_Click" />
            </div>
        </div>
    </form>
</body>
</html>
