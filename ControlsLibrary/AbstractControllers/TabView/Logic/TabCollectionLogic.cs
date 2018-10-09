using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    class TabCollectionLogic : ITabCollection
    {
        public object Control { get => _tabsPanel.Control; }

        const int TabsThreshold = 5;

        private IPanel _tabsPanel;

        private event PropertyChangedEventHandler<int> CurrentTabWidthChanged;

        public TabCollectionLogic(IPanel tabsPanel)
        {
            MaxTabWidth = 50;
            _tabsPanel = tabsPanel;
            CurrentTabWidthChanged += OnCurrentTabWidthChanged;
        }

        public int MaxTabWidth { get; }
        public ITabPanel SelectedTab { get; set; }
        public int Indent { get; set; }

        public int CurrentTabWidth
        {
            get => _currentTabWidth;
            set
            {
                int oldValue = _currentTabWidth;
                _currentTabWidth = value;
                CurrentTabWidthChanged.Invoke(this, new PropertyChangedEventArgs<int>(oldValue, value));
            }
        }

        public Orientation Orientation
        {
            get => _tabsPanel.Orientation;
            set
            {
                _tabsPanel.Orientation = value;
                Render();
            }
        }

        public IControlList Controls
        {
            get =>  _tabsPanel.Controls;
            set => _tabsPanel.Controls = value;
        }
        IControlList IContainer.Controls { get => _tabsPanel.Controls; set => _tabsPanel.Controls = value; }
        public string Name { get => _tabsPanel.Name; set => _tabsPanel.Name = value; }
        public Point Location { get => _tabsPanel.Location; set => _tabsPanel.Location = value; }
        public bool Visible { get => _tabsPanel.Visible; set => _tabsPanel.Visible = value; }
        public int Width { get => _tabsPanel.Width; set => _tabsPanel.Width = value; }
        public int Height { get => _tabsPanel.Height; set => _tabsPanel. Height = value; }
        public int Count => Controls.Count;
        public bool IsReadOnly => Controls.IsReadOnly;

        ITabPanel IList<ITabPanel>.this[int index] { get => (ITabPanel)Controls[index]; set => Controls[index] = value; }

        public void OnTabDeleted(object sender, TabEventArgs args)
        {
            TabUnbinding(args.TabPanel);
            //TODO возможно требует оптимизации
            int index = Controls.IndexOf(args.TabPanel);
            Controls.RemoveAt(index);
            Surfacing(index, Controls.Count - 1, -CurrentTabWidth - Indent);
            TryRender();
        }
        private void Surfacing(int from, int to, int width)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    for (int i = from; i <= to; i++)
                    {
                        ChangeLocationWidth(Controls[i], width);
                    }
                    break;
                case Orientation.Vertical:
                    for (int i = from; i <= to; i++)
                    {
                        ChangeLocationHeight(Controls[i], width);
                    }
                    break;
            }
        }
        private void CalcWidth()
        {
            if(Controls.Count < TabsThreshold) CurrentTabWidth = MaxTabWidth;
            else CurrentTabWidth = (int) (Orientation == Orientation.Horizontal ? Width : Height / (double)Controls.Count);
        }
        protected void TryRender()
        {
            CalcWidth();
        }

        protected void OnCurrentTabWidthChanged(object sender, PropertyChangedEventArgs<int> args)
        {
            if (args.OldValue != args.NewValue)
            {
                Render();
            }
        }
        protected virtual void Render()
        {
            int curPosition = 0;
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    foreach (ITabPanel panel in Controls.Cast<ITabPanel>())
                    {
                        panel.Location = new Point(curPosition, panel.Location.Y);
                        curPosition += (panel.Width = CurrentTabWidth) + Indent;
                    }

                    break;
                case Orientation.Vertical:
                    foreach (ITabPanel panel in Controls.Cast<ITabPanel>())
                    {
                        panel.Location = new Point(panel.Location.X, curPosition);
                        curPosition += (panel.Height = CurrentTabWidth) + Indent;
                    }

                    break;
            }
        }

        protected void Render(int index)
        {
            ITabPanel panel = (ITabPanel) Controls[index];
            int curPosition = index * (CurrentTabWidth + Indent);
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    Controls[index].Location = new Point(curPosition, panel.Location.Y);
                    panel.Width = CurrentTabWidth;
                    break;
                case Orientation.Vertical:
                    panel.Location = new Point(panel.Location.Y, curPosition);
                    panel.Height = CurrentTabWidth;
                    break;
            }
        }

        protected void ChangeLocationWidth(IControl control, int width)
        {
            control.Location = new Point(control.Location.X + width, control.Location.Y);
        }
        protected void ChangeLocationHeight(IControl control, int height)
        {
            control.Location = new Point(control.Location.X, control.Location.Y + height);
        }

        public void OnTabMoved(object sender, TabMovedEventArgs args)
        {
            double position = Orientation == Orientation.Vertical ? args.RequesLocation.Y : args.RequesLocation.X;

            int index = CalcIndexFromPosition(position);
            int oldIndex = Controls.IndexOf(args.TabPanel);
            if(index == oldIndex) return;

            SwitchCollectionPositions(oldIndex, index);
            if (oldIndex < index)
                 Surfacing(oldIndex, index, -CurrentTabWidth - Indent);
            else Surfacing(index, oldIndex, CurrentTabWidth + Indent);
            TryRender();
        }
        void SwitchCollectionPositions(int oldIndex, int index)
        {
            Controls.Swap(oldIndex, index);
        }
        int CalcIndexFromPosition(double position)
        {
            if (position <= 0) return 0;
            if (position >= (Count - 1) * CurrentTabWidth) return Count - 1;
            return (int)Math.Round(position / (CurrentTabWidth + Indent));
        }
        public void OnTabSelected(object sender, TabEventArgs args)
        {
            SelectedTab = args.TabPanel;
            SelectedTab.BringToFront();
            //SelectedTab.MouseClick += OnMouseClickOnSelectedTab;
            TabSelected.Invoke(this, args);
        }

        //private void OnMouseClickOnSelectedTab(object sender, MouseEventArgs arg2)
        //{
            
        //}

        private event TabSelectedEventHandler ButtonAddClickedHandler;
        private event TabEventHandler TabDisposing;

        event TabEventHandler ITabCollection.TabDisposing
        {
            add { this.TabDisposing += value; }
            remove { this.TabDisposing -= value; }
        }

        event TabSelectedEventHandler ITabCollection.ButtonAddClickedHandler
        {
            add { this.ButtonAddClickedHandler += value; }
            remove { this.ButtonAddClickedHandler -= value; }
        }

        public void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (SelectedTab.IsClicked && args.Button == MouseButtons.Left)
            {
                CalcNewPosition(args.X, args.Y, SelectedTab);
            }
        }

        private void CalcNewPosition(int argsX, int argsY, ITabPanel tab)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    tab.Location = new Point(tab.Location.X + (argsX - tab.ClickPosition.X), tab.Location.Y); ;
                    break;
                case Orientation.Vertical:
                    tab.Location = new Point(tab.Location.X, tab.Location.Y + (argsY - tab.ClickPosition.Y));
                    break;
            }
            OnTabMoved(null, new TabMovedEventArgs{TabPanel = tab, RequesLocation = tab.Location});
        }

        public void OnParentLocationChanged(object sender, LocationChangedHandlerArgs args)
        {
        }

        public void OnSizeChanged(object sender, SizeChangedHandlerArgs args)
        {
            TryRender();
        }

        public void OnTabDrop(object sender, TabEventArgs args)
        {
            if (RectangleContains(args.TabPanel.Location, CurrentTabWidth/2))
            {
                if (Contains(args.TabPanel))
                {
                    Render();
                }
                else
                {
                    double position = Orientation == Orientation.Horizontal
                        ? args.TabPanel.Location.X
                        : args.TabPanel.Location.Y;
                    int index = CalcIndexFromPosition(position);
                    Insert(index, args.TabPanel);
                }
            }
        }

        public void OnTabDisposing(object sender, TabEventArgs arg)
        {
            ITabPanel tab = arg.TabPanel;
            TabUnbinding(tab);
            TabDisposing.Invoke(this, arg);
        }

        private bool RectangleContains(Point tabPanelLocation, int delta)
        {
            //TODO нормаьная дельта нужна
            return
                (-delta < tabPanelLocation.Y && tabPanelLocation.Y < Height + delta) &&
                (-delta < tabPanelLocation.X && tabPanelLocation.X < Width + delta);
            //return
            //    (Location.Y < tabPanelLocation.Y && tabPanelLocation.Y < Location.Y + Height) &&
            //    (Location.X < tabPanelLocation.X && tabPanelLocation.X < Location.X + Width);
        }

        private event TabSelectedEventHandler TabSelected;
        event TabSelectedEventHandler ITabCollection.TabSelected
        {
            add { this.TabSelected += value; }
            remove { this.TabSelected -= value; }
        }

        public void InitializeComponent()
        {
            TryRender();
            _tabsPanel.InitializeComponent();
        }

        public int IndexOf(ITabPanel item) => Controls.IndexOf(item);
        public void Insert(int index, ITabPanel item)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    item.Width = CurrentTabWidth;
                    break;
                case Orientation.Vertical:
                    item.Height = CurrentTabWidth;
                    break;
            }

            TabBinding(item);
            Controls.Insert(index, item);
            SetPosition(index, item);
            if(index < Count - 1) Surfacing(Controls.Count - 1, index + 1, CurrentTabWidth + Indent);
            TryRender();
            item.Select();
        }

        private void SetPosition(int index, ITabPanel item)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    ChangeLocationWidth(item, index * CurrentTabWidth);
                    break;
                case Orientation.Vertical:
                    ChangeLocationHeight(item, index * CurrentTabWidth);
                    break;
            }
        }

        private void TabBinding(ITabPanel item)
        {
            TabSelected += item.Unselect;
            item.TabSelected += OnTabSelected;
            item.TabDeleted += OnTabDeleted;
            item.TabMoved += OnTabMoved;
            item.TabDrop += OnTabDrop;
            item.Disposing += OnTabDisposing;
        }

        private void TabUnbinding(ITabPanel item)
        {
            TabSelected -= item.Unselect;
            item.TabSelected -= OnTabSelected;
            item.TabDeleted -= OnTabDeleted;
            item.TabMoved -= OnTabMoved;
            item.TabDrop -= OnTabDrop;
        }

        public void Add(ITabPanel item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Insert(Count, item);
        }
        public bool Contains(ITabPanel item) => Controls.Contains(item);
        public void CopyTo(ITabPanel[] array, int arrayIndex) => Controls.CopyTo(array, arrayIndex);

        public bool Remove(ITabPanel item)
        {
            if (!Controls.Contains(item)) return false;
            int index = Controls.IndexOf(item);
            RemoveAt(index);
            return true;
        }
        public void RemoveAt(int index)
        {
            Controls.RemoveAt(index, dispose: true);
            Surfacing(index, Controls.Count - 1, -CurrentTabWidth - Indent);
            TryRender();
            if (Count > index)
            {
                ((ITabPanel)Controls[index]).Select();
            }
            else if(Count > 0)
            {
                ((ITabPanel)Controls.Last()).Select();
            }
        }
        public void Clear()
        {
            CurrentTabWidth = MaxTabWidth;
            foreach (ITabPanel tabPanel in Controls)
                tabPanel.Dispose();
            Controls.Clear();
        }
        IEnumerator<ITabPanel> IEnumerable<ITabPanel>.GetEnumerator()
        {
            return Controls.Cast<ITabPanel>().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return Controls.GetEnumerator();
        }
        private bool _disposedValue = false; // To detect redundant calls
        private int _currentTabWidth;

        public void OnAddClicked(object sender, TabEventArgs tabCollectionEventArgs)
        {
            ButtonAddClickedHandler.Invoke(this, tabCollectionEventArgs);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tabsPanel?.Dispose();
                SelectedTab?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
