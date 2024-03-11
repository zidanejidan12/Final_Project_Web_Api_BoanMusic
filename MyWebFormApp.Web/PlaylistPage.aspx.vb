Imports System.Web.Services
Imports BoanMusicApp.BO
Imports BoanMusicApp.BLL
Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic.ApplicationServices

Public Class PlaylistPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadUserPlaylists()
            LoadTracks()
        End If
    End Sub

    ' Load user's playlists
    Private Sub LoadUserPlaylists()
        Dim UserID As Integer = Convert.ToInt32(Session("UserID"))
        Dim playlistBLL As New PlaylistBLL(ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString)
        Dim playlists As List(Of Playlist) = playlistBLL.GetPlaylistsByUserID(UserID)

        ddlPlaylists.Items.Clear() ' Clear existing items before adding new ones

        For Each playlist As Playlist In playlists
            Dim listItem As New ListItem(playlist.Name, playlist.PlaylistID.ToString())
            ddlPlaylists.Items.Add(listItem)
        Next
    End Sub

    Protected Sub btnAddTracks_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Add your event handling code here
    End Sub

    ' Load tracks
    Private Sub LoadTracks()
        Try
            ' Retrieve the selected playlist ID from the dropdown list
            Dim selectedPlaylistID As Integer = Convert.ToInt32(ddlPlaylists.SelectedValue)

            ' Use the selected playlist ID to retrieve tracks associated with that playlist
            Dim playlistBLL As New PlaylistBLL(ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString)
            Dim tracks As List(Of Track) = playlistBLL.GetTracksByPlaylistID(selectedPlaylistID)

            If tracks IsNot Nothing AndAlso tracks.Count > 0 Then
                litTracks.Text = String.Empty
                For Each track As Track In tracks
                    litTracks.Text &= "<div class='track'>" &
                "<img src='data:image/jpeg;base64," & If(track.Image IsNot Nothing, Convert.ToBase64String(track.Image), "") & "' />" &
                "<span class='track-name'>" & track.Name & "</span>" &
                "<span class='artist-name'>" & track.ArtistName & "</span>" &
                "<button class='btnAddToPlaylist' data-trackid='" & track.TrackID & "'>Add to Playlist</button>" &
                "</div>"
                Next
            Else
                litTracks.Text = "No tracks found."
            End If
        Catch ex As Exception
            litTracks.Text = "Error loading tracks: " & ex.Message
        End Try
    End Sub

    Protected Sub ddlPlaylists_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPlaylists.SelectedIndexChanged
        LoadTracks()
    End Sub

    ' Event handler for creating a new playlist
    Protected Sub btnCreatePlaylist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreatePlaylist.Click
        ' Retrieve UserID from the session
        Dim userID As Integer
        If Session("UserID") IsNot Nothing AndAlso Integer.TryParse(Session("UserID").ToString(), userID) Then
            ' UserID retrieved successfully
            Dim playlistName As String = txtNewPlaylistName.Text.Trim()
            Dim playlistBLL As New PlaylistBLL(ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString)

            ' Create a new playlist with UserID
            playlistBLL.CreatePlaylist(New Playlist With {.UserID = userID, .Name = playlistName})

            ' Refresh the page to reflect the new playlist
            Response.Redirect(Request.Url.AbsoluteUri)
        Else
            ' Handle case where UserID is not available or not valid
            ' You can redirect the user to the login page or display an error message
            Response.Redirect("Login.aspx")
        End If
    End Sub

    ' Web method to add a track to a playlist
    <WebMethod()>
    Public Shared Function AddTrackToPlaylist(ByVal trackID As Integer, ByVal playlistID As Integer) As String
        Dim playlistBLL As New PlaylistBLL(ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString)
        playlistBLL.AddTrackToPlaylist(playlistID, trackID, 0) ' You may need to pass the order as well
        Return "Track added to playlist successfully!"
    End Function
End Class
