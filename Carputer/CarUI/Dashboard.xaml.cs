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

namespace CarUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        //Dashboard Delegate and Event
        public delegate void SelectionChangedHandler(object sender, DashboardItem e);
        public event SelectionChangedHandler OnSelectionChanged;

        private List<DashboardItem> items = new List<DashboardItem>();
        private int page;

        /// <summary>
        /// The dashboard page
        /// </summary>
        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        /// <summary>
        /// A list of items to display on the dashboards
        /// </summary>
        public List<DashboardItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor that generates items based on the page number
        /// </summary>
        public Dashboard(int dashboardPage)
        {
            InitializeComponent();

            //Set the dashboard page
            page = dashboardPage;

            BuildDashboard(page);
        }

        /// <summary>
        /// Add items to the dashboard
        /// </summary>
        public void BuildDashboard(int page)
        {
            //clear the dashboad
            items.Clear();

            switch (page)
            {
                case 1:
                    DashboardPageOne();
                    break;
                case 2:
                    DashboardPageTwo();
                    break;
            }

            //Update the dashboard list source
            lstDashboard.ItemsSource = null;
            lstDashboard.ItemsSource = items;
        }

        private void DashboardPageOne()
        {
            //Music
            DashboardItem dbMusic = new DashboardItem()
            {
                Name = "Music",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/music-icon.png")),
                ItemType = DashboardType.Music
            };
            items.Add(dbMusic);

            //Power
            DashboardItem dbBrowse = new DashboardItem()
            {
                Name = "Browse",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/folder-icon.png")),
                ItemType = DashboardType.Browse
            };
            items.Add(dbBrowse);

            //Power
            DashboardItem dbPower = new DashboardItem()
            {
                Name = "Power",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/power-icon.png")),
                ItemType = DashboardType.Power
            };
            items.Add(dbPower);

            //Previous
            DashboardItem dbPrevious = new DashboardItem()
            {
                Name = "Previous",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/arrow-left.png")),
                ItemType = DashboardType.Previous
            };
            items.Add(dbPrevious);

            //Next
            DashboardItem dbNext = new DashboardItem()
            {
                Name = "Next",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/arrow-right.png")),
                ItemType = DashboardType.Next
            };
            items.Add(dbNext);
        }

        private void DashboardPageTwo()
        {
            //Settings
            DashboardItem dbSettings = new DashboardItem()
            {
                Name = "Settings",
                Icon = new BitmapImage(new Uri(@"pack://application:,,,/CarUI;component/Images/settings.png")),
                ItemType = DashboardType.Settings
            };
            items.Add(dbSettings);
        }

        /// <summary>
        /// Selected item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDashboard_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectionChanged != null)
            {
                OnSelectionChanged(this, (DashboardItem)lstDashboard.SelectedItem);
            }
        }
    }
}
