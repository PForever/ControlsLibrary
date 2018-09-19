﻿using ControlsLibrary.AbstractControllers.TabView.Tab;
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
        ITabCollection TabCollection { get; set; }
        IBufferedCollection BufferedCollection { get; set; }
        void OnTabSelected(object sender, TabEventArgs args);
        void Show(ITabContent tabContent = null);
        void OnControlLocationChanged(Point oldLocation, Point newLocation);
        void OnControlSizeChanged(Size oldSize, Size newSize);
        void OnNewTabAdded(object sender, TabEventArgs arg);
    }
}
