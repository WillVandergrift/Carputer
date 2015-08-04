using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarPC.Dashboard;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;

namespace CarViewModel.Dashboard
{
    public class PowerVM : IDisposable
    {
        public void ProcessPowerCommand(PowerType command)
        {
            switch (command)
            {
                case PowerType.Shutdown:
                    Process.Start("shutdown", "/s /t 0");
                    break;
                case PowerType.Restart:
                    Process.Start("shutdown", "/r /t 0");
                    break;
                case PowerType.Hibernate:
                    System.Windows.Forms.Application.SetSuspendState(PowerState.Hibernate, true, true);
                    break;
                case PowerType.Close:
                    System.Windows.Application.Current.Shutdown();
                    break;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
