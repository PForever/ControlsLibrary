using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using System;
using System.Windows.Forms;

namespace ControlsLibrary.AbstractControllers.TabView.Tab
{
    public interface ITabPanel : IPanel
    {
        //object Panel { get; set; }
        ITabContent TabContent { get; }
        void Delete();
        void Select();
        bool IsSelected { get; set; }
        event TabEventHandler TabDeleted;
        event TabMoveHandler TabMoved;
        event TabSelectedEventHandler TabSelected;
        void OnMouseClick(object sender, MouseEventArgs e);
    }
}
