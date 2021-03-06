﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events.Handlers;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Tab
{
    public interface ITabPanel : IPanel
    {
        //object Panel { get; set; }
        ITabContent TabContent { get; }
        void Delete(bool disposing);
        void Select();
        void Unselect(object sender, TabEventArgs args);
        bool IsSelected { get; }
        event TabDeletingEventHandler TabDeleting;
        event TabMoveHandler TabMoved;
        event TabSelectedEventHandler TabSelected;
        event TabDropEventHandler TabDrop;
        event TabEventHandler Disposing;
        void OnMouseClick(object sender, MouseEventArgs e);
        void OnMouseUp(object sender, TabDropEventArgs e);
        void ChangeLocation(Point point);
        Func<Point, Task> MoveAnimation { set; }
        bool IsClicked { get; set; }
        Point ClickPosition { get; set; }
        void OnMouseMove(object sender, MouseEventArgs e);
        void BringToFront();
    }
}
