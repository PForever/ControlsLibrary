using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            InitializeComponent();
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
                _panel.BackColor = value ? Color.DarkRed : Color.LightGray;
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
        private event TabEventHandler TabDrop;
        private event TabEventHandler Disposing;

        event TabEventHandler ITabPanel.Disposing
        {
            add { this.Disposing += value; }
            remove { this.Disposing -= value; }
        }

        event TabEventHandler ITabPanel.TabDrop
        {
            add { this.TabDrop += value; }
            remove { this.TabDrop -= value; }
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (_panel.ClientRectangle.Contains(e.Location))
            {
                IsClicked = true;
                ClickPosition = e.Location;
                if(!IsSelected) Select();
                //MovingStart();
            }
        }

        public void OnMouseCaptureChanged(object sender, EventArgs e)
        {
            if (IsClicked) IsClicked = false;
            if (IsSelected) TabDrop.Invoke(this, new TabEventArgs(this));
        }

        private static SynchronizationContext _context = SynchronizationContext.Current;
        public void ChangeLocation(Point point)
        {
            if (_rendering != null && !_rendering.IsCompleted) _rendering = _rendering.ContinueWith(t => MoveAnimation(point));
            else _rendering = MoveAnimation(point);
        }

        private Task _rendering;
        public Func<Point, Task> MoveAnimation { get; set; }

        public bool IsClicked { get; set; }
        public Point ClickPosition { get; set; }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        public void BringToFront()
        {
            _panel.BringToFront();
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
            MoveAnimation = MoveAnimationHandlerAsync;
        }

        private const int _speed = 3;
        private const int _step = 10;
        private const int _delte = 0;
        private async Task MoveAnimationHandlerAsync(Point point)
        {
            int dY = point.Y - Location.Y;
            int dX = point.X - Location.X;
            int stepY = dY == 0 ? 0 : _step * Math.Sign(dY);
            int stepX = dX == 0 ? 0 : _step * Math.Sign(dX);

            int stepsY = dY == 0 ? 0 : dY / stepY;
            int stepsX = dX == 0 ? 0 : dX / stepX;

            //TODO шаги должны быть разные
            int steps = Math.Max(stepsX, stepsY);

            for (int i = 0; i < steps; i++)
            {
                AddLocation(stepX, stepY);
                await Task.Delay(_speed);
            }
            if(_context != SynchronizationContext.Current) _context.Send(state => Location = (Point)state, point);
            else Location = point;
        }
        //TODO перенести лишнюю логику в хелп
        private void AddLocation(float stepX, float stepY)
        {
            Point point = new Point((int)(Location.X + stepX), (int)(Location.Y + stepY));
            if (_context != SynchronizationContext.Current) _context.Send(state => Location = (Point)state, point);
            else Location = point;
        }

        private double ApproxDistance(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public void Select()
        {
            IsSelected = true;
            if (TabSelected == null) throw new Exception("Content not found");
            TabSelected.Invoke(this, new TabEventArgs(this));
        }

        public void Dispose()
        {
            Disposing.Invoke(this, new TabEventArgs(this));
            _panel?.Dispose();
            TabContent?.Dispose();
        }
    }
}
