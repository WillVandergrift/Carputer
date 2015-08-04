using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CarPC.Dashboard
{
    public enum DashboardType 
    {
        Music,
        Settings,
        Power,
        Browse,
        Next,
        Previous
    }

    public class DashboardItem : INotifyPropertyChanged
    {
        private DashboardType itemType;
        private ImageSource icon;
        private string name;

        /// <summary>
        /// The name of the dashboard item
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
        /// The Icon for the dashboard item
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
        /// The type of dashboard item this item represents
        /// </summary>
        public DashboardType ItemType
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
