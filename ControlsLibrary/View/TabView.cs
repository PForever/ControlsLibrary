using System;
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
        public TabView()
        {
            InitializeComponent();
        }

        protected void InitializeComponent()
        {
            InitializeComponent(true);
        }
        protected virtual void InitializeComponent(bool init)
        {
            if (!init) return;

            this.Dock = DockStyle.Fill;
            this.BackColor = Color.OrangeRed;

            BorderStyle = BorderStyle.Fixed3D;
            Panel TabContent()
            {
                var page = new DefaultPage();
                Panel panel = new Panel
                {
                    Name = "TabContent",
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = RandomColor
                };
                panel.Controls.Add(page);
                return panel;
            }
            Panel Panel() => new Panel
            {
                Name = "TabsPanel",
                BorderStyle = BorderStyle.FixedSingle,
                Height = 30,
                BackColor = Color.White
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

        private readonly Random _rnd = new Random();
        private readonly byte[] _bytes = new byte[3];
        private Color RandomColor
        {
            get
            {

                _rnd.NextBytes(_bytes);
                return Color.FromArgb(_bytes[0], _bytes[1], _bytes[2]);
            }
        }
    }
}