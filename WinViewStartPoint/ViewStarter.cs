using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary;
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

            Form window = (Form)Factory.CreateWindow(tabView).Control;

            var menu = Factory.CreateStripMenu();
            var tol0 = Factory.CreateStripMenuTool();
            tol0.Text = nameof(tol0);
            var tol00 = Factory.CreateStripMenuTool();
            tol00.Text = nameof(tol00);
            var tol01 = Factory.CreateStripMenuTool();
            tol01.Text = nameof(tol01);
            var tol010 = Factory.CreateStripMenuTool();
            tol010.Text = nameof(tol010);
            tol010.ShortcutKeys = Keys.Control | Keys.A;
            var tol1 = Factory.CreateStripMenuTool();
            tol1.Text = nameof(tol1);

            tol01.InnerTools.Add(tol010);
            tol0.InnerTools.Add(tol00);
            tol0.InnerTools.Add(tol01);

            menu.InnerTools.Add(tol0);
            menu.InnerTools.Add(tol1);
            //TODO сделать AddRange с params
            window.Controls.Add((Control)menu.Control);
            window.MainMenuStrip = (MenuStrip) menu.Control;
            Application.Run(window);
        }
    }
}