using System.Drawing;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView.Tab;

namespace ControlsLibrary.Containers
{
    public struct TabKvP : IControl
    {
        internal ITabPanel TabPanel; 
        internal ITabContent TabContent;

        public TabKvP(ITabPanel tabPanel, ITabContent tabContent)
        {
            TabPanel = tabPanel;
            TabContent = tabContent;
        }

        public object Control { get => TabPanel; set => TabPanel = (ITabPanel) value; }
        public string Name { get => TabPanel.Name; set => TabPanel.Name = value; }
        public Point Location { get => TabPanel.Location; set => TabPanel.Location = value; }
        public bool Visible { get => TabPanel.Visible; set => TabPanel.Visible = value; }
        public int Width { get => TabPanel.Width; set => TabPanel.Width = value; }
        public int Height { get => TabPanel.Height; set => TabPanel.Height = value; }

        public void Dispose()
        {
            TabPanel.Dispose();
        }

        public void InitializeComponent()
        {
            TabPanel.InitializeComponent();
        }
    }
}