using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary;
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

            ViewStarter.Factory = FactoryCreator.CreateFactory(); ;
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
