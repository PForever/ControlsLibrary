using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
{
    internal class TabViewContainer : ISplitContainer
    {
        private WinFactory _factory;
        public Panel SplitContainer;
        public object Control { get => SplitContainer; set => SplitContainer = (Panel) value; }

        public TabViewContainer(Panel splitContainer, WinFactory factory)
        {
            _factory = factory;
            SplitContainer = splitContainer;
        }

        public void Dispose()
        {
            SplitContainer.Dispose();
        }

        public string Name { get => SplitContainer.Name; set => SplitContainer.Name = value; }
        public Point Location { get => SplitContainer.Location; set => SplitContainer.Location = value; }

        public bool Visible 
        {
            get => SplitContainer.Visible;
            set => SplitContainer.Visible = value;
        }

        public int Width
        {
            get => SplitContainer.Width;
            set => SplitContainer.Width = value;
        }

        public int Height
        {
            get => SplitContainer.Height;
            set => SplitContainer.Height = value;
        }

        public void InitializeComponent()
        {
            throw new System.NotImplementedException();
        }

        public IList<IControl> Controls { get; set; }
        public Orientation Orientation { get; set; }

        public IPanel Panel1
        {
            get => _factory.CreateTabCollection(SplitContainer.Controls[0]);
            set => SplitContainer.Controls.SetChildIndex((Panel) value.Control, 0);
        }

        public IPanel Panel2
        {
            get => _factory.CreateBufferedCollection(SplitContainer.Controls[1]);
            set => SplitContainer.Controls.SetChildIndex((Panel) value.Control, 1);
        }
    }
}