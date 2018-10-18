using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.Factories;
using ControlsLibrary.Factories.Concrete;
using Orientation = ControlsLibrary.Containers.Orientation;

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

                IStripMenu menu = factory.CreateStripMenu();

                IStripMenuItem fileTool = factory.CreateStripMenuItem("File", "&File");
                IStripMenuItem windowTool = factory.CreateStripMenuItem("Window", "&Window");

                void ChangeOrientation(object sender, EventArgs e)
                {
                    tabWindow.Container.Orientation = tabWindow.Container.Orientation == Orientation.Horizontal? Orientation.Vertical : Orientation.Horizontal;
                }
                IStripMenuItem changeOrientation = factory.CreateStripMenuItem("Change Orientation", "&Change Orientation", Keys.Control | Keys.Shift | Keys.O, ChangeOrientation);
                void GoToParent(object sender, EventArgs e)
                {
                    tabWindow.ComebackToParent();
                }
                IStripMenuItem toParentItem = factory.CreateStripMenuItem("Go to parent", "&Go to parent", Keys.Control | Keys.Home, GoToParent);
                void JoinChildrenItem(object sender, EventArgs e)
                {
                    tabWindow.JoinChildren();
                }
                IStripMenuItem joinChildrenItem = factory.CreateStripMenuItem("Join children", "&Join children", Keys.Control | Keys.Shift | Keys.Home, JoinChildrenItem);
                windowTool.InnerTools.Add(changeOrientation, toParentItem, joinChildrenItem);

                IStripMenuItem tabsTool = factory.CreateStripMenuItem("Tabs", "&Tabs");
                void TabAddItem(object sender, EventArgs e)
                {
                    tabWindow.Container.AddNew();
                }
                IStripMenuItem tabAddItem = factory.CreateStripMenuItem("Add tab", "&Add tab", Keys.Control | Keys.T, TabAddItem);

                void TabRemoveItem(object sender, EventArgs e)
                {
                    tabWindow.Container.RemoveSelected();
                }
                IStripMenuItem tabRemoveItem = factory.CreateStripMenuItem("Remove tab", "&Remove tab", Keys.Control | Keys.W, TabRemoveItem);
                tabsTool.InnerTools.Add(tabAddItem, tabRemoveItem);

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
