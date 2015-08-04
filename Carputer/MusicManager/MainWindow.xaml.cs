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

using System.IO;
using CarPC;
using CarPC.FileSys;
using CarViewModel.Database;

namespace MusicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<MusicFile> songs = new List<MusicFile>();
        private List<String> supportedAudio = new List<string>();

        //Database fields
        private CarPCDataContext db = new CarPCDataContext();
        private List<Artist> dbArtists = new List<Artist>();
        private List<Album> dbAlbums = new List<Album>();
        private List<Song> dbSongs = new List<Song>();
        private ArtistVM vmArtists = new ArtistVM();
        private AlbumVM vmAlbums = new AlbumVM();
        private SongVM vmSongs = new SongVM();

        public MainWindow()
        {
            InitializeComponent();

            //Prepare our supported audio extensions list
            supportedAudio = new List<string>()
            {
                "asf", "wma", "aif", "aifc", "aiff",
                "wav", "cda", "mp2", "mp3"
            };

            dbArtists = vmArtists.GetAllArtists();
            dbAlbums = vmAlbums.GetAllAlbums();
            dbSongs = vmSongs.GetAllSongs();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            MusicFile curFile;
            songs.Clear();
            string extension;

            foreach (string file in Directory.GetFiles(txtSearch.Text, "*", SearchOption.AllDirectories))
            {
                extension = file.Substring(file.LastIndexOf(".") + 1);
                if (supportedAudio.Contains(extension))
                {
                    curFile = new MusicFile(file, null);

                    if (curFile.Name == null)
                    {
                        curFile = null;
                        continue;
                    }

                    if (curFile.Artist == null)
                    {
                        curFile = null;
                        continue;
                    }

                    if (curFile.Album == null)
                    {
                        curFile = null;
                        continue;
                    }

                    if (curFile != null)
                    {
                        songs.Add(curFile);
                    }
                    else
                    {
                        tbAlbum.Text = "Failed to store media";
                    }
                    
                }
            }

            lstSongs.ItemsSource = null;
            lstSongs.ItemsSource = songs;

            MessageBox.Show(songs.Count.ToString());
        }

        private void lstSongs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstSongs.SelectedItem == null)
            {
                return;
            }

            MusicFile curSong = (MusicFile)lstSongs.SelectedItem;
            mediaPlayer.Source = new Uri(curSong.Path);
        }

        private void lstSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MusicFile curSong = (MusicFile)lstSongs.SelectedItem;
            bool foundArtist = false;

            foreach (Artist artist in dbArtists)
            {
                if (artist.Name == curSong.Artist)
                {
                    foundArtist = true;
                    break;
                }
            }

            if (!foundArtist)
            {
                Artist newArtist = new Artist();
                newArtist.Name = curSong.Artist;
                db.Artists.InsertOnSubmit(newArtist);
                db.SubmitChanges();
            }
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool foundSong = false;

            foreach (MusicFile song in songs)
            {
                foundSong = false;

                foreach (Song sng in dbSongs)
                {
                    if (sng.Name.Trim().ToUpper() == song.Name.Trim().ToUpper())
                    {
                        foundSong = true;
                        break;
                    }
                }

                if (!foundSong)
                {
                    Song newSong = new Song();
                    newSong.AlbumID = vmAlbums.FindAlbumByName(song.Album).ID;
                    newSong.Name = song.Name;
                    newSong.Genre = song.Genre;
                    newSong.Path = song.Path;
                    newSong.ListenCount = 0;
                    newSong.Favorite = false;
                    newSong.Dislike = false;
                    db.Songs.InsertOnSubmit(newSong);
                    dbSongs.Add(newSong);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            db.SubmitChanges();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
