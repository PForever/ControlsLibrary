using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.Factories.Concrete.WinForms.Controls.TabForm;

namespace ControlsLibrary.Factories.Concrete.WinForms.Controls
{
    public class StripMenuItem : IStripMenuItem
    {
        private readonly ToolStripMenuItem _stripItem;

        public string Text
        {
            get => _stripItem.Text;
            set => _stripItem.Text = value;
        }

        public Keys ShortcutKeys
        {
            get => _stripItem.ShortcutKeys;
            set => _stripItem.ShortcutKeys = value;
        }
        public string Name
        {
            get => _stripItem.Name;
            set => _stripItem.Name = value;
        }

        public IStripMenuItemsCollection InnerTools { get; }
        public event EventHandler Clocked
        {
            add => _stripItem.Click += value;
            remove => _stripItem.Click -= value;
        }

        public StripMenuItem(ToolStripMenuItem toolStripItem, IFactory factory)
        {
            _stripItem = toolStripItem;
            InnerTools = new ItemsStripMenuItemsCollection(toolStripItem, factory);
        }

        public void Dispose()
        {
            _stripItem.Dispose();
        }

        public object Control => _stripItem;
    }
}