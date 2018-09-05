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
        private ITabView _tabView;
        public void InitializeComponent()
        {
            _tabContent = new Panel();
            _tabPanel = new Panel();
            WinFactory factory = new WinFactory
            {
                DefaultTabContent = _tabContent,
                DefaultTabPanel = _tabPanel,
                DefaultPanel = this,
                DefaultSeparator = new Label { BackColor = Color.Black }
            };
            _tabView = new TabViewLogic(new WinFactory());
            _tabView.InitializeComponent();
        }
    }
}