using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    public class TabPanel : Panel, ITabPanel
    {
        protected TabPanel()
        {
        }

        //public TabPanel(Panel panel)
        //{
        //    Panel = panel;
        //}

        public static TabPanel CreateDefaultPanel(Control control)
        {
            throw new NotImplementedException();
        }
        public static TabPanel CreateDefaultPanel()
        {
            throw new NotImplementedException();
        }

        //object ITabPanel.Panel { get => Panel; set => Panel = (Panel) value; }
        //public Panel Panel { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Containers.Orientation Orientation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<IControl> Childs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        Containers.Orientation IPanel.Orientation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event TabDeleteEventHandler TabDeleted;
        public event TabMoveHandler TabMoved;
        public event TabSelectedEventHandler TabSelected;

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public void Select()
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
        // ~TabPanel() {
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
