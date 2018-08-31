using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    internal class TabContent : Control, ITabContent
    {
        public TabContent()
        {
        }

        //public TabContent(Control control)
        //{
        //    Control = control;
        //}

        public static TabContent CreateDefaultContentol()
        {
            throw new NotImplementedException();
        }

        //object ITabContent.Control { get => Control; set => Control = (Control) value; }
        //public Control Control { get; set; }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public void OnTabDeleted(object sender, TabDeletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            throw new NotImplementedException();
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
        // ~TabContent() {
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
