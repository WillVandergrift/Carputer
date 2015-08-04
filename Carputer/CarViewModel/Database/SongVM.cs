using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarPC;
using CarPC.Dashboard;

namespace CarViewModel.Database
{
    public class SongVM
    {
        CarPCDataContext db = new CarPCDataContext();

        /// <summary>
        /// Return a list of all songs
        /// </summary>
        /// <returns></returns>
        public List<CarPC.Song> GetAllSongs()
        {
            var songs = from s in db.Songs
                          select s;

            if (songs.Count() > 0)
            {
                return songs.ToList<CarPC.Song>();
            }
            else
            {
                return new List<Song>();
            }
        }

        /// <summary>
        /// Get a list of songs by the genre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public List<CarPC.Song> GetSongsByGenre(GenreType genre)
        {
            var songs = from s in db.Songs
                        where s.Genre.ToUpper() == genre.ToString()
                        select s;

            if (songs.Count() > 0)
            {
                return songs.ToList<CarPC.Song>();
            }
            else
            {
                return new List<Song>();
            }
        }
    }
}
