using System.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using ControlsLibrary.AbstractControllers;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.AbstractControllers.TabForms.TabView;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Logic;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events;
using ControlsLibrary.Factories.Concrete.WinForms;
using ControlsLibrary.Factories.Concrete.WinForms.Controls;
using ControlsLibrary.Factories.Concrete.WinForms.Controls.TabForm;
using ControlsLibrary.Factories.Concrete.WinForms.Controls.TabForm.TabView.Tab;
using ControlsLibrary.Factories.Concrete.WinForms.WinHelp;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete
{

    public class WinFactory : IFactory
    {
        #region WinForms
        #region Default
        public const double DefaultSep = 0.5;
        public Func<MenuStrip> CreateDefaultStripMenu { get; set; }
        public Func<ToolStripMenuItem> CreateDefaultStripMenuTool { get; set; }
        public Func<TableLayoutPanel> CreateDefaultSplitPanel { get; set; }
        public Func<Panel> CreateDefaultTabContent { get; set; }
        public Func<Panel> CreateDefaultViewPanel { get; set; }
        public Func<Panel> CreateDefaultTabPanel { get; set; }
        public Func<Panel> CreateDefaultTabsPanel { get; set; }
        public Func<Form> CreateDefaultTabWindow { get; set; }
        #endregion


        private static T TypeCheck<T>(object obj)
        {
            switch (obj)
            {
                case null:
                    throw new ArgumentNullException();
                case T res:
                    return res;
                default:
                    throw new ArgumentException();
            }
        }
        #endregion

        public IControl CreateControl(object control)
        {
            Control winControl = TypeCheck<Control>(control);
            return new SimpleControl(winControl);
        }

        public IPanel CreatePanel(object panel)
        {
            Panel winPanel = TypeCheck<Panel>(panel);
            return new SimplePanel(winPanel, this);
        }



        public ITabContent CreateTabContent()
        {
            return CreateTabContent(CreateDefaultTabContent());
        }

        public ITabContent CreateTabContent(object panel)
        {
            Panel winPanel = TypeCheck<Panel>(panel);
            return new TabContent(winPanel);
        }



        public ITabCollection CreateTabCollection()
        {
            ITabCollection tabCollection = CreateTabCollection(CreateDefaultTabsPanel());
            ((Panel)tabCollection.Control).Dock = DockStyle.Fill;
            return tabCollection;
        }

        public ITabCollection CreateTabCollection(object panel)
        {
            Panel winPanel = TypeCheck<Panel>(panel);

            IPanel pnl = CreatePanel(winPanel);
            var tabCollection = new TabCollectionLogic(pnl, this);

            winPanel.MouseDoubleClick += (sender, e) => tabCollection.OnAddClicked(this, new TabEventArgs(null));
            winPanel.SizeChanged += (sender, e) => tabCollection.OnSizeChanged(this, new SizeChangedHandlerArgs(winPanel.Size, Size.Empty));
            winPanel.MouseMove += tabCollection.OnMouseMove;
            return tabCollection;
        }



        public ITabPanel CreateTabPanel()
        {
            ITabPanel tabPanel = CreateTabPanel(CreateDefaultTabPanel());
            Panel panel = (Panel) tabPanel.Control;
            panel.MouseDown += tabPanel.OnMouseClick;
            panel.MouseUp += (sender, e) => tabPanel.OnMouseUp(sender, new TabDropEventArgs(null, e.Location, Control.MousePosition));
            return tabPanel;
        }

        public ITabPanel CreateTabPanel(object panel)
        {
            Panel winPanel = TypeCheck<Panel>(panel);
            return new TabPanel(winPanel, this);
        }


        public IBufferedCollection CreateBufferedCollection()
        {
            IBufferedCollection viewPanel = CreateBufferedCollection(CreateDefaultViewPanel());
            return viewPanel;
        }
        public IBufferedCollection CreateBufferedCollection(object panel)
        {
            Panel winPanel = TypeCheck<Panel>(panel);
            IPanel pnl = CreatePanel(winPanel);
            return new ViewCollectionLogic(pnl);
        }



        public ISplitContainer CreateSplitContainer()
        {
            return CreateSplitContainer(CreateDefaultSplitPanel());
        }
        public ISplitContainer CreateSplitContainer(object splitContainer)
        {
            TableLayoutPanel table = TypeCheck<TableLayoutPanel>(splitContainer);
            var container = new TableSplitContainer(table, this);
            Control control = (Control) container.Control;
            control.KeyUp += container.OnKeyUp;
            return container;
        }



        public IStripMenu CreateStripMenu()
        {
            return CreateStripMenu(CreateDefaultStripMenu());
        }

        public IStripMenu CreateStripMenu(object menu)
        {
            MenuStrip menuStrip = TypeCheck<MenuStrip>(menu);
            return new StripMenu(menuStrip, this);
        }



        public IStripMenuItem CreateStripMenuTool()
        {
            return CreateStripMenuTool(CreateDefaultStripMenuTool());
        }
        public IStripMenuItem CreateStripMenuTool(object tool)
        {
            ToolStripMenuItem toolStripItem = TypeCheck<ToolStripMenuItem>(tool);
            return new StripMenuItem(toolStripItem, this);
        }



        public IEnumerable<IControl> CreateControls(IEnumerable controls)
        {
            foreach (object control in controls)
            {
                yield return CreateControl(control);
            }
        }
        public IDictionary<string, IStripMenuItem> CreateStripMenuItems(IEnumerable controls)
        {
            var result = new Dictionary<string, IStripMenuItem>();
            foreach (object control in controls)
            {
                IStripMenuItem item = CreateStripMenuTool(control);
                result.Add(item.Text, item);
            }
            return result;
        }

        public ITabWindow CreateWindow(ITabView parent, ITabPanel tab)
        {
            Form window = CreateDefaultTabWindow();
            ITabView tabView = new TabViewLogic(tab, this);

            Control control = (Control)(tabView.Control);
            ((Control)(tabView.Control)).BubblingFromParent();
            ControlExtensions.BindingConcreteEvents(window, control);

            tabView.Orientation = parent.Orientation;
            return new TabWindow(window, parent, tabView);
        }
        public ITabWindow CreateWindow(ITabView tabView)
        {
            Form window = CreateDefaultTabWindow();
            Control control = (Control) (tabView.Control);
            control.BubblingFromParent();
            ControlExtensions.BindingConcreteEvents(window, control);

            tabView.Orientation = Orientation.Vertical;
            return new TabWindow(window, null, tabView);
        }
    }
}
