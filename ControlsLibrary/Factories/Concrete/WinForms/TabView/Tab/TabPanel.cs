using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.Factories.Concrete.WinForms.Containers;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    public class TabPanel : ITabPanel
    { 
        private WinFactory _factory;
        private Panel _panel;
        public ITabContent TabContent { get; }
        protected TabPanel()
        {
        }

        public TabPanel(Panel panel, WinFactory factory) : this(panel, factory, factory.CreateTabContent())
        {
        }

        public TabPanel(Panel panel, WinFactory factory, ITabContent tabContent)
        {
            _factory = factory;
            _panel = panel;
            TabContent = tabContent;
        }

        object IControl.Control { get => _panel; /*set => _panel = (Panel)value;*/ }
        public bool IsSelected { get; set; }
        public Orientation Orientation { get; set; }
        private ControlList _controlList;
        public IControlList Controls
        {
            get => _controlList;
            set => _controlList = (ControlList) value;
        }

        public string Name
        {
            get => _panel.Name;
            set => _panel.Name = value;
        }

        public Point Location
        {
            get => _panel.Location;
            set => _panel.Location = value;
        }

        public bool Visible
        {
            get => _panel.Visible;
            set => _panel.Visible = value;
        }

        public int Width
        {
            get => _panel.Width;
            set => _panel.Width = value;
        }

        public int Height
        {
            get => _panel.Height;
            set => _panel.Height = value;
        }

        private event TabEventHandler TabDeleted;
        event TabEventHandler ITabPanel.TabDeleted
        {
            add { this.TabDeleted += value; }
            remove { this.TabDeleted -= value; }
        }

        private event TabMoveHandler TabMoved;
        event TabMoveHandler ITabPanel.TabMoved
        {
            add { this.TabMoved += value; }
            remove { this.TabMoved -= value; }
        }

        private event TabSelectedEventHandler TabSelected;

        event TabSelectedEventHandler ITabPanel.TabSelected
        {
            add { this.TabSelected += value; }
            remove { this.TabSelected -= value; }
        }

        public void Delete()
        {
            if (TabDeleted == null) throw new Exception("Content not found");
            TabDeleted.Invoke(this, new TabEventArgs(this));
        }

        public void InitializeComponent()
        {
        }

        public void Select()
        {
            if (TabSelected == null) throw new Exception("Content not found");
            TabSelected.Invoke(this, new TabEventArgs(this));
        }

        public void Dispose()
        {
            _panel?.Dispose();
        }
    }
}
