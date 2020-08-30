namespace CommonLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryEx
    {
        public static IDictionary<TKey, TValue> DeepClone<TKey, TValue>(this IDictionary<TKey, TValue> @this) where TValue : ICloneable
        {
            return @this.ToDictionary(pair => pair.Key, pair => (TValue) pair.Value.Clone());
        }

        public static TValue TryGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key)
        {
            TValue value;
            return @this.TryGetValue(key, out value) ? value : default(TValue);
        }

        public static TValue TryGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this,
                                                                TKey key,
                                                                TValue defaultValue)
        {
            TValue value;
            return @this.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static void TryGetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, out TValue value, Func<TValue> factory)
        {
            if (!@this.TryGetValue(key, out value))
            {
                value = factory();
                @this.Add(key, value);
            }
        }

        public static void TryGetOrAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, out TValue value)
        {
            if (!@this.TryGetValue(key, out value))
            {
                value = default(TValue);
                @this.Add(key, value);
            }
        }
    }
}
