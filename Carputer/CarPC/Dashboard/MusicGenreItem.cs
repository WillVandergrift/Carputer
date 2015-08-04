using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CarPC.Dashboard
{
    public enum GenreType 
    {
        Country,
        Rock,
        Christian,
        Pop,
        SoundTrack
    }

    public class MusicGenreItem : INotifyPropertyChanged
    {
        private GenreType itemType;
        private ImageSource icon;
        private string name;

        /// <summary>
        /// The name of the Genre item
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
        /// The Icon for the genre item
        /// </summary>
        public ImageSource Icon
        {
            get { return icon; }
            set 
            { 
                icon = value;
                Notify("Icon");
            }
        }

        /// <summary>
        /// The type of genre this item represents
        /// </summary>
        public GenreType ItemType
        {
            get { return itemType; }
            set 
            { 
                itemType = value; 
                Notify("ItemType");
            }
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
