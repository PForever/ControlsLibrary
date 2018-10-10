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
    partial class TabCollectionLogic : TabCollectionBase
    {
        protected override TabCollectionState StateManager { get; set; }
        const int TabsThreshold = 5;

        protected override IPanel TabsPanel { get; }

        public TabCollectionLogic(IPanel tabsPanel)
        {
            MaxTabWidth = 50;
            TabsPanel = tabsPanel;
            CurrentTabWidthChanged += OnCurrentTabWidthChanged;
            InitializeComponent();
        }

        public override int MaxTabWidth { get; }
        public override ITabPanel SelectedTab { get; set; }
        public override int Indent { get; set; }

        public override int CurrentTabWidth
        {
            get => _currentTabWidth;
            set
            {
                int oldValue = _currentTabWidth;
                _currentTabWidth = value;
                CurrentTabWidthChangedInvoke(new PropertyChangedEventArgs<int>(oldValue, value));
            }
        }

        public override Orientation Orientation
        {
            get => TabsPanel.Orientation;
            set
            {
                TabsPanel.Orientation = value;
                StateManager.Orientation = value;
                Render();
            }
        }

        public override void OnTabDeleted(object sender, TabEventArgs args)
        {
            TabUnbinding(args.TabPanel);
            //TODO возможно требует оптимизации
            int index = Controls.IndexOf(args.TabPanel);
            Controls.RemoveAt(index);
            Surfacing(index, Controls.Count - 1, -CurrentTabWidth - Indent);
            TryRender();
        }

        protected override void Surfacing(int from, int to, int lenValue)
        {
            StateManager.OnSurfacing(Controls, from, to, lenValue);
        }

        protected override void CalcWidth()
        {
            if(Controls.Count < TabsThreshold) CurrentTabWidth = MaxTabWidth;
            else CurrentTabWidth = (int) (StateManager.ControllerLen(Width, Height) / (double)Controls.Count);
        }
        protected override void TryRender()
        {
            CalcWidth();
        }

        protected override void OnCurrentTabWidthChanged(object sender, PropertyChangedEventArgs<int> args)
        {
            if (args.OldValue != args.NewValue)
            {
                Render();
            }
        }
        protected override void Render()
        {
            StateManager.OnRender(Controls, CurrentTabWidth, Indent);
        }

/*
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
*/

        public override void OnTabMoved(object sender, TabMovedEventArgs args)
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

        protected override void SwitchCollectionPositions(int oldIndex, int index)
        {
            Controls.Swap(oldIndex, index);
        }

        protected override int CalcIndexFromPosition(double position)
        {
            if (position <= 0) return 0;
            if (position >= (Count - 1) * CurrentTabWidth) return Count - 1;
            return (int)Math.Round(position / (CurrentTabWidth + Indent));
        }
        public override void OnTabSelected(object sender, TabEventArgs args)
        {
            SelectedTab = args.TabPanel;
            SelectedTab.BringToFront();
            TabSelectedInvoke(args);
        }

        public override void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (SelectedTab.IsClicked && args.Button == MouseButtons.Left)
            {
                CalcNewPosition(args.X, args.Y, SelectedTab);
            }
        }

        protected override void CalcNewPosition(int argsX, int argsY, ITabPanel tab)
        {
            StateManager.OnCalcNewPosition(argsX, argsY, tab);
            OnTabMoved(null, new TabMovedEventArgs{TabPanel = tab, RequesLocation = tab.Location});
        }

        public void OnParentLocationChanged(object sender, LocationChangedHandlerArgs args)
        {
        }

        public override void OnSizeChanged(object sender, SizeChangedHandlerArgs args)
        {
            TryRender();
        }

        public override void OnTabDrop(object sender, TabEventArgs args)
        {
            if (RectangleContains(args.TabPanel.Location, CurrentTabWidth/2))
            {
                if (Contains(args.TabPanel))
                {
                    Render();
                }
                else
                {
                    double position = StateManager.GetPosition(args.TabPanel.Location);
                    int index = CalcIndexFromPosition(position);
                    Insert(index, args.TabPanel);
                }
            }
        }

        public override void OnTabDisposing(object sender, TabEventArgs arg)
        {
            ITabPanel tab = arg.TabPanel;
            TabUnbinding(tab);
            TabDisposingInvoke(arg);
        }

        protected override bool RectangleContains(Point tabPanelLocation, int delta)
        {
            //TODO нормаьная дельта нужна
            return
                (-delta < tabPanelLocation.Y && tabPanelLocation.Y < Height + delta) &&
                (-delta < tabPanelLocation.X && tabPanelLocation.X < Width + delta);
            //return
            //    (Location.Y < tabPanelLocation.Y && tabPanelLocation.Y < Location.Y + Height) &&
            //    (Location.X < tabPanelLocation.X && tabPanelLocation.X < Location.X + Width);
        }

        protected override void InitializeComponent()
        {
            StateManager = new TabCollectionState(Orientation);
            TryRender();
        }

        public override void Insert(int index, ITabPanel item)
        {
            StateManager.OnInsert(index, item, CurrentTabWidth);

            TabBinding(item);
            Controls.Insert(index, item);
            SetPosition(index, item);
            if(index < Count - 1) Surfacing(Controls.Count - 1, index + 1, CurrentTabWidth + Indent);
            TryRender();
            item.Select();
        }

        protected override void SetPosition(int index, ITabPanel item)
        {
            StateManager.OnSetPosition(index, item, CurrentTabWidth);
        }

        protected override void TabBinding(ITabPanel item)
        {
            TabSelected += item.Unselect;
            item.TabSelected += OnTabSelected;
            item.TabDeleted += OnTabDeleted;
            item.TabMoved += OnTabMoved;
            item.TabDrop += OnTabDrop;
            item.Disposing += OnTabDisposing;
        }

        protected override void TabUnbinding(ITabPanel item)
        {
            TabSelected -= item.Unselect;
            item.TabSelected -= OnTabSelected;
            item.TabDeleted -= OnTabDeleted;
            item.TabMoved -= OnTabMoved;
            item.TabDrop -= OnTabDrop;
        }

        public override void Add(ITabPanel item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Insert(Count, item);
        }

        public override bool Remove(ITabPanel item)
        {
            if (!Controls.Contains(item)) return false;
            int index = Controls.IndexOf(item);
            RemoveAt(index);
            return true;
        }
        public override void RemoveAt(int index)
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
        public override void Clear()
        {
            CurrentTabWidth = MaxTabWidth;
            foreach (ITabPanel tabPanel in Controls)
                tabPanel.Dispose();
            Controls.Clear();
        }

        public override IEnumerator GetEnumerator()
        {
            return Controls.GetEnumerator();
        }
        private int _currentTabWidth;

        public override void OnAddClicked(object sender, TabEventArgs tabCollectionEventArgs)
        {
            ButtonAddClickedHandlerInvoke(tabCollectionEventArgs);
        }
    }
}
