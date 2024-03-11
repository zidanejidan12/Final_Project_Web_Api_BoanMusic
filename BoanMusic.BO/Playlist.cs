using System;
using System.Collections.Generic;
using System.Text;

namespace BoanMusicApp.BO
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }

    public class PlaylistTrack
    {
        public int PlaylistID { get; set; }
        public int TrackID { get; set; }
        public int Order { get; set; }
    }

}
