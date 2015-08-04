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

using CarPC.Dashboard;
using CarViewModel.Dashboard;

namespace CarUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Power : UserControl
    {
        //Power Delegate and Event
        public delegate void SelectionChangedHandler(object sender, PowerItem e);
        public event SelectionChangedHandler OnSelectionChanged;

        private List<PowerItem> items = new List<PowerItem>();

        /// <summary>
        /// A list of items to display on the power menu
        /// </summary>
        public List<PowerItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Power()
        {
            InitializeComponent();
            BuildPowerMenu();
        }

        /// <summary>
        /// Add items to the power menu
        /// </summary>
        public void BuildPowerMenu()
        {
            //clear the dashboad
            items.Clear();

            //Shutdown
            PowerItem dbShutdown = new PowerItem()
            {
                Name = "Shutdown",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/shutdown-icon.png")),
                ItemType = PowerType.Shutdown
            };
            items.Add(dbShutdown);

            //Restart
            PowerItem dbRestart = new PowerItem()
            {
                Name = "Restart",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/restart-icon.png")),
                ItemType = PowerType.Restart
            };
            items.Add(dbRestart);

            //Hibernate
            PowerItem dbHibernate = new PowerItem()
            {
                Name = "Hibernate",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/hibernate-icon.png")),
                ItemType = PowerType.Hibernate
            };
            items.Add(dbHibernate);

            //Close
            PowerItem dbClose = new PowerItem()
            {
                Name = "Close",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/close-icon.png")),
                ItemType = PowerType.Close
            };
            items.Add(dbClose);

            //Update the dashboard list source
            lstPower.ItemsSource = null;
            lstPower.ItemsSource = items;
        }

        /// <summary>
        /// Selected item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstPower_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RunPowerCommand(((PowerItem)lstPower.SelectedItem).ItemType);

            //if (OnSelectionChanged != null)
            //{
            //    OnSelectionChanged(this, (PowerItem)lstPower.SelectedItem);
            //}
        }

        /// <summary>
        /// Process the power command
        /// </summary>
        /// <param name="command"></param>
        private void RunPowerCommand(PowerType command)
        {
            using (PowerVM power = new PowerVM())
            {
                power.ProcessPowerCommand(command);
            }
        }
    }
}
