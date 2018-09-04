using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlsLibrary.AbstractControllers.TabView.Tab;

namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface IBufferedCollection : IPanel
    {
        TimeSpan TimeOut { get; set; }
        int Capacity { get; set; }
        ITabContent Current { get; set; }
    }
}
