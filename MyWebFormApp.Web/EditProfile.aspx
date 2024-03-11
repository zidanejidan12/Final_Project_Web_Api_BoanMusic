<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="EditProfile.aspx.vb" Inherits="MyWebFormApp.Web.EditProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Profile</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #181818;
            color: #fff;
            margin: 0;
            padding: 0;
        }

        h1 {
            font-size: 36px;
            margin-top: 20px;
            margin-bottom: 20px;
            text-align: center;
        }

        .form-container {
            width: 50%;
            margin: auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #282828;
        }

        label {
            display: block;
            font-size: 18px;
            margin-bottom: 5px;
        }

        input[type="text"], input[type="email"], input[type="password"], input[type="date"] {
            width: 100%;
            padding: 8px;
            font-size: 16px;
            border-radius: 5px;
            border: 1px solid #ccc;
            margin-bottom: 10px;
            background-color: #333;
            color: #fff;
        }

        input[type="submit"] {
            padding: 8px 20px;
            font-size: 16px;
            border-radius: 5px;
            border: none;
            background-color: #4CAF50;
            color: white;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="formEditProfile" runat="server">
        <h1>Edit Profile</h1>
        <div class="form-container">
            <label for="txtName">Name:</label>
            <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>

            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" Text=""></asp:TextBox>

            <label for="txtDateOfBirth">Date of Birth:</label>
            <asp:TextBox ID="txtDateOfBirth" runat="server" Text=""></asp:TextBox>

            <label for="FileUploadProfileImage">Profile Image:</label>
            <asp:FileUpload ID="FileUploadProfileImage" runat="server" />

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>

            <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" OnClick="btnUpdateProfile_Click" />
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>

            <asp:Button ID="btnDeleteProfile" runat="server" Text="Delete Account" OnClick="btnDeleteProfile_Click" CssClass="btn btn-danger" />
        </div>
    </form>
</body>
</html>
