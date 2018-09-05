using System.Collections.Generic;

namespace ControlsLibrary.AbstractControllers
{
    public interface IControlList : IList<IControl>
    {
        object Controls { get; set; }
    }
}