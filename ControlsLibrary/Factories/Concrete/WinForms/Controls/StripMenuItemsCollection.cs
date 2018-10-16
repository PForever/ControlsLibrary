using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;
using ControlsLibrary.Containers;

namespace ControlsLibrary.Factories.Concrete.WinForms.Controls
{
    public class StripMenuItemsCollection : IStripMenuItemsCollection
    {
        public object Control => Owner;
        private MenuStrip Owner { get; }
        private readonly IDictionary<string, IStripMenuItem> _items;
        public StripMenuItemsCollection(MenuStrip stripMenu, IFactory _factory)
        {
            Owner = stripMenu;
            _items = _factory.CreateStripMenuItems(stripMenu.Items);
        }

        public IStripMenuItem this[int index] => _items.ElementAt(index).Value;

        public IStripMenuItem this[string name] => _items[name];

        public void Add(IStripMenuItem item)
        {
            _items.Add(item.Text, item);
            Owner.Items.Add((ToolStripItem)item.Control);
        }

        public void AddRange(IEnumerable<IStripMenuItem> items)
        {
            foreach (IStripMenuItem item in items)
            {
                Add(item);
            }
        }

        public void Remove(IStripMenuItem item)
        {
            _items.Remove(item.Text);
            Owner.Items.Remove((ToolStripItem)item.Control);
        }

        public void RemoveAt(int index)
        {
            Remove(this[index]);
        }
    }
}