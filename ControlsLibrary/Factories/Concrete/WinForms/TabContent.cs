using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.AbstractControllers.Tab.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms
{
    internal class TabContent : ITabContent
    {
        object ITabContent.Control { get => Control; set => Control = (Control) value; }
        Control Control { get; set; }
        public TabContent(Control control)
        {
            Control = control;
        }
        public TabContent()
        {
            Control = new Control();
        }
        public string Name { get => Control.Name; set => Control.Name = value; }
        public bool Visible { get => Control.Visible; set => Control.Visible = value; }

        public void Dispose()
        {
            Control.Dispose();
        }

        public void InitAsync()
        {
            throw new NotImplementedException();
        }

        public void MoveNextAsync(int count = 0)
        {
            throw new NotImplementedException();
        }

        public void MovePreviousAsync(int count = 0)
        {
            throw new NotImplementedException();
        }

        public void OnTabDeleted(object sender, TabDeletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
