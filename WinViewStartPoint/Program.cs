using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.Factories;
using ControlsLibrary.Factories.Concrete;

namespace WinViewStartPoint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            FactoryCreator.DefaultContext = () =>
            {
                {
                    var page = new DefaultPage();
                    Panel panel = new Panel
                    {
                        Name = "TabContent",
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = RandomColor
                    };
                    panel.Controls.Add(page);
                    return panel;
                }
            };

            IFactory factory = FactoryCreator.CreateFactory();
            factory.CustomTabWindow = CustomTabWindow;

            ITabWindow CustomTabWindow(ITabWindow tabWindow)
            {

                var menu = factory.CreateStripMenu();

                var fileTool = factory.CreateStripMenuItem("File", "&File");
                var windowTool = factory.CreateStripMenuItem("Window", "&Window");

                void GoToParent(object sender, EventArgs e)
                {
                    tabWindow.ComebackToParent();
                }
                var toParentItem = factory.CreateStripMenuItem("Go to parent", "&Go to parent", Keys.Control | Keys.Home, GoToParent);
                void JoinChildrenItem(object sender, EventArgs e)
                {
                    tabWindow.JoinChildren();
                }
                var joinChildrenItem = factory.CreateStripMenuItem("Join children", "&Join children", Keys.Control | Keys.Shift | Keys.Home, JoinChildrenItem);
                windowTool.InnerTools.Add(toParentItem, joinChildrenItem);
                var tabsTool = factory.CreateStripMenuItem("Tabs", "&Tabs");
                menu.InnerTools.Add(fileTool, windowTool, tabsTool);
                tabWindow.StripMenu = menu;
                return tabWindow;
            }

            ViewStarter.Factory = factory;
            ViewStarter.Start();
        }



        private static readonly Random _rnd = new Random();
        private static readonly byte[] _bytes = new byte[3];

        private static Color RandomColor
        {
            get
            {
                _rnd.NextBytes(_bytes);
                return Color.FromArgb(_bytes[0], _bytes[1], _bytes[2]);
            }
        }
    }
}
