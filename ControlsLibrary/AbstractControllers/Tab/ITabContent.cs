using ControlsLibrary.AbstractControllers.Tab.Events;
using System;
using System.Windows.Forms;

namespace ControlsLibrary.AbstractControllers.Tab
{
    public interface ITabContent : IDisposable
    {
        object Control { get; set; }
        string Name { get; set; }
        void RemoveAsync();
        void MoveNextAsync(int count = 0);
        void MovePreviousAsync(int count = 0);
        bool Visible { get; set; }
        void InitAsync();
        void OnTabDeleted(object sender, TabDeletedEventArgs args);
        void OnTabSelected(object sender, TabSelectedEventArgs args);
    }
}