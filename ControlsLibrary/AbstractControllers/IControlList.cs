using System.Collections.Generic;

namespace ControlsLibrary.AbstractControllers
{
    public interface IControlList : IList<IControl>
    {
        object Controls { get; set; }
        void Swap(int oldIndex, int newIndex);
        void RemoveAt(int index, bool dispose);
    }
}