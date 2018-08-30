using System;
using System.Drawing;

namespace ControlsLibrary.AbstractControllers.Tab.Events
{
    public class TabMovedEventArgs : EventArgs
    {
        internal Point RequesLocation { get; set; }
    }
}