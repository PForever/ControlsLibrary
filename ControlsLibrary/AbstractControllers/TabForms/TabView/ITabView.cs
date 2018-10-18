using System.Collections.Generic;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView
{
    public interface ITabView : IPanel
    {
        //TODO позиция зависит от ориентации и наоборот
        ITabWindow Owner { get; set; }
        Position Position { get; set; }
        ITabCollection TabCollection { get; }
        void Join(IEnumerable<ITabPanel> childsTab);
        void Show(ITabContent tabContent = null);
        void RemoveSelected();
        void AddNew();
    }
}
