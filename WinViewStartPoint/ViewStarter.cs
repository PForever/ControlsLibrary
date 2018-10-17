using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary;
using ControlsLibrary.AbstractControllers.TabForms;
using ControlsLibrary.AbstractControllers.TabForms.TabView;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Logic;
using ControlsLibrary.Factories;
using ControlsLibrary.Factories.Concrete;

namespace WinViewStartPoint
{
    public static class ViewStarter
    {
        public static IFactory Factory { private get; set; }

        public static void Start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            ITabView tabView = new TabViewLogic(Factory);

            ITabWindow tabWindow = Factory.CreateWindow(tabView);

            Form window = (Form)tabWindow.Control;
            Application.Run(window);
        }
    }
}