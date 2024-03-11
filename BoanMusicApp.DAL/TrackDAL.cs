using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BoanMusicApp;
using BoanMusicApp.BO;

public class TrackDAL : ITrackDAL
{
    private string connectionString;

    public TrackDAL(string connectionString)
    {
        // Retrieve the connection string from web.config
        this.connectionString = ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
    }

    public void AddNewTrack(Track track)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use Dapper's Execute method to execute the stored procedure
                connection.Execute("dbo.AddNewTrack",
                    new
                    {
                        ArtistID = track.ArtistID,
                        AlbumID = track.AlbumID,
                        Name = track.Name,
                        Duration = track.Duration,
                        Genre = track.Genre
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions here
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public List<TrackDTO> GetTracksByGenreDTO(string genre)
    {
        List<TrackDTO> tracks = new List<TrackDTO>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use Dapper's Query method to execute the stored procedure and retrieve results
                tracks = connection.Query<TrackDTO>("dbo.GetTracksByGenre",
                    new { Genre = genre },
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions here
            Console.WriteLine("Error: " + ex.Message);
        }

        return tracks;
    }

    public List<Track> GetTracks()
    {
        List<Track> tracks = new List<Track>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetTracks", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Track track = new Track();
                    track.TrackID = Convert.ToInt32(reader["Track_ID"]);
                    track.Name = reader["TrackName"].ToString();
                    track.Duration = Convert.ToInt32(reader["Duration"]);
                    track.Genre = reader["Genre"].ToString();
                    track.Image = reader["TrackImage"] as byte[];
                    track.ArtistName = reader["ArtistName"].ToString();
                    track.AlbumName = reader["AlbumName"].ToString();

                    tracks.Add(track);
                }
            }
        }

        return tracks;
    }

    public List<Track> SearchTracks(string searchQuery)
    {
        List<Track> tracks = new List<Track>();

        string sqlQuery = @"
        SELECT t.Track_ID, t.Artist_ID, t.Album_ID, t.Name, t.Duration, t.Genre, t.Image,
               a.Name AS ArtistName, al.Name AS AlbumName
        FROM Songs.Tracks t
        INNER JOIN Songs.Artists a ON t.Artist_ID = a.Artist_ID
        LEFT JOIN Songs.Albums al ON t.Album_ID = al.Album_ID
        WHERE a.Name LIKE @SearchQuery OR t.Name LIKE @SearchQuery OR t.Genre LIKE @SearchQuery";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@SearchQuery", $"%{searchQuery}%");

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Track track = new Track
                            {
                                TrackID = Convert.ToInt32(reader["Track_ID"]),
                                ArtistID = Convert.ToInt32(reader["Artist_ID"]),
                                AlbumID = reader["Album_ID"] == DBNull.Value ? null : (int?)reader["Album_ID"],
                                Name = reader["Name"].ToString(),
                                Duration = Convert.ToInt32(reader["Duration"]),
                                Genre = reader["Genre"].ToString(),
                                Image = reader["Image"] == DBNull.Value ? null : (byte[])reader["Image"],
                                ArtistName = reader["ArtistName"].ToString(),
                                AlbumName = reader["AlbumName"] == DBNull.Value ? null : reader["AlbumName"].ToString()
                            };
                            tracks.Add(track);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Error retrieving tracks: {ex.Message}");
                }
            }
        }

        return tracks;
    }


    public class GenreDAL : IGenreDAL
    {
        private readonly string connectionString;

        public GenreDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> GetGenres()
        {
            List<string> genres = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Use Dapper's Query method to execute the SQL query and retrieve results
                    genres = connection.Query<string>("SELECT DISTINCT Genre FROM Songs.Tracks").AsList();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine("Error in GetGenres: " + ex.ToString());
                throw; // Rethrow the exception for higher-level handling
            }

            return genres;
        }
    }
    public DataTable GetTopArtistsWithSongCount()
    {
        DataTable dataTable = new DataTable();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use Dapper's Query method to execute the SQL query and retrieve results
                var results = connection.Query<TopArtistsDTO>("SELECT TOP (10) A.Name AS Artist_Name, COUNT(T.Track_ID) AS Song_Count " +
                                                               "FROM Songs.Artists AS A INNER JOIN Songs.Tracks AS T ON A.Artist_ID = T.Artist_ID " +
                                                               "GROUP BY A.Artist_ID, A.Name ORDER BY Song_Count DESC").AsList();

                // Convert the list to a DataTable using Dapper's extension method
                dataTable = results.ToDataTable();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            // Handle or log the exception as needed
            throw; // Rethrow the exception for higher-level handling
        }

        return dataTable;
    }

}

