using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ControlsLibrary.View
{
    public partial class WindowTabControl : TabControl
    {
        private const int TabCtrl_AdjustRect = 0x1328;
        public WindowTabControl()
        { }
        protected override void WndProc(ref Message m)  //Скрываем стандартные кнопки
        {
            if (m.Msg == TabCtrl_AdjustRect && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }
    internal class WindowsTabPanel : Panel
    {

    }
}
