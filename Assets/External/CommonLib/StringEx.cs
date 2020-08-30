namespace CommonLib
{
    using System;
    using UnityEngine;

    public static class StringEx
    {
        public static string Remove(this string @this, string substring)
        {
            return @this.Remove(substring, 0, StringComparison.Ordinal);
        }

        public static string Remove(this string @this, string substring, int startIndex, StringComparison comparison)
        {
            int index;
            string temp = @this;

            while ((index = temp.IndexOf(substring, startIndex, comparison)) != -1)
            {
                temp = temp.Remove(index, substring.Length);
            }

            return temp;
        }

        public static string Capitolized(this string @this)
        {
            return Char.ToUpper(@this[0]) + @this.Substring(1, @this.Length - 1);
        }

        public static string Trim(this string str, int fromStart, int fromEnd)
        {
            return str.Substring(fromStart, (str.Length - 1) - fromEnd);
        }

        public static string Trim(this string str, int fromBoth)
        {
            return str.Trim(fromBoth, fromBoth);
        }

        public static string Until(this string str, string terminator)
        {
            int index = str.IndexOf(terminator, StringComparison.CurrentCulture);
            return index == -1 ? str : str.Substring(0, index);
        }

        public static string Stringify(object item)
        {
            return item != null ? item.ToString() : string.Empty;
        }

        public static string Stringify(GUIContent item)
        {
            return item != null ? item.text : string.Empty;
        }
    }
}
