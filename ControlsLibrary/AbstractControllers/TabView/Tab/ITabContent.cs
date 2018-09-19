using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using System;

namespace ControlsLibrary.AbstractControllers.TabView.Tab
{
    public interface ITabContent : IControl
    {
        void OnTabDeleted(object sender, TabEventArgs args);
        void OnTabSelected(object sender, TabEventArgs args);
        bool Featch { get; set; }
    }
}