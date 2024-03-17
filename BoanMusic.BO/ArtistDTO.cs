using System.Collections.Generic;

namespace BoanMusicApp.BO
{
    public class ArtistDTO
    {
        public List<Artist> Artists { get; set; }
        public int SelectedArtistId { get; set; }
        public string SelectedArtistName { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
    }
}
