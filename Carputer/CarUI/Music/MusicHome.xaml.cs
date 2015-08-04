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

namespace CarUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class MusicHome : UserControl
    {
        //Dashboard Delegate and Event
        public delegate void SelectionChangedHandler(object sender, MusicItem e);
        public event SelectionChangedHandler OnSelectionChanged;

        private List<MusicItem> items = new List<MusicItem>();
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
        /// A list of items to display on the music home page
        /// </summary>
        public List<MusicItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MusicHome()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor that generates items based on the page number
        /// </summary>
        public MusicHome(int musicHomePage)
        {
            InitializeComponent();

            //Set the music home page
            page = musicHomePage;

            BuildMusicHomePage(page);
        }

        /// <summary>
        /// Add items to the music home page
        /// </summary>
        public void BuildMusicHomePage(int page)
        {
            //clear the dashboad
            items.Clear();

            switch (page)
            {
                case 1:
                    MusicHomePageOne();
                    break;
            }

            //Update the dashboard list source
            lstMusic.ItemsSource = null;
            lstMusic.ItemsSource = items;
        }

        private void MusicHomePageOne()
        {
            //Album
            MusicItem dbAlbum = new MusicItem()
            {
                Name = "Album",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/album-icon.png")),
                ItemType = MusicType.Album
            };
            items.Add(dbAlbum);

            //Artist
            MusicItem dbArtist = new MusicItem()
            {
                Name = "Artist",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/artist-icon.png")),
                ItemType = MusicType.Artist
            };
            items.Add(dbArtist);

            //Genre
            MusicItem dbGenre = new MusicItem()
            {
                Name = "Genre",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/genre-icon.png")),
                ItemType = MusicType.Genre
            };
            items.Add(dbGenre);

            //Stations
            MusicItem dbStations = new MusicItem()
            {
                Name = "Stations",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/stations-icon.png")),
                ItemType = MusicType.Stations
            };
            items.Add(dbStations);

            //Playlists
            MusicItem dbPlaylists = new MusicItem()
            {
                Name = "Playlists",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/playlist-icon.png")),
                ItemType = MusicType.Playlists
            };
            items.Add(dbPlaylists);

            //Search
            MusicItem dbSearch = new MusicItem()
            {
                Name = "Search",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/search-icon.png")),
                ItemType = MusicType.Search
            };
            items.Add(dbSearch);
        }

        /// <summary>
        /// Selected item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMusic_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectionChanged != null)
            {
                OnSelectionChanged(this, (MusicItem)lstMusic.SelectedItem);
            }
        }
    }
}
