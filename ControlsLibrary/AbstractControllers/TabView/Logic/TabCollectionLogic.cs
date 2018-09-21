using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories.Concrete;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    class TabCollectionLogic : ITabCollection
    {
        public object Control { get => _tabsPanel.Control; }

        const int TabsThreshold = 5;

        private IPanel _tabsPanel;

        public TabCollectionLogic(IPanel tabsPanel)
        {
            MaxTabWidth = 50;
            _tabsPanel = tabsPanel;
        }

        public int MaxTabWidth { get; }
        public int Indent { get; set; }
        public int CurrentTabWidth { get; set; }
        public Orientation Orientation { get => _tabsPanel.Orientation; set => _tabsPanel.Orientation = value; }

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
            //TODO возможно требует оптимизации
            int index = Controls.IndexOf(args.TabPanel);
            Controls.RemoveAt(index);
            Surfacing(index, Controls.Count - 1, -CalcWidth() - Indent);
        }
        void Surfacing(int from, int to, int width)
        {
            if(from < to) return;
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
                default:
                    break;
            }
        }
        int CalcWidth()
        {
            if(Controls.Count < TabsThreshold) return CurrentTabWidth = MaxTabWidth;
            CurrentTabWidth = (int) (Width / (double)Controls.Count);
            Render();
            return CurrentTabWidth;
        }
        protected void Render()
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
                        panel.Location = new Point(panel.Location.Y, curPosition);
                        curPosition += (panel.Height = CurrentTabWidth) + Indent;
                    }
                    break;
                default:
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
                default:
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
            int theMostRightPosition = Width - MaxTabWidth;
            //проверить как работает маус мув для формы
            if (position < 0) position = 0;
            if (position > theMostRightPosition) position = theMostRightPosition;
            int index = CalcIndexFromPosition(position);
            int oldIndex = Controls.IndexOf(args.TabPanel);
            SwitchCollectionPositions(oldIndex, index);
            if (oldIndex < index)
                 Surfacing(oldIndex, index, -CalcWidth() - Indent);
            else Surfacing(index, oldIndex, CalcWidth() + Indent);
        }
        void SwitchCollectionPositions(int oldindex, int index)
        {
            ITabPanel temp = (ITabPanel) Controls[oldindex];
            Controls[oldindex] = Controls[index];
            Controls[index] = temp;
        }
        int CalcIndexFromPosition(double position)
        {
            return (int)Math.Round(position / Width);
        }
        public void OnTabSelected(object sender, TabEventArgs args)
        {
            TabSelected.Invoke(this, args);
        }

        private event TabSelectedEventHandler ButtonAddClickedHandler;
        event TabSelectedEventHandler ITabCollection.ButtonAddClickedHandler
        {
            add { this.ButtonAddClickedHandler += value; }
            remove { this.ButtonAddClickedHandler -= value; }
        }

        public void OnParentLocationChanged(object sender, LocationChangedHandlerArgs args)
        {
        }

        public void OnSizeChanged(object sender, SizeChangedHandlerArgs args)
        {
            CalcWidth();
        }

        private event TabSelectedEventHandler TabSelected;
        event TabSelectedEventHandler ITabCollection.TabSelected
        {
            add { this.TabSelected += value; }
            remove { this.TabSelected -= value; }
        }

        public void InitializeComponent()
        {
            Render();
            _tabsPanel.InitializeComponent();
        }

        public int IndexOf(ITabPanel item) => Controls.IndexOf(item);
        public void Insert(int index, ITabPanel item)
        {
            TabBinding(item);
            Controls.Insert(index, item);
            SetPosition(index, item);
            Surfacing(Controls.Count - 1, index + 1, CalcWidth() + Indent);
            item.Select();
        }

        private void SetPosition(int index, ITabPanel item)
        {
            ChangeLocationWidth(item, index * CurrentTabWidth);
        }

        private void TabBinding(ITabPanel item)
        {
            TabSelected += item.Unselect;
            item.TabSelected += OnTabSelected;
            item.TabDeleted += OnTabDeleted;
            item.TabMoved += OnTabMoved;
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
            if (Controls.Contains(item)) return false;
            int index = Controls.IndexOf(item);
            RemoveAt(index);
            return true;
        }
        public void RemoveAt(int index)
        {
            Controls[index].Dispose();
            Controls.RemoveAt(index);
            Surfacing(index, Controls.Count - 1, -CalcWidth() - Indent);
            ((ITabPanel)Controls[index]).Select();
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

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TabCollection() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public void OnAddClicked(object sender, TabEventArgs tabCollectionEventArgs)
        {
            ButtonAddClickedHandler.Invoke(this, tabCollectionEventArgs);
        }
    }
}
