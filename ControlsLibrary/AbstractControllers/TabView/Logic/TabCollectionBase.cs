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
    internal abstract partial class TabCollectionBase : ITabCollection
    {
        public virtual int MaxTabWidth { get; }

        public abstract ITabPanel SelectedTab { get; set; }
        public abstract int Indent { get; set; }
        public abstract int CurrentTabWidth { get; set; }
        public abstract Orientation Orientation { get; set; }
        protected abstract TabCollectionState StateManager { get; set; }
        protected abstract IPanel TabsPanel { get; }
        public abstract void OnTabDeleted(object sender, TabEventArgs args);
        protected abstract void Surfacing(int from, int to, int lenValue);
        protected abstract void CalcWidth();
        protected abstract void TryRender();
        protected abstract void OnCurrentTabWidthChanged(object sender, PropertyChangedEventArgs<int> args);
        protected abstract void Render();
        public abstract void OnTabMoved(object sender, TabMovedEventArgs args);
        protected abstract void SwitchCollectionPositions(int oldIndex, int index);
        protected abstract int CalcIndexFromPosition(double position);
        public abstract void OnTabSelected(object sender, TabEventArgs args);

        public abstract void OnMouseMove(object sender, MouseEventArgs args);
        public abstract void OnSizeChanged(object sender, SizeChangedHandlerArgs args);
        protected abstract void CalcNewPosition(int argsX, int argsY, ITabPanel tab);
        public abstract void OnTabDrop(object sender, TabEventArgs args);
        public abstract void OnAddClicked(object sender, TabEventArgs tabCollectionEventArgs);
        public abstract void OnTabDisposing(object sender, TabEventArgs arg);
        protected abstract bool RectangleContains(Point tabPanelLocation, int delta);
        protected abstract void InitializeComponent();
        public abstract void Insert(int index, ITabPanel item);
        protected abstract void SetPosition(int index, ITabPanel item);
        protected abstract void TabBinding(ITabPanel item);
        protected abstract void TabUnbinding(ITabPanel item);
        public abstract void Add(ITabPanel item);

        public abstract bool Remove(ITabPanel item);
        public abstract void RemoveAt(int index);
        public abstract void Clear();


        public virtual int IndexOf(ITabPanel item) => Controls.IndexOf(item);
        public virtual bool Contains(ITabPanel item) => Controls.Contains(item);
        public virtual void CopyTo(ITabPanel[] array, int arrayIndex) => Controls.CopyTo(array, arrayIndex);
        public virtual IControlList Controls
        {
            get => TabsPanel.Controls;
            set => TabsPanel.Controls = value;
        }

        IControlList IContainer.Controls
        {
            get => TabsPanel.Controls;
            set => TabsPanel.Controls = value;
        }

        public virtual string Name
        {
            get => TabsPanel.Name;
            set => TabsPanel.Name = value;
        }

        public virtual Point Location
        {
            get => TabsPanel.Location;
            set => TabsPanel.Location = value;
        }

        public virtual bool Visible
        {
            get => TabsPanel.Visible;
            set => TabsPanel.Visible = value;
        }

        public virtual int Width
        {
            get => TabsPanel.Width;
            set => TabsPanel.Width = value;
        }

        public virtual int Height
        {
            get => TabsPanel.Height;
            set => TabsPanel.Height = value;
        }
        public virtual int Count => Controls.Count;
        public virtual bool IsReadOnly => Controls.IsReadOnly;

        ITabPanel IList<ITabPanel>.this[int index]
        {
            get => (ITabPanel) Controls[index];
            set => Controls[index] = value;
        }


        #region Events

        private event PropertyChangedEventHandler<int> _CurrentTabWidthChanged;
        protected event PropertyChangedEventHandler<int> CurrentTabWidthChanged
        {
            add { this._CurrentTabWidthChanged += value; }
            remove { this._CurrentTabWidthChanged -= value; }
        }
        protected virtual void CurrentTabWidthChangedInvoke(PropertyChangedEventArgs<int> args)
        {
            _CurrentTabWidthChanged.Invoke(this, args);
        }

        private event TabEventHandler _TabDisposing;
        public virtual event TabEventHandler TabDisposing
        {
            add { this._TabDisposing += value; }
            remove { this._TabDisposing -= value; }
        }
        protected virtual void TabDisposingInvoke(TabEventArgs arg)
        {
            _TabDisposing.Invoke(this, arg);
        }

        private event TabSelectedEventHandler _ButtonAddClickedHandler;
        public virtual event TabSelectedEventHandler ButtonAddClickedHandler
        {
            add { this._ButtonAddClickedHandler += value; }
            remove { this._ButtonAddClickedHandler -= value; }
        }
        protected virtual void ButtonAddClickedHandlerInvoke(TabEventArgs args)
        {
            _ButtonAddClickedHandler.Invoke(this, args);
        }

        private event TabSelectedEventHandler _TabSelected;
        public virtual event TabSelectedEventHandler TabSelected
        {
            add { this._TabSelected += value; }
            remove { this._TabSelected -= value; }
        }
        protected virtual void TabSelectedInvoke(TabEventArgs args)
        {
            _TabSelected.Invoke(this, args);
        }

        #endregion

        IEnumerator<ITabPanel> IEnumerable<ITabPanel>.GetEnumerator()
        {
            return Controls.Cast<ITabPanel>().GetEnumerator();
        }

        public virtual IEnumerator GetEnumerator()
        {
            return Controls.GetEnumerator();
        }

        public object Control { get => TabsPanel.Control; }

        const int TabsThreshold = 5;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TabsPanel?.Dispose();
                SelectedTab?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}