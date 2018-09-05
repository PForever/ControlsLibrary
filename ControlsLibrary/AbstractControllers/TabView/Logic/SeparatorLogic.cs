﻿using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class SeparatorLogic : ISetarator
    {
        private IControl control;

        public SeparatorLogic(IControl control)
        {
            this.control = control;
        }

        public void Dispose()
        {
            control.Dispose();
        }

        public object Control { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public void InitializeComponent()
        {
        }

        public double RelativePosition { get; set; }
    }
}