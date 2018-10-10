using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView.Logic;

namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface ITabCollection : IPanel, IList<ITabPanel>
    {
        #region TabProperty
        int MaxTabWidth { get; }
        ITabPanel SelectedTab { get; set; }

        #endregion
        #region TabEvents
        void OnTabDeleted(object sender, TabEventArgs args);
        void OnTabMoved(object sender, TabMovedEventArgs args);
        void OnTabSelected(object sender, TabEventArgs args);
        event TabSelectedEventHandler TabSelected;
        event TabSelectedEventHandler ButtonAddClickedHandler;
        event TabEventHandler TabDisposing;
        #endregion

        void OnMouseMove(object sender, MouseEventArgs args);
        void OnSizeChanged(object sender, SizeChangedHandlerArgs args);
        void OnTabDrop(object sender, TabEventArgs args);
        void OnAddClicked(object sender, TabEventArgs tabCollectionEventArgs);
        void OnTabDisposing(object sender, TabEventArgs arg);
    }

}