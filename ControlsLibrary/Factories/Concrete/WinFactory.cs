using ControlsLibrary.AbstractControllers.TabView;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab;
using System;
using System.Collections.Generic;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Logic;

namespace ControlsLibrary.Factories.Concrete
{
    public class WinFactory : IFactory
    {
        public Panel DefaultTabContent { get; set; }
        public Panel DefaultViewPanel { get; set; }
        public Panel DefaultTabPanel { get; set; }
        public Panel DefaultSplitPanel { get; set; }
        public ITabContent CreateTabContent(object content)
        {
            return new TabContent((Control)content);
        }

        public ITabContent CreateTabContent()
        {
            return CopyTabContent(DefaultTabContent);
        }

        public ITabCollection CreateTabCollection()
        {
            throw new NotImplementedException();
        }

        private ITabContent CopyTabContent(Panel control)
        {
            return CreateTabContent(new Panel());
        }
        private ITabPanel CopyTabPanel(Panel panel)
        {
            return CreateTabPanel(new Panel());
        }

        public IControl CreateControl(object control)
        {
            if(control == null) throw new ArgumentNullException();
            return control is IControl ? (IControl) control : new SimplControl((Control) control);
        }

        public ITabCollection CreateTabCollection(object panel)
        {
            if (panel is ITabCollection) return (ITabCollection) panel;
            IPanel pnl = CreatePanel((Panel) panel);
            return new TabCollectionLogic(pnl);
        }

        private IPanel CreatePanel(Panel panel)
        {
            throw new NotImplementedException();
        }

        public ITabPanel CreateTabPanel()
        {
            return CopyTabPanel(DefaultTabPanel);
        }
        public ITabPanel CreateTabPanel(object panel)
        {
            return panel is ITabPanel ? (ITabPanel) panel : new TabPanel((Panel)panel, this);
        }
        public ITabPanel CreateTabPanel(ITabContent tabContent)
        {
            return TabPanel.CreateDefaultPanel((Control)CreateTabContent(tabContent).Control, this);
        }

        public IBufferedCollection CreateBufferedCollection(object panel)
        {
            if (panel is IBufferedCollection) return (IBufferedCollection) panel;
            IPanel pnl = CreatePanel((Panel)panel);
            return new ViewCollectionLogic(pnl);
        }
        public IBufferedCollection CreateBufferedCollection()
        {
            return CopyViewPanel(DefaultViewPanel);
        }

        private IBufferedCollection CopyViewPanel(object panel)
        {
            IPanel pnl = CreatePanel(new Panel());
            return new ViewCollectionLogic(pnl);
        }

        public ISplitContainer CreateSplitContainer(object splitContainer)
        {
            return new TabViewContainer((Panel) splitContainer, this);
        }
        public ISplitContainer CreateSplitContainer()
        {
            return CopySplitPanel(DefaultSplitPanel);
        }

        private ISplitContainer CopySplitPanel(object panel)
        {
            IPanel pnl = CreatePanel(new Panel());
            return new SplitPanelLogic(pnl);
        }

        public IEnumerable<IControl> CreateControls(IEnumerable<object> controls)
        {
            foreach (object control in controls)
            {
                yield return CreateControl(controls);
            }
        }
    }
}
