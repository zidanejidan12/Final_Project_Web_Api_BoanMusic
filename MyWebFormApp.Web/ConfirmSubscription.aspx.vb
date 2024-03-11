Imports System.Data.SqlClient
Imports Dapper
Imports BoanMusicApp.BLL ' Import your BLL namespace here

Public Class ConfirmSubscription
    Inherits System.Web.UI.Page

    Private connectionString As String = ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
    Private premiumSubscriptionBLL As New PremiumSubscriptionBLL() ' Create an instance of your BLL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindDropDownLists() ' Bind dropdown lists when page loads for the first time
        End If
    End Sub

    Private Sub BindDropDownLists()
        ' Bind subscription plans and premium features to dropdown lists
        ddlSubscriptionPlan.DataSource = premiumSubscriptionBLL.GetSubscriptionPlans()
        ddlSubscriptionPlan.Items.Insert(0, New ListItem("Select Plan", "-1"))
        ddlSubscriptionPlan.DataBind()

        ddlPremiumFeature.DataSource = premiumSubscriptionBLL.GetPremiumFeatures()
        ddlPremiumFeature.Items.Insert(0, New ListItem("Select Feature", "-1"))
        ddlPremiumFeature.DataBind()
    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click
        Dim userID As Integer = Convert.ToInt32(Session("UserID"))
        Dim premiumFeatureID As Integer = CInt(ddlPremiumFeature.Text)
        Dim subscriptionPlanID As Integer = CInt(ddlSubscriptionPlan.Text)

        ' Call the method to purchase premium subscription from BLL
        premiumSubscriptionBLL.PurchasePremiumSubscription(userID, premiumFeatureID, subscriptionPlanID)

        Session("UserType") = "premium"

        ' Redirect the user to a success page
        Response.Redirect("PremiumPage.aspx")
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        ' Logic to handle cancellation of subscription
        ' For example, redirect the user back to the previous page
        Response.Redirect("RegularPage.aspx")
    End Sub
End Class
