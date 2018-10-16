using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events
{
    public struct LocationChangedHandlerArgs
    {
        public Point NewLocation { get; set; }
        public Point OldLocation { get; set; }

        public LocationChangedHandlerArgs(Point newLocation, Point oldLocation)
        {
            NewLocation = newLocation;
            OldLocation = oldLocation;  
        }
    }
}