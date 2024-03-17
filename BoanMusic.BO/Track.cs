using System;

namespace BoanMusicApp.BO
{
public class Track
{
    public int Track_ID { get; set; }
    public string TrackName { get; set; }
    public int ArtistID { get; set; }
    public int? AlbumID { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public string Genre { get; set; }
    public byte[] TrackImage { get; set; }
    public string AlbumName { get; set; }
    public string ArtistName { get; set;}
    }
}

