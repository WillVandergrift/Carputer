using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management;
using System.ComponentModel;
using System.IO;

namespace CarPC.FileSys
{
    /// <summary>
    /// This class is used to detect insertion and removal or removable drives
    /// </summary>
    public class RemovableDrive
    {
        //WMI variables
        ManagementEventWatcher watcherAdd = new ManagementEventWatcher();
        ManagementEventWatcher watcherRemove = new ManagementEventWatcher();
        WqlEventQuery queryAdd;
        WqlEventQuery queryRemove;

        //Background worker
        BackgroundWorker workerWatcher = new BackgroundWorker();

        //Delegates and Events
        public delegate void DeviceInsertedHandler(object sender, EventArrivedEventArgs e);
        public event DeviceInsertedHandler OnDeviceInserted;
        public delegate void DeviceRemovedHandler(object sender, EventArrivedEventArgs e);
        public event DeviceRemovedHandler OnDeviceRemoved;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RemovableDrive()
        {
            //Setup the background worker
            workerWatcher.DoWork += workerWatcher_DoWork;
            workerWatcher.Disposed += workerWatcher_Disposed;
            workerWatcher.WorkerSupportsCancellation = true;

            //WMI for inserted removable devices
            queryAdd = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            watcherAdd.EventArrived += watcherAdd_EventArrived;
            watcherAdd.Query = queryAdd;

            //WMI for removed removable devices
            queryRemove = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 3");
            watcherRemove.EventArrived += watcherRemove_EventArrived;
            watcherRemove.Query = queryRemove;
        }

        void workerWatcher_Disposed(object sender, EventArgs e)
        {
            watcherAdd.Stop();
            watcherRemove.Stop();
        }

        /// <summary>
        /// Background worker for monitoring for removable device events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void workerWatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            watcherAdd.Start();
            watcherAdd.WaitForNextEvent();
            watcherRemove.Start();
            watcherRemove.WaitForNextEvent();
        }

        /// <summary>
        /// Fire the OnDeviceInserted event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void watcherAdd_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (OnDeviceInserted != null)
            {
                OnDeviceInserted(sender, e);
            }
        }

        /// <summary>
        /// Fire the OnDeviceRemoved event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void watcherRemove_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (OnDeviceRemoved != null)
            {
                OnDeviceRemoved(sender, e);
            }
        }

        /// <summary>
        /// Start listening for removable devices
        /// </summary>
        public void StartListening()
        {
            workerWatcher.RunWorkerAsync();
        }

        /// <summary>
        /// Stop listening for removable devices
        /// </summary>
        public void StopListening()
        {
            workerWatcher.Dispose();
        }

        public List<DriveInfo> GetRemovableDrives()
        {
            try
            {
                List<DriveInfo> driveList = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable).ToList();

                return driveList;
            }
            catch
            {
                //TODO: Log Error
                return null;
            }
        }
    }
}
