using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Tab;

namespace ControlsLibrary.Factories
{
    public interface IFactory
    {
        ITabContent CreateTabContent(object content);
        ITabContent CreateTabContent();
        void SetPanelProperty(IPanel panel, Point location, int width, int height);
        IControl CreateSeparator();
        ITabCollection CreateTabCollection();
        IControl CreateControl(object obj);
        IPanel CreatePanel(object obj);
        ITabCollection CreateTabCollection(Point location, int width, int height);
        ITabCollection CreateTabCollection(object panel);
        ITabPanel CreateTabPanel();
        ITabPanel CreateTabPanel(object panel);
        ITabPanel CreateTabPanel(ITabContent tabContent);
        IBufferedCollection CreateBufferedCollection(Point location, int width, int height);
        IBufferedCollection CreateBufferedCollection(object panel);
        IBufferedCollection CreateBufferedCollection();
        ISplitContainer CreateSplitContainer(object splitContainer);
        ISplitContainer CreateSplitContainer();
        IEnumerable<IControl> CreateControls(IEnumerable<object> controls);
        IControl CreateSeparator(int height, double defaultSep);
    }
}