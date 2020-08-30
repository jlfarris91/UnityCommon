namespace CommonLib.Collections
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();

        [SerializeField] private List<TValue> values = new List<TValue>();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keys.Clear();
            this.values.Clear();

            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                this.keys.Add(pair.Key);
                this.values.Add(pair.Value);
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();

            if (this.keys.Count != this.values.Count)
            {
                throw new Exception(string.Format(
                    "Unable to deserialize dictionary. Key value mismatch ({0} keys and {1} values).", this.keys.Count,
                    this.values.Count));
            }

            for (var i = 0; i < this.keys.Count; i++)
            {
                this.Add(this.keys[i], this.values[i]);
            }
        }
    }
}