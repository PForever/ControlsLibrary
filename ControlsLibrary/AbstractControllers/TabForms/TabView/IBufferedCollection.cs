using System;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView
{
    public interface IBufferedCollection : IPanel
    {
        TimeSpan TimeOut { get; set; }
        int Capacity { get; set; }
        ITabContent Current { get; set; }
        void Remove(ITabContent tabPanelTabContent, bool disposing = true);
    }
}
