using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using System;

namespace ControlsLibrary.AbstractControllers.TabView.Tab
{
    public interface ITabContent : IControl
    {
        void OnTabDeleted(object sender, TabDeletedEventArgs args);
        void OnTabSelected(object sender, TabSelectedEventArgs args);
        bool Featch { get; set; }
    }
}