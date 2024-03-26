using BoanMusicApp.BO;
using System.Collections.Generic;

namespace BoanMusicApp.BLL
{
    public class PremiumSubscriptionBLL
    {
        private PremiumSubscriptionDAL premiumSubscriptionDAL;

        public PremiumSubscriptionBLL(string connectionString)
        {
            premiumSubscriptionDAL = new PremiumSubscriptionDAL(connectionString);
        }

        public void PurchasePremiumSubscription(int userID, int premiumFeatureID, int subscriptionPlanID)
        {
            // Here you can add any additional validation or business logic before calling the DAL
            premiumSubscriptionDAL.PurchasePremiumSubscription(userID, premiumFeatureID, subscriptionPlanID);
        }

        public List<SubscriptionPlan> GetSubscriptionPlans()
        {
            return premiumSubscriptionDAL.GetSubscriptionPlans();
        }

        public List<PremiumFeature> GetPremiumFeatures()
        {
            return premiumSubscriptionDAL.GetPremiumFeatures();
        }

        // You may add additional methods here for retrieving premium features, subscription plans, etc.
    }
}
