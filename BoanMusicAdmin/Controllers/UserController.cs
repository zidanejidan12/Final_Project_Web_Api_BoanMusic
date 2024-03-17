using BoanMusicApp.BLL;
using BoanMusicApp.BO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X.PagedList;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BoanMusicApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserBLL _userBLL;

        public UserController(UserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        public IActionResult GetUsers(int? page)
        {
            const int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<User> users = _userBLL.GetUsers();
            IPagedList<User> pagedUsers = users.ToPagedList(pageNumber, pageSize);

            return View(pagedUsers);
        }

        [HttpGet("{userID}")]
        public IActionResult GetUserByUserID(int userID)
        {
            var user = _userBLL.GetUsersbyUserID(userID);
            if (user == null)
            {
                return NotFound(); // Return 404 if user is not found
            }
            return Ok(user); // Return user object if found
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userBLL.AuthenticateUser(username, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.User_Type),
                    new Claim("UserID", user.User_ID.ToString()) // Add user ID as a claim
                    // Add more claims as needed
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect to the desired page after login
                return RedirectToAction("Index", "Home");
            }

            // Authentication failed
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User"); // Redirect to login page after logout
        }

        [Authorize] // Require authentication to access this action
        public IActionResult Profile()
        {
            // Retrieve the user ID from claims
            var userID = User.FindFirstValue("UserID");

            if (userID == null)
            {
                return NotFound(); // User ID not found in claims
            }

            // Retrieve the user information based on the user ID using the stored procedure
            var user = _userBLL.GetUsersbyUserID(int.Parse(userID));

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return View(user); // Render the Profile view with the user object
        }

        [HttpPost]
        public IActionResult UpdateProfile(int userId, string name, string email, DateTime? dateOfBirth, IFormFile profileImage, string password)
        {
            byte[]? profileImageData = null;

            // Convert profile image to byte array if provided
            if (profileImage != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    profileImage.CopyTo(ms);
                    profileImageData = ms.ToArray();
                }
            }

            // Call the UpdateUser method from BLL
            _userBLL.UpdateUser(userId, name, email, dateOfBirth, profileImageData, password);

            // Update the username claim in the authentication cookie
            var userIdClaim = User.FindFirstValue("UserID");
            if (userIdClaim != null)
            {
                // Retrieve the updated user information
                var updatedUser = _userBLL.GetUsersbyUserID(int.Parse(userIdClaim));

                // Find the username claim
                var usernameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    // Update the username claim value
                    var identity = (ClaimsIdentity)User.Identity;
                    identity.RemoveClaim(usernameClaim);
                    identity.AddClaim(new Claim(ClaimTypes.Name, updatedUser.Name));

                    // Update the authentication cookie
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
            }

            // Redirect to the Profile action to view the updated profile
            return RedirectToAction("Profile");
        }

        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [Route("register")]
        public IActionResult Register(string name, string email, string password, DateTime dateOfBirth, byte[] profileImage, string userType)
        {
            try
            {
                _userBLL.CreateNewUser(name, email, password, dateOfBirth, profileImage, userType);
                // Redirect to a success page or return a success message
                return RedirectToAction("Login", "User");
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "An error occurred while creating the user. Please try again.");
                // Return the view with the model to display validation errors
                return View();
            }
        }
    }
}
