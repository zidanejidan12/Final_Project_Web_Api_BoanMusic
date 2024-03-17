namespace BoanMusicApp.BO
{
public class Artist
{
    public int Artist_ID { get; set; }
    public string Name { get; set; }
    public byte[] Image { get; set; } // Assuming the Image field is stored as binary data
}
}