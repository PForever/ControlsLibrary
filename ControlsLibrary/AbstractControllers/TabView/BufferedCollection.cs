using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary.AbstractControllers.TabView
{
    public interface IBufferedCollection : IContainer
    {
        TimeSpan TimeOut { get; set; }
        int Capacity { get; set; }
    }
}
