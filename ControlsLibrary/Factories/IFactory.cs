using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.Tab.AbstractControllers;

namespace ControlsLibrary.Factories
{
    public interface IFactory
    {
        ITabPanel CreateTabPanel(object panel);
        ITabPanel CreateTabPanel(ITabContent tabContent);
        ITabPanel CreateTabPanel();
        ITabContent CreateTabContent(object content);
        ITabContent CreateTabContent();
    }
}