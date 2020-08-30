namespace CommonLib
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static bool TryGetValueSafe<TValue>(this IList<TValue> array, int index, out TValue value)
        {
            ThrowIf.ArgumentIsNull(array, "array");

            if (index < 0 || index >= array.Count)
            {
                value = default(TValue);
                return false;
            }

            value = array[index];
            return true;
        }
    }
}
