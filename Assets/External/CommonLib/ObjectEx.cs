namespace CommonLib
{
    using UnityEngine;

    public static class ObjectEx
    {
        public static void SafeDestroyImmediate(Object obj)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj, false);
            }
        }

        public static string ToLowerString(this object obj)
        {
            return obj.ToString().ToLower();
        }
    }
}