using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView
{
    public interface ITabView : IPanel
    {
        //позиция зависит от ориентации и наоборот
        Position Position { get; set; }
        void Show(ITabContent tabContent = null);
    }
}
