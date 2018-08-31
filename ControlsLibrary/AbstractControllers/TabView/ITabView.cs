using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System.Collections.Generic;

namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface ITabView : IPanel
    {
        //позиция зависит от ориентации и наоборот
        Position Position { get; set; }
        ITabCollection TabCollection { get; set; }
        IBufferedCollection BufferedCollection { get; set; }
        void OnTabSelected(object sender, TabSelectedEventArgs args);
        void Show(ITabContent tabContent = null);
    }
}
