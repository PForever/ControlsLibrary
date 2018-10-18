using System;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events
{
    public class TabDeletingEventArgs : TabEventArgs
    {
        public bool Disposing { get; }
        public TabDeletingEventArgs(ITabPanel tabPanel, bool disposing) : base(tabPanel)
        {
            Disposing = disposing;
        }
    }
}