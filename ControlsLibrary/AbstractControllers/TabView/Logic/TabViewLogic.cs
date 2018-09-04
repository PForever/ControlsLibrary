using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    class TabViewLogic : ITabView, ICreator
    {
        public object Control { get => Container; set => Container = (ISplitContainer) value; }

        public ISplitContainer Container;

        public TabViewLogic(IFactory factory)
        {
            Factory = factory;
            Container = Factory.CreateSplitContainer();
            TabCollection = Factory.CreateTabCollection();
            BufferedCollection = Factory.CreateBufferedCollection();
            Container.Panel1 = TabCollection;
            Container.Panel2 = BufferedCollection;
        }
        //public TabViewLogic(ISplitContainer splitContainer, IFactory factory)
        //{
        //    Factory = factory;
        //    Container = splitContainer;
        //}

        public Position Position { get; set; }
        public ITabCollection TabCollection { get; set; }
        public IBufferedCollection BufferedCollection { get; set; }
        public IList<IControl> Controls { get; set; }
        Orientation IPanel.Orientation { get; set; }

        public IFactory Factory { get; set; }

        public string Name
        {
            get => Container.Name;
            set => Container.Name = value;
        }

        public Point Location
        {
            get => Container.Location;
            set => Container.Location = value;
        }

        public bool Visible
        {
            get => Container.Visible;
            set => Container.Visible = value;
        }

        //TODO изменение шириы и высоты должно вызывать перерисовку табов внутри коллекции. Лучше всего сделать ивент
        public int Width
        {
            get => Container.Width;
            set => Container.Width = value;
        }

        public int Height
        {
            get => Container.Height;
            set => Container.Height = value;
        }

        public void InitializeComponent()
        {
            Container = Factory.CreateSplitContainer();
            TabCollection = Factory.CreateTabCollection();
            BufferedCollection = Factory.CreateBufferedCollection();
        }

        public void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            Show(args.TabContent);
        }

        public void Show(ITabContent tabContent = null)
        {
            BufferedCollection.Current = tabContent;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TabView() {
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
    }
}
