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
        void Unselect(object sender, TabEventArgs args);
        bool IsSelected { get; }
        event TabEventHandler TabDeleted;
        event TabMoveHandler TabMoved;
        event TabSelectedEventHandler TabSelected;
        void OnMouseClick(object sender, MouseEventArgs e);
        void OnMouseCaptureChanged(object sender, EventArgs e);
        Action MovingStart { get; set; }
        Action MovingStop { get; set; }
        bool IsClicked { get; set; }
        void OnMouseMove(object sender, MouseEventArgs e);
    }
}
