using System.Drawing;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class SeparatorLogic : ISetarator
    {
        public IControl _control { get; set; }

        public SeparatorLogic(IControl control, int height, double relativePosition)
        {
            this._control = control;
            RelativePosition = relativePosition;
            Location = new Point(0, (int)(height*relativePosition));
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