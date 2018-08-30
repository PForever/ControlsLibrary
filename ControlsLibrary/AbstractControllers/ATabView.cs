using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.AbstractControllers.Tab.Events;
using ControlsLibrary.Tab.AbstractControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlsLibrary.AbstractControllers
{
    public abstract class ATabView : ITabView
    {
        protected ITabCollection TabCollection;
        public abstract TabsPosition Position { get; set; }
        public abstract void AddTab(ITabContent tab);
        public abstract void InsertTab(ITabContent tab, int keyTab);
        public abstract void RemoveTab(int keyTab);
        public abstract void RemoveTab(ITabPanel tabKvP);
    }
}
