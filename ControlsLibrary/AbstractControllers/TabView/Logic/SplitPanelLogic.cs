using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class SplitPanelLogic : ISplitContainer
    {
        private IPanel _container;
        private ISetarator _setarator;

        public SplitPanelLogic(IPanel pnl, IFactory factory)
        {
            this._container = pnl;
            _setarator = new SeparratorLogic(factory.CreateControl());
        }
        public SplitPanelLogic(IPanel pnl, IControl control)
        {
            this._container = pnl;
            _setarator = new SeparratorLogic(control);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public object Control { get => _container; set => _container = (IPanel) _container.Control; }
        public string Name { get => _container.Name; set => _container.Name = value; }
        public Point Location { get => _container.Location; set => _container.Location = value; }
        public bool Visible { get => _container.Visible; set => _container.Visible = value; }
        public int Width { get => _container.Width; set => _container.Width = value; }
        public int Height { get => _container.Height; set => _container.Height = value; }
        public void InitializeComponent()
        {
            throw new System.NotImplementedException();
        }

        public IList<IControl> Controls { get; set; }
        public Orientation Orientation { get; set; }
        public IPanel Panel1 { get; set; }
        public IPanel Panel2 { get; set; }

        public ISetarator Setarator
        {
            get => Setarator;
            set
            {
                _setarator = value;
                RatioChanged(value.RelatavePosition);
            }
        }

        public double SplitPosition
        {
            get => _setarator.RelatavePosition;
            set
            {
                _setarator.RelatavePosition = value;
                RatioChanged(value);
            }
        }

        private void RatioChanged(double ratio)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    Panel1.Height = (int)(Height * ratio);
                    break;
                case Orientation.Vertical:
                    Panel1.Width = (int)(Width * ratio);
                    break;
            }
        }
    }
}