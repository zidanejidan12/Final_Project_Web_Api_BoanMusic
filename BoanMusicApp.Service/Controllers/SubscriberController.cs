using Microsoft.AspNetCore.Mvc;
using BoanMusicApp.BLL;
using BoanMusicAdmin.Models;

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
        public IActionResult PurchaseSubscription(SubscriptionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _premiumSubscriptionBLL.PurchasePremiumSubscription(model.UserID, model.Premium_Feature_ID, model.Subscription_Plan_ID);
                return Ok("Premium subscription purchased successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while purchasing the premium subscription. Please try again.");
            }
        }
    }
}
