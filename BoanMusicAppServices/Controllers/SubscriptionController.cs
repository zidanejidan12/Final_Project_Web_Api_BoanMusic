using Microsoft.AspNetCore.Mvc;
using BoanMusicApp.BLL;
using BoanMusicAdmin.Models;
using Microsoft.AspNetCore.Authorization;

namespace BoanMusicApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribeController : ControllerBase
    {
        private readonly PremiumSubscriptionBLL _premiumSubscriptionBLL;

        public SubscribeController(PremiumSubscriptionBLL premiumSubscriptionBLL)
        {
            _premiumSubscriptionBLL = premiumSubscriptionBLL;
        }

        [HttpGet("subscription-plans")]
        public IActionResult GetSubscriptionPlans()
        {
            var subscriptionPlans = _premiumSubscriptionBLL.GetSubscriptionPlans();
            return Ok(subscriptionPlans);
        }

        [HttpGet("premium-features")]
        public IActionResult GetPremiumFeatures()
        {
            var premiumFeatures = _premiumSubscriptionBLL.GetPremiumFeatures();
            return Ok(premiumFeatures);
        }

        [HttpPost("purchase")]
        [Authorize]
        public IActionResult PurchaseSubscription([FromBody] SubscriptionViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _premiumSubscriptionBLL.PurchasePremiumSubscription(request.UserID, request.Premium_Feature_ID, request.Subscription_Plan_ID);
                return Ok("Premium subscription purchased successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while purchasing the premium subscription. Please try again.");
            }
        }

        public class SubscriptionRequestModel
        {
            public int UserID { get; set; }
            public int Subscription_Plan_ID { get; set; }
            public int Premium_Feature_ID { get; set; }
        }

    }
}
