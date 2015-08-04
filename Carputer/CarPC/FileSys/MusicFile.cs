using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TagLib;

namespace CarPC.FileSys
{
    public class MusicFile : INotifyPropertyChanged, fso
    {
        private string name;
        private string path;
        private fso parent;
        private ImageSource thumbnail;
        private fsoType fileType = fsoType.Music;
        private string artist;
        private string album;
        private string genre;
        

        /// <summary>
        /// The name of the song
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify("Name");
            }
        }

        /// <summary>
        /// The full path of the audio file
        /// </summary>
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                Notify("Path");
            }
        }

        /// <summary>
        /// The parent directory
        /// </summary>
        public fso Parent
        {
            get { return parent; }
            set 
            { 
                parent = value;
                Notify("Parent");
            }
        }

        /// <summary>
        /// The album art for the song
        /// </summary>
        public ImageSource Thumbnail
        {
            get { return thumbnail; }
            set 
            { 
                thumbnail = value;
                Notify("Thumbnail");
            }
        }

        /// <summary>
        /// The type of file this object represents
        /// </summary>
        public fsoType FileType
        {
            get { return fileType; }
            set 
            { 
                fileType = value;
                Notify("FileType");
            }
        }

        /// <summary>
        /// The song album
        /// </summary>
        public string Album
        {
            get { return album; }
            set 
            { 
                album = value;
                Notify("Album");
            }
        }

        /// <summary>
        /// The songs artist
        /// </summary>
        public string Artist
        {
            get { return artist; }
            set 
            { 
                artist = value;
                Notify("Artist");
            }
        }

        /// <summary>
        /// The song's musical genre
        /// </summary>
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MusicFile()
        {

        }

        /// <summary>
        /// Create a new audio file from the given file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public MusicFile(string file, fso fsoParent)
        {
            path = file;
            parent = fsoParent;
            GetSongInfo();
        }

        /// <summary>
        /// Read data from the audio file
        /// </summary>
        private void GetSongInfo()
        {
            var file = TagLib.File.Create(path);

            //Song Name
            name = file.Tag.Title;

            //Song Artist
            artist = file.Tag.FirstPerformer;

            //Song Album
            album = file.Tag.Album;

            genre = file.Tag.FirstGenre;

            //Thumbnail
            thumbnail = CarPC.Audio.TagManager.GetCoverArt(path);
        }

        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion



    }
}
