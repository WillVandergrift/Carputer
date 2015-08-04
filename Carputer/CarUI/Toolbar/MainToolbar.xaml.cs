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

namespace CarUI
{
    /// <summary>
    /// Interaction logic for MainToolbar.xaml
    /// </summary>
    public partial class MainToolbar : Animation.AnimatedControl
    {
        //Toolbar Delegate and Event
        public delegate void HideToolbarHandler(object sender);
        public event HideToolbarHandler OnHideToolbar;
        public delegate void HideToolbarCompleteHandler(object sender);
        public event HideToolbarCompleteHandler OnHideToolbarComplete;
        public delegate void ShowToolbarCompleteHandler(object sender);
        public event ShowToolbarCompleteHandler OnShowToolbarComplete;
        //Volume Button Delegate and Event
        public delegate void VolumeButtonPressedHandler(object sender);
        public event VolumeButtonPressedHandler OnVolumeButtonPressed;
        //Home Button Delegate and Event
        public delegate void HomeButtonPressedHandler(object sender);
        public event HomeButtonPressedHandler OnHomeButtonPressed;
        //Back Button Delegate and Event
        public delegate void BackButtonPressedHandler(object sender);
        public event BackButtonPressedHandler OnBackButtonPressed;


        public MainToolbar()
        {
            InitializeComponent();

            //Register events for animations
            AnimController.OnAnimationComplete += AnimController_OnAnimationComplete;
        }

        /// <summary>
        /// Current animation completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AnimController_OnAnimationComplete(object sender, AnimationState e)
        {
            //Raise OnHideToolbarComplete if the control just finished hiding
            if (e == AnimationState.Hidden)
            {
                RaiseOnHideToolbarComplete();
            }
            else if (e == AnimationState.Visible)
            {
                RaiseOnShowToolbarComplete();
            }
        }

        /// <summary>
        /// Raise the OnHideToolbarComplete event
        /// </summary>
        void RaiseOnHideToolbarComplete()
        {
            if (OnHideToolbarComplete != null)
            {
                OnHideToolbarComplete(this);
            }
        }

        /// <summary>
        /// Raise the OnShowToolbarComplete event
        /// </summary>
        void RaiseOnShowToolbarComplete()
        {
            if (OnShowToolbarComplete != null)
            {
                OnShowToolbarComplete(this);
            }
        }

        /// <summary>
        /// Hide the toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCollapse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideToolbar();
        }

        # region Events

        /// <summary>
        /// Hide the toolbar
        /// </summary>
        private void HideToolbar()
        {
            //Hide the toolbar
            this.AnimController.HideControl();

            //Raise the HideToolbar Event
            if (OnHideToolbar != null)
            {
                OnHideToolbar(this);
            }

        }

        #endregion

        /// <summary>
        /// The volume button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVolume_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Raise the HideToolbar Event
            if (OnVolumeButtonPressed != null)
            {
                OnVolumeButtonPressed(this);
            }
        }

        /// <summary>
        /// The home button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Raise the HomeButtonPressed Event
            if (OnHomeButtonPressed != null)
            {
                OnHomeButtonPressed(this);
            }
        }

        /// <summary>
        /// The back button was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Raise the BackButtonPressed Event
            if (OnBackButtonPressed != null)
            {
                OnBackButtonPressed(this);
            }
        }

        /// <summary>
        /// Display/Hide the back button
        /// </summary>
        /// <param name="visibile"></param>
        public void ToggleBackButton(System.Windows.Visibility visible)
        {
            btnBack.Visibility = visible;
        }

        /// <summary>
        /// Display/Hide the back button
        /// </summary>
        /// <param name="visibile"></param>
        public void ToggleBackButton(bool visible)
        {
            if (visible)
            {
                btnBack.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = System.Windows.Visibility.Hidden;
            }
            
        }
    }
}
