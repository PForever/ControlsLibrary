using System;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Logic
{
    internal abstract class ViewCollectionBase : IBufferedCollection
    {
        protected abstract BufferedPage Buffer { get; set; }
        object IControl.Control { get => Panel.Control; }
        protected abstract IPanel Panel { get; }
        protected abstract void InitializeComponent();
        public abstract TimeSpan TimeOut { get; set; }
        public abstract int Capacity { get; set; }
        public abstract ITabContent Current { get; set; }
        public abstract void Remove(ITabContent tabPanelTabContent);

        public virtual string Name
        {
            get => Panel.Name;
            set => Panel.Name = value;
        }

        public virtual Point Location
        {
            get => Panel.Location;
            set => Panel.Location = value;
        }

        public virtual bool Visible
        {
            get => Panel.Visible;
            set => Panel.Visible = value;
        }

        public virtual int Width
        {
            get => Panel.Width;
            set => Panel.Width = value;
        }

        public virtual int Height
        {
            get => Panel.Height;
            set => Panel.Height = value;
        }

        public virtual IControlList Controls
        {
            get => Panel.Controls;
            set => Panel.Controls = value;
        }

        public virtual Orientation Orientation
        {
            get => Panel.Orientation;
            set => Panel.Orientation = value;
        }

        public virtual void Dispose(bool dispose)
        {
            if (!dispose) return;
            Panel.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}