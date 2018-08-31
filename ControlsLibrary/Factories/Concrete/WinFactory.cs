using ControlsLibrary.AbstractControllers.TabView;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.Factories.Concrete.WinForms.TabView.Tab;
using System;

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
            return TabContent.CreateDefaultContentol();
        }

        public ITabPanel CreateTabPanel(object panel)
        {
            return new TabPanel((Panel)panel);
        }
        public ITabPanel CreateTabPanel(ITabContent tabContent)
        {
            return TabPanel.CreateDefaultPanel((Control)CreateTabContent(tabContent).Control);
        }
        public ITabPanel CreateTabPanel()
        {
            return TabPanel.CreateDefaultPanel();
        }
        public IBufferedCollection CreateBufferedCollection()
        {
            throw new NotImplementedException();
        }
    }
}
