using System;
using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Containers;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class ViewCollectionLogic : IBufferedCollection
    {
        object IControl.Control { get => Panel; set => Panel = (IPanel) value; }
        public IPanel Panel;

        public ViewCollectionLogic(IPanel pnl)
        {
            Panel = pnl;
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

        public IList<IControl> Controls { get => Panel.Controls; set => Panel.Controls = value; }
        public Orientation Orientation { get => Panel.Orientation; set => Panel.Orientation = value; }
        public TimeSpan TimeOut { get; set; }
        public int Capacity { get; set; }
        public ITabContent Current { get; set; }
    }
}