using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarPC;
using CarPC.Audio;
using CarPC.FileSys;
using CarViewModel;
using CarViewModel.Browser;
using CarUI;
using System.Windows.Controls;

namespace Carputer
{
    public enum DisplayScreen
    {
        Dashboard,
        Power,
        Browser,
        MusicHome
    }

    public static class Session
    {
        //Source List
        private static List<fso> sourceList = new List<fso>();
        
        //Supported audio format extensions
        private static List<string> supportedAudio = new List<string>();

        //Supported video format extensions
        private static List<string> supportedVideo = new List<string>();

        //The current screen being displayed
        private static DisplayScreen currentScreen = DisplayScreen.Dashboard;

        //File Browser View-Model
        private static FileBrowserVM fileVM = new FileBrowserVM();

        //Dashboard pages
        private static Dashboard dashboardOne = new Dashboard(1);
        private static Dashboard dashboardTwo = new Dashboard(2);

        //File Browsr page
        private static FileBrowser browser = new FileBrowser();

        //Music Home Pages
        private static MusicHome musicHomeOne = new MusicHome(1);
        private static MusicGenreHome musGenreHome = new MusicGenreHome(1);

        //Current Music Genre Page
        private static CarUI.MusicGenre curMusicGenrePage;

        /// <summary>
        /// The current music genre page
        /// </summary>
        public static CarUI.MusicGenre CurMusicGenrePage
        {
            get { return Session.curMusicGenrePage; }
            set { Session.curMusicGenrePage = value; }
        }

        //Music Queue
        private static MusicQueue curMusicQueue;

        /// <summary>
        /// Songs that are in line to be played
        /// </summary>
        public static MusicQueue CurMusicQueue
        {
            get { return Session.curMusicQueue; }
            set { Session.curMusicQueue = value; }
        }

        /// <summary>
        /// A list of sources to display files from
        /// </summary>
        public static List<fso> SourceList
        {
            get { return Session.sourceList; }
            set { Session.sourceList = value; }
        }

        /// <summary>
        /// A file browser window
        /// </summary>
        public static FileBrowser Browser
        {
            get { return Session.browser; }
            set { Session.browser = value; }
        }

        /// <summary>
        /// The current screen being displayed
        /// </summary>
        public static DisplayScreen CurrentScreen
        {
            get { return Session.currentScreen; }
            set { Session.currentScreen = value; }
        }

        /// <summary>
        /// The first dashboard page
        /// </summary>
        public static Dashboard DashboardOne
        {
            get { return Session.dashboardOne; }
            set { Session.dashboardOne = value; }
        }

        /// <summary>
        /// The second dashboard page
        /// </summary>
        public static Dashboard DashboardTwo
        {
            get { return Session.dashboardTwo; }
            set { Session.dashboardTwo = value; }
        }

        /// <summary>
        /// The first music home page
        /// </summary>
        public static MusicHome MusicHomeOne
        {
            get { return Session.musicHomeOne; }
            set { Session.musicHomeOne = value; }
        }

        /// <summary>
        /// The first music genre home page
        /// </summary>
        public static MusicGenreHome MusGenreHome
        {
            get { return Session.musGenreHome; }
            set { Session.musGenreHome = value; }
        }

        /// <summary>
        /// File Browser View Model
        /// </summary>
        public static FileBrowserVM FileVM
        {
            get { return Session.fileVM; }
            set { Session.fileVM = value; }
        }

        /// <summary>
        /// Prepare the application for use
        /// </summary>
        static Session()
        {
            //Prepare our file type list
            sourceList.Add(new MediaSource("My Music", @"C:\Users\William\Music\", "/Images/folder-music.png"));
            sourceList.Add(new MediaSource("My Videos", @"C:\Users\William\Videos\", "/Images/folder-music.png"));

            //Prepare our supported audio extensions list
            supportedAudio = new List<string>()
            {
                "asf", "wma", "aif", "aifc", "aiff",
                "wav", "cda", "mp2", "mp3"
            };

            //Prepare our supported video extensions list
            supportedVideo = new List<string>()
            {
                "wmv", "mpg", "mpeg", "mov"
            };

            //Setup our file browser
            browser = new FileBrowser(sourceList, supportedAudio, supportedVideo);
        }
    }
}
