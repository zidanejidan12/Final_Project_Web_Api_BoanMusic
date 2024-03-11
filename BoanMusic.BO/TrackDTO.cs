public class TrackDTO
{
    private string _albumName;

    public string Artist_Name { get; set; }
    public string Track_Name { get; set; }
    public string Album_Name
    {
        get => _albumName ?? "";
        set => _albumName = value;
    }
    public int Duration { get; set; }
    public string Genre { get; set; }
    // Additional properties as needed
}
