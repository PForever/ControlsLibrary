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

        #region Custom
        public Func<IStripMenu, IStripMenu> CustomStripMenu { get; set; }
        public Func<IStripMenuItem, IStripMenuItem> CustomStripMenuTool { get; set; }
        public Func<ISplitContainer, ISplitContainer> CustomSplitPanel { get; set; }
        public Func<ITabContent, ITabContent> CustomTabContent { get; set; }
        public Func<IBufferedCollection, IBufferedCollection> CustomViewPanel { get; set; }
        public Func<ITabPanel, ITabPanel> CustomTabPanel { get; set; }
        public Func<ITabCollection, ITabCollection> CustomTabsPanel { get; set; }
        public Func<ITabWindow, ITabWindow> CustomTabWindow { get; set; }
        #endregion

        public WinFactory()
        {
            CustomStripMenu = v => v;
            CustomStripMenuTool = v => v;
            CustomSplitPanel = v => v;
            CustomTabContent = v => v;
            CustomViewPanel = v => v;
            CustomTabPanel = v => v;
            CustomTabsPanel = v => v;
            CustomTabWindow = v => v;
        }

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
            return CustomTabContent(CreateTabContent(CreateDefaultTabContent()));
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
            return CustomTabsPanel(tabCollection);
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
            return CustomTabPanel(tabPanel);
        }

        public ITabPanel CreateTabPanel(object panel)
        {
            Panel winPanel = TypeCheck<Panel>(panel);
            return new TabPanel(winPanel, this);
        }


        public IBufferedCollection CreateBufferedCollection()
        {
            IBufferedCollection viewPanel = CreateBufferedCollection(CreateDefaultViewPanel());
            return CustomViewPanel(viewPanel);
        }
        public IBufferedCollection CreateBufferedCollection(object panel)
        {
            Panel winPanel = TypeCheck<Panel>(panel);
            IPanel pnl = CreatePanel(winPanel);
            return new ViewCollectionLogic(pnl);
        }



        public ISplitContainer CreateSplitContainer()
        {
            return CustomSplitPanel(CreateSplitContainer(CreateDefaultSplitPanel()));
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
            return CustomStripMenu(CreateStripMenu(CreateDefaultStripMenu()));
        }

        public IStripMenu CreateStripMenu(object menu)
        {
            MenuStrip menuStrip = TypeCheck<MenuStrip>(menu);
            return new StripMenu(menuStrip, this);
        }



        public IStripMenuItem CreateStripMenuItem(string name, string text = null, Keys shortcutKeys = Keys.None, EventHandler handler = null)
        {
            ToolStripMenuItem item = CreateDefaultStripMenuTool();
            item.Name = name;
            item.Text = string.IsNullOrEmpty(text) ? name : text;
            item.ShortcutKeys = shortcutKeys;
            item.Click += handler;
            return CustomStripMenuTool(CreateStripMenuItem(item));
        }
        public IStripMenuItem CreateStripMenuItem(object tool)
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
                IStripMenuItem item = CreateStripMenuItem(control);
                result.Add(item.Name, item);
            }
            return result;
        }


        public void SwitchWindow(IBufferedCollection newParent, ITabPanel tab)
        {
            ((Control)tab.Control).ForgetAll();
            ((Control)tab.TabContent.Control).ForgetAll();
            Panel panel = (Panel)tab.Control;
            panel.MouseDown += tab.OnMouseClick;
            panel.MouseUp += (sender, e) => tab.OnMouseUp(sender, new TabDropEventArgs(null, e.Location, Control.MousePosition));

            ((Panel)newParent.Control).Controls.Add((Control)tab.TabContent.Control);
            //((Control) newParent.Container.TabCollection.Control).Controls.Add();
            //((Control)tab.TabContent.Control).BindingConcreteEvents((Control) newParent.Container.TabCollection.Control);
        }

        public ITabWindow CreateWindow(ITabWindow parent, ITabPanel tab)
        {
            ((Control)tab.Control).ForgetAll();
            ((Control)tab.TabContent.Control).ForgetAll();
            Panel panel = (Panel)tab.Control;
            panel.MouseDown += tab.OnMouseClick;
            panel.MouseUp += (sender, e) => tab.OnMouseUp(sender, new TabDropEventArgs(null, e.Location, Control.MousePosition));

            Form window = CreateDefaultTabWindow();
            ITabView tabView = new TabViewLogic(tab, this);

            //window.BubblingFromParent();

            tabView.Orientation = parent.Container.Orientation;
            var result = CustomTabWindow(new TabWindow(window, parent, tabView, this));
            //((Control)tab.Control).Parent.Parent.Parent.BindingConcreteEvents(window);
            window.BubblingFromParent();
            return result;
        }
        public ITabWindow CreateWindow(ITabView tabView)
        {
            Form window = CreateDefaultTabWindow();
            window.BubblingFromParent();

            tabView.Orientation = Orientation.Vertical;

            return CustomTabWindow(new TabWindow(window, null, tabView, this));
        }
    }
}
