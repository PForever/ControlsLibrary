using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.Tab;
using ControlsLibrary.Factories.Concrete.WinForms;
using ControlsLibrary.Tab.AbstractControllers;

namespace ControlsLibrary.Factories.Concrete
{
    class WinFactory : IFactory
    {
        public ITabContent CreateTabContent(object content)
        {
            return new TabContent((Control)content);
        }

        public ITabContent CreateTabContent()
        {
            return new TabContent();
        }

        public ITabPanel CreateTabPanel(object panel)
        {
            return new TabPanel((Panel)panel);
        }
        public ITabPanel CreateTabPanel(ITabContent tabContent)
        {
            return new TabPanel((Control)CreateTabContent(tabContent).Control);
        }
        public ITabPanel CreateTabPanel()
        {
            return new TabPanel();
        }
    }
}
