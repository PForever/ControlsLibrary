using System.Collections.Generic;
using ControlsLibrary.AbstractControllers.TabForms.TabView;
using ControlsLibrary.AbstractControllers.TabForms.TabView.Tab;

namespace ControlsLibrary.AbstractControllers.TabForms
{
    public interface ITabWindow : IControl
    {
        ITabView Container { get; }
        ITabWindow Parent { get; set; }
        ICollection<ITabWindow> Children { get; }
        IStripMenu StripMenu { get; set; }
        void Open();
        void Close();
        void ComebackToParent();
        void JoinChildren();
        void Join(ITabWindow child, IEnumerable<ITabPanel> childsTabs);
    }
}