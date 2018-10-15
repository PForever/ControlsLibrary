using System;
using ControlsLibrary.AbstractControllers.TabView.Tab;

namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface ITabWindow : IControl
    {
        ITabView Container { get; }
        ITabView Parent { get; }
        void Open();
        void Close();
    }
}