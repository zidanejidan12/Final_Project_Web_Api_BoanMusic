using System;

public class Album
{
    public int AlbumID { get; set; }
    public int ArtistID { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public byte[] Image { get; set; } // Assuming the Image field is stored as binary data
}
