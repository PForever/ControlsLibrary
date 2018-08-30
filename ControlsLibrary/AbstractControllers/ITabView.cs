using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.AbstractControllers.Tab.Events;
using ControlsLibrary.Tab.AbstractControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlsLibrary.AbstractControllers
{
    public enum TabsPosition
    {
        Top, Left, Right, Bottom
    }
    public interface ITabView
    {
        TabsPosition Position { get; set; }
        void AddTab(ITabContent tab);
        void InsertTab(ITabContent tab, int keyTab);
        void RemoveTab(ITabPanel tabKvP);
        void RemoveTab(int keyTab);
    }
}
