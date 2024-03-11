using System;
using System.Collections.Generic;
using System.Text;

namespace BoanMusicApp.BO
{
    public class SubscriptionPlan
    {
        public int Subscription_Plan_ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
