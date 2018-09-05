using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.Factories.Concrete.WinForms.Containers;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    public class SimplePanel : IPanel
    {
        private WinFactory _factory;
        private Panel _panel;
        protected SimplePanel()
        {
                
        }
        public SimplePanel(Panel panel, WinFactory factory)
        {
            _factory = factory;
            _panel = panel;
            Controls = new ControlList(_panel.Controls);
        }
        public void Dispose()
        {
            _panel.Dispose();
        }

        public object Control
        {
            get => _panel;
            set
            {
                _panel = (Panel) value;
                Controls = new ControlList(_panel.Controls);
            }
        }

        public string Name { get => _panel.Name; set => _panel.Name = value; }
        public Point Location { get => _panel.Location; set => _panel.Location = value; }
        public bool Visible { get => _panel.Visible; set => _panel.Visible = value; }
        public int Width { get => _panel.Width; set => _panel.Width = value; }
        public int Height { get => _panel.Height; set => _panel.Height = value; }
        public void InitializeComponent()
        {
        }
        
        public IControlList Controls { get; set; }
        public Orientation Orientation { get; set; }
    }
}