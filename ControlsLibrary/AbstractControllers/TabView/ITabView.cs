using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System.Collections.Generic;
using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface ITabView : IPanel
    {
        //позиция зависит от ориентации и наоборот
        Position Position { get; set; }
        void Show(ITabContent tabContent = null);
    }
}
