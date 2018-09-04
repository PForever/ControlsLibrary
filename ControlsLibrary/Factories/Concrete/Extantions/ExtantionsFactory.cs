using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;

namespace ControlsLibrary.Factories.Concrete.Extantions
{
    public static class ExtantionsFactory
    {
        internal static WinFactory WinFactory { get; set; }
        public static IControl AsControl(this Control control)
        {
            return WinFactory.
        }
    }
}