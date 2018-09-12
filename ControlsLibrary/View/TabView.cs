using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Logic;
using ControlsLibrary.Factories;
using ControlsLibrary.Factories.Concrete;

namespace ControlsLibrary.View
{
    public class TabView : TableLayoutPanel
    {
        private Panel _tabContent;
        private Panel _tabPanel;
        private Panel _panel;
        private Panel _viewPanel;
        private ITabView _tabView;

        public TabView()
        {
            InitializeComponent();
        }
        public void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.Aqua;

            BorderStyle = BorderStyle.Fixed3D;
            _tabContent = new Panel{BorderStyle = BorderStyle.FixedSingle, BackColor = Color.GreenYellow/*, BackgroundImage = Image.FromFile("C:\\Users\\st804476.RECTORAT\\Pictures\\wallhaven-181730.jpg")*/, BackgroundImageLayout = ImageLayout.Stretch};
            _tabPanel = new Panel() { BorderStyle = BorderStyle.FixedSingle, BackColor = Color.Green/*, BackgroundImage = Image.FromFile("C:\\Users\\st804476.RECTORAT\\Pictures\\393678.jpg")*/, BackgroundImageLayout = ImageLayout.Stretch};
            _panel = new Panel() { BorderStyle = BorderStyle.FixedSingle, BackColor = Color.Blue};
            _viewPanel = new Panel() { BorderStyle = BorderStyle.FixedSingle, BackColor = Color.Red/*, Dock = DockStyle.Fill*/};

            WinFactory factory = new WinFactory
            {
                DefaultTabContent = _tabContent,
                DefaultTabPanel = _tabPanel,
                DefaultTabsPanel = _panel,
                DefaultSplitPanel = this,
                DefaultViewPanel = _viewPanel
            };

            _tabView = new TabViewLogic(factory);
        }
    }
}