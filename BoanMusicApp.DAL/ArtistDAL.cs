using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

public class ArtistDAL : IArtistDAL
{
    private string connectionString;

    public ArtistDAL(string connectionString)
    {
        this.connectionString = ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
    }

    public void AddNewArtist(Artist artist)
    {
        try
        {
            // Use Dapper to execute the stored procedure
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define the parameters object
                var parameters = new
                {
                    Name = artist.Name,
                    Image = artist.Image ?? (object)DBNull.Value
                };

                // Execute the stored procedure using Dapper
                connection.Execute("dbo.AddNewArtist", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions here
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
