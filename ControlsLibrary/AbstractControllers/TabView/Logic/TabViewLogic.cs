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

        public ISplitContainer Container;

        public TabViewLogic(IFactory factory)
        {
            Factory = factory;
            
            InitializeComponent();
        }
        //public TabViewLogic(ISplitContainer splitContainer, IFactory factory)
        //{
        //    Factory = factory;
        //    Container = splitContainer;
        //}

        public Position Position { get; set; }
        public ITabCollection TabCollection { get; set; }
        public IBufferedCollection BufferedCollection { get; set; }
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
            set => Container.Location = value;
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
            Container = Factory.CreateSplitContainer();
            TabCollection = Factory.CreateTabCollection();
            BufferedCollection = Factory.CreateBufferedCollection();

            TabCollection.TabSelected += OnTabSelected;

            Container.Panel1 = TabCollection;
            Container.Panel2 = BufferedCollection;

            ITabPanel tabPanel = Factory.CreateTabPanel();
            TabCollection.Add(tabPanel);
        }

        public void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            Show(args.TabContent);
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
}
