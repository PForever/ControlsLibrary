﻿using System.Drawing;
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
        private Label _separator;
        private ITabView _tabView;

        public TabView()
        {
            InitializeComponent();
        }
        public void InitializeComponent()
        {
            BorderStyle = BorderStyle.Fixed3D;
            _tabContent = new Panel{BorderStyle = BorderStyle.FixedSingle};
            _tabPanel = new Panel() { BorderStyle = BorderStyle.FixedSingle };
            _panel = new Panel() { BorderStyle = BorderStyle.FixedSingle };
            _viewPanel = new Panel() { BorderStyle = BorderStyle.FixedSingle };
            _separator = new Label {BackColor = Color.Black};

            WinFactory factory = new WinFactory
            {
                DefaultTabContent = _tabContent,
                DefaultTabPanel = _tabPanel,
                DefaultTabsPanel = _panel,
                DefaultSeparator = _separator,
                DefaultSplitPanel = this,
                DefaultViewPanel = _viewPanel
            };
            _tabView = new TabViewLogic(factory);
            _tabView.InitializeComponent();
        }
    }
}