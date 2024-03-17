using System.Collections.Generic;
using BoanMusicApp.BO;

public interface ITrackDAL
{
    void AddNewTrack(Track track);
    List<TrackDTO> GetTracksByGenreDTO(string genre);
}
