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

using CarPC.FileSys;
using CarViewModel.Browser;

namespace CarUI
{
    /// <summary>
    /// Interaction logic for FileBrowser.xaml
    /// </summary>
    public partial class FileBrowser : UserControl
    {
        //Directory Changed Delegate and Event
        public delegate void DirectoryChangedHandler(object sender, fso Directory);
        public event DirectoryChangedHandler OnDirectoryChanged;
        //File Changed Delegate and Event
        public delegate void FileChangedHandler(object sender, fso file);
        public event FileChangedHandler OnFileChanged;

        //The list of fso objects being displayed
        private List<fso> files = new List<fso>();
        private List<string> audioFormats = new List<string>();
        private List<string> videoFormats = new List<string>();
        List<fso> sources = new List<fso>();
        private fso curItem;

        //Boolean for enabling/disabling the back button
        private bool backNavEnabled = false;

        /// <summary>
        /// Can we navigate back in the directory structure
        /// </summary>
        public bool BackNavEnabled
        {
            get { return backNavEnabled; }
            set { backNavEnabled = value; }
        }

        /// <summary>
        /// A list of all the fso objects being displayed
        /// </summary>
        public List<fso> Files
        {
            get { return files; }
            set { files = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileBrowser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for initializing the browser
        /// </summary>
        /// <param name="path"></param>
        /// <param name="audio"></param>
        /// <param name="video"></param>
        public FileBrowser(List<fso> mediaSources, List<string> audio, List<string> video)
        {
            InitializeComponent();

            //Store the passed file format lists
            audioFormats = audio;
            videoFormats = video;

            //Set the media source list to the file list
            sources = mediaSources;
            files = sources;
            DisplaySourceList();
        }

        /// <summary>
        /// Browse to the given directory and contained display all files and folders
        /// </summary>
        /// <param name="path"></param>
        public void BrowseDirectory(string path, fso parent)
        {
            using (FileBrowserVM browser = new FileBrowserVM())
            {
                files = browser.GetFilesInFolder(path, parent, audioFormats, videoFormats);
            }

            lstFiles.ItemsSource = null;
            lstFiles.ItemsSource = files;
            backNavEnabled = true;

            CallOnDirectoryChanged();
        }

        /// <summary>
        /// Selected item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFiles_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Convert the selected item to a fso
            curItem = (fso)lstFiles.SelectedItem;

            //Check for a null value
            if (curItem == null)
            {
                return;
            }

            //If the selected item is a folder, browse to it
            if (curItem.FileType == fsoType.Folder || curItem.FileType == fsoType.Source)
            {
                BrowseDirectory(curItem.Path, curItem);
            }
            else if (curItem.FileType == fsoType.Music)
            {
                //Fire the file changed event
                OnFileChanged(this, curItem);
            }
        }

        /// <summary>
        /// Navigate back in the directory structure
        /// </summary>
        public void NavigateBack()
        {
            //Update the current item
            curItem = curItem.Parent;
   
            if (curItem == null)
            {
                //We don't know where to go, so go back to the source list
                DisplaySourceList();
            }            
            else if (curItem.Path == "")
            {
                //We don't know where to go, so go back to the source list
                DisplaySourceList();
            }            
            else if (curItem.Path == "MediaSource")
            {
                //Display the source list
                DisplaySourceList();
            }
            else
            {
                //Get the parent files
                BrowseDirectory(curItem.Path, curItem);
            }
        }

        /// <summary>
        /// Display the source list
        /// </summary>
        private void DisplaySourceList()
        {
            lstFiles.ItemsSource = null;
            lstFiles.ItemsSource = sources;
            backNavEnabled = false;

            CallOnDirectoryChanged();
        }

        /// <summary>
        /// Fire the directory changed event
        /// </summary>
        private void CallOnDirectoryChanged()
        {
            if (OnDirectoryChanged != null)
            {
                OnDirectoryChanged(this, curItem);
            }
        }

        /// <summary>
        /// File the file changed event
        /// </summary>
        private void CallOnFileChanged()
        {
            if (OnFileChanged != null)
            {
                OnFileChanged(this, curItem);
            }
        }
    }
}
