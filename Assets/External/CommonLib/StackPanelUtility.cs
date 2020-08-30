namespace CommonLib
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class StackPanelUtility
    {
        private static readonly Stack<StackPanelData> Stack = new Stack<StackPanelData>();
        private static int depth;
        
        static StackPanelUtility()
        {
            StackPanelUtility.Initialize();
        }

        public static Axis Orientation
        {
            get { return StackPanelUtility.Current.Orientation; }
        }

        public static Rect Area
        {
            get { return StackPanelUtility.Current.Area; }
        }

        private static bool Initialized
        {
            get { return StackPanelUtility.depth > 0; }
        }

        private static StackPanelData Current
        {
            get { return StackPanelUtility.Stack.Peek(); }
        }
        
        public static void Initialize()
        {
            StackPanelUtility.Stack.Clear();
            StackPanelUtility.depth = 0;
        }

        public static void Begin(Rect area, Axis direction, bool relativePosition = true)
        {
            StackPanelUtility.depth++;

            var adjustedRect = new Rect(0.0f, 0.0f, area.width, area.height);

            if (!relativePosition)
            {
                adjustedRect.x = area.x;
                adjustedRect.y = area.y;
            }

            StackPanelUtility.Stack.Push(
                new StackPanelData(
                    direction,
                    adjustedRect));

            UnityEngine.GUI.BeginClip(area);
        }

        public static void End()
        {
            StackPanelUtility.depth--;

            if (StackPanelUtility.Stack.Count > 0)
            {
                StackPanelUtility.Stack.Pop();
            }

            UnityEngine.GUI.EndClip();
        }

        public static Rect Reserve(float size, Rect area)
        {
            if (!StackPanelUtility.Initialized)
            {
                Debug.LogError("StackPanel.Reserve must be called between StackPanel.Begin and StackPanel.End");
                Debug.LogFormat("StackPanel current depth is {0}", StackPanelUtility.depth);
                return area;
            }

            StackPanelData current = StackPanelUtility.Current;

            if (current.Orientation == Axis.Vertical)
            {
                //float height = Mathf.Clamp(size, 0.0f, StackPanel.position.height);
                float height = size;
                area.Set(current.Area.xMin, current.Area.yMin, current.Area.width, height);
                current.Area.yMin += height;
            }
            else
            {
                //float width = Mathf.Clamp(size, 0.0f, StackPanel.position.width);
                float width = size;
                area.Set(current.Area.xMin, current.Area.yMin, width, current.Area.height);
                current.Area.xMin += width;
            }

            return area;
        }

        private class StackPanelData
        {
            public StackPanelData(Axis orientation, Rect area)
            {
                this.Orientation = orientation;
                this.Area = area;
            }

            public readonly Axis Orientation;

            public Rect Area;
        }
    }
}
