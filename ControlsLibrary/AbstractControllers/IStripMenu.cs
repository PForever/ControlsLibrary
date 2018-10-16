using ControlsLibrary.Factories.Concrete.WinForms.Controls;

namespace ControlsLibrary.AbstractControllers
{
    public interface IStripMenu : IContainer
    {
        IStripMenuItemsCollection InnerTools { get; }
    }
}