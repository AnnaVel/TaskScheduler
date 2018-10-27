using System;
using System.Collections.Generic;
using System.Text;

namespace TaskSchedulerCore.Utilities
{
    public class CollectionChangedEventArgs<T> : EventArgs
    {
        public CollectionChangedType CollectionChangedType { get; set; }

        public T ChangedElement { get; set; }
    }
}
