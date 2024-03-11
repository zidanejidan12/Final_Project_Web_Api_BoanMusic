Imports System.Configuration
Imports BoanMusicApp.BLL
Imports BoanMusicApp.BO

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Check if the user is already authenticated
        If Session("Authenticated") IsNot Nothing AndAlso CBool(Session("Authenticated")) Then
            ' Check user type and redirect accordingly
            Dim userType As String = Session("UserType").ToString()
            Select Case userType
                Case "regular"
                    Response.Redirect("RegularPage.aspx")
                Case "premium"
                    Response.Redirect("PremiumPage.aspx")
            End Select
        End If
    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs)
        Response.Redirect("RegistrationPage.aspx")
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
        Dim userBLL As New UserBLL(connectionString)

        ' Authenticate user
        Dim authenticationResult As AuthenticationResult = userBLL.AuthenticateUser(username, password)

        If authenticationResult.IsAuthenticated Then
            ' Set session variables
            Session("Authenticated") = True
            Session("UserType") = authenticationResult.User_Type
            Session("UserID") = authenticationResult.User_ID
            Session("Name") = authenticationResult.Name

            ' Redirect based on user type
            Select Case authenticationResult.User_Type
                Case "premium"
                    Response.Redirect("PremiumPage.aspx")
                Case "regular"
                    Response.Redirect("RegularPage.aspx")
                Case Else
                    ' Redirect to default page if user type is not recognized
                    Response.Redirect("Default.aspx")
            End Select
        Else
            ' Show error message for invalid username or password
            lblMessage.Visible = True
            lblMessage.Text = "Invalid username or password."
        End If
    End Sub
End Class
