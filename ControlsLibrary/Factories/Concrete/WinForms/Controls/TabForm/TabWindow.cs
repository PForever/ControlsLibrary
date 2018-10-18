using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.AbstractControllers.TabForms.TabView;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;

namespace ControlsLibrary.Factories.Concrete.WinForms.Controls.TabForm
{
    public class TabWindow : ITabWindow
    {
        private readonly Form _window;
        public object Control => _window;
        private readonly IFactory _factory;
        private IStripMenu _stripMenu;
        private ITabWindow _parent;

        //TODO переместить в отдельную логику
        public TabWindow(Form window, ITabWindow parent, ITabView container, IFactory factory)
        {
            _factory = factory;
            _window = window;
            Parent = parent;
            Container = container;
            container.Owner = this;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            if (_window.MainMenuStrip != null) _stripMenu = _factory.CreateStripMenu(_window.MainMenuStrip);
            _window.Controls["Content"].Controls.Add((TableLayoutPanel)Container.Control);
            Children = new List<ITabWindow>();
        }

        public string Name
        {
            get => _window.Name;
            set => _window.Name = value;
        }

        public Point Location
        {
            get => _window.Location;
            set => _window.Location = value;
        }

        public bool Visible
        {
            get => _window.Visible;
            set => _window.Visible = value;
        }

        public int Width
        {
            get => _window.Width;
            set => _window.Width = value;
        }

        public int Height
        {
            get => _window.Height;
            set => _window.Height = value;
        }

        public ITabView Container { get; }

        public ITabWindow Parent
        {
            get => _parent;
            set
            {
                _parent?.Children.Remove(this);
                _parent = value;
                value?.Children.Add(this);
            }
        }

        public ICollection<ITabWindow> Children { get; private set; }

        public IStripMenu StripMenu
        {
            get => _stripMenu;
            set
            {
                if (_window.MainMenuStrip != null)
                {
                    _window.Controls.Remove(_window.MainMenuStrip);
                }
                _stripMenu = value;
                var menu = (MenuStrip) value.Control;
                _window.Controls.Add(menu);
                _window.MainMenuStrip = menu;
            }
        }

        public void Open()
        {
            _window.Show();
            //Application.Run(_window);
        }

        public void Close()
        {
            _window.Close();
        }

        public void ComebackToParent()
        {
            //TODO Enable = false if Parent == null
            if(Parent == null) return;
            ITabPanel[] panels = Container.TabCollection.ToArray();
            int count = Container.TabCollection.Count;
            for (int i = 0; i < count; i++)
            {
                Container.TabCollection.Remove(Container.TabCollection.First(), false);
            }
            Parent.Join(this, panels);
            //Container.TabCollection.Clear();

            while (Children.Count > 0)
            {
                Children.First().Parent = Parent;
            }
            Close();
        }

        public void JoinChildren()
        {
            while (Children.Count > 0)
            {
                Children.First().ComebackToParent();
            }
        }

        public void Join(ITabWindow child, IEnumerable<ITabPanel> childsTabs)
        {
            Container.Join(childsTabs);
            Children.Remove(child);
        }

        public void Dispose()
        {
            _window.Dispose();
        }
    }
}