using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.Containers;
using ControlsLibrary.Factories.Concrete.WinForms.Containers;

namespace ControlsLibrary.Factories.Concrete.WinForms.Controls
{
    public class StripMenu : IStripMenu
    {
        private readonly IFactory _factory;
        private readonly MenuStrip _menuStrip;

        public StripMenu(MenuStrip menuStrip, IFactory factory)
        {
            _menuStrip = menuStrip;
            _factory = factory;
            Controls = new ControlList(menuStrip.Controls, factory);
            InnerTools = new StripMenuItemsCollection(menuStrip, factory);
        }

        public void Dispose()
        {
            _menuStrip?.Dispose();
        }

        public object Control => _menuStrip;
        public string Name
        {
            get => _menuStrip.Name;
            set => _menuStrip.Name = value;
        }

        public Point Location
        {
            get => _menuStrip.Location;
            set => _menuStrip.Location = value;
        }

        public bool Visible
        {
            get => _menuStrip.Visible;
            set => _menuStrip.Visible = value;
        }

        public int Width
        {
            get => _menuStrip.Width;
            set => _menuStrip.Width = value;
        }

        public int Height
        {
            get => _menuStrip.Height;
            set => _menuStrip.Height = value;
        }


        public IStripMenuItemsCollection InnerTools { get; }
        //TODO set -- это проблема
        public IControlList Controls { get; set; }
    }
}