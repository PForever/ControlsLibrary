using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class SeparratorLogic : ISetarator
    {
        private IControl control;

        public SeparratorLogic(IControl control)
        {
            this.control = control;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public object Control { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public void InitializeComponent()
        {
            throw new System.NotImplementedException();
        }

        public double RelatavePosition { get; set; }
    }
}