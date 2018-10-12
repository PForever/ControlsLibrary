using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using System;
using System.Drawing;
using System.Threading.Tasks;
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
        event TabEventHandler TabDrop;
        event TabEventHandler Disposing;
        void OnMouseClick(object sender, MouseEventArgs e);
        void OnMouseCaptureChanged(object sender, EventArgs e);
        void ChangeLocation(Point point);
        Func<Point, Task> MoveAnimation { set; }
        bool IsClicked { get; set; }
        Point ClickPosition { get; set; }
        void OnMouseMove(object sender, MouseEventArgs e);
        void BringToFront();
    }
}
