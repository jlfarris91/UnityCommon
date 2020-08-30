namespace CommonLib.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class FixedStack<T> : IEnumerable<T>, IObservableCollection
    {
        [SerializeField]
        private T[] array;

        [SerializeField]
        private int start;

        [SerializeField]
        private int write;

        [SerializeField]
        private int count;

        public int Capacity { get { return this.array.Length; } }
        public int Count { get { return this.count; } }

        public T this[int index]
        {
            get
            {
                int offset = this.start + index;
                return (offset > this.Capacity - 1) ? this.array[offset - this.Capacity] : this.array[offset];
            }
        }

        public FixedStack()
            : this(10)
        {
        }

        public FixedStack(int capacity)
        {
            this.array = new T[capacity];
            this.count = 0;
        }

        public T Peek()
        {
            return (this.write == 0) ? this.array[this.Capacity - 1] : this.array[this.write - 1];
        }

        public void Push(T item)
        {
            this.count = Math.Min(++this.count, this.Capacity);

            if (this.Count == this.Capacity)
            {
                this.start = (this.start == this.Capacity - 1) ? 0 : ++this.start;
            }

            this.array[this.write] = item;
            this.write = (this.write == this.Capacity - 1) ? 0 : ++this.write;

            this.RaiseCollectionChangedEvent();
        }

        public T Pop()
        {
            if (this.write == this.start)
            {
                return default(T);
            }

            --this.count;
            this.write = (this.write == 0) ? this.Capacity - 1 : --this.write;
            T item = this.array[this.write];
            this.array[this.write] = default(T);

            this.RaiseCollectionChangedEvent();

            return item;
        }

        public void Clear()
        {
            for (var i = 0; i < this.Capacity; ++i)
            {
                this.array[i] = default(T);
            }

            this.start = 0;
            this.write = 0;
            this.count = 0;

            this.RaiseCollectionChangedEvent();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) this.array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public event CollectionChangedEventHandler CollectionChanged;

        private void RaiseCollectionChangedEvent()
        {
            CollectionChangedEventHandler handler = this.CollectionChanged;
            if (handler != null)
            {
                handler(this, CollectionChangedEventArgs.Reset);
            }
        }
    }
}
