using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Tab
{
    public interface ITabContent : IControl
    {
        void OnTabDeleted(object sender, TabEventArgs args);
        void OnTabSelected(object sender, TabEventArgs args);
        bool Fetch { get; set; }
    }
}