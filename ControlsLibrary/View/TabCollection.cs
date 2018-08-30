using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.AbstractControllers.Tab.Events;
using ControlsLibrary.Factories;
using ControlsLibrary.Tab.AbstractControllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary.View
{
    class TabCollection : ITabCollection, ICreator
    {
        public TabsPosition Position { get; set; }

        public double MaxTabWidth { get; set; }

        public IList<TabKvP> Tabs { get; set; }

        public int Count => Tabs.Count;

        public int Capacity { get => Tabs.Count; set => throw new NotImplementedException(); }

        public IFactory factory { get; }

        public void Add(ITabContent tab)
        {
            ITabPanel tabPanel = factory.CreateTabPanel(tab);
            TabKvP tabKvP = new TabKvP(tabPanel, tab);
            //TODO проверка длины
            Tabs.Add(tabKvP);
        }

        public void Insert(ITabContent tab, int position)
        {
            Add(tab);
            //TODO возможны проблемы с анимацией, мб понадобится флаг
            MoveTo(Tabs.Last(), position);
        }

        public void Insert(ITabContent tab, Point location)
        {
            int position = PositionFromLocation(location);
            Insert(tab, position);
        }

        private int PositionFromLocation(Point location)
        {
            throw new NotImplementedException();
        }

        public void MoveNextAsync(TabKvP tabKvP, int count = 0)
        {
            throw new NotImplementedException();
        }

        public void MovePreviousAsync(TabKvP tabKvP, int count = 0)
        {
            throw new NotImplementedException();
        }

        public void MoveTo(TabKvP tabKvP, int localPosition)
        {
            throw new NotImplementedException();
        }

        public void OnTabDeleted(object sender, TabDeletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnTabMoved(object sender, TabMovedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Remove(TabKvP tabKvP)
        {
            throw new NotImplementedException();
        }

        public void Remove(ITabPanel tabKvP)
        {
            throw new NotImplementedException();
        }

        public void Remove(int keyTab)
        {
            throw new NotImplementedException();
        }
    }
}
