using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Logic;
using ControlsLibrary.Factories.Concrete;
using Orientation = ControlsLibrary.Containers.Orientation;

namespace ControlsLibrary.View
{
    public class TabView : TableLayoutPanel
    {

        private ITabView _tabView;
        public int Value { get; set; }
        public TabView()
        {
            InitializeComponent();
        }
        public void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.Aqua;

            BorderStyle = BorderStyle.Fixed3D;
            Panel TabContent()
            {
                var page = new DefaultPage();
                Panel panel = new Panel
                {
                    Name = "TabContent",
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.GreenYellow
                };
                panel.Controls.Add(page);
                return panel;
            }
            Panel Panel() => new Panel
            {
                Name = "TabsPanel",
                BorderStyle = BorderStyle.FixedSingle,
                Height = 30,
                BackColor = Color.Blue
            };
            Panel ViewPanel() => new Panel
            {
                Name = "ViewPanel",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Red
            };
            Panel TabPanel() => new Panel
            {
                Name = "TabPanel",
                BorderStyle = BorderStyle.FixedSingle,
                Width = 50,
                BackColor =
                    Color.Green,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            this.Name = "TabView";

            WinFactory factory = new WinFactory
            {
                CreateDefaultTabContent = TabContent,
                CreateDefaultTabPanel = TabPanel,
                CreateDefaultTabsPanel = Panel,
                DefaultSplitPanel = this,
                CreateDefaultViewPanel = ViewPanel
            };
            _tabView = new TabViewLogic(factory);
            _tabView.Orientation = Orientation.Vertical;
        }
    }
}