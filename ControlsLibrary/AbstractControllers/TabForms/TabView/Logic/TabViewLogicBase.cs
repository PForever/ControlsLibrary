﻿using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Logic
{
    public abstract class TabViewLogicBase : ITabView, ICreator
    {

        protected abstract void InitializeComponent();
        protected abstract void OnTabDisposing(object sender, TabEventArgs arg);
        protected abstract void OnSelectedTabRemoved(object sender, TabEventArgs arg);
        protected abstract void OnNewTabAdded(object sender, TabEventArgs arg);
        protected abstract void OnTabSelected(object sender, TabEventArgs args);
        protected abstract void OnTabDeleting(object sender, TabDeletingEventArgs arg);

        public abstract object Control { get; }

        public abstract IControlList Controls { get; set; }
        public abstract Orientation Orientation { get; set; }
        public abstract void Join(IEnumerable<ITabPanel> childsTab);
        public abstract void Show(ITabContent tabContent = null);
        public abstract void RemoveSelected();
        public abstract void AddNew();
        public abstract IFactory Factory { get; }
        public abstract ITabWindow Owner { get; set; }

        protected abstract ISplitContainer Container { get; }
        public abstract ITabCollection TabCollection { get; protected set; }
        protected abstract IBufferedCollection BufferedCollection { get; }

        public virtual Point Location { get; set; }
        public virtual Position Position { get; set; }

        public virtual string Name
        {
            get => Container.Name;
            set => Container.Name = value;
        }
        public virtual bool Visible
        {
            get => Container.Visible;
            set => Container.Visible = value;
        }

        public virtual int Width
        {
            get => Container.Width;
            set => Container.Width = value;
        }

        public virtual int Height
        {
            get => Container.Height;
            set => Container.Height = value;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool dispose)
        {
            if (!dispose) return;
            Container?.Dispose();
            TabCollection?.Dispose();
            BufferedCollection?.Dispose();
        }

    }
}