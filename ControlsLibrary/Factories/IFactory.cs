﻿using System.Collections;
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
        ITabCollection CreateTabCollection();
        IControl CreateControl(object obj);
        IPanel CreatePanel(object obj);
        ITabCollection CreateTabCollection(object panel);
        ITabPanel CreateTabPanel();
        ITabPanel CreateTabPanel(object panel);
        IBufferedCollection CreateBufferedCollection(object panel);
        IBufferedCollection CreateBufferedCollection();
        ISplitContainer CreateSplitContainer(object splitContainer);
        ISplitContainer CreateSplitContainer(bool copy = true);
        IEnumerable<IControl> CreateControls(IEnumerable controls);
        ITabWindow CreateWindow(ITabView parent, ITabPanel tab);

    }
}