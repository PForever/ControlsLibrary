//using System.Collections.Generic;
//using System.Drawing;
//using System.Windows.Forms;
//using ControlsLibrary.AbstractControllers;
//using ControlsLibrary.AbstractControllers.TabView;
//using ControlsLibrary.Factories.Concrete.WinForms.Containers;
//using Orientation = ControlsLibrary.Containers.Orientation;

//namespace ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab
//{
//    internal class TabViewContainer : ISplitContainer
//    {
//        private WinFactory _factory;
//        private Panel _splitContainer;
//        public object Control { get => _splitContainer; set => _splitContainer = (Panel) value; }

//        public TabViewContainer(Panel splitContainer, WinFactory factory)
//        {
//            _factory = factory;
//            _splitContainer = splitContainer;
//            _controlList = new ControlList(_splitContainer.Controls);
//            (IPanel panel1, IPanel panel2) = factory.CreateTwoPanel(Width, Height, Separator.RelativePosition);
//            IControl separator = factory.CreateSeparator(Height, DefaultSep);
//            _controlList.Add(panel1);
//            _controlList.Add(panel2);
//            _controlList.Add(separator);
//        }

//        public void Dispose()
//        {
//            _splitContainer.Dispose();
//        }

//        public string Name { get => _splitContainer.Name; set => _splitContainer.Name = value; }
//        public Point Location { get => _splitContainer.Location; set => _splitContainer.Location = value; }

//        public bool Visible 
//        {
//            get => _splitContainer.Visible;
//            set => _splitContainer.Visible = value;
//        }

//        public int Width
//        {
//            get => _splitContainer.Width;
//            set => _splitContainer.Width = value;
//        }

//        public int Height
//        {
//            get => _splitContainer.Height;
//            set => _splitContainer.Height = value;
//        }

//        public void InitializeComponent()
//        {
//        }

//        private ControlList _controlList;

//        public IControlList Controls
//        {
//            get => _controlList;
//            set => _controlList = (ControlList) value;
//        }

//        //TODO изменение ориентации => изменение позиций
//        public Orientation Orientation { get; set; }

//        public IPanel Panel1 { get => (IPanel)_controlList.Collection[0]; set => _controlList.Collection[0] = value; }
//        public IPanel Panel2 { get => (IPanel)_controlList.Collection[1]; set => _controlList.Collection[1] = value; }
//        public ISetarator Separator { get => (ISetarator)_controlList.Collection[2]; set => _controlList.Collection[2] = value; }
//    }
//}