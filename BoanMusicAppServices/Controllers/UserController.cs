using BoanMusicApp.BLL;
using BoanMusicApp.BO;
using BoanMusicApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoanMusicApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserBLL _userBLL;
        private readonly IConfiguration _configuration;

        public UserController(UserBLL userBLL, IConfiguration configuration)
        {
            _userBLL = userBLL;
            _configuration = configuration;
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public IActionResult GetUsers(int? page)
        {
            const int pageSize = 10;
            int pageNumber = page ?? 1;

            List<User> users = _userBLL.GetUsers();
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return Ok(pagedUsers);
        }

        [HttpGet("")]
        [Authorize] // Only authenticated users can access this endpoint
        public IActionResult GetUserByUserID([FromQuery(Name = "id")] int userID)
        {
            var user = _userBLL.GetUsersbyUserID(userID);
            if (user == null)
            {
                return NotFound(); // Return 404 if user is not found
            }
            return Ok(user); // Return user object if found
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequestModel model)
        {
            var user = _userBLL.AuthenticateUser(model.Username, model.Password);

            if (user != null)
            {
                // Generate JWT token using JwtHelper
                var token = JwtHelper.GenerateJwtToken(user.User_ID.ToString(), user.Name, user.User_Type, _configuration);

                // Return the token along with any other necessary data
                return Ok(new { Token = token, Name = user.Name, User_Type = user.User_Type });
            }

            // Authentication failed
            return BadRequest("Invalid username or password");
        }


        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            // Perform logout actions here
            return Ok("Logged out successfully");
        }

        [HttpPost("UpdateProfile")]
        [Authorize] // Only authenticated users can access this endpoint
        public IActionResult UpdateProfile(UpdateProfileRequestModel model)
        {
            // Update user profile logic goes here
            return Ok("Profile updated successfully");
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequestModel model)
        {
            try
            {
                _userBLL.CreateNewUser(model.Name, model.Email, model.Password, model.DateOfBirth, model.ProfileImage, model.UserType);
                // Redirect to a success page or return a success message
                return Ok("User registered successfully");
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return BadRequest("An error occurred while creating the user. Please try again.");
            }
        }
    }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UpdateProfileRequestModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte[] ProfileImage { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] ProfileImage { get; set; }
        public string UserType { get; set; }
    }
}
