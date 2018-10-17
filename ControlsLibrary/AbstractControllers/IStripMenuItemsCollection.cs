using System.Collections.Generic;

namespace ControlsLibrary.AbstractControllers
{
    public interface IStripMenuItemsCollection
    {
        IStripMenuItem this[int index] { get; }
        IStripMenuItem this[string name] { get; }

        object Control { get; }

        void Add(IStripMenuItem item);
        void Add(params IStripMenuItem[] items);
        void Remove(IStripMenuItem item);
        void RemoveAt(int index);
    }
}