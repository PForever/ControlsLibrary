using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    public class SimpleControl : IControl
    {
        object IControl.Control { get => Control; set => Control = (Control) value; }
        public Control Control { get; set; }

        protected SimpleControl()
        {
        }

        public SimpleControl(Control control)
        {
            Control = control;
        }

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
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            Control.Dispose();
        }
    }
}