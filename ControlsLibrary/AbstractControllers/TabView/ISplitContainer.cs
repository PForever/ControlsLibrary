using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;

namespace ControlsLibrary.AbstractControllers.TabView
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