using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class SplitPanelLogic : ISplitContainer
    {
        private IPanel _container;
        private ISetarator _separator;
        private IFactory _factory;

        public SplitPanelLogic(IPanel pnl, IFactory factory, double relativePosition)
        {
            _factory = factory;
            this._container = pnl;
            Controls = pnl.Controls;
            _separator = new SeparatorLogic(factory.CreateSeparator());
            _separator.RelativePosition = relativePosition;
            (IPanel panel1, IPanel panel2) = CreateTwoPanel(Width, Height, _separator.RelativePosition);

            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(_separator);
        }

        public (IPanel, IPanel) CreateTwoPanel(int width, int height, double separatorRelativePosition)
        {
            IPanel panel1 = _factory.CreateTabCollection(new Point(0, 0), width, (int)(height * separatorRelativePosition));
            IPanel panel2 = _factory.CreateBufferedCollection(new Point(0, (int)(height * (1 - separatorRelativePosition))), width, (int)(height * (1 - separatorRelativePosition)));
            return (panel1, panel2);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object Control { get => _container.Control; }
        public string Name { get => _container.Name; set => _container.Name = value; }
        public Point Location { get => _container.Location; set => _container.Location = value; }
        public bool Visible { get => _container.Visible; set => _container.Visible = value; }
        public int Width { get => _container.Width; set => _container.Width = value; }
        public int Height { get => _container.Height; set => _container.Height = value; }
        public void InitializeComponent()
        {
        }

        public IControlList Controls { get; set; }
        //TODO изменение ориентации => изменение позиций
        public Orientation Orientation { get; set; }

        public IPanel Panel1 { get => (IPanel) Controls[0]; set => Controls[0] = value; }
        public IPanel Panel2 { get => (IPanel) Controls[1]; set => Controls[1] = value; }
        public ISetarator Separator
        {
            get => (ISetarator) Controls[2];
            set
            {
                Controls[2] = value;
                RatioChanged(value.RelativePosition);
            }
        }

        public double SplitPosition
        {
            get => _separator.RelativePosition;
            set
            {
                _separator.RelativePosition = value;
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