<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PremiumPage.aspx.vb" Inherits="MyWebFormApp.Web.PremiumPage" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>SoundCloud</title>
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

        .track {
            display: flex;
            align-items: center;
            padding: 20px;
            border-bottom: 1px solid #282828;
        }

            .track img {
                width: 80px;
                height: 80px;
                margin-right: 20px;
                border-radius: 5px;
            }

        .track-name {
            font-size: 18px;
            margin-bottom: 5px;
        }

        .artist-name {
            font-size: 14px;
            color: #b3b3b3;
        }

        audio {
            width: 100%;
            margin-top: 10px;
        }

        .search-container {
            text-align: center;
            margin-bottom: 20px;
        }

        .search-input {
            padding: 8px;
            font-size: 16px;
            border-radius: 5px;
            border: 1px solid #ccc;
            width: 300px;
        }

        .search-button {
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
    <form id="form1" runat="server">
        <div style="position: absolute; top: 10px; right: 10px; color: #fff;">
            Welcome, <% Response.Write(Session("Name")) %> you are a <% Response.Write(Session("UserType")) %> user
        <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="btn btn-danger ml-2" />
        </div>
        <div>
            <h1>Welcome to BoanMusic</h1>
            <div class="search-container">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="Search by artist, track, or genre"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="search-button" OnClick="btnSearch_Click" />
            </div>
            <hr />
            <asp:Repeater ID="rptTracks" runat="server">
                <ItemTemplate>
                    <div class="track">
                        <asp:Image ID="imgTrack" runat="server" CssClass="track-image" />
                        <asp:Label ID="lblTrackName" runat="server" CssClass="track-name" Text='<%# Eval("Name") %>'></asp:Label>
                        <span>&nbsp;&nbsp;</span>
                        <asp:Label ID="lblArtistName" runat="server" CssClass="artist-name" Text='<%# Eval("ArtistName") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button ID="btnViewPlaylists" runat="server" Text="View Playlists" OnClick="btnViewPlaylists_Click" />

            <!-- Add the button for editing profile -->
            <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" OnClick="btnEditProfile_Click" />
        </div>
    </form>
</body>

</html>
