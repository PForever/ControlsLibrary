using System;

namespace ControlsLibrary.AbstractControllers.TabView.Tab.Events
{
    public class TabDeletedEventArgs : EventArgs
    {
        public ITabPanel TabPanel { get; set; }
        public TabDeletedEventArgs(ITabPanel tabPanel)
        {
            TabPanel = tabPanel;
        }
    }
}