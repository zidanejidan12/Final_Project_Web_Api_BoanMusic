Imports System
Imports System.Collections.Generic
Imports BoanMusicApp.BO
Imports BoanMusicApp.BLL
Imports System.Configuration

Public Class PremiumPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindTracks()
        End If
    End Sub

    Private Sub BindTracks()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
        Dim trackBLL As New TrackBLL(connectionString) ' Create an instance of TrackBLL with connection string
        Dim tracks As List(Of Track) = trackBLL.GetTracks() ' Get tracks from the BLL

        ' Bind tracks to the repeater control
        rptTracks.DataSource = tracks
        rptTracks.DataBind()
    End Sub

    Public Sub btnViewPlaylists_Click(sender As Object, e As EventArgs)
        Response.Redirect("PlaylistPage.aspx")
    End Sub

    Protected Sub btnEditProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditProfile.Click
        ' Redirect the user to the edit profile page
        Response.Redirect("EditProfile.aspx")
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        Dim searchQuery As String = txtSearch.Text.Trim()
        If Not String.IsNullOrEmpty(searchQuery) Then
            BindSearchedTracks(searchQuery)
        Else
            ' If the search query is empty, bind all tracks
            BindTracks()
        End If
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        ' Clear session variables
        Session.Clear()
        Session.Abandon()

        ' Redirect to the login page
        Response.Redirect("Login.aspx")
    End Sub

    Private Sub BindSearchedTracks(searchQuery As String)
        ' Call the BLL method to search for tracks
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
        Dim trackBLL As New TrackBLL(connectionString)

        ' Get the tracks based on the search query
        Dim searchedTracks As List(Of Track) = trackBLL.SearchTracks(searchQuery)

        ' Bind the searched tracks to the repeater control
        rptTracks.DataSource = searchedTracks
        rptTracks.DataBind()
    End Sub

End Class
