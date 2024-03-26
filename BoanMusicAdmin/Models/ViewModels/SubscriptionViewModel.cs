using System.Collections.Generic;
using BoanMusicApp.BO;

namespace BoanMusicAdmin.Models
{
public class SubscriptionViewModel
    {
        public int UserID { get; set; }
        public int Subscription_Plan_ID { get; set; }
        public int Premium_Feature_ID { get; set; }
        public List<SubscriptionPlan>? SubscriptionPlans { get; set; }
        public List<PremiumFeature>? PremiumFeatures { get; set; }
    }
}
