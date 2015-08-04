using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CarPC.Dashboard;
using CarPC;

namespace CarUI
{
    /// <summary>
    /// Interaction logic for MusicArtists.xaml
    /// </summary>
    public partial class MusicArtist : UserControl
    {
        //Dashboard Delegate and Event
        public delegate void SelectionChangedHandler(object sender, MusicItem e);
        public event SelectionChangedHandler OnSelectionChanged;

        private List<Artist> items = new List<Artist>();
        private int page;

        /// <summary>
        /// The music home page
        /// </summary>
        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        /// <summary>
        /// A list of items to display on the artists page
        /// </summary>
        public List<Artist> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MusicArtist()
        {
            InitializeComponent();

        }


        /// <summary>
        /// Selected item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstArtists_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectionChanged != null)
            {
                OnSelectionChanged(this, (MusicItem)lstArtists.SelectedItem);
            }
        }
    }
}
