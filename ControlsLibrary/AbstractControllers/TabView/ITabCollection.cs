using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System.Collections.Generic;
using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface ITabCollection : IPanel, IList<ITabPanel>
    {
        #region TabProperty
        int MaxTabWidth { get; }
        #endregion
        #region TabEvents
        void OnTabDeleted(object sender, TabDeletedEventArgs args);
        void OnTabMoved(object sender, TabMovedEventArgs args);
        void OnTabSelected(object sender, TabSelectedEventArgs args);
        #endregion
        #region CollectionProperty
        //TODO инкапсулировать
        //IList<TabKvP> Tabs { get; set; }
        #endregion
    }
}