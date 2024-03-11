using BoanMusicApp.BO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

public class PremiumSubscriptionDAL
{
    private string connectionString;

    public PremiumSubscriptionDAL()
    {
        // Retrieve connection string from web.config
        connectionString = ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
    }

    public void PurchasePremiumSubscription(int userID, int premiumFeatureID, int subscriptionPlanID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the stored procedure name
            string storedProcedure = "PurchasePremiumSubscription";

            // Define the parameters for the stored procedure
            var parameters = new
            {
                UserID = userID,
                PremiumFeatureID = premiumFeatureID,
                SubscriptionPlanID = subscriptionPlanID
            };

            try
            {
                // Execute the stored procedure using Dapper
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                // Handle the exception 
                Console.WriteLine("Error: " + ex.Message);
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
    }
    public List<SubscriptionPlan> GetSubscriptionPlans()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return connection.Query<SubscriptionPlan>("SELECT [Subscription_Plan_ID], [Name], [Price], [Description] FROM [Person].[Subscription_Plan]").AsList();
        }
    }

    public List<PremiumFeature> GetPremiumFeatures()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return connection.Query<PremiumFeature>("SELECT [Premium_Feature_ID], [Name] FROM [Person].[Premium_Features]").AsList();
        }
    }
    // Other methods for retrieving data as needed
}
