using ControlsLibrary.AbstractControllers.TabForms.TabView;

namespace ControlsLibrary.AbstractControllers.TabForms
{
    public interface ITabWindow : IControl
    {
        ITabView Container { get; }
        ITabView Parent { get; }
        void Open();
        void Close();
    }
}