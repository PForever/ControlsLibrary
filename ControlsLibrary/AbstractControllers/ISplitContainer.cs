using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events.Handlers;

namespace ControlsLibrary.AbstractControllers
{
    public interface ISplitContainer : IPanel
    {
        IPanel Panel1 { get; set; }
        IPanel Panel2 { get; set; }
        int RelativePosition { get; set; }
        event TabEventHandler AddNewTab;
        event TabEventHandler RemoveSelectedTab;
    }
}