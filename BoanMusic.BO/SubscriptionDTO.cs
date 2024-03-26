using System.Collections.Generic;

namespace BoanMusicApp.BO
{
    public class SubscriptionDTO
    {
        public int UserID { get; set; }
        public int SubscriptionPlanID { get; set; }
        public int PremiumFeatureID { get; set; }
        public List<SubscriptionPlan> SubscriptionPlans { get; set; }
        public List<PremiumFeature> PremiumFeatures { get; set; }
    }
}
