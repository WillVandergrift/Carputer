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
using System.Windows.Forms;

using MediaInfoNET;
using TagLib;
using System.IO;
using CarPC.FileSys;

namespace Carputer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Constants for grid sizes
        private GridLength GridRowTopHeight = new GridLength(100);
        private GridLength GridRowBottomHeight = new GridLength(100);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Play the background video
            mediaBackground.Play();

            //Setup the Music Queue
            Session.CurMusicQueue = new CarUI.MusicQueue(musicPlayer);

            //Setup the main toolbar
            toolbarMain.OnHideToolbar += toolbarMain_OnHideToolbar;
            toolbarMain.OnHideToolbarComplete += toolbarMain_OnHideToolbarComplete;
            toolbarMain.OnVolumeButtonPressed += toolbarMain_OnVolumeButtonPressed;
            toolbarMain.OnHomeButtonPressed += toolbarMain_OnHomeButtonPressed;
            toolbarMain.OnBackButtonPressed += toolbarMain_OnBackButtonPressed;

            //Setup the file browser
            Session.Browser.OnDirectoryChanged += Browser_OnDirectoryChanged;
            Session.Browser.OnFileChanged += Browser_OnFileChanged;

            //Setup the Music Home Pages
            Session.MusicHomeOne.OnSelectionChanged += MusicHomeOne_OnSelectionChanged;

            //Setup the Genre Home Pages
            Session.MusGenreHome.OnSelectionChanged += MusGenreHome_OnSelectionChanged;

            //Initialize the dashboard
            Session.DashboardOne.OnSelectionChanged += CurDashboard_OnSelectionChanged;
            Session.DashboardTwo.OnSelectionChanged += CurDashboard_OnSelectionChanged;
            transMain.Content = Session.DashboardOne;

            if (Screen.AllScreens.Count() > 1)
            {
                this.WindowStyle = System.Windows.WindowStyle.None;
                System.Drawing.Rectangle bounds = Screen.AllScreens[1].Bounds;
                this.Left = bounds.Left;
                this.Top = bounds.Top;
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }

        /// <summary>
        /// The selected item changed on the genre home page
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MusGenreHome_OnSelectionChanged(object sender, CarPC.Dashboard.MusicGenreItem e)
        {
            Session.CurMusicGenrePage = new CarUI.MusicGenre(e.ItemType);
            Session.CurMusicGenrePage.OnSelectionChanged += curMusicGenrePage_OnSelectionChanged;

            transMain.Content = Session.CurMusicGenrePage;
        }

        /// <summary>
        /// The selected item changed on a music home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MusicHomeOne_OnSelectionChanged(object sender, CarPC.Dashboard.MusicItem e)
        {
            switch (e.ItemType)
            {
                case CarPC.Dashboard.MusicType.Album:
                    //Display the album music page
                    break;
                case CarPC.Dashboard.MusicType.Artist:
                    //Display the artist music page
                    break;
                case CarPC.Dashboard.MusicType.Genre:
                    //Display the genre music page
                    transMain.Content = Session.MusGenreHome;
                    break;
            }
        }

        /// <summary>
        /// A media file was opened in the Session's music player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MusicPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            //Update the now playing control
        }

        /// <summary>
        /// The selected file in the file browser changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="file"></param>
        void Browser_OnFileChanged(object sender, fso file)
        {
            switch (file.FileType)
            {
                case fsoType.Music:
                    //Insert all songs in the currrent directory into the musicQueue and play the selected one
                    Session.CurMusicQueue.AddNPlay(
                        CarPC.Helper.MusicConverter.MusicFileToSong(Session.Browser.Files),
                        CarPC.Helper.MusicConverter.MusicFileToSong(file)
                        );
                    break;
            }
        }

        /// <summary>
        /// The selected file in the music genre page changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void curMusicGenrePage_OnSelectionChanged(object sender, CarPC.Song e)
        {
            //Insert all songs in the currrent genre into the musicQueue and play the selected one
            Session.CurMusicQueue.AddNPlay(Session.CurMusicGenrePage.Songs, e);
        }

        /// <summary>
        /// The back button was pressed on the main toolbar
        /// </summary>
        /// <param name="sender"></param>
        void toolbarMain_OnBackButtonPressed(object sender)
        {
            //The current screen determines what the back button does
            switch (Session.CurrentScreen)
            {
                case DisplayScreen.Browser:
                    //Tell the file browser to go back
                    Session.Browser.NavigateBack();
                    break;
            }
        }

        /// <summary>
        /// File browser directory changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Directory"></param>
        void Browser_OnDirectoryChanged(object sender, fso Directory)
        {
            toolbarMain.ToggleBackButton(Session.Browser.BackNavEnabled);
        }

        /// <summary>
        /// Home button on main toolbar pressed
        /// </summary>
        /// <param name="sender"></param>
        void toolbarMain_OnHomeButtonPressed(object sender)
        {
            
            transMain.Content = Session.DashboardOne;
            toolbarMain.ToggleBackButton(System.Windows.Visibility.Hidden);
        }

        /// <summary>
        /// The selected dashboard item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurDashboard_OnSelectionChanged(object sender, CarPC.Dashboard.DashboardItem e)
        {
            switch (e.ItemType)
            {
                case CarPC.Dashboard.DashboardType.Music:
                    //Display the Music screen
                    transMain.Content = Session.MusicHomeOne;
                    Session.CurrentScreen = DisplayScreen.MusicHome;
                    break;
                case CarPC.Dashboard.DashboardType.Power:
                    //Display the power screen
                    transMain.Content = new CarUI.Power();
                    Session.CurrentScreen = DisplayScreen.Power;
                    break;
                case CarPC.Dashboard.DashboardType.Settings:
                    //Display the settings screen
                    transMain.Content = Session.DashboardTwo;
                    Session.CurrentScreen = DisplayScreen.Dashboard;
                    break;
                case CarPC.Dashboard.DashboardType.Browse:
                    //Display the file browser
                    transMain.Content = Session.Browser;
                    Session.CurrentScreen = DisplayScreen.Browser;
                    toolbarMain.ToggleBackButton(Session.Browser.BackNavEnabled);
                    break;
            }
        }

        //Toggle the volume slider visibility
        void toolbarMain_OnVolumeButtonPressed(object sender)
        {
            sldVolume.AnimController.ToggleVisibility();
        }

        /// <summary>
        /// The toolbar is hiding
        /// </summary>
        /// <param name="sender"></param>
        void toolbarMain_OnHideToolbar(object sender)
        {
            //Hide the volume toolbar
            sldVolume.AnimController.HideControl();
            //Hide the statusbar
            statusBar.AnimController.HideControl();
            //Hide the now playing cover art
            coverArt.AnimController.HideControl();
        }

        /// <summary>
        /// The main toolbar just finished hiding
        /// </summary>
        /// <param name="sender"></param>
        void toolbarMain_OnHideToolbarComplete(object sender)
        {
            //Collapse the top and bottom grid rows
            gridRowTop.Height = new GridLength(10);
            gridRowBottom.Height = new GridLength(0);

            //Set the visibility of the show toolbar button
            btnShowToolbars.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Replay the background video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediaBackground_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaBackground.Position = TimeSpan.Zero;
            mediaBackground.Play();
        }

        /// <summary>
        /// Show the toolbars
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowToolbars_Click(object sender, RoutedEventArgs e)
        {
            //Expand the top and bottom grid rows
            gridRowTop.Height = GridRowTopHeight;
            gridRowBottom.Height = GridRowBottomHeight;

            //Show the toolbar
            toolbarMain.AnimController.ShowControl();

            //Show the statusbar
            statusBar.AnimController.ShowControl();

            //Show the now playing cover art
            coverArt.AnimController.ShowControl();
        }

        /// <summary>
        /// The music player's state changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void musicPlayer_OnStateChanged(object sender, CarUI.PlayerState e)
        {
            //Update the cover art
            if (e == CarUI.PlayerState.Opened)
            {
                coverArt.UpdateCoverArt(Session.CurMusicQueue.NowPlaying.CoverArt);
                statusBar.NowPlayingChanged(Session.CurMusicQueue.NowPlaying.Name);
            }

            //Notify the status bar of the change
            statusBar.StateChanged(e);
        }

        /// <summary>
        /// A media button was pressed on the status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusBar_OnMediaButtonPressed(object sender, CarUI.MediaButton e)
        {
            switch (e)
            {
                case CarUI.MediaButton.Back:
                    //Back button
                    Session.CurMusicQueue.PreviousSong();
                    break;
                case CarUI.MediaButton.Forward:
                    //Forward button
                    Session.CurMusicQueue.NextSong();
                    break;
                case CarUI.MediaButton.Pause:
                    //Pause button
                    Session.CurMusicQueue.Pause();
                    break;
                case CarUI.MediaButton.Play:
                    Session.CurMusicQueue.Play();
                    //Play button
                    break;
                case CarUI.MediaButton.RepeatOn:
                    Session.CurMusicQueue.Repeat = true;
                    //Repeat button
                    break;
                case CarUI.MediaButton.RepeatOff:
                    Session.CurMusicQueue.Repeat = false;
                    //Repeat button
                    break;
                case CarUI.MediaButton.ShuffleOn:
                    Session.CurMusicQueue.Shuffle = true;
                    //Shuffle button
                    break;
                case CarUI.MediaButton.ShuffleOff:
                    Session.CurMusicQueue.Shuffle = false;
                    //Shuffle button
                    break;
            }
        }
    }
}
