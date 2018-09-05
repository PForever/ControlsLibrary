using ControlsLibrary.AbstractControllers.TabView;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab;
using System;
using System.Collections.Generic;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Logic;
using System.Drawing;

namespace ControlsLibrary.Factories.Concrete
{
    public class WinFactory : IFactory
    {
        #region Default
        public const double DefaultSep = 0.5;

        public Panel DefaultTabContent { get; set; }
        public Panel DefaultViewPanel { get; set; }
        public Panel DefaultTabPanel { get; set; }
        public Panel DefaultSplitPanel { get; set; }
        public Control DefaultSeparator { get; set; }
        public Panel DefaultPanel { get; set; }

        #endregion

        #region Copy
        private IPanel CopyPanel(Panel panel)
        {
            return new SimplePanel(new Panel(), this);
        }

        private ITabContent CopyTabContent(Panel control)
        {
            return CreateTabContent(new Panel());
        }
        private ITabPanel CopyTabPanel(Panel panel)
        {
            return CreateTabPanel(new Panel());
        }

        private IBufferedCollection CopyViewPanel(object panel)
        {
            IPanel pnl = CreatePanel(new Panel());
            return new ViewCollectionLogic(pnl);
        }

        private ISplitContainer CopySplitPanel(object panel)
        {
            IPanel pnl = CreatePanel(new Panel());
            return new SplitPanelLogic(pnl, this, DefaultSep);
        }
        private IControl CopyControl(Control control)
        {
            return new SimpleControl(new Control());
        }

        #endregion

        public ITabContent CreateTabContent(object content)
        {
            return new TabContent((Control)content);
        }

        public ITabContent CreateTabContent()
        {
            return CopyTabContent(DefaultTabContent);
        }

        public IPanel CreatePanel(Point location, int width, int height)
        {
            IPanel panel = CopyPanel(DefaultPanel);
            panel.Location = location;
            panel.Width = width;
            panel.Height = height;
            return panel;
        }

        public IControl CreateSeparator()
        {
            return CopyControl(DefaultSeparator);
        }


        public ITabCollection CreateTabCollection()
        {
            IPanel panel = CopyPanel(DefaultTabPanel);
            return new TabCollectionLogic(panel);
        }


        public IControl CreateControl(object control)
        {
            if(control == null) throw new ArgumentNullException();
            return control is IControl ? (IControl) control : new SimpleControl((Control) control);
        }

        public ITabCollection CreateTabCollection(object panel)
        {
            if (panel is ITabCollection) return (ITabCollection) panel;
            IPanel pnl = CreatePanel((Panel) panel);
            return new TabCollectionLogic(pnl);
        }

        public IPanel CreatePanel(Panel panel)
        {
            return new SimplePanel(panel, this);
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
            ITabPanel panel = CopyTabPanel(DefaultTabPanel);
            panel.Name = tabContent.Name;
            return panel;
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


        public ISplitContainer CreateSplitContainer(object splitContainer)
        {
            IPanel panel = CreatePanel((Panel) splitContainer);
            return new SplitPanelLogic(panel, this, DefaultSep);
        }
        public ISplitContainer CreateSplitContainer()
        {
            return CopySplitPanel(DefaultSplitPanel);
        }

        public IEnumerable<IControl> CreateControls(IEnumerable<object> controls)
        {
            foreach (object control in controls)
            {
                yield return CreateControl(controls);
            }
        }

        public IControl CreateSeparator(int height, double defaultSep)
        {
            IControl separator = new SimpleControl(DefaultSeparator);
            separator.Location = new Point(0, (int)(height * defaultSep));
            return separator;
        }

        //public (IPanel, IPanel) CreateTwoPanel(int width, int height, double separatorRelativePosition)
        //{
        //    //TODO подумать над тем, чтобы такие штуки определить в абстрактной логике
        //    IPanel panel1 = CreatePanel(new Point(0, 0), width, (int) (height * separatorRelativePosition));
        //    IPanel panel2 = CreatePanel(new Point(0, (int)(height * (1 - separatorRelativePosition))), width, (int) (height * (1 - separatorRelativePosition)));
        //    return (panel1, panel2);
        //}
    }
}
