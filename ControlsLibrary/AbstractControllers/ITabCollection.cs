using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.AbstractControllers.Tab.Events;
using ControlsLibrary.Tab.AbstractControllers;
using System.Collections.Generic;
using System.Drawing;

namespace ControlsLibrary.AbstractControllers
{
    public interface ITabCollection
    {
        TabsPosition Position { get; set; }
        double MaxTabWidth { get; }
        //TODO инкапсулировать
        IList<TabKvP> Tabs { get; set; }
        int Count { get; }
        int Capacity { get; set; }
        void OnTabDeleted(object sender, TabDeletedEventArgs args);
        void OnTabMoved(object sender, TabMovedEventArgs args);
        void OnTabSelected(object sender, TabSelectedEventArgs args);


        void MoveNextAsync(TabKvP tabKvP, int count = 0);
        void MovePreviousAsync(TabKvP tabKvP, int count = 0);
        void MoveTo(TabKvP tabKvP, int localPosition);

        void Add(ITabContent tab);
        void Insert(ITabContent tab, int position);
        void Insert(ITabContent tab, Point location);

        void Remove(TabKvP tabKvP);
        void Remove(ITabPanel tabKvP);
        void Remove(int keyTab);
    }
}