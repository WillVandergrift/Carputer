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

using CarUI;
using CarUI.Animation;
using System.Windows.Media.Animation;

namespace CarUI
{
    public enum MediaButton
    {
        Forward,
        Back,
        RepeatOn,
        RepeatOff,
        ShuffleOn,
        ShuffleOff,
        Play,
        Pause
    }

    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// </summary>
    public partial class StatusBar : Animation.AnimatedControl
    {
        private PlayerState state;

        //Bitmap images for the UI
        BitmapImage playButtonImage = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/play-button.png"));
        BitmapImage pauseButtonImage = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/pause-button.png"));
        BitmapImage shuffleOffImage = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/shuffle-button.png"));
        BitmapImage shuffleOnImage = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/shuffle-on-button.png"));
        BitmapImage repeatOffImage = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/repeat-button.png"));
        BitmapImage repeatOnImage = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/repeat-on-button.png"));

        //Button Delegates and Events
        public delegate void MediaButtonPressedHandler(object sender, MediaButton e);
        public event MediaButtonPressedHandler OnMediaButtonPressed;


        public StatusBar()
        {
            InitializeComponent();

            //Set the initial images for the repeat and shuffle buttons
            btnRepeat.Source = repeatOffImage;
            btnShuffle.Source = shuffleOffImage;

            tbNowPlaying.Text = "";
        }


        /// <summary>
        /// The shuffle button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShuffle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Check to see if shuffle is currently on or off
            if (btnShuffle.Source == shuffleOffImage)
            {
                btnShuffle.Source = shuffleOnImage;
                RaiseMediaButtonPressedEvent(MediaButton.ShuffleOn);
            }
            else
            {
                btnShuffle.Source = shuffleOffImage;
                RaiseMediaButtonPressedEvent(MediaButton.ShuffleOff);
            }            
        }

        /// <summary>
        /// The Previous button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseMediaButtonPressedEvent(MediaButton.Back);
        }

        /// <summary>
        /// The play pause button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayPause_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Check to see if the button is set to play or pause
            if (btnPlayPause.Source == playButtonImage)
            {
                RaiseMediaButtonPressedEvent(MediaButton.Play);
            }
            else
            {
                RaiseMediaButtonPressedEvent(MediaButton.Pause);
            }      
        }

        /// <summary>
        /// The Next button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseMediaButtonPressedEvent(MediaButton.Forward);
        }

        /// <summary>
        /// The Repeat button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRepeat_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Check to see if repeat is currently on or off
            if (btnRepeat.Source == repeatOffImage)
            {
                btnRepeat.Source = repeatOnImage;
                RaiseMediaButtonPressedEvent(MediaButton.RepeatOn);
            }
            else
            {
                btnRepeat.Source = repeatOffImage;
                RaiseMediaButtonPressedEvent(MediaButton.RepeatOff);
            }    
        }

        /// <summary>
        /// Raise the media button pressed event
        /// </summary>
        /// <param name="btn"></param>
        public void RaiseMediaButtonPressedEvent(MediaButton btn)
        {
            if (OnMediaButtonPressed != null)
            {
                OnMediaButtonPressed(this, btn);
            }
        }

        /// <summary>
        /// The media player's state changed
        /// </summary>
        /// <param name="e"></param>
        public void StateChanged(PlayerState e)
        {
            state = e;

            switch (state)
            {
                case PlayerState.Paused:
                    btnPlayPause.Source = playButtonImage;
                    break;
                case PlayerState.Playing:
                    btnPlayPause.Source = pauseButtonImage;
                    break;
                case PlayerState.Stopped:
                    btnPlayPause.Source = playButtonImage;
                    break;
                case PlayerState.Ended:
                    btnPlayPause.Source = playButtonImage;
                    break;
            }
        }

        private void AnimateNowPlaying()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -tbNowPlaying.ActualWidth;
            doubleAnimation.To = canNowPlaying.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:15"));
            tbNowPlaying.BeginAnimation(Canvas.RightProperty, doubleAnimation);
        }


        /// <summary>
        /// Update the Now Playing text
        /// </summary>
        /// <param name="title"></param>
        public void NowPlayingChanged(string title)
        {
            tbNowPlaying.Text = title;
            AnimateNowPlaying();
        }
    }
}
