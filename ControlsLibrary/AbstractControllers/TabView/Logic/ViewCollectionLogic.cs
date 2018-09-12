using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class ViewCollectionLogic : IBufferedCollection
    {
        private ITabContent _current;
        private BufferedPage _buffer;
        object IControl.Control { get => Panel.Control; }
        public IPanel Panel { get; set; }

        public ViewCollectionLogic(IPanel pnl)
        {
            Panel = pnl;
            _buffer = new BufferedPage();
        }

        public void Dispose()
        {
            Panel.Dispose();
        }

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
            Panel.InitializeComponent();
        }

        public IControlList Controls { get => Panel.Controls; set => Panel.Controls = value; }
        public Orientation Orientation { get => Panel.Orientation; set => Panel.Orientation = value; }
        public TimeSpan TimeOut { get; set; }
        public int Capacity { get; set; }

        public ITabContent Current
        {
            get => _current;
            set
            {
                if(!_buffer.Pages.Contains(value)) _buffer.Add(value);
                if (_current != null) //TODO заменить на заглушку
                {
                    _buffer.Start(_current);
                    //_current.Visible = false;
                    Controls.Remove(_current);
                }
                _current = value;
                //_current.Visible = true;
                Controls.Add(_current);
            }
        }

        public void OnParentLocationChanged(object sender, LocationChangedHandlerArgs args)
        {
        }

        public void OnParentSizeChanged(object sender, SizeChangedHandlerArgs args)
        {
        }
    }
}