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
using System.Windows.Forms.VisualStyles;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Factories.Concrete.WinForms;
using ControlsLibrary.Factories.Concrete.WinForms.WinHelp;
using ControlsLibrary.View;
using Orientation = ControlsLibrary.Containers.Orientation;

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
        public Func<Form> CreateDefaultTabWindow { get; set; }
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
            var tabCollection = new TabCollectionLogic(pnl, this);

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
            panel.MouseUp += (sender, e) => tabPanel.OnMouseUp(sender, new TabDropEventArgs(null, e.Location, Control.MousePosition));
            //tabPanel.MovingStart = () => panel.GetGodfather().MouseMove += tabPanel.OnMouseMove;
            //tabPanel.MovingStop = () => panel.GetGodfather().MouseMove -= tabPanel.OnMouseMove;
            return tabPanel;
        }

        public ITabPanel CreateTabPanel(object panel)
        {
            return panel is ITabPanel tabPanel ? tabPanel : new TabPanel((Panel)panel, this);
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

        public ITabWindow CreateWindow(ITabView parent, ITabPanel tab)
        {
            Form window = CreateDefaultTabWindow();
            TabView tabView = new TabView(tab);

            tabView.BubblingFromParent();
            ControlExtensions.BindingConcreteEvents(window, tabView);

            tabView.TabViewModel.Orientation = parent.Orientation;
            ITabView container = tabView.TabViewModel;
            return new TabWindow(window, parent, container);
        }
        public ITabWindow CreateWindow(TabView tabView)
        {
            Form window = CreateDefaultTabWindow();

            tabView.BubblingFromParent();
            ControlExtensions.BindingConcreteEvents(window, tabView);

            tabView.TabViewModel.Orientation = Orientation.Vertical;
            ITabView container = tabView.TabViewModel;
            return new TabWindow(window, null, container);
        }
    }
}
