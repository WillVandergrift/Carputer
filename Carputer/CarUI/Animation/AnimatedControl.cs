using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CarUI.Animation
{
    public class AnimatedControl : UserControl
    {
        private Animation.Animator animController;

        public Animation.Animator AnimController
        {
            get { return animController; }
            set { animController = value; }
        }

        /// <summary>
        /// The left margin position of the control when it's on screen
        /// </summary>
        public double AnimOnScreenX
        {
            get { return animController.OnScreenX; }
            set { animController.OnScreenX = value; }
        }

        /// <summary>
        /// The left margin position of the control when it's off screen
        /// </summary>
        public double AnimOffScreenX
        {
            get { return animController.OffScreenX; }
            set { animController.OffScreenX = value; }
        }

        /// <summary>
        /// The top margin position of the control when it's on screen
        /// </summary>
        public double AnimOnScreenY
        {
            get { return animController.OnScreenY; }
            set { animController.OnScreenY = value; }
        }

        /// <summary>
        /// The top margin position of the control when it's off screen
        /// </summary>
        public double AnimOffScreenY
        {
            get { return animController.OffScreenY; }
            set { animController.OffScreenY = value; }
        }

        /// <summary>
        /// The direction of the animation
        /// </summary>
        public Animation.AnimationPosition AnimDirection
        {
            get { return animController.AnimationDirection; }
            set { animController.AnimationDirection = value; }
        }

        /// <summary>
        /// The Initial state of the animation, should only be Showing or Hiding
        /// </summary>
        public Animation.AnimationState AnimInitialState
        {
            get { return animController.State; }
            set
            {
                animController.State = value;
                if (value == Animation.AnimationState.Hiding)
                {
                    animController.HideControl();
                }
                else if (value == Animation.AnimationState.Showing)
                {
                    animController.ShowControl();
                }
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AnimatedControl()
        {
            animController = new Animator(this);
        }
    }
}
