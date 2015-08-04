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

using CarViewModel.Database;
using CarPC.Dashboard;
using CarPC;

namespace CarUI
{
    /// <summary>
    /// Interaction logic for MusicGenreHome.xaml
    /// </summary>
    public partial class MusicGenre : UserControl
    {
        //Selected Item Changed Delegate and Event
        public delegate void SelectionChangedHandler(object sender, Song e);
        public event SelectionChangedHandler OnSelectionChanged;

        private SongVM vmSong = new SongVM();
        private List<Song> songs = new List<Song>();

        /// <summary>
        /// A list of songs to display
        /// </summary>
        public List<Song> Songs
        {
            get { return songs; }
            set { songs = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MusicGenre()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MusicGenre(GenreType genre)
        {
            InitializeComponent();

            FindSongs(genre);
        }

        /// <summary>
        /// Add items to the genre page
        /// </summary>
        public void FindSongs(GenreType genre)
        {
            //clear the gener home page
            songs.Clear();

            songs = vmSong.GetSongsByGenre(genre);

            //Update the genre list source
            lstSongs.ItemsSource = null;
            lstSongs.ItemsSource = songs;
        }

        /// <summary>
        /// Selected item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSongs_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectionChanged != null)
            {
                OnSelectionChanged(this, (Song)lstSongs.SelectedItem);
            }
        }
    }
}
