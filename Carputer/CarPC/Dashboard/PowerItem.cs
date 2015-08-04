using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CarPC.Dashboard
{
    public enum PowerType 
    {
        Shutdown,
        Restart,
        Hibernate,
        Close
    }

    public class PowerItem : INotifyPropertyChanged
    {
        private PowerType itemType;
        private ImageSource icon;
        private string name;

        /// <summary>
        /// The name of the power item
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
        /// The Icon for the power item
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
        /// The type of power item this item represents
        /// </summary>
        public PowerType ItemType
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
