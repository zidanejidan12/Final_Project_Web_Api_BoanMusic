Imports BoanMusicApp.BLL
Imports System.Configuration

Public Class RegistrationPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnAddUser_Click(sender As Object, e As EventArgs)
        Dim userName As String = txtNewUserName.Text.Trim()
        Dim userEmail As String = txtNewUserEmail.Text.Trim()
        Dim userPassword As String = txtNewUserPassword.Text.Trim()
        Dim userDateOfBirth As Date
        Dim userProfileImage As Byte() = Nothing

        ' Validation checks for user input
        If String.IsNullOrEmpty(userName) OrElse String.IsNullOrEmpty(userEmail) OrElse String.IsNullOrEmpty(userPassword) Then
            ShowAlert("Name, Email, and Password cannot be empty.")
            Return
        End If

        If Not userEmail.Contains("@") Then
            ShowAlert("Invalid email address.")
            Return
        End If

        If Not Date.TryParse(txtNewUserDateOfBirth.Text, userDateOfBirth) Then
            ShowAlert("Invalid date of birth format. Please enter the date in YYYY/MM/DD format.")
            Return
        End If

        If fuProfileImage.HasFile AndAlso fuProfileImage.PostedFile.ContentLength > 1048576 Then
            ShowAlert("Profile image size exceeds the limit (1MB).")
            Return
        End If

        ' Call the BLL method to create a new user
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
        Dim userBLL As New UserBLL(connectionString)

        ' Attempt to create a new user
        Try
            userBLL.CreateNewUser(userName, userEmail, userPassword, userDateOfBirth, userProfileImage, "regular")
            ' If successful, show success message and optionally redirect
            ClientScript.RegisterStartupScript(Me.GetType(), "RegistrationSuccess", "showAlert('Registration successful.');", True)
            ' Optionally, you can redirect the user to another page after registration
            Response.Redirect("Login.aspx")
        Catch ex As Exception
            ' If there's an error, show error message
            ShowAlert("Error occurred during registration.")
        End Try
    End Sub

    Protected Sub btnLoginRedirect_Click(sender As Object, e As EventArgs)
        Response.Redirect("Login.aspx")
    End Sub

    Private Sub ShowAlert(message As String)
        ' Implement logic to show alert message to the user
        ' You can use JavaScript or any other method to show the alert
        ClientScript.RegisterStartupScript(Me.GetType(), "RegistrationError", $"alert('{message}');", True)
    End Sub

    Private Sub ClearInputFields()
        ' Implement logic to clear input fields after registration
        txtNewUserName.Text = ""
        txtNewUserEmail.Text = ""
        txtNewUserPassword.Text = ""
        txtNewUserDateOfBirth.Text = ""
    End Sub
End Class
