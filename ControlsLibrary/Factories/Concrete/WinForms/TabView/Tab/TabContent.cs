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
            Control = control;
        }

        public static TabContent CreateDefaultContentol()
        {
            throw new NotImplementedException();
        }

        object IControl.Control { get => Control; set => Control = (Control)value; }
        public Control Control { get; set; }

        public string Name
        {
            get => Control.Name;
            set => Control.Name = value;
        }

        public Point Location
        {
            get => Control.Location;
            set => Control.Location = value;
        }

        public bool Visible
        {
            get => Control.Visible;
            set => Control.Visible = value;
        }

        public int Width
        {
            get => Control.Width;
            set => Control.Width = value;
        }

        public int Height
        {
            get => Control.Height;
            set => Control.Height = value;
        }

        public void InitializeComponent()
        {
        }

        public void OnTabDeleted(object sender, TabDeletedEventArgs args)
        {
            Control.Dispose();
        }

        public void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            Control.Select();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Control.Dispose();
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
