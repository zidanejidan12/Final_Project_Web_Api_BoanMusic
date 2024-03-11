using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BoanMusicApp.BO;
using Dapper;
using System.Text; // Add this namespace for ConfigurationManager

public class UserDAL : IUserDAL
{
    private string connectionString;

    // Modify the constructor to accept the connection string
    public UserDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public User GetUsersbyUserID(int userID)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM [Person].[Users] WHERE [User_ID] = @UserID";
            var user = db.QueryFirstOrDefault<User>(query, new { UserID = userID });

            return user;
        }
    }

    public void DeleteUser(int userId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM [Person].[Users] WHERE [User_ID] = @User_ID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@User_ID", userId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateUser(int userId, string name, string email, DateTime? dateOfBirth, byte[] profileImage, string password)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string storedProcedureName = "UpdateUser";

            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ProfileImage", profileImage ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Password", password ?? (object)DBNull.Value);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle the exception, log, or rethrow if necessary
                    throw;
                }
            }
        }
    }

    // Method to hash the password string using a secure hashing algorithm (e.g., SHA256)
    private byte[] HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return hashedBytes;
            }
        }

    public void CreateNewUser(string name, string email, string password, DateTime dateOfBirth = default, byte[] profileImage = null, string userType = "regular")
    {
        try
        {
            // Retrieve the connection string from web.config if not provided
            if (string.IsNullOrEmpty(connectionString))
            {
                this.connectionString = ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("CreateNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 100).Value = password;

                    // Handle DBNull for DateOfBirth
                    if (dateOfBirth == default)
                    {
                        command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = dateOfBirth;
                    }

                    // Handle DBNull for ProfileImage
                    if (profileImage == null)
                    {
                        command.Parameters.Add("@ProfileImage", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@ProfileImage", SqlDbType.VarBinary).Value = profileImage;
                    }

                    command.Parameters.Add("@UserType", SqlDbType.NVarChar, 50).Value = userType;

                    // Open connection and execute command
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions here
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public AuthenticationResult AuthenticateUser(string username, string password)
    {
        AuthenticationResult result = new AuthenticationResult();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UserLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        result.IsAuthenticated = true;
                        result.User_Type = reader["User_Type"].ToString();
                        // Retrieve and assign the UserID
                        result.User_ID = Convert.ToInt32(reader["User_ID"]);
                        result.Name = reader["Name"].ToString();
                    }
                    else
                    {
                        result.IsAuthenticated = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception appropriately
            Console.WriteLine("An error occurred during authentication: " + ex.Message);
            result.IsAuthenticated = false;
        }

        return result;
    }
}






