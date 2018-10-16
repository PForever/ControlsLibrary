using System;
using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events
{
    public class TabMovedEventArgs : EventArgs
    {
        internal Point RequesLocation { get; set; }
        internal ITabPanel TabPanel { get; set; }
    }
}