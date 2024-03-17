using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BoanMusicApp.BO;
using Dapper;

public class ArtistDAL : IArtistDAL
{
    private String connectionString;

    public ArtistDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Artist> GetAllArtists()
        {
            List<Artist> artists = new List<Artist>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute the query to retrieve all artists
                    artists = connection.Query<Artist>("SELECT [Artist_ID], [Name] FROM [BoanMusic].[Songs].[Artists]").AsList();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine("Error: " + ex.Message);
            }

            return artists;
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
