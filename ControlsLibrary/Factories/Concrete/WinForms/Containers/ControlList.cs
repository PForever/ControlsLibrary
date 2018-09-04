using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ControlsLibrary.AbstractControllers;

namespace ControlsLibrary.Factories.Concrete.WinForms.Containers
{
    public class ControlList : IList<IControl>
    {
        private WinFactory _factory;
        public Control.ControlCollection Collection;
        public IEnumerator<IControl> GetEnumerator()
        {
            foreach (Control control in Collection)
            {
                yield return _factory.CreateControl(control);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            Collection.Add((Control)item.Control);
        }

        public void Clear()
        {
            Collection.Clear();
        }

        public bool Contains(IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            return Collection.Contains((Control) item.Control);
        }

        public void CopyTo(IControl[] array, int arrayIndex)
        {
            Collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(IControl item)
        {
            if(item == null) throw new ArgumentNullException();
            Collection.Remove((Control) item.Control);
            return true;
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(IControl item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, IControl item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public IControl this[int index]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }
    }
}