using System.Collections.Generic;

public interface ITrackDAL
{
    void AddNewTrack(Track track);
    List<TrackDTO> GetTracksByGenreDTO(string genre);
}
