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

        public TabPanel(Panel panel, WinFactory factory)
        {
            _factory = factory;
            _panel = panel;
            TabContent = _factory.CreateTabContent();
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

        public event TabDeleteEventHandler TabDeleted;
        public event TabMoveHandler TabMoved;
        public event TabSelectedEventHandler TabSelected;

        public void Delete()
        {
            if (TabDeleted == null) throw new Exception("Content not found");
            //что-то пошло не так
            TabDeleted.Invoke(this, new TabDeletedEventArgs(this));
        }

        public void InitializeComponent()
        {
        }

        public void Select()
        {
            if (TabSelected == null) throw new Exception("Content not found");
            TabSelected.Invoke(this, new TabSelectedEventArgs(this, TabContent));
        }

        public void Dispose()
        {
            _panel?.Dispose();
        }
    }
}
