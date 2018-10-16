using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.AbstractControllers.TabForms.TabView;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;

namespace ControlsLibrary.Factories
{
    public interface IFactory
    {
        IControl CreateControl(object control);
        IPanel CreatePanel(object panel);

        ITabContent CreateTabContent();
        ITabContent CreateTabContent(object content);

        ITabCollection CreateTabCollection();
        ITabCollection CreateTabCollection(object panel);

        ITabPanel CreateTabPanel();
        ITabPanel CreateTabPanel(object panel);

        IBufferedCollection CreateBufferedCollection();
        IBufferedCollection CreateBufferedCollection(object panel);

        ISplitContainer CreateSplitContainer();
        ISplitContainer CreateSplitContainer(object splitContainer);

        IStripMenu CreateStripMenu();
        IStripMenu CreateStripMenu(object menu);

        IStripMenuItem CreateStripMenuTool();
        IStripMenuItem CreateStripMenuTool(object tool);

        IEnumerable<IControl> CreateControls(IEnumerable controls);
        IDictionary<string, IStripMenuItem> CreateStripMenuItems(IEnumerable controls);

        ITabWindow CreateWindow(ITabView parent, ITabPanel tab);
        ITabWindow CreateWindow(ITabView tabView);
    }
}