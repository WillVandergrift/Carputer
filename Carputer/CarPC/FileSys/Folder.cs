using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CarPC.FileSys 
{
    public class Folder : INotifyPropertyChanged, fso
    {
        private string name;
        private string path;
        private fso parent;
        private ImageSource thumbnail;
        private fsoType fileType = fsoType.Folder;

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

        //Default Constructor
        public Folder()
        {
            //Generic music icon
            BitmapImage imgThumb = new BitmapImage();
            imgThumb.BeginInit();
            imgThumb.UriSource = new Uri(@"pack://siteoforigin:,,,/Images/folder-blue.png");
            imgThumb.EndInit();
            thumbnail = imgThumb;
        }

        //Default Constructor
        public Folder(string folderPath, fso fsoParent)
        {
            //Name
            name = folderPath.Substring(folderPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
            
            //Path
            path = folderPath;
            parent = fsoParent;

            //Generic folder icon
            BitmapImage imgThumb = new BitmapImage();
            imgThumb.BeginInit();
            imgThumb.UriSource = new Uri(@"pack://siteoforigin:,,,/Images/folder-blue.png");
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
