using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Tab;

namespace ControlsLibrary.Factories
{
    public interface IFactory
    {
        ITabPanel CreateTabPanel(object panel);
        ITabPanel CreateTabPanel(ITabContent tabContent);
        ITabPanel CreateTabPanel();
        ITabContent CreateTabContent(object content);
        ITabContent CreateTabContent();
        ITabCollection CreateTabCollection();
        IBufferedCollection CreateBufferedCollection();
    }
}