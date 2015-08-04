using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CarUI
{
    public enum PlayerState
    {
        Opened,
        Ended,
        Stopped,
        Playing,
        Paused
    }

    public class CarMediaElement : MediaElement
    {
        //State Change Delegate and Event
        public delegate void StateChangedHandler(object sender, PlayerState e);
        public event StateChangedHandler OnStateChanged;

        private PlayerState state = PlayerState.Stopped;

        /// <summary>
        /// The current state of the media player
        /// </summary>
        public PlayerState State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CarMediaElement()
        {
            base.MediaEnded += CarMediaElement_MediaEnded;
            base.MediaOpened += CarMediaElement_MediaOpened;
        }

        /// <summary>
        /// A file was opened in the media player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CarMediaElement_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            state = PlayerState.Opened;
            RaiseOnStateChanged();
        }

        /// <summary>
        /// The current song ended
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CarMediaElement_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            state = PlayerState.Ended;
            RaiseOnStateChanged();
        }
        
        /// <summary>
        /// Start played the loaded media file
        /// </summary>
        public void Play()
        {
            state = PlayerState.Playing;
            RaiseOnStateChanged();
            base.Play();
        }

        /// <summary>
        /// Pause the media file
        /// </summary>
        public void Pause()
        {
            state = PlayerState.Paused;
            RaiseOnStateChanged();
            base.Pause();
        }

        /// <summary>
        /// Stop the media file
        /// </summary>
        public void Stop()
        {
            state = PlayerState.Stopped;
            RaiseOnStateChanged();
            base.Stop();
        }

        /// <summary>
        /// Raise the on state changed event
        /// </summary>
        public void RaiseOnStateChanged()
        {
            if (OnStateChanged != null)
            {
                OnStateChanged(this, state);
            }
        }
    }
}
