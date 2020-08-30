#if UNITY_EDITOR

namespace CommonLib
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public static class HandlesEx
    {
        private static Stack<Color> colorStack = new Stack<Color>();

        public static void PushColor(Color color)
        {
            if (HandlesEx.colorStack.Count == 0)
            {
                HandlesEx.colorStack.Push(Gizmos.color);
            }

            HandlesEx.colorStack.Push(color);
            Handles.color = color;
        }

        public static void PopColor()
        {
            if (HandlesEx.colorStack.Count > 1)
            {
                HandlesEx.colorStack.Pop();
            }

            Color color = Color.red;

            if (HandlesEx.colorStack.Count > 0)
            {
                color = HandlesEx.colorStack.Peek();
            }

            Handles.color = color;
        }
    }
}

#endif