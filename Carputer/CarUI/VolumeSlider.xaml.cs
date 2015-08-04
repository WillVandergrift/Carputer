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
using System.Windows.Threading;
using System.ComponentModel;

namespace CarUI
{
    /// <summary>
    /// Interaction logic for VolumeSlider.xaml
    /// </summary>
    public partial class VolumeSlider : Animation.AnimatedControl
    {

        #region Properties

        /// <summary>
        /// The current slider level
        /// </summary>
        public double Level
        {
            get { return sldVolume.Value; }
            set { sldVolume.Value = value; }
        }

        /// <summary>
        /// The minimum value for the slider
        /// </summary>
        public double MinValue
        {
            get { return sldVolume.Minimum; }
            set { sldVolume.Minimum = value; }
        }

        /// <summary>
        /// The maxinum value for the slider
        /// </summary>
        public double MaxValue
        {
            get { return sldVolume.Maximum; }
            set { sldVolume.Maximum = value; }
        }

        /// <summary>
        /// The small change value for the slider
        /// </summary>
        public double SmallChange
        {
            get { return sldVolume.SmallChange; }
            set { sldVolume.SmallChange = value; }
        } 

        /// <summary>
        /// The large change value for the slider
        /// </summary>
        public double LargeChange
        {
            get { return sldVolume.LargeChange; }
            set { sldVolume.LargeChange = value; }
        }

        #endregion

        public VolumeSlider()
        {
            InitializeComponent();

            //Initialize the animation controller
            AnimController = new Animation.Animator(this); 

            //Set the slider value
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                sldVolume.Value = VolumeToValueConverter(CarPC.Audio.VolumeManager.MasterVolumeLevel());
            }    
        }

        /// <summary>
        /// Convert a volume level to a slider value
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private float ValueToVolumeConverter(double level)
        {
            return (float)(level / sldVolume.Maximum);
        }

        /// <summary>
        /// Convert the volume slider value to a usable float value
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private double VolumeToValueConverter(float level)
        {
            return (level * sldVolume.Maximum);
        }

        /// <summary>
        /// Mute the volume
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMute_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CarPC.Audio.VolumeManager.Mute(CarPC.Audio.MuteCommand.Toggle);
        }

        /// <summary>
        /// Adjust the volume level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sldVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CarPC.Audio.VolumeManager.SetLevel(ValueToVolumeConverter(sldVolume.Value));
        }
    }
}
