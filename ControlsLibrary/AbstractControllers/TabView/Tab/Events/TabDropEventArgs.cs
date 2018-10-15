using System;
using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabView.Tab.Events
{
    public class TabDropEventArgs : EventArgs
    {
        public Point MouseAbsolutePoint { get; }
        public Point MousePoint { get; }
        public ITabPanel TabPanel { get; }
        public TabDropEventArgs(ITabPanel tabPanel, Point mousePoint, Point mouseAbsolutePoint)
        {
            TabPanel = tabPanel;
            MousePoint = mousePoint;
            MouseAbsolutePoint = mouseAbsolutePoint;
        }
    }
}