using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabView.Logic;

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
        event TabSelectedEventHandler TabSelected;

        #endregion

        #region CollectionProperty

        //TODO инкапсулировать
        //IList<TabKvP> Tabs { get; set; }

        #endregion

        void OnParentLocationChanged(object sender, LocationChangedHandlerArgs args);
        void OnParentSizeChanged(object sender, SizeChangedHandlerArgs args);
    }
}