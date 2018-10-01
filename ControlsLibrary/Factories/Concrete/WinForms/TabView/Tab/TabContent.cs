using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    internal class TabContent : ITabContent
    {

        protected TabContent()
        {
        }

        public TabContent(Control control)
        {
            _control = control;
        }

        object IControl.Control { get => _control; /*set => _control = (Control)value;*/ }
        private Control _control;

        public string Name
        {
            get => _control.Name;
            set => _control.Name = value;
        }

        public Point Location
        {
            get => _control.Location;
            set => _control.Location = value;
        }

        public bool Visible
        {
            get => _control.Visible;
            set => _control.Visible = value;
        }

        public int Width
        {
            get => _control.Width;
            set => _control.Width = value;
        }

        public int Height
        {
            get => _control.Height;
            set => _control.Height = value;
        }

        public void InitializeComponent()
        {
        }

        public void OnTabDeleted(object sender, TabEventArgs args)
        {
            _control.Dispose();
        }

        public void OnTabSelected(object sender, TabEventArgs args)
        {
            _control.Select();
        }
        
        public bool Fetch
        {
            get => _control.Dock == DockStyle.Fill;
            set => _control.Dock = value ? DockStyle.Fill : DockStyle.Left;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _control.Dispose();
                }
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
