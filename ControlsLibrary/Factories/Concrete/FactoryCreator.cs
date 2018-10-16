using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete
{
    public static class FactoryCreator
    {
        public static Func<Panel> DefaultContext { private get; set; }

        //static FactoryCreator()
        //{
        //    DefaultContext = () =>
        //    {
        //        {
        //            var page = new DefaultPage();
        //            Panel panel = new Panel
        //            {
        //                Name = "TabContent",
        //                BorderStyle = BorderStyle.FixedSingle,
        //                BackColor = RandomColor
        //            };
        //            panel.Controls.Add(page);
        //            return panel;
        //        }
        //    };
        //}

        public static IFactory CreateFactory()
        {

            Panel Panel() => new Panel
            {
                Name = "TabsPanel",
                BorderStyle = BorderStyle.FixedSingle,
                Height = 30,
                BackColor = Color.White
            };
            Panel ViewPanel() => new Panel
            {
                Name = "ViewPanel",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Red
            };
            Panel TabPanel() => new Panel
            {
                Name = "TabPanel",
                BorderStyle = BorderStyle.FixedSingle,
                Width = 50,
                BackColor =
                    Color.Green,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            TableLayoutPanel TabViewPanel()
            {
                return new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.OrangeRed,
                    Name = "TabView",
                    BorderStyle = BorderStyle.Fixed3D
                };
            }

            MenuStrip MenuStrip() => new MenuStrip();
            ToolStripMenuItem ToolStripMenuItem() => new ToolStripMenuItem();

            Form TabWindow() => new TabForm();

            return new WinFactory
            {
                CreateDefaultTabContent = DefaultContext,
                CreateDefaultTabPanel = TabPanel,
                CreateDefaultTabsPanel = Panel,
                CreateDefaultSplitPanel = TabViewPanel,
                CreateDefaultViewPanel = ViewPanel,
                CreateDefaultTabWindow = TabWindow,
                CreateDefaultStripMenu = MenuStrip,
                CreateDefaultStripMenuTool = ToolStripMenuItem
            };
        }
    }
}