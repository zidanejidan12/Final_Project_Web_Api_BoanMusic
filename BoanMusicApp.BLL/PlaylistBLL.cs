using System;
using System.Collections.Generic;
using BoanMusicApp.BO;
using BoanMusicApp.DAL;

namespace BoanMusicApp.BLL
{
    public class PlaylistBLL
    {
        private readonly PlaylistDAL playlistDAL;

        public PlaylistBLL(string connectionString)
        {
            playlistDAL = new PlaylistDAL(connectionString);
        }

        public List<Playlist> GetPlaylistsByUserID(int userID)
        {
            return playlistDAL.GetPlaylistsByUserID(userID);
        }

        public void CreatePlaylist(Playlist playlist)
        {
            playlistDAL.CreatePlaylist(playlist);
        }

        public void DeletePlaylist(int playlistID)
        {
            playlistDAL.DeletePlaylist(playlistID);
        }

        public void AddTrackToPlaylist(int playlistID, int trackID, int order)
        {
            playlistDAL.AddTrackToPlaylist(playlistID, trackID, order);
        }

        public void RemoveTrackFromPlaylist(int playlistID, int trackID)
        {
            playlistDAL.RemoveTrackFromPlaylist(playlistID, trackID);
        }

        public List<Track> GetTracksByPlaylistID(int playlistID)
        {
            return playlistDAL.GetTracksByPlaylistID(playlistID);
        }

        // Additional methods for managing playlists
    }
}
