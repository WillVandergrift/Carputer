using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarPC;

namespace CarViewModel.Database
{
    public class ArtistVM
    {
        CarPCDataContext db = new CarPCDataContext();

        /// <summary>
        /// Return a list of all artists
        /// </summary>
        /// <returns></returns>
        public List<CarPC.Artist> GetAllArtists()
        {
            var artists = from a in db.Artists
                          select a;

            return artists.ToList<CarPC.Artist>();
        }

        /// <summary>
        /// Find an artist by their name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Artist FindArtistByName(string name)
        {
            var artist = from a in db.Artists
                            where a.Name.Trim().ToUpper() == name.Trim().ToUpper()
                            select a;

            return artist.First();
        }

        /// <summary>
        /// Find an artist by their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Artist FindArtistByID(int id)
        {
            var artist = from a in db.Artists
                         where a.ID == id
                         select a;

            return artist.First();
        }
    }
}
