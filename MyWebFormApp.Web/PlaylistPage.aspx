<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlaylistPage.aspx.vb" Inherits="MyWebFormApp.Web.PlaylistPage" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Playlist Page</title>
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
        .spacer {
            margin-right: 10px; /* Adjust the margin to your preference */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to BoanMusic</h1>
            <hr />

            <!-- Create a new playlist -->
            <asp:TextBox ID="txtNewPlaylistName" runat="server" placeholder="Enter playlist name"></asp:TextBox>
            <asp:Button ID="btnCreatePlaylist" runat="server" Text="Create Playlist" OnClick="btnCreatePlaylist_Click" />

            <!-- Dropdown list to select playlist for adding tracks -->
            <asp:DropDownList ID="ddlPlaylists" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlaylists_SelectedIndexChanged">
            </asp:DropDownList>

            <!-- Button to open the modal -->
            <asp:Button ID="btnAddTrackModal" runat="server" Text="Add Track" data-toggle="modal" data-target="#addTrackModal" />

            <!-- Modal for adding tracks -->
            <div id="addTrackModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content -->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Track to Playlist</h4>
                        </div>
                        <div class="modal-body">
                            <!-- Display tracks and checkboxes here -->
                            <asp:CheckBoxList ID="chkTracks" runat="server"></asp:CheckBoxList>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAddTracks" runat="server" Text="Add Selected Tracks" OnClick="btnAddTracks_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Display tracks -->
            <div class="tracks">
                <!-- Track details will be loaded dynamically from the server -->
                <asp:Literal ID="litTracks" runat="server"></asp:Literal>
            </div>
        </div>
    </form>
</body>


</html>
