using System;
using System.Windows.Forms;
using ControlsLibrary.Factories.Concrete.WinForms.Controls;

namespace ControlsLibrary.AbstractControllers
{
    public interface IStripMenuItem
    {
        object Control { get; }
        string Name { get; set; }
        string Text { get; set; }
        Keys ShortcutKeys { get; set; }
        IStripMenuItemsCollection InnerTools { get; }
        event EventHandler Clocked;
    }
}