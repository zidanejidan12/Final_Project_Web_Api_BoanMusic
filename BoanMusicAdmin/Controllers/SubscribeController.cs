using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoanMusicApp.BLL;
using BoanMusicAdmin.Models;
using System.Reflection;

namespace BoanMusicApp.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly PremiumSubscriptionBLL _premiumSubscriptionBLL;

        public SubscribeController(PremiumSubscriptionBLL premiumSubscriptionBLL)
        {
            _premiumSubscriptionBLL = premiumSubscriptionBLL;
        }

        public IActionResult Index()
        {
            // Retrieve the UserID from the claim
            var userIDClaim = HttpContext.User.FindFirst("UserID");
            if (userIDClaim != null && int.TryParse(userIDClaim.Value, out int userID))
            {
                // You can use the userID as needed, for example, to personalize the subscription plans
                // or to filter plans based on the user's preferences.
                // If not needed in this action, you can skip this part.
            }

            // Get subscription plans and premium features
            var subscriptionPlans = _premiumSubscriptionBLL.GetSubscriptionPlans();
            var premiumFeatures = _premiumSubscriptionBLL.GetPremiumFeatures();

            // Populate ViewBag with subscription plans and premium features
            ViewBag.SubscriptionPlans = subscriptionPlans;
            ViewBag.PremiumFeatures = premiumFeatures;

            return View();
        }

        public IActionResult PurchaseSubscription()
        {
            // Retrieve the UserID from the claim
            var userIDClaim = HttpContext.User.FindFirst("UserID");
            if (userIDClaim == null || !int.TryParse(userIDClaim.Value, out int userID))
            {
                // Handle the case where UserID is not found in the claim or cannot be parsed
                ViewBag.ErrorMessage = "User ID not found or invalid.";
                return View("Login", "User"); // Redirect to the login page or handle as appropriate
            }
            SubscriptionViewModel model = new SubscriptionViewModel();

            // Repopulate the view model with data
            var subscriptionPlans = _premiumSubscriptionBLL.GetSubscriptionPlans();
            var premiumFeatures = _premiumSubscriptionBLL.GetPremiumFeatures();
            model.SubscriptionPlans = subscriptionPlans;
            model.PremiumFeatures = premiumFeatures;
            model.UserID = userID;
            return View(model);
        }

        [HttpPost]
        public IActionResult PurchaseSubscription(int Subscription_Plan_ID, int UserID, int Premium_Feature_ID)
        {
            try
            {
                _premiumSubscriptionBLL.PurchasePremiumSubscription(UserID, Premium_Feature_ID, Subscription_Plan_ID);
                ViewBag.SuccessMessage = "Premium subscription purchased successfully!";
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewBag.ErrorMessage = "An error occurred while purchasing the premium subscription. Please try again.";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
