Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports BoanMusicApp.BLL
Imports BoanMusicApp.BO

Public Class EditProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        LoadUserProfile()
    End Sub

    Protected Sub LoadUserProfile()
        Dim UserID As Integer = Convert.ToInt32(Session("UserID"))
        Dim userBLL As New UserBLL(ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString)
        Dim user As User = userBLL.GetUsersbyUserID(UserID)

        If user IsNot Nothing Then
            ' Populate the form fields with user data
            txtName.Text = user.Name
            txtEmail.Text = user.Email
            txtDateOfBirth.Text = user.Date_of_Birth.ToString("yyyy-MM-dd")
            ' Assuming the PasswordString property exists in your User class
            txtPassword.Text = user.PasswordString
        End If
    End Sub


    Protected Sub btnUpdateProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateProfile.Click
        ' Retrieve user input from the form
        Dim userID As Integer = Convert.ToInt32(Session("UserID"))
        Dim name As String = txtName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim dateOfBirth As DateTime? = Nothing
        If Not String.IsNullOrEmpty(txtDateOfBirth.Text) Then
            dateOfBirth = DateTime.Parse(txtDateOfBirth.Text)
        End If

        Dim profileImage As Byte() = Nothing
        If FileUploadProfileImage.HasFile Then
            Using memoryStream As New MemoryStream()
                FileUploadProfileImage.PostedFile.InputStream.CopyTo(memoryStream)
                profileImage = memoryStream.ToArray()
            End Using
        End If

        Dim password As String = txtPassword.Text.Trim()

        ' Get the connection string from the web.config file
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString

        ' Create an instance of the BLL
        Dim userBLL As New UserBLL(connectionString)

        ' Update the user using the BLL method
        userBLL.UpdateUser(userID, name, email, dateOfBirth, profileImage, password)

        ' Optionally, display a success message
        lblMessage.Text = "User information updated successfully!"
        lblMessage.Visible = True
    End Sub


End Class
