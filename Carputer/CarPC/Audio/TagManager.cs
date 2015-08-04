using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TagLib;

namespace CarPC.Audio
{
    public static class TagManager
    {
        /// <summary>
        /// The Songs coverart
        /// </summary>
        /// <param name="path">The full path to the song file</param>
        /// <returns></returns>
        public static ImageSource GetCoverArt(string path)
        {

            var file = TagLib.File.Create(path);

            //Thumbnail
            if (file.Tag.Pictures.Length >= 1)
            {
                try
                {
                    var bin = (byte[])(file.Tag.Pictures[0].Data.Data);

                    using (MemoryStream stream = new MemoryStream(bin))
                    {
                        return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }
                }
                catch
                {
                    //Generic music icon
                    BitmapImage imgThumb = new BitmapImage();
                    imgThumb.BeginInit();
                    imgThumb.UriSource = new Uri(@"pack://siteoforigin:,,,/Images/music-file.png");
                    imgThumb.EndInit();
                    return imgThumb;
                }

            }
            else
            {
                //Generic music icon
                BitmapImage imgThumb = new BitmapImage();
                imgThumb.BeginInit();
                imgThumb.UriSource = new Uri(@"pack://siteoforigin:,,,/Images/music-file.png");
                imgThumb.EndInit();
                return imgThumb;
            }
        }

        /// <summary>
        /// The songs artist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetArtist(string path)
        {
            var file = TagLib.File.Create(path);

            return file.Tag.FirstAlbumArtist;
        }

        /// <summary>
        /// The album the song was released on
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetAlbum(string path)
        {
            var file = TagLib.File.Create(path);

            return file.Tag.Album;
        }

        /// <summary>
        /// The year the song was released
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetYear(string path)
        {
            var file = TagLib.File.Create(path);

            return (int)file.Tag.Year;
        }

        /// <summary>
        /// The genre the song falls under
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetGenre(string path)
        {
            var file = TagLib.File.Create(path);

            return file.Tag.FirstGenre;
        }
    }
}
