namespace CommonLib
{
    using System;

    public static class BitField
    {
        public static bool HasFlag(this int field, int flag)
        {
            return (field & flag) == flag;
        }

        public static bool HasFlag<T>(this T field, T value) where T : struct, IFormattable, IConvertible, IComparable
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            int fieldAsInt = Convert.ToInt32(field);
            int valueAsint = Convert.ToInt32(value);

            return BitField.HasFlag(fieldAsInt, valueAsint);
        }

        public static int SetFlag(this int field, int value)
        {
            return field | value;
        }

        public static int SetIndex(int field, int index)
        {
            return field | (1 << index);
        }

        public static T SetFlag<T>(this T field, T value) where T : struct, IFormattable, IConvertible, IComparable
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            int fieldAsInt = Convert.ToInt32(field);
            int valueAsint = Convert.ToInt32(value);

            int resultAsInt = fieldAsInt.SetFlag(valueAsint);

            return (T)Convert.ChangeType(resultAsInt, Enum.GetUnderlyingType(typeof(T)));
        }

        public static T ClearFlag<T>(this T field, T value) where T : struct, IFormattable, IConvertible, IComparable
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            int fieldAsInt = Convert.ToInt32(field);
            int valueAsint = Convert.ToInt32(value);

            int resultAsInt = fieldAsInt.ClearFlag(valueAsint);

            return (T)Convert.ChangeType(resultAsInt, Enum.GetUnderlyingType(typeof(T)));
        }

        public static int ClearFlags(this int field, int value)
        {
            return field & ~value;
        }

        public static int ClearIndex(int field, int index)
        {
            return field & ~(1 << index);
        }

        public static int ClearFlag(this int field, int value)
        {
            return field & ~value;
        }

        public static T ClearFlags<T>(this T field, T value) where T : struct, IFormattable, IConvertible, IComparable
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            int fieldAsInt = Convert.ToInt32(field);
            int valueAsint = Convert.ToInt32(value);

            int resultAsInt = BitField.ClearFlags(fieldAsInt, valueAsint);

            return (T)Convert.ChangeType(resultAsInt, Enum.GetUnderlyingType(typeof(T)));
        }
    }
}