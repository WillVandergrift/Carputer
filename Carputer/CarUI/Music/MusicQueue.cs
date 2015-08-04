using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using CarPC;

namespace CarUI
{
    /// <summary>
    /// Stores music waiting to be played and controls song selection
    /// </summary>
    public class MusicQueue
    {
        CarMediaElement player;
        private List<Song> songs = new List<Song>();
        private List<Song> played = new List<Song>();
        private Song nowPlaying;
        private int curIndex = 0;
        private bool shuffle = false;
        private bool repeat = false;

        /// <summary>
        /// The current song being played
        /// </summary>
        public Song NowPlaying
        {
            get { return nowPlaying; }
            set { nowPlaying = value; }
        }

        /// <summary>
        /// A list of songs that are in queue to be played
        /// </summary>
        public List<Song> Songs
        {
            get { return songs; }
            set { songs = value; }
        }

        /// <summary>
        /// Is the music being played in a random order
        /// </summary>
        public bool Shuffle
        {
            get { return shuffle; }
            set { shuffle = value; }
        }

        /// <summary>
        /// Is the current song being repeated
        /// </summary>
        public bool Repeat
        {
            get { return repeat; }
            set { repeat = value; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MusicQueue(CarMediaElement mediaPlayer)
        {
            player = mediaPlayer;

            //Wire up player events
            player.MediaEnded += player_MediaEnded;
        }

        /// <summary>
        /// The current song ended
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void player_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Start playing the next song
            NextSong();
        }

        /// <summary>
        /// Play the next song in the list
        /// </summary>
        public void NextSong()
        {
            //Check to see if we're repeating the current song
            if (repeat)
            {
                player.Position = TimeSpan.Zero;
                player.Play();
                return;
            }

            if (shuffle)
            {
                curIndex = GetRandomSongIndex();
                PlaySong();
                return;
            }

            //Nothing special going on, just play the next song
            if (curIndex >= songs.Count - 1)
            {
                //End of the line, start from the beginning
                curIndex = 0;
                PlaySong();
            }
            else
            {
                curIndex++;
                PlaySong();
            }

        }

        /// <summary>
        /// Get a random integer between 0 and the song list count - 1
        /// </summary>
        /// <returns></returns>
        private int GetRandomSongIndex()
        {
            //If there's only one song in the list, play it again
            if (songs.Count == 1)
            {
                return curIndex;
            }

            Random r = new Random();
            int index = r.Next(0, songs.Count - 1);

            //Make sure we're not playing the song that was just played
            while (index == curIndex)
            {
                index = r.Next(0, songs.Count);
            }

            return index;
        }

        /// <summary>
        /// Play the previous song in the list
        /// </summary>
        public void PreviousSong()
        {
            //Always play the previous song in the list, despite repeat and shuffle settings
            if (curIndex != 0)
            {
                curIndex -= 1;
            }
            else
            {
                curIndex = songs.Count - 1;
            }

            PlaySong();
        }

        /// <summary>
        /// Pause the media player
        /// </summary>
        public void Pause()
        {
            player.Pause();
        }

        /// <summary>
        /// Begin playing the current song
        /// </summary>
        public void Play()
        {
            player.Play();
        }

        /// <summary>
        /// Play the specified song
        /// </summary>
        /// <param name="songToPlay"></param>
        public void PlaySong(Song songToPlay)
        {
            nowPlaying = songToPlay;
            player.Source = new Uri(nowPlaying.Path);
            player.Play();
        }

        /// <summary>
        /// Play the current song specified by the curIndex
        /// </summary>
        /// <param name="songToPlay"></param>
        public void PlaySong()
        {
            nowPlaying = songs[curIndex];
            player.Source = new Uri(nowPlaying.Path);
            player.Play();
        }

        /// <summary>
        /// Add the list of songs to our music queue and play the selected one
        /// </summary>
        /// <param name="songs"></param>
        /// <param name="SongToPlay"></param>
        public void AddNPlay(List<Song> songList, Song SongToPlay)
        {
            //Add the selected songs to the queue
            songs = songList;

            //Play the selected song
            for (int i = 0; i <= songs.Count(); i++)
            {
                if (songs[i].Name == SongToPlay.Name)
                {
                    curIndex = i;
                    PlaySong(SongToPlay);
                    break;
                }
            }
        }
    }
}
