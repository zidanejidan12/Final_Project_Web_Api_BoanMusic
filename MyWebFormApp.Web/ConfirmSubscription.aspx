<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="ConfirmSubscription.aspx.vb" Inherits="MyWebFormApp.Web.ConfirmSubscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirm Subscription</title>
    <!-- Add Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet"/>

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

    .container {
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

    input[type="text"], 
    input[type="email"], 
    input[type="password"], 
    input[type="date"],
    .btn {
        width: 100%;
        padding: 8px;
        font-size: 16px;
        border-radius: 5px;
        border: 1px solid #ccc;
        margin-bottom: 10px;
        background-color: #333;
        color: #fff;
        box-sizing: border-box; /* Ensures padding and border are included in width */
    }

    .btn {
        background-color: #4CAF50;
        border: none;
        cursor: pointer;
    }

    .btn-secondary {
        background-color: #555;
    }

    .text-center {
        text-align: center;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h1 class="text-center mb-4">Confirm Subscription</h1>
            <div class="alert alert-info">
                Are you sure you want to become a premium user?
            </div>
            <div class="form-group">
                <label for="ddlSubscriptionPlan">Select Subscription Plan:</label>
                <asp:DropDownList ID="ddlSubscriptionPlan" runat="server" DataTextField="Name" DataValueField="Subscription_Plan_ID" CssClass="form-control" >
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="ddlPremiumFeature">Select Premium Feature:</label>
                <asp:DropDownList ID="ddlPremiumFeature" runat="server" DataTextField="Name" DataValueField="Premium_Feature_ID" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="text-center">
                <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-success mr-2" OnClick="btnConfirm_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
            </div>
        </div>
    </form>
    <!-- Include Bootstrap JS -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
