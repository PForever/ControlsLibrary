using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.Factories.Concrete.WinForms.Containers;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    public class TabsPanel : IPanel
    {
        private WinFactory _factory;
        public void Dispose()
        {
            _panel.Dispose();
        }

        object IControl.Control
        {
            get => _panel;
            set
            {
                _panel = (Panel) value;
                _controlList = new ControlList(_panel.Controls);
            }
        }

        private Panel _panel;

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

        public void InitializeComponent()
        {
        }

        private ControlList _controlList;
        public IControlList Controls
        {
            get => _controlList;
            set => _controlList = (ControlList) value;
        }

        public Orientation Orientation { get; set; }

        public TabsPanel(Panel panel, WinFactory factory)
        {
            _panel = panel;
            _factory = factory;
            _controlList = new ControlList(_panel.Controls);
        }
    }
}