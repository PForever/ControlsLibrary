using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView
{
    abstract class ATabView : ITabView, ICreator
    {
        public ATabView()
        {
            Factory = new WinFactory();
        }

        public abstract Position Position { get; set; }
        public abstract ITabCollection TabCollection { get; set; }
        public abstract IBufferedCollection BufferedCollection { get; set; }
        public abstract IList<IControl> Childs { get; set; }
        Containers.Orientation IPanel.Orientation { get; set; }

        public IFactory Factory { get; set; }
        public abstract string Name { get; set; }
        public abstract Point Location { get; set; }
        public abstract bool Visible { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public virtual void InitializeComponent()
        {
            TabCollection = Factory.CreateTabCollection();
            BufferedCollection = Factory.CreateBufferedCollection();
        }

        public virtual void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            Show(args.TabContent);
        }

        public abstract void Show(ITabContent tabContent = null);

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
