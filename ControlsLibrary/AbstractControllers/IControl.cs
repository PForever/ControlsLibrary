    using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary.AbstractControllers
{
    public interface IControl : IDisposable
    {
        object Control { get; }
        string Name { get; set; }
        Point Location { get; set; }
        bool Visible { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}
