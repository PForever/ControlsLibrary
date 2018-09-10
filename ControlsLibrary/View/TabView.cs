using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Logic;
using ControlsLibrary.Factories;
using ControlsLibrary.Factories.Concrete;

namespace ControlsLibrary.View
{
    public class TabView : Panel
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
            _tabContent = new Panel();
            _tabPanel = new Panel();
            _panel = new Panel();
            _viewPanel = new Panel();

            WinFactory factory = new WinFactory
            {
                DefaultTabContent = _tabContent,
                DefaultTabPanel = _tabPanel,
                DefaultTabsPanel = _panel,
                DefaultSeparator = new Label { BackColor = Color.Black },
                DefaultSplitPanel = this,
                DefaultViewPanel = _viewPanel
            };
            _tabView = new TabViewLogic(factory);
            _tabView.InitializeComponent();
        }
    }
}