using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarPC;

namespace CarViewModel.Database
{
    public class AlbumVM
    {
        CarPCDataContext db = new CarPCDataContext();

        /// <summary>
        /// Return a list of all albums
        /// </summary>
        /// <returns></returns>
        public List<CarPC.Album> GetAllAlbums()
        {
            var albums = from a in db.Albums
                          select a;

            return albums.ToList<CarPC.Album>();
        }

        /// <summary>
        /// Find an album by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Album FindAlbumByName(string name)
        {
            var album = from a in db.Albums
                         where a.Name.Trim().ToUpper() == name.Trim().ToUpper()
                         select a;

            return album.First();
        }

        /// <summary>
        /// Find an album by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Album FindAlbumByID(int id)
        {
            var album = from a in db.Albums
                         where a.ID == id
                         select a;

            return album.First();
        }
    }
}
