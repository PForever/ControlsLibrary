using System.Collections.Generic;
using System.Drawing;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    class TabViewLogic : ITabView, ICreator
    {
        public object Control { get => Container.Control; }
        protected event SizeChangedHandler SizeChanged;
        protected event LocationChangedHandler LocationChanged;
        public ISplitContainer Container;
        private ITabCollection _tabCollection;
        private IBufferedCollection _bufferedCollection;

        public TabViewLogic(IFactory factory)
        {
            Factory = factory;
            InitializeComponent();
        }

        public Position Position { get; set; }

        public ITabCollection TabCollection
        {
            get => _tabCollection;
            set
            {
                _tabCollection = value;

                LocationChanged += _tabCollection.OnParentLocationChanged;
                SizeChanged += _tabCollection.OnSizeChanged;
            }
        }

        public IBufferedCollection BufferedCollection
        {
            get => _bufferedCollection;
            set
            {
                _bufferedCollection = value;
                
                LocationChanged += _bufferedCollection.OnParentLocationChanged;
                SizeChanged += _bufferedCollection.OnParentSizeChanged;
            }
        }

        public IControlList Controls { get; set; }
        Orientation IPanel.Orientation { get; set; }

        public IFactory Factory { get; set; }

        public string Name
        {
            get => Container.Name;
            set => Container.Name = value;
        }

        public Point Location
        {
            get => Container.Location;
            set
            {
                Point oldValue = Container.Location;
                Container.Location = value;
                LocationChanged?.Invoke(this, new LocationChangedHandlerArgs(value, oldValue));
            }
        }

        public void OnControlLocationChanged(Point oldLocation, Point newLocation)
        {
            LocationChanged?.Invoke(this, new LocationChangedHandlerArgs(newLocation, oldLocation));
        }

        public void OnControlSizeChanged(Size oldSize, Size newSize)
        {
            SizeChanged?.Invoke(this, new SizeChangedHandlerArgs(newSize, oldSize));
        }

        public bool Visible
        {
            get => Container.Visible;
            set => Container.Visible = value;
        }

        //TODO изменение шириы и высоты должно вызывать перерисовку табов внутри коллекции. Лучше всего сделать ивент
        public int Width
        {
            get => Container.Width;
            set => Container.Width = value;
        }
        
        public int Height
        {
            get => Container.Height;
            set => Container.Height = value;
        }

        public void InitializeComponent()
        {
            Container = Factory.CreateSplitContainer(false);
            TabCollection = Factory.CreateTabCollection();
            BufferedCollection = Factory.CreateBufferedCollection();

            TabCollection.TabSelected += OnTabSelected;
            Container.AddNewTab += OnNewTabAdded;
            TabCollection.ButtonAddClickedHandler += OnNewTabAdded;

            Container.Panel1 = TabCollection;
            Container.Panel2 = BufferedCollection;
            Container.RelativePosition = 30;
            ITabPanel tabPanel = Factory.CreateTabPanel();
            TabCollection.Add(tabPanel);
        }

        public void OnNewTabAdded(object sender, TabEventArgs arg)
        {
            ITabPanel item = arg.TabPanel ?? Factory.CreateTabPanel();
            TabCollection.Add(item);
        }

        public void OnTabSelected(object sender, TabEventArgs args)
        {
            Show(args.TabPanel.TabContent);
        }

        public void Show(ITabContent tabContent = null)
        {
            BufferedCollection.Current = tabContent;
        }

        public void Dispose()
        {
            Container?.Dispose();
            TabCollection?.Dispose();
            BufferedCollection?.Dispose();
        }
    }

    internal delegate void LocationChangedHandler(object sender, LocationChangedHandlerArgs args);
    internal delegate void SizeChangedHandler(object sender, SizeChangedHandlerArgs args);
}
