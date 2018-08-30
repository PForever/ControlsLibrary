using ControlsLibrary.AbstractControllers.Tab.Events;
using ControlsLibrary.Tab.AbstractControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms
{
    class TabPanel : ITabPanel
    {
        object ITabPanel.Panel { get => Panel; set => Panel = (Panel) value; }
        Panel Panel { get; set; }
        public TabPanel(Panel panel)
        {
        }
        public TabPanel(Control control)
        {
            Panel = new Panel { Name = control.Name };
        }
        public TabPanel()
        {
            Panel = new Panel();
        }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => Panel.Name; set => Panel.Name = value; }

        public event TabDeleteEventHandler TabDeleted;
        public event TabMoveHandler TabMoved;
        public event TabSelectedEventHandler TabSelected;

        public void Dispose()
        {
            Panel.Dispose();
        }

        public void RemoveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
