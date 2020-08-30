namespace CommonLib.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OneToManyDictionary<TKey, TValue, TValueCollection> : Dictionary<TKey, TValueCollection>
        where TValueCollection : ICollection<TValue>, new()
    {
        public void Add(TKey key, TValue value)
        {
            TValueCollection collection = this.GetOrAddCollection(key);
            collection.Add(value);
        }

        public void Remove(TKey key, TValue value)
        {
            TValueCollection collection;
            if (this.TryGetValue(key, out collection))
            {
                collection.Remove(value);
            }
        }

        public void Clear(TKey key)
        {
            TValueCollection collection;
            if (this.TryGetValue(key, out collection))
            {
                collection.Clear();
            }
        }

        public bool ContainsValue(TKey key, TValue value)
        {
            bool containsValue = false;
            TValueCollection collection;
            if (this.TryGetValue(key, out collection))
            {
                containsValue = collection.Contains(value);
            }
            return containsValue;
        }

        public TValueCollection GetOrAddCollection(TKey key)
        {
            TValueCollection collection;

            if (!this.TryGetValue(key, out collection))
            {
                collection = new TValueCollection();
                this.Add(key, collection);
            }

            return collection;
        }

        public void RemoveFromAll(TValue trophy)
        {
            foreach (TValueCollection collection in this.Values)
            {
                collection.Remove(trophy);
            }
        }

        public TValue FirstOrDefault(TKey key, Func<TValue, bool> predicate)
        {
            TValueCollection collection;
            if (this.TryGetValue(key, out collection))
            {
                return collection.FirstOrDefault(predicate);
            }
            return default(TValue);
        }
    }

    public class OneToManyDictionary<TKey, TValue> : OneToManyDictionary<TKey, TValue, List<TValue>>
    {
    }
}
