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

namespace CarUI
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        DispatcherTimer tmrTick = new DispatcherTimer();

        public Clock()
        {
            InitializeComponent();

            tmrTick.Tick += tmrTick_Tick;
            tmrTick.Interval = TimeSpan.FromSeconds(1);
            tmrTick.Start();
        }

        /// <summary>
        /// Update the clock label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmrTick_Tick(object sender, EventArgs e)
        {
            lblClock.Content = DateTime.Now.ToString("t");
        }
    }
}
