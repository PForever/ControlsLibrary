using System.Collections.Generic;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Logic;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events.Handlers;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView
{
    public interface ITabCollection : IPanel, IList<ITabPanel>
    {
        #region TabProperty
        int MaxTabLen { get; }
        ITabPanel SelectedTab { get; set; }

        #endregion
        #region TabEvents
        void OnTabMoved(object sender, TabMovedEventArgs args);
        void OnTabSelected(object sender, TabEventArgs args);
        event TabSelectedEventHandler TabSelected;
        event TabSelectedEventHandler ButtonAddClickedHandler; 
        event TabEventHandler TabDisposing;
        event TabDeletingEventHandler TabDeleting;
        #endregion

        ITabView Owner { get; set; }
        void OnMouseMove(object sender, MouseEventArgs args);
        void OnSizeChanged(object sender, SizeChangedHandlerArgs args);
        void OnTabDrop(object sender, TabDropEventArgs args);
        void OnAddClicked(object sender, TabEventArgs tabCollectionEventArgs);
        void OnTabDisposing(object sender, TabEventArgs arg);
        void OnTabDeleting(object sender, TabDeletingEventArgs arg);
        bool Remove(ITabPanel item, bool disposing);
    }

}