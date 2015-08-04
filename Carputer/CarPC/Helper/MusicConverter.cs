using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarPC.FileSys;

namespace CarPC.Helper
{
    /// <summary>
    /// Convert Music File Sys objects to Song database objects
    /// </summary>
    public static class MusicConverter
    {
        /// <summary>
        /// Convert a list of fso music files to songs
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static List<Song> MusicFileToSong(List<fso> files)
        {
            List<Song> songs = new List<Song>();

            foreach (MusicFile file in files)
            {
                Song newSong = new Song();

                newSong.Name = file.Name;
                newSong.Path = file.Path;
                songs.Add(newSong);
            }

            return songs;
        }

        /// <summary>
        /// Convert a fso music file into a song
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static Song MusicFileToSong(fso file)
        {
            Song newSong = new Song();

            newSong.Name = file.Name;
            newSong.Path = file.Path;

            return newSong;
        }
    }
}
