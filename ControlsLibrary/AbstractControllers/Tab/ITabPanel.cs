using ControlsLibrary.AbstractControllers.Tab.Events;
using System;
using System.Windows.Forms;

namespace ControlsLibrary.Tab.AbstractControllers
{
    public interface ITabPanel : IDisposable
    {
        object Panel { get; set; }
        string Name { get; set; }
        void RemoveAsync();
        event TabDeleteEventHandler TabDeleted;
        bool IsSelected { get; set; }
        event TabMoveHandler TabMoved;
        event TabSelectedEventHandler TabSelected;
    }
}
