using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    public class TabPanel : ITabPanel
    { 
        private WinFactory _factory;
        private Panel _panel;
        protected TabPanel()
        {
        }

        public TabPanel(Panel panel, WinFactory factory)
        {
            _factory = factory;
            _panel = panel;
        }

        public static TabPanel CreateDefaultPanel(Control control, WinFactory factory)
        {
            return new TabPanel(new Panel {Name = control.Name}, factory);
        }
        public static TabPanel CreateDefaultPanel()
        {
            throw new NotImplementedException();
        }

        object IControl.Control { get => Panel; set => Panel = (Panel)value; }
        public Panel Panel { get; set; }
        public bool IsSelected { get; set; }
        public Containers.Orientation Orientation { get; set; }

        public IList<IControl> Controls
        {
            //TODO починить
            get => _factory.CreateControls(Controls).ToList();
            set => _panel.Controls.AddRange(value.Select(c => (Control)c.Control).ToArray());
        }

        public string Name
        {
            get => _panel.Name;
            set => _panel.Name = value;
        }

        public Point Location
        {
            get => _panel.Location;
            set => _panel.Location = value;
        }

        public bool Visible
        {
            get => _panel.Visible;
            set => _panel.Visible = value;
        }

        public int Width
        {
            get => _panel.Width;
            set => _panel.Width = value;
        }

        public int Height
        {
            get => _panel.Height;
            set => _panel.Height = value;
        }

        Containers.Orientation IPanel.Orientation { get; set; }

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
        private Panel panel;

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
