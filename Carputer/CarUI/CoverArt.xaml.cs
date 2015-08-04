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

using CarPC;
using CarPC.Audio;
using CarPC.FileSys;

namespace CarUI
{
    /// <summary>
    /// Interaction logic for CoverArt.xaml
    /// </summary>
    public partial class CoverArt : Animation.AnimatedControl
    {
        //The image used to display the cover art
        Image cover1 = new Image();
        Image cover2 = new Image();

        #region Properties

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CoverArt()
        {
            InitializeComponent();

            //Set an initial startup image
            cover1.Source = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/cd-cover.png"));
            transitionBox.Content = cover1;
        }

        /// <summary>
        /// Update the Cover Art image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateCoverArt(Uri curFile)
        {
            ImageSource img = TagManager.GetCoverArt(curFile.OriginalString);

            //Update our transition content to the new cover image
            if (transitionBox.Content == cover1)
            {
                cover2.Source = img;
                transitionBox.Content = cover2;
            }
            else
            {
                cover1.Source = img;
                transitionBox.Content = cover1;
            }            
        }

        /// <summary>
        /// Update the Cover Art image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateCoverArt(ImageSource curFile)
        {
            //Update our transition content to the new cover image
            if (transitionBox.Content == cover1)
            {
                cover2.Source = curFile;
                transitionBox.Content = cover2;
            }
            else
            {
                cover1.Source = curFile;
                transitionBox.Content = cover1;
            }
        }

        /// <summary>
        /// Update the Cover Art image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateCoverArt(string curFile)
        {
            ImageSource img = TagManager.GetCoverArt(curFile);

            //Update our transition content to the new cover image
            if (transitionBox.Content == cover1)
            {
                cover2.Source = img;
                transitionBox.Content = cover2;
            }
            else
            {
                cover1.Source = img;
                transitionBox.Content = cover1;
            }
        }
    }
}
