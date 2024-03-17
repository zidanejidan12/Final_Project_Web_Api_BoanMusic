using BoanMusicApp.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BoanMusicApp.BLL
{
    public class UserBLL
    {
        private UserDAL userDAL;

        public UserBLL(string connectionString)
        {
            userDAL = new UserDAL(connectionString);
        }

        public User GetUsersbyUserID(int userID)
        {
            return userDAL.GetUsersbyUserID(userID);
        }

        public void DeleteUser(int userId)
        {
            userDAL.DeleteUser(userId);
        }

        public void UpdateUser(int userId, string name, string email, DateTime? dateOfBirth, byte[] profileImage, string password)
        {
            userDAL.UpdateUser(userId, name, email, dateOfBirth, profileImage, password);
        }

        public List<User> GetUsers()
        {
            return userDAL.GetUsers();
        }

        public AuthenticationResult AuthenticateUser(string username, string password)
        {
            return userDAL.AuthenticateUser(username, password);
        }

        public void CreateNewUser(string name, string email, string password, DateTime dateOfBirth, byte[] profileImage, string userType)
        {
            // Here you can add any additional validation or business logic before calling the DAL
            userDAL.CreateNewUser(name, email, password, dateOfBirth, profileImage, userType);
        }
    }
}
