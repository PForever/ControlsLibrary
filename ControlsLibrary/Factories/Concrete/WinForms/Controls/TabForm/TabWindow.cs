using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.AbstractControllers.TabForms.TabView;

namespace ControlsLibrary.Factories.Concrete.WinForms.Controls.TabForm
{
    public class TabWindow : ITabWindow
    {
        private readonly Form _window;
        public object Control => _window;

        public TabWindow(Form window, ITabView parent, ITabView container)
        {
            _window = window;
            Parent = parent;
            Container = container;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _window.Controls["Content"].Controls.Add((TableLayoutPanel)Container.Control);
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

        public ITabView Parent { get; }

        public void Open()
        {
            _window.Show();
            //Application.Run(_window);
        }

        public void Close()
        {
            _window.Close();
        }

        public void Dispose()
        {
            _window.Dispose();
        }
    }
}