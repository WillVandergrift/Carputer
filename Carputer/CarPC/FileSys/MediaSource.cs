using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CarPC.FileSys
{
    public enum SrcType
    {
        Music,
        Video,
        Removable
    }

    public class MediaSource : INotifyPropertyChanged, fso
    {
        private string name;
        private string path;
        private fso parent;
        private ImageSource thumbnail;
        private fsoType fileType = fsoType.Source;
        private SrcType sourceType;

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
        /// The type of source file
        /// </summary>
        public SrcType SourceType
        {
            get { return sourceType; }
            set 
            { 
                sourceType = value;
                Notify("SourceType");
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MediaSource()
        {
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        public MediaSource(string srcName, string srcPath, string relativeThumbnailUrl)
        {
            name = srcName;
            path = srcPath;
            //thumbnail = new BitmapImage(new Uri(@"C:\Users\William\Documents\Software Projects\Carputer\Media\folder-music.png"));
            thumbnail = new BitmapImage(new Uri(@"pack://siteoforigin:,,," + relativeThumbnailUrl));
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
