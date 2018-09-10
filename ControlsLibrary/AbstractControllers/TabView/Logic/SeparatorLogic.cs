using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class SeparatorLogic : ISetarator
    {
        public IControl _control { get; set; }

        public SeparatorLogic(IControl control)
        {
            this._control = control;
        }

        public void Dispose()
        {
            _control.Dispose();
        }

        public object Control { get => _control.Control; }
        public string Name { get => _control.Name; set => _control.Name = value; }
        public Point Location { get => _control.Location; set => _control.Location = value; }
        public bool Visible { get => _control.Visible; set => _control.Visible = value; }
        public int Width { get => _control.Width; set => _control.Width = value; }
        public int Height { get => _control.Height; set => _control.Height = value; }
        public void InitializeComponent()
        {
        }

        public double RelativePosition { get; set; }
    }
}