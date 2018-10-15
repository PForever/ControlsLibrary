﻿using System.Drawing;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    class TabViewLogic : TabViewLogicBase
    {
        private ITabCollection _tabCollection;
        public override object Control { get => Container.Control; }
        protected override ISplitContainer Container { get; }

        public TabViewLogic(IFactory factory) : this(factory, factory.CreateTabPanel())
        {
        }
        public TabViewLogic(IFactory factory, ITabPanel tab)
        {
            Factory = factory;
            Container = factory.CreateSplitContainer(false);
            TabCollection = factory.CreateTabCollection();
            BufferedCollection = factory.CreateBufferedCollection();

            InitializeComponent();

            TabCollection.Add(tab);
        }

        protected override ITabCollection TabCollection
        {
            set
            {
                _tabCollection = value;
                _tabCollection.Parent = this;
            }
            get => _tabCollection;
        }

        protected override IBufferedCollection BufferedCollection { get; }

        public override IControlList Controls { get; set; }

        public override Point Location
        {
            get => Container.Location;
            set
            {
                Point oldValue = Container.Location;
                Container.Location = value;
            }
        }
        public override Orientation Orientation
        {
            get => Container.Orientation;
            set => Container.Orientation = value;
        }

        public override IFactory Factory { get; }


        protected override void InitializeComponent()
        {

            TabCollection.TabSelected += OnTabSelected;
            Container.AddNewTab += OnNewTabAdded;
            Container.RemoveSelectedTab += OnSelectedTabRemoved;
            TabCollection.ButtonAddClickedHandler += OnNewTabAdded;

            Container.Panel1 = TabCollection;
            Container.Panel2 = BufferedCollection;
            Container.RelativePosition = 30;

            TabCollection.TabDisposing += OnTabDisposing;
        }

        protected override void OnTabDisposing(object sender, TabEventArgs arg)
        {
            BufferedCollection.Remove(arg.TabPanel.TabContent);
        }

        protected override void OnSelectedTabRemoved(object sender, TabEventArgs arg)
        {
            TabCollection.Remove(TabCollection.SelectedTab);
        }

        protected override void OnNewTabAdded(object sender, TabEventArgs arg)
        {
            ITabPanel item = arg.TabPanel ?? Factory.CreateTabPanel();
            TabCollection.Add(item);
        }

        protected override void OnTabSelected(object sender, TabEventArgs args)
        {
            Show(args.TabPanel.TabContent);
        }

        public override void Show(ITabContent tabContent = null)
        {
            BufferedCollection.Current = tabContent;
        }


        protected override void Dispose(bool dispose)
        {
            base.Dispose();
        }
    }
}
