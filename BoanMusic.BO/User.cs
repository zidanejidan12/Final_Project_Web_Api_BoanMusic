using System;

namespace BoanMusicApp.BO
{
public class User
{
    public int User_ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date_of_Birth { get; set; }
    public byte[] Profile_Image { get; set; }
    public byte[] Password { get; set; } // Change the type to byte array for password storage
    // Optional: If you still want to store the password as a string for certain operations
    public string PasswordString { get; set; } // Add PasswordString property type to string
    public string User_Type { get ; set ;}
}
}
