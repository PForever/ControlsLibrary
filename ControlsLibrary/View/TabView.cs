using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.AbstractControllers.Tab.Events;
using ControlsLibrary.Tab.AbstractControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary.View
{
    public class TabView : ATabView
    {
        public override TabsPosition Position { get => TabCollection.Position; set => TabCollection.Position = value; }

        public override void AddTab(ITabContent tab)
        {
            TabCollection.Add(tab);
        }

        public override void InsertTab(ITabContent tab, int keyTab)
        {
            TabCollection.Insert(tab, keyTab);
        }

        public override void RemoveTab(int keyTab)
        {
            TabCollection.Remove(keyTab);
        }

        public override void RemoveTab(ITabPanel tabKvP)
        {
            TabCollection.Remove(tabKvP);
        }
    }
}
