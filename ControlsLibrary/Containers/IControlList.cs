using System.Collections.Generic;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;

namespace ControlsLibrary.Containers
{
    public interface IControlList : IList<IControl>
    {
        object Controls { get; set; }
        void Swap(int oldIndex, int newIndex);
        void RemoveAt(int index, bool dispose);
        void AddRange(IEnumerable<IControl> controls);
        bool Remove(IControl tabPanelTabContent, bool disposing);
    }
}