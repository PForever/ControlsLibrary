using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.Tab.AbstractControllers;

namespace ControlsLibrary.AbstractControllers
{
    public struct TabKvP
    {
        internal ITabPanel TabPanel; 
        internal ITabContent TabView;

        public TabKvP(ITabPanel tabPanel, ITabContent tabView)
        {
            TabPanel = tabPanel;
            TabView = tabView;
        }
    }
}