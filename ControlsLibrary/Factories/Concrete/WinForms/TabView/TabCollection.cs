using ControlsLibrary.AbstractControllers;
using ControlsLibrary.AbstractControllers.TabView;
using ControlsLibrary.AbstractControllers.TabView.Tab;
using ControlsLibrary.AbstractControllers.TabView.Tab.Events;
using ControlsLibrary.Containers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary.Factories.Concrete.WinForms.TabView
{
    abstract class ATabCollection : ITabCollection
    {
        const int TabsTreshold = 5;
        private bool disposedValue = false; // To detect redundant calls

        public abstract int MaxTabWidth { get; }
        public abstract int CurrentTabWidth { get; set; }
        public abstract Orientation Orientation { get; set; }
        
        public abstract IList<ITabPanel> Childs { get; set; } 
        IList<IControl> IContainer.Childs { get => Childs.ToList<IControl>(); set => Childs = value.Cast<ITabPanel>().ToList(); }
        public abstract string Name { get; set; }
        public abstract Point Location { get; set; }
        public abstract bool Visible { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public int Count => Childs.Count;
        public abstract bool IsReadOnly { get; }

        ITabPanel IList<ITabPanel>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public void OnTabDeleted(object sender, TabDeletedEventArgs args)
        {
            //TODO возможно требует оптимизации
            int index = Childs.IndexOf(args.TabPanel);
            Childs.RemoveAt(index);
            Sarfacing(index, Childs.Count - 1, -CalcWidth());
        }
        void Sarfacing(int from, int to, int width)
        {
            for (int i = from; i <= to; i++)
            {
                ChangeLocation(Childs[i], width);
            }
        }
        int CalcWidth()
        {
            if(Childs.Count < TabsTreshold) return MaxTabWidth;
            CurrentTabWidth = (int) (Width / (double)Childs.Count);
            Render();
            return CurrentTabWidth;
        }
        protected abstract void Render();
        protected abstract void Render(int index);

        protected abstract void ChangeLocation(IControl control, int width);

        public void OnTabMoved(object sender, TabMovedEventArgs args)
        {
            double position = Orientation == Orientation.Vertical ? args.RequesLocation.Y : args.RequesLocation.X;
            int theMostRightPosition = Width - MaxTabWidth;
            //проверить как работает маус мув для фоормы
            if (position < 0) position = 0;
            if (position > theMostRightPosition) position = theMostRightPosition;
            int index = CalcIndexFromPosition(position);
            int oldIndex = Childs.IndexOf(args.TabPanel);
            SwitchCollectionPositions(oldIndex, index);
            if (oldIndex < index)
                 Sarfacing(oldIndex, index, -CalcWidth());
            else Sarfacing(index, oldIndex, CalcWidth());
        }
        void SwitchCollectionPositions(int oldindex, int index)
        {
            ITabPanel temp = Childs[oldindex];
            Childs[oldindex] = Childs[index];
            Childs[index] = temp;
        }
        int CalcIndexFromPosition(double position)
        {
            return (int)Math.Round(position / Width);
        }
        public void OnTabSelected(object sender, TabSelectedEventArgs args)
        {
            throw new NotImplementedException();
        }
        public abstract void InitializeComponent();

        public int IndexOf(ITabPanel item) => Childs.IndexOf(item);
        public void Insert(int index, ITabPanel item)
        {
            Childs.Insert(index, item);
            Sarfacing(Childs.Count - 1, index + 1, CalcWidth());
            item.Select();
        }
        public void Add(ITabPanel item)
        {

        }
        public abstract bool Contains(ITabPanel item);
        public abstract void CopyTo(ITabPanel[] array, int arrayIndex);

        public abstract bool Remove(ITabPanel item);
        public abstract void RemoveAt(int index);
        public abstract void Clear();

        public abstract IEnumerator GetEnumerator();
        IEnumerator<ITabPanel> IEnumerable<ITabPanel>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TabCollection() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
