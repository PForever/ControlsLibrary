using System;

namespace ControlsLibrary.AbstractControllers.TabView.Tab.Events
{
    public class TabEventArgs : EventArgs
    {
        public ITabPanel TabPanel { get; set; }
        public TabEventArgs(ITabPanel tabPanel)
        {
            TabPanel = tabPanel;
        }
    }
}