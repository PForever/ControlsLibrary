using ControlsLibrary.AbstractControllers.TabView;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab;
using System;
using System.Collections;
using System.Collections.Generic;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Logic;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Factories.Concrete.WinForms;
using ControlsLibrary.Factories.Concrete.WinForms.WinHelp;

namespace ControlsLibrary.Factories.Concrete
{

    public class WinFactory : IFactory
    {
        #region WinForms
        #region Default
        public const double DefaultSep = 0.5;
        public TableLayoutPanel DefaultSplitPanel;
        public Func<Panel> CreateDefaultTabContent { get; set; }
        public Func<Panel> CreateDefaultViewPanel { get; set; }
        public Func<Panel> CreateDefaultTabPanel { get; set; }
        public Func<TableLayoutPanel> CreateDefaultSplitPanel { get; set; }
        public Func<Panel> CreateDefaultTabsPanel { get; set; }

        #endregion


        private T TypeCheck<T>(object obj)
        {
            if(obj == null) throw new ArgumentNullException();
            if (obj is T res) return res;
            throw new ArgumentException();
        }

        #region Copy
        private IPanel CopyPanel(object obj)
        {
            Panel panel = TypeCheck<Panel>(obj);
            return new SimplePanel(panel.Clone(), this);
        }

        private ITabContent CopyTabContent(object obj)
        {
            Control control = TypeCheck<Control>(obj);
            return CreateTabContent(control.Clone());
        }
        private ITabPanel CopyTabPanel(object obj)
        {
            Panel panel = TypeCheck<Panel>(obj);
            return CreateTabPanel(panel.Clone());
        }

        private ISplitContainer CopySplitPanel(object obj)
        {
            TableLayoutPanel panel = TypeCheck<TableLayoutPanel>(obj);

            return new TableSplitContainer(panel);//SplitPanelLogic(pnl, this, DefaultSep);
        }
        private IControl CopyControl(object obj)
        {
            Control control = TypeCheck<Control>(obj);
            return new SimpleControl(control.Clone());
        }
        #endregion
        #endregion

        public ITabContent CreateTabContent(object content)
        {
            return new TabContent((Control)content);
        }

        public ITabContent CreateTabContent()
        {
            return CreateTabContent(CreateDefaultTabContent());
        }

        //public void SetPanelProperty(IPanel panel, Point location, int width, int height)
        //{
        //    panel.Location = location;
        //    panel.Width = width;
        //    panel.Height = height;
        //}

        //public ISetarator CreateSeparator()
        //{
        //    IControl separator = CopyControl(DefaultSeparator);
        //    return new SeparatorLogic(separator, DefaultSplitPanel.Height, DefaultSep);
        //}

        public IControl CreateControl(object obj)
        {
            Control control = TypeCheck<Control>(obj);
            return new SimpleControl(control);
        }

        public IPanel CreatePanel(object obj)
        {
            Panel panel = TypeCheck<Panel>(obj);
            return new SimplePanel(panel, this);
        }

        public ITabCollection CreateTabCollection()
        {
            ITabCollection tabCollection = CreateTabCollection(CreateDefaultTabsPanel());
            ((Panel)tabCollection.Control).Dock = DockStyle.Fill;
            //SetPanelProperty(tabCollection, location, width, height);
            return tabCollection;
        }

        public ITabCollection CreateTabCollection(object panel)
        {
            if (panel is ITabCollection) return (ITabCollection) panel;
            IPanel pnl = CreatePanel((Panel) panel);
            var tabCollection = new TabCollectionLogic(pnl);

            Panel control = (Panel) tabCollection.Control;

            control.MouseDoubleClick += (sender, e) => tabCollection.OnAddClicked(this, new TabEventArgs(null));
            control.SizeChanged += (sender, e) => tabCollection.OnSizeChanged(this, new SizeChangedHandlerArgs(((Panel)tabCollection.Control).Size, Size.Empty));
            control.MouseMove += tabCollection.OnMouseMove;
            return tabCollection;
        }


        public ITabPanel CreateTabPanel()
        {
            ITabPanel tabPanel = CreateTabPanel(CreateDefaultTabPanel());
            Panel panel = (Panel) tabPanel.Control;
            panel.MouseDown += tabPanel.OnMouseClick;
            panel.MouseCaptureChanged += tabPanel.OnMouseCaptureChanged;
            //tabPanel.MovingStart = () => panel.GetGodfather().MouseMove += tabPanel.OnMouseMove;
            //tabPanel.MovingStop = () => panel.GetGodfather().MouseMove -= tabPanel.OnMouseMove;
            return tabPanel;
        }

        public ITabPanel CreateTabPanel(object panel)
        {
            return panel is ITabPanel tabPanel ? tabPanel : new TabPanel((Panel)panel, this);
        }
        public ITabPanel CreateTabPanel(ITabContent tabContent)
        {
            ITabPanel panel = CreateTabPanel(CreateDefaultTabPanel());
            panel.Name = tabContent.Name;
            return panel;
        }

        public IBufferedCollection CreateBufferedCollection()
        {
            IBufferedCollection viewPanel = CreateBufferedCollection(CreateDefaultViewPanel());
            //SetPanelProperty(viewPanel, location, width, height);
            return viewPanel;
        }
        public IBufferedCollection CreateBufferedCollection(object panel)
        {
            if (panel is IBufferedCollection collection) return collection;
            IPanel pnl = CreatePanel((Panel)panel);
            return new ViewCollectionLogic(pnl);
        }

        public ISplitContainer CreateSplitContainer(object splitContainer)
        {
            //IPanel panel = CreatePanel((Panel) splitContainer);
            TableLayoutPanel table = TypeCheck<TableLayoutPanel>(splitContainer);
            var container = new TableSplitContainer(table); //new SplitPanelLogic(panel, this, DefaultSep);
            Control control = (Control) container.Control;
            control.KeyUp += container.OnKeyUp;
            return container;
        }
        public ISplitContainer CreateSplitContainer(bool copy = true)
        {
            return CreateSplitContainer(copy ? CreateDefaultSplitPanel() : DefaultSplitPanel);
        }

        public IEnumerable<IControl> CreateControls(IEnumerable controls)
        {
            foreach (object control in controls)
            {
                yield return CreateControl(control);
            }
        }

        //public ISetarator CreateSeparator(int height, double defaultSep)
        //{
        //    IControl separator = new SimpleControl(CreateDefaultDefaultSeparator);
        //    return new SeparatorLogic(separator, height, defaultSep);
        //}

        public ITabView CreateTabView()
        {
            ITabView tabView = new TabViewLogic(this);
            Panel panel = (Panel) tabView.Control;
            //panel.LocationChanged += (sender, arg) =>
            //    tabView.OnControlLocationChanged(Point.Empty, panel.Location);
            //panel.SizeChanged += (sender, arg) => tabView.OnControlSizeChanged(Size.Empty, panel.Size);
            return tabView;
        }
    }
}
