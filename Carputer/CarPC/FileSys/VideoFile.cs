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
    public class VideoFile : INotifyPropertyChanged, fso
    {
        private string name;
        private string path;
        private fso parent;
        private ImageSource thumbnail;
        private fsoType fileType = fsoType.Video;
        

        /// <summary>
        /// The name of the video
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
        /// The full path of the video file
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
        /// The picture for the video
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
        /// Default Constructor
        /// </summary>
        public VideoFile()
        {

        }

        /// <summary>
        /// Create a new video file from the given file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public VideoFile(string file, fso fsoParent)
        {
            path = file;
            parent = fsoParent;
            GetVideoInfo();
        }

        /// <summary>
        /// Get information about the video
        /// </summary>
        private void GetVideoInfo()
        {
            //Video file name
            name = path.Substring(path.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
            name = name.Substring(0, name.LastIndexOf("."));

            //Generic music icon
            BitmapImage imgThumb = new BitmapImage();
            imgThumb.BeginInit();
            imgThumb.UriSource = new Uri(@"pack://siteoforigin:,,,/Images/music-file.png");
            imgThumb.EndInit();
            thumbnail = imgThumb;
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
