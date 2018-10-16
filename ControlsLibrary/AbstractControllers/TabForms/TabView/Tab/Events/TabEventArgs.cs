using System;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events
{
    public class TabEventArgs : EventArgs
    {
        public ITabPanel TabPanel { get; }
        public TabEventArgs(ITabPanel tabPanel)
        {
            TabPanel = tabPanel;
        }
    }
}