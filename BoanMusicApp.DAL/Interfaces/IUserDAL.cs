using System;

public interface IUserDAL
{
    void CreateNewUser(string name, string email, string password, DateTime dateOfBirth = default(DateTime), byte[] profileImage = null, string userType = "regular");
}
