using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;

namespace ControlsLibrary.Factories.Concrete.WinForms.Controls.TabForm
{
    public class ItemsStripMenuItemsCollection : IStripMenuItemsCollection
    {
        public object Control => Owner;
        private ToolStripMenuItem Owner { get; }
        private readonly IDictionary<string, IStripMenuItem> _items;
        public ItemsStripMenuItemsCollection(ToolStripMenuItem stripMenu, IFactory _factory)
        {
            Owner = stripMenu;
            _items = _factory.CreateStripMenuItems(stripMenu.DropDownItems);
        }

        public IStripMenuItem this[int index] => _items.ElementAt(index).Value;

        public IStripMenuItem this[string name] => _items[name];

        public void Add(IStripMenuItem item)
        {
            _items.Add(item.Text, item);
            Owner.DropDownItems.Add((ToolStripItem)item.Control);
        }

        public void Add(params IStripMenuItem[] items)
        {
            foreach (IStripMenuItem item in items)
            {
                Add(item);
            }
        }

        public void Remove(IStripMenuItem item)
        {
            _items.Remove(item.Name);
            Owner.DropDownItems.Remove((ToolStripItem)item.Control);
        }

        public void RemoveAt(int index)
        {
            Remove(this[index]);
        }
    }
}