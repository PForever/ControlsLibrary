using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    public class TabsPanel : IPanel
    {
        private WinFactory _factory;
        public void Dispose()
        {
            Panel.Dispose();
        }

        object IControl.Control { get => Panel; set => Panel = (Panel) value; }
        public Panel Panel { get; set; }

        public string Name
        {
            get => Panel.Name;
            set => Panel.Name = value;
        }

        public Point Location
        {
            get => Panel.Location;
            set => Panel.Location = value;
        }

        public bool Visible
        {
            get => Panel.Visible;
            set => Panel.Visible = value;
        }

        public int Width
        {
            get => Panel.Width;
            set => Panel.Width = value;
        }

        public int Height
        {
            get => Panel.Height;
            set => Panel.Height = value;
        }

        public void InitializeComponent()
        {
            throw new System.NotImplementedException();
        }

        public IList<IControl> Controls
        {
            get => Panel.Controls.Cast<Control>().Select(c => _factory.CreateControl(c)).ToList();
            set
            {
                foreach (var control in value)
                {
                    Panel.Controls.Add((Control)control.Control);
                }
            }
        }

        public Orientation Orientation { get; set; }

        public TabsPanel(Panel panel, WinFactory factory)
        {
            Panel = panel;
            _factory = factory;
        }
    }
}