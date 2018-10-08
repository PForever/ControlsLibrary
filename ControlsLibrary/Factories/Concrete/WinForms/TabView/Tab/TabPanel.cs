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
        public void Unselect(object sender, TabEventArgs args)
        {
            if (args.TabPanel == this) return;
            IsSelected = false;
        }

        public bool IsSelected
        {
            get => _isSelected;
            private set
            {
                _panel.BackColor = value ? Color.BlueViolet : Color.Gray;
                _isSelected = value;
            }
        }

        public Orientation Orientation { get; set; }
        private ControlList _controlList;
        private bool _isSelected;

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
        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (_panel.ClientRectangle.Contains(e.Location))
            {
                IsClicked = true;
                ClickPosition = e.Location;
                if(!IsSelected) Select();
                MovingStart();
            }
        }

        public void OnMouseCaptureChanged(object sender, EventArgs e)
        {
            if (IsClicked) IsClicked = false;
            if (IsSelected) MovingStop();
        }

        public Action MovingStart { get; set; }
        public Action MovingStop { get; set; }
        public bool IsClicked { get; set; }
        public Point ClickPosition { get; set; }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

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
            IsSelected = true;
            if (TabSelected == null) throw new Exception("Content not found");
            TabSelected.Invoke(this, new TabEventArgs(this));
        }

        public void Dispose()
        {
            _panel?.Dispose();
        }
    }
}
