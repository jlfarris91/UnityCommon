namespace CommonLib.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class ObservableCollection<T> : IList<T>, IObservableCollection
    {
        public event CollectionChangedEventHandler CollectionChanged;

        [SerializeField]
        private List<T> items;

        public ObservableCollection()
        {
            this.items = new List<T>();
        }

        public ObservableCollection(IEnumerable<T> initialItems)
        {
            this.items = new List<T>(initialItems);
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public T this[int index]
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }
        
        public virtual void Add(T item)
        {
            this.items.Add(item);
            if (this.CollectionChanged != null)
            {
                var args = new CollectionChangedEventArgs(
                    ChangeAction.Add,
                    new List<object>(new object[] {item}),
                    this.items.Count - 1,
                    null,
                    -1);

                this.CollectionChanged(this, args);
            }
        }

        public virtual bool Remove(T item)
        {
            int index = this.items.IndexOf(item);
            bool result = this.items.Remove(item);

            if (this.CollectionChanged != null)
            {
                var args = new CollectionChangedEventArgs(
                    ChangeAction.Remove,
                    null,
                    -1,
                    new List<object>(new object[] {item}),
                    index);

                this.CollectionChanged(this, args);
            }

            return result;
        }

        public virtual void Clear()
        {
            this.items.Clear();

            if (this.CollectionChanged == null)
            {
                return;
            }

            var args = new CollectionChangedEventArgs(
                ChangeAction.Reset,
                null, 
                -1,
                null, 
                -1);

            this.CollectionChanged(this, args);
        }

        public bool Contains(T item)
        {
            return this.items.Contains(item);
        }

        public int IndexOf(T item)
        {
            return this.items.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            this.items.Insert(index, item);

            if (this.CollectionChanged == null)
            {
                return;

            }
            var args = new CollectionChangedEventArgs(
                ChangeAction.Insert,
                new List<object>(new object[] {item}),
                index,
                null,
                -1);

            this.CollectionChanged(this, args);
        }

        public virtual void RemoveAt(int index)
        {
            T old = this.items[index];
            this.items.RemoveAt(index);

            if (this.CollectionChanged == null)
            {
                return;

            }
            var args = new CollectionChangedEventArgs(
                ChangeAction.Remove,
                null, 
                -1,
                new List<object>(new object[] { old }),
                index);

            this.CollectionChanged(this, args);
        }

        #region ICollection

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        #endregion ICollection

        #region IEnumerator

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion IEnumerator
    }
}
