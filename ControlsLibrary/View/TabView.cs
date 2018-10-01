using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Logic;
using ControlsLibrary.Factories.Concrete;

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
                Panel panel = new Panel{ Name = "TabContent", BorderStyle = BorderStyle.FixedSingle, BackColor = Color.GreenYellow };
                panel.Controls.Add(page);
                return panel;
            }
//new Panel{ Name = "TabContent", BorderStyle = BorderStyle.FixedSingle, BackColor = Color.GreenYellow, /*BackgroundImage = Image.FromFile("C:\\Users\\st804476.RECTORAT\\Pictures\\wallhaven-181730.jpg"), */BackgroundImageLayout = ImageLayout.Stretch};
            Panel Panel() => new Panel { Name = "TabsPanel", BorderStyle = BorderStyle.FixedSingle, Height = 30, BackColor = Color.Blue};
            Panel ViewPanel() => new Panel { Name = "ViewPanel", BorderStyle = BorderStyle.FixedSingle, BackColor = Color.Red};
            Panel TabPanel() => new Panel { Name = "TabPanel", BorderStyle = BorderStyle.FixedSingle, Width = 50, BackColor = Color.Green, /*BackgroundImage = Image.FromFile("C:\\Users\\st804476.RECTORAT\\Pictures\\393678.jpg"), */BackgroundImageLayout = ImageLayout.Stretch };
            this.Name = "TabView";
            //Button button = new Button{Dock = DockStyle.Fill, BackgroundImage = Image.FromFile("C:\\Users\\st804476.RECTORAT\\Pictures\\393678.jpg"), BackgroundImageLayout = ImageLayout.Stretch};
            //_tabPanel.Controls.Add(button);

            WinFactory factory = new WinFactory
            {
                CreateDefaultTabContent = TabContent,
                CreateDefaultTabPanel = TabPanel,
                CreateDefaultTabsPanel = Panel,
                DefaultSplitPanel = this,
                CreateDefaultViewPanel = ViewPanel
            };
            _tabView = new TabViewLogic(factory);
            //_tabView.BufferedCollection.Current = factory.CreateTabContent(_tabPanel);
        }
    }
}