using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CarUI.Animation
{
    /// <summary>
    /// Positions used to determine how to animate the control
    /// </summary>
    public enum AnimationPosition
    {
        Top,
        Bottom,
        Left,
        Right
    }

    /// <summary>
    /// The current state of the animation
    /// </summary>
    public enum AnimationState
    {
        Showing,
        Hiding,
        Visible,
        Hidden
    }

    /// <summary>
    /// This class is used to create sliding animations for user controls
    /// </summary>
    public class Animator
    {
        //Animation Timer
        private DispatcherTimer tmrAnimate = new DispatcherTimer();

        //Delegates and Events
        public delegate void AnimationCompleteHandler(object sender, AnimationState e);
        public event AnimationCompleteHandler OnAnimationComplete;

        //Animation Variables
        private AnimationPosition animationDirection;
        private AnimationState state;
        private UserControl animatedControl;
        private double onScreenX;
        private double offScreenX;
        private double onScreenY;
        private double offScreenY;

        #region Properties

        /// <summary>
        /// The current state of the animation
        /// </summary>
        public AnimationState State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Which direction will the control animate around
        /// </summary>
        public AnimationPosition AnimationDirection
        {
            get { return animationDirection; }
            set { animationDirection = value; }
        }

        /// <summary>
        /// The control to animate
        /// </summary>
        public UserControl AnimatedControl
        {
            get { return animatedControl; }
            set { animatedControl = value; }
        }

        /// <summary>
        /// The controls On Screen X margin position
        /// </summary>
        public double OnScreenX
        {
            get { return onScreenX; }
            set { onScreenX = value; }
        }

        /// <summary>
        /// The controls On Screen Y margin position
        /// </summary>
        public double OnScreenY
        {
            get { return onScreenY; }
            set { onScreenY = value; }
        }

        /// <summary>
        /// The controls Off Screen X margin position
        /// </summary>
        public double OffScreenX
        {
            get { return offScreenX; }
            set { offScreenX = value; }
        }

        /// <summary>
        /// The controls Off Screen Y margin position
        /// </summary>
        public double OffScreenY
        {
            get { return offScreenY; }
            set { offScreenY = value; }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public Animator(UserControl animateControl)
        {
            InitializeTimer();
            animatedControl = animateControl;
        }

        /// <summary>
        /// Constructor that specifies a starting state for the animation
        /// </summary>
        /// <param name="startingState">Only Showing or Hiding should be used</param>
        public Animator(UserControl animateControl, AnimationState startingState)
        {
            InitializeTimer();
            animatedControl = animateControl;
            state = startingState;
            tmrAnimate.Start();
        }

        /// <summary>
        /// Set up the animation timer
        /// </summary>
        private void InitializeTimer()
        {
            //Setup the animation timer
            tmrAnimate.Interval = TimeSpan.FromMilliseconds(10);
            tmrAnimate.Tick += tmrAnimate_Tick;
        }

        /// <summary>
        /// Animate the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmrAnimate_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                case AnimationState.Showing:
                    //Play the correct showing animation based on direction
                    switch (animationDirection)
                    {
                        case AnimationPosition.Left:
                            ShowFromLeft();
                            break;
                        case AnimationPosition.Right:
                            ShowFromRight();
                            break;
                        case AnimationPosition.Top:
                            ShowFromTop();
                            break;
                        case AnimationPosition.Bottom:
                            ShowFromBottom();
                            break;
                    }
                    break;
                case AnimationState.Hiding:
                    //Play the correct hiding animation based on direction
                    switch (animationDirection)
                    {
                        case AnimationPosition.Left:
                            HideFromLeft();
                            break;
                        case AnimationPosition.Right:
                            HideFromRight();
                            break;
                        case AnimationPosition.Top:
                            HideFromTop();
                            break;
                        case AnimationPosition.Bottom:
                            HideFromBottom();
                            break;
                    }
                    break;
                default:
                    //Stop animation
                    StopAnimation();
                    break; 
            }
        }

        /// <summary>
        /// Stop the animation timer
        /// </summary>
        private void StopAnimation()
        {
            tmrAnimate.Stop();
        }

        private void AnimationComplete()
        {
            tmrAnimate.Stop();

            //Raise our on animation complete event
            if (OnAnimationComplete != null)
            {
                OnAnimationComplete(this, state);
            }
        }

        /// <summary>
        /// Slide the control into view from the left
        /// </summary>
        private void ShowFromLeft()
        {
            //Slide the control into view
            if (animatedControl.Margin.Left < onScreenX)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left + 2, animatedControl.Margin.Top, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(onScreenX, animatedControl.Margin.Top, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
                state = AnimationState.Visible;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Slide the control off screen to the left
        /// </summary>
        private void HideFromLeft()
        {
            //Slide the control out of view
            if (animatedControl.Margin.Left > offScreenX)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left - 2, animatedControl.Margin.Top, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(offScreenX, animatedControl.Margin.Top, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
                state = AnimationState.Hidden;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Slide the control into view from the right
        /// </summary>
        private void ShowFromRight()
        {
            //Slide the control into view
            if (animatedControl.Margin.Right < onScreenX)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, animatedControl.Margin.Top, animatedControl.Margin.Right + 2, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(onScreenX, animatedControl.Margin.Top, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
                state = AnimationState.Visible;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Slide the control off screen to the right
        /// </summary>
        private void HideFromRight()
        {
            //Slide the control out of view
            if (animatedControl.Margin.Right > offScreenX)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, animatedControl.Margin.Top, animatedControl.Margin.Right - 2, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(offScreenX, animatedControl.Margin.Top, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
                state = AnimationState.Hidden;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Slide the control into view from the top
        /// </summary>
        private void ShowFromTop()
        {
            //Slide the control into view
            if (animatedControl.Margin.Top < onScreenY)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, animatedControl.Margin.Top + 2, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, animatedControl.Margin.Top, animatedControl.Margin.Right, onScreenY);
                animatedControl.Margin = t;
                state = AnimationState.Visible;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Slide the control off screen to the top
        /// </summary>
        private void HideFromTop()
        {
            //Slide the control into view
            if (animatedControl.Margin.Top > OffScreenY)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, animatedControl.Margin.Top -2, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, offScreenY, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
                state = AnimationState.Hidden;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Slide the control into view from the bottom
        /// </summary>
        private void ShowFromBottom()
        {
            //Slide the control into view
            if (animatedControl.Margin.Top > onScreenY)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, animatedControl.Margin.Top - 2, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, onScreenY, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
                state = AnimationState.Visible;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Slide the control out of view from the bottom
        /// </summary>
        private void HideFromBottom()
        {
            //Slide the control into view
            if (animatedControl.Margin.Top < offScreenY)
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, animatedControl.Margin.Top + 2, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
            }
            else
            {
                Thickness t = new Thickness(animatedControl.Margin.Left, offScreenY, animatedControl.Margin.Right, animatedControl.Margin.Bottom);
                animatedControl.Margin = t;
                state = AnimationState.Hidden;
                AnimationComplete();
            }
        }

        /// <summary>
        /// Show the volume slider
        /// </summary>
        public void ShowControl()
        {
            state = AnimationState.Showing;
            tmrAnimate.Start();
        }

        /// <summary>
        /// Hide the volume slider
        /// </summary>
        public void HideControl()
        {
            state = AnimationState.Hiding;
            tmrAnimate.Start();
        }

        /// <summary>
        /// Toggle the animation state based on its current value
        /// </summary>
        public void ToggleVisibility()
        {
            if (state == AnimationState.Visible || state == AnimationState.Showing)
            {
                state = AnimationState.Hiding;
            }
            else if (state == AnimationState.Hidden || state == AnimationState.Hiding)
            {
                state = AnimationState.Showing;
            }

            tmrAnimate.Start();
        }
    }
}
