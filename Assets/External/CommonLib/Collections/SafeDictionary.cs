namespace CommonLib.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Doesn't save the values but ensures that the Dictonary object
    /// exists before operating on it. The SafeDictionary class is serializable
    /// so that it does not get set to null when serialized.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        [NonSerialized]
        private IDictionary<TKey, TValue> dictionary;
        
        public TValue this[TKey key]
        {
            get
            {
                TValue temp;
                this.TryGetValue(key, out temp);
                return temp;
            }

            set
            {
                this.VerifiedDictionary[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get { return this.VerifiedDictionary.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return this.VerifiedDictionary.Values; }
        }

        public int Count
        {
            get { return this.VerifiedDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return this.VerifiedDictionary.IsReadOnly; }
        }

        private IDictionary<TKey, TValue> VerifiedDictionary
        {
            get
            {
                if (this.dictionary == null)
                {
                    this.dictionary = new Dictionary<TKey, TValue>();
                }

                return this.dictionary;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.VerifiedDictionary.TryGetValue(key, out value);
        }

        public void Add(TKey key, TValue value)
        {
            this.VerifiedDictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return this.VerifiedDictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return this.VerifiedDictionary.Remove(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.VerifiedDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.VerifiedDictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.VerifiedDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.VerifiedDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.VerifiedDictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.VerifiedDictionary.Remove(item);
        }
    }
}