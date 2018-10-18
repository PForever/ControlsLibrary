using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.AbstractControllers.TabForms.TabView;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;

namespace ControlsLibrary.Factories
{
    public interface IFactory
    {
        Func<IStripMenu, IStripMenu> CustomStripMenu { get; set; }
        Func<IStripMenuItem, IStripMenuItem> CustomStripMenuTool { get; set; }
        Func<ISplitContainer, ISplitContainer> CustomSplitPanel { get; set; }
        Func<ITabContent, ITabContent> CustomTabContent { get; set; }
        Func<IBufferedCollection, IBufferedCollection> CustomViewPanel { get; set; }
        Func<ITabPanel, ITabPanel> CustomTabPanel { get; set; }
        Func<ITabCollection, ITabCollection> CustomTabsPanel { get; set; }
        Func<ITabWindow, ITabWindow> CustomTabWindow { get; set; }

        void SwitchWindow(ITabPanel tab);

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

        IStripMenuItem CreateStripMenuItem(string name, string text = null, Keys shortcutKeys = Keys.None, EventHandler handler = null);
        IStripMenuItem CreateStripMenuItem(object tool);

        IEnumerable<IControl> CreateControls(IEnumerable controls);
        IDictionary<string, IStripMenuItem> CreateStripMenuItems(IEnumerable controls);

        ITabWindow CreateWindow(ITabWindow parent, ITabPanel tab);
        ITabWindow CreateWindow(ITabView tabView);
    }
}