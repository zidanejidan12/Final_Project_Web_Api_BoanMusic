using System.Collections.Generic;
using BoanMusicApp.BO;

namespace BoanMusicApp.BLL
{
    public class ArtistBLL
    {
        private readonly ArtistDAL artistDAL;
        private readonly string connectionString;
        
        public ArtistBLL(ArtistDAL artistDAL)
        {
            this.artistDAL = artistDAL;
        }
        public ArtistBLL(string connectionString)
        {
            this.connectionString = connectionString;
            this.artistDAL = new ArtistDAL(connectionString);
        }

        public List<Artist> GetAllArtists()
        {
            return artistDAL.GetAllArtists();
        }
    }
}
