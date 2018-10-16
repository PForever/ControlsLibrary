using System;

namespace ControlsLibrary.AbstractControllers.TabForms.TabView.Tab.Events
{
    internal class PropertyChangedEventArgs<T> : EventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }

        public PropertyChangedEventArgs(T oldValue, T newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}