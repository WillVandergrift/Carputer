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
    /// Interaction logic for MusicGenreHome.xaml
    /// </summary>
    public partial class MusicGenreHome : UserControl
    {
        //Dashboard Delegate and Event
        public delegate void SelectionChangedHandler(object sender, MusicGenreItem e);
        public event SelectionChangedHandler OnSelectionChanged;

        private List<MusicGenreItem> items = new List<MusicGenreItem>();
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
        /// A list of items to display on the genre page
        /// </summary>
        public List<MusicGenreItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MusicGenreHome()
        {
            InitializeComponent();

            BuildGenreHome(1);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MusicGenreHome(int page)
        {
            InitializeComponent();

            BuildGenreHome(page);
        }

        /// <summary>
        /// Add items to the genre home page
        /// </summary>
        public void BuildGenreHome(int page)
        {
            //clear the gener home page
            items.Clear();

            switch (page)
            {
                case 1:
                    GenrePageOne();
                    break;
            }

            //Update the genre list source
            lstGenres.ItemsSource = null;
            lstGenres.ItemsSource = items;
        }

        private void GenrePageOne()
        {
            //Country
            MusicGenreItem dbCountry = new MusicGenreItem()
            {
                Name = "Country",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/country-icon.png")),
                ItemType = GenreType.Country
            };
            items.Add(dbCountry);

            //Christian
            MusicGenreItem dbChristian = new MusicGenreItem()
            {
                Name = "Christian",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/christian-icon.png")),
                ItemType = GenreType.Christian
            };
            items.Add(dbChristian);

            //Rock
            MusicGenreItem dbRock = new MusicGenreItem()
            {
                Name = "Rock",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/rock-icon.png")),
                ItemType = GenreType.Rock
            };
            items.Add(dbRock);

            //Pop
            MusicGenreItem dbPop = new MusicGenreItem()
            {
                Name = "Pop",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/pop-icon.png")),
                ItemType = GenreType.Pop
            };
            items.Add(dbPop);

            //Soundtrack
            MusicGenreItem dbSoundtrack = new MusicGenreItem()
            {
                Name = "Soundtrack",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/soundtrack-icon.png")),
                ItemType = GenreType.SoundTrack
            };
            items.Add(dbSoundtrack);
        }

        /// <summary>
        /// Selected item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstGenres_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectionChanged != null)
            {
                OnSelectionChanged(this, (MusicGenreItem)lstGenres.SelectedItem);
            }
        }
    }
}
