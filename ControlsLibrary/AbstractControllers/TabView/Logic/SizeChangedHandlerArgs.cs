using System.Drawing;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    public struct SizeChangedHandlerArgs
    {
        public SizeChangedHandlerArgs(Size newSize, Size oldSize) : this()
        {
            NewSize = newSize;
            OldSize = oldSize;
        }

        public Size NewSize { get; set; }
        public Size OldSize { get; set; }
    }
}