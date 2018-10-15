using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlsLibrary.Factories.Concrete;
using ControlsLibrary.View;

namespace ControlsLibrary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TabView tabView = new TabView();
            WinFactory factory = tabView.Factory;
            Form window = (Form)factory.CreateWindow(tabView).Control;

            Application.Run(window);
        }
    }
}
