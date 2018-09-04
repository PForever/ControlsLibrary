using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Tab;

namespace ControlsLibrary.Factories
{
    public interface IFactory
    {
        ITabPanel CreateTabPanel(object panel);
        ITabPanel CreateTabPanel(ITabContent tabContent);
        ITabPanel CreateTabPanel();
        IControl CreateControl(object control);
        ITabContent CreateTabContent(object content);
        ITabContent CreateTabContent();
        IControl CreateControl();
        ITabCollection CreateTabCollection();
        IBufferedCollection CreateBufferedCollection();
        ISplitContainer CreateSplitContainer();
    }
}