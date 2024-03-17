using System;
using System.Collections.Generic;
using System.Data;
using BoanMusicApp.BO;
using BoanMusicApp;

namespace BoanMusicApp.BLL
{
    public class TrackBLL
    {
        private readonly TrackDAL trackDAL;

        public TrackBLL(string connectionString)
        {
            trackDAL = new TrackDAL(connectionString);
        }

        public List<Track> GetTracks()
        {
            return trackDAL.GetTracks();
        }

        public Track GetTrackById(int trackId)
        {
            return trackDAL.GetTrackById(trackId);
        }

        public List<Track> SearchTracks(string searchQuery)
        {
            // Call the appropriate method in the DAL to search for tracks
            return trackDAL.SearchTracks(searchQuery);
        }

        public void AddNewTrack(Track track)
        {
            trackDAL.AddNewTrack(track);
        }

        public List<TrackDTO> GetTracksByGenreDTO(string genre)
        {
            return trackDAL.GetTracksByGenreDTO(genre);
        }

        public DataTable GetTopArtistsWithSongCount()
        {
            return trackDAL.GetTopArtistsWithSongCount();
        }
    }
}
