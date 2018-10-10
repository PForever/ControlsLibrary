using System.Drawing;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    abstract class TabViewLogicBase : ITabView, ICreator
    {
        protected abstract ISplitContainer Container { get; }
        protected abstract ITabCollection TabCollection { get; }
        protected abstract IBufferedCollection BufferedCollection { get; }
        protected abstract void InitializeComponent();
        protected abstract void OnTabDisposing(object sender, TabEventArgs arg);
        protected abstract void OnSelectedTabRemoved(object sender, TabEventArgs arg);
        protected abstract void OnNewTabAdded(object sender, TabEventArgs arg);
        protected abstract void OnTabSelected(object sender, TabEventArgs args);

        public abstract object Control { get; }
        public abstract string Name { get; set; }
        public abstract Point Location { get; set; }
        public abstract bool Visible { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public abstract IControlList Controls { get; set; }
        public abstract Orientation Orientation { get; set; }
        public abstract Position Position { get; set; }
        public abstract void Show(ITabContent tabContent = null);
        public abstract IFactory Factory { get; }

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