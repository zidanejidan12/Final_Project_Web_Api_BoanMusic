using BoanMusicApp.BO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BoanMusicApp.DAL
{
    public class PlaylistDAL
    {
        private readonly string connectionString;

        public PlaylistDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Playlist> GetPlaylistsByUserID(int userID)
        {
            List<Playlist> playlists = new List<Playlist>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Playlist_ID, User_ID, Name, Image FROM Songs.Playlists WHERE User_ID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Playlist playlist = new Playlist();
                    playlist.PlaylistID = Convert.ToInt32(reader["Playlist_ID"]);
                    playlist.UserID = Convert.ToInt32(reader["User_ID"]);
                    playlist.Name = reader["Name"].ToString();

                    // Check if the 'Image' field is DBNull or null before attempting to convert
                    if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                    {
                        playlist.Image = (byte[])reader["Image"];
                    }
                    else
                    {
                        // Handle the case where the 'Image' field is DBNull or null
                        // For example, you can set the Image property to null:
                        playlist.Image = null;
                    }

                    playlists.Add(playlist);
                }
            }

            return playlists;
        }


        public void CreatePlaylist(Playlist playlist)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Songs.Playlists (User_ID, Name) VALUES (@User_ID, @Name)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@User_ID", playlist.UserID);
                command.Parameters.AddWithValue("@Name", playlist.Name);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void DeletePlaylist(int playlistID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Songs.Playlists WHERE Playlist_ID = @PlaylistID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlaylistID", playlistID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddTrackToPlaylist(int playlistID, int trackID, int order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Songs.Playlist_Tracks (Playlist_ID, Track_ID, [Order]) VALUES (@PlaylistID, @TrackID, @Order)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlaylistID", playlistID);
                command.Parameters.AddWithValue("@TrackID", trackID);
                command.Parameters.AddWithValue("@Order", order);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RemoveTrackFromPlaylist(int playlistID, int trackID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Songs.Playlist_Tracks WHERE Playlist_ID = @PlaylistID AND Track_ID = @TrackID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlaylistID", playlistID);
                command.Parameters.AddWithValue("@TrackID", trackID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Track> GetTracksByPlaylistID(int playlistID)
        {
            List<Track> tracks = new List<Track>();

            string query = @"
        SELECT T.Track_ID, T.Name AS TrackName, A.Name AS ArtistName, T.Image
        FROM Songs.Tracks T
        JOIN Songs.Artists A ON T.Artist_ID = A.Artist_ID
        JOIN Songs.Playlist_Tracks PT ON T.Track_ID = PT.Track_ID
        WHERE PT.Playlist_ID = @PlaylistID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlaylistID", playlistID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Track track = new Track();
                    track.Track_ID = Convert.ToInt32(reader["Track_ID"]);
                    track.Name = reader["TrackName"].ToString();
                    track.ArtistName = reader["ArtistName"].ToString();

                    // Check for DBNull for Image column to handle null values
                    if (reader["Image"] != DBNull.Value)
                    {
                        track.TrackImage = (byte[])reader["Image"];
                    }

                    tracks.Add(track);
                }
            }

            return tracks;
        }



        // Additional methods for CRUD operations on playlists and playlist tracks
    }


}
