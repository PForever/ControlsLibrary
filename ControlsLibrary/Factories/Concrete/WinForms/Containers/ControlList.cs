using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;

namespace ControlsLibrary.Factories.Concrete.WinForms.Containers
{
    public class ControlList : IControlList
    {
        private WinFactory _factory;
        public Control.ControlCollection Controls;
        object IControlList.Controls { get => Controls; set => Controls = (Control.ControlCollection) value; }
        public IList<IControl> Collection;

        protected ControlList()
        {
        }

        public ControlList(Control.ControlCollection controls) : this(controls, new WinFactory())
        {
        }

        public ControlList(Control.ControlCollection controls, WinFactory factory)
        {
            _factory = factory;
            Controls = controls;
            Collection = Controls.Cast<IControl>().ToList();
        }
        public IEnumerator<IControl> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            Controls.Add((Control)item.Control);
            Collection.Add(_factory.CreateControl(item));
        }

        public void Clear()
        {
            Controls.Clear();
            Collection.Clear();
        }

        public bool Contains(IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            return Collection.Contains(item);
        }

        public void CopyTo(IControl[] array, int arrayIndex)
        {
            Collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            if (!Collection.Contains(item)) return false;
            Controls.Remove((Control) item.Control);
            Collection.Remove(item);
            return true;
        }

        public int Count => Collection.Count;
        public bool IsReadOnly => Collection.IsReadOnly;
        public int IndexOf(IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            return Collection.IndexOf(item);
        }

        public void Insert(int index, IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            Insert(index, (Control) item.Control);
            Collection.Insert(index, item);
        }

        private void Insert(int index, Control control)
        {
            Controls.Add(control);
            for (int i = Controls.Count - 1; i > index; i--)
            {
                Controls.SetChildIndex(Controls[i], i - 1);
            }
        }
        public void RemoveAt(int index)
        {
            Controls.RemoveAt(index);
            Collection.RemoveAt(index);
        }

        public IControl this[int index]
        {
            get => Collection[index];
            set
            {
                Controls.RemoveAt(index);
                Insert(index, (Control) value.Control);
                Collection[index] = value;
            }
        }
    }
}