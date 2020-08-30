namespace CommonLib
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// DockSides are evaluated in this order:
    ///                                      
    ///          +-----------+   1. Bottom   
    ///          |     2     |               
    ///          |-----------|   2. Top      
    ///          |   |   |   |               
    ///          | 3 | 5 | 4 |   3. Left     
    ///          |   |   |   |               
    ///          |-----------|   4. Right    
    ///          |     1     |               
    ///          +-----------+   5. Fill     
    ///                                      
    public static class DockUtility
    {
        /// <summary>
        /// A stack of <see cref="DockRect"/> objects.
        /// </summary>
        private static Stack<DockRect> dockStack;

        private static int depth;

        static DockUtility()
        {
            DockUtility.Initialize();
        }

        private static bool Initialized
        {
            get { return DockUtility.depth > 0; }
        }

        public static float Bottom
        {
            set
            {
                if (!DockUtility.Initialized)
                {
                    return;
                }
                DockUtility.Update(DockSide.Bottom, value);
            }
        }

        public static float Top
        {
            set
            {
                if (!DockUtility.Initialized)
                {
                    return;
                }
                DockUtility.Update(DockSide.Top, value);
            }
        }

        public static float Left
        {
            set
            {
                if (!DockUtility.Initialized)
                {
                    return;
                }
                DockUtility.Update(DockSide.Left, value);
            }
        }

        public static float Right
        {
            set
            {
                if (!DockUtility.Initialized)
                {
                    return;
                }
                DockUtility.Update(DockSide.Right, value);
            }
        }

        public static Rect BottomRect
        {
            get { return DockUtility.Current.Bottom; }
        }

        public static Rect TopRect
        {
            get { return DockUtility.Current.Top; }
        }

        public static Rect LeftRect
        {
            get { return DockUtility.Current.Left; }
        }

        public static Rect RightRect
        {
            get { return DockUtility.Current.Right; }
        }

        public static Rect FillRect
        {
            get { return DockUtility.Current.Fill; }
        }

        private static DockRect Current
        {
            get
            {
                return DockUtility.dockStack.Peek();
            }
        }

        public static void Initialize()
        {
            DockUtility.dockStack = new Stack<DockRect>();
            DockUtility.depth = 0;
        }

        public static void Begin(Rect area)
        {
            DockUtility.depth++;

            DockUtility.dockStack.Push(new DockRect
            {
                Fill = new Rect(0.0f, 0.0f, area.width, area.height)
            });

            UnityEngine.GUI.BeginClip(area);
        }

        public static void End()
        {
            DockUtility.depth--;

            if (DockUtility.dockStack.Count > 0)
            {
                DockUtility.dockStack.Pop();
            }

            UnityEngine.GUI.EndClip();
        }

        private static void Update(DockSide panel, float requested)
        {
            float size = requested;

            DockRect dockRect = DockUtility.dockStack.Peek();

            switch (panel)
            {
                case DockSide.Bottom:
                case DockSide.Top:
                    size = (int) Mathf.Clamp(size, 0, dockRect.Fill.height);
                    break;
                case DockSide.Left:
                case DockSide.Right:
                    size = (int) Mathf.Clamp(size, 0, dockRect.Fill.width);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("panel", panel, null);
            }

            switch (panel)
            {
                case DockSide.Bottom:
                    dockRect.Bottom = new Rect(dockRect.Fill.xMin, dockRect.Fill.yMax - size, dockRect.Fill.width, size);
                    dockRect.Fill.height -= size;
                    break;
                case DockSide.Top:
                    dockRect.Top = new Rect(dockRect.Fill.xMin, dockRect.Fill.yMin, dockRect.Fill.width, size);
                    dockRect.Fill.height -= size;
                    dockRect.Fill.y += size;
                    break;
                case DockSide.Left:
                    dockRect.Left = new Rect(dockRect.Fill.xMin, dockRect.Fill.yMin, size, dockRect.Fill.height);
                    dockRect.Fill.width -= size;
                    dockRect.Fill.x += size;
                    break;
                case DockSide.Right:
                    dockRect.Right = new Rect(dockRect.Fill.xMax - size, dockRect.Fill.yMin, size, dockRect.Fill.height);
                    dockRect.Fill.width -= size;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("panel", panel, null);
            }
        }

        private class DockRect
        {
            public Rect Bottom;
            public Rect Top;
            public Rect Left;
            public Rect Right;
            public Rect Fill;
        }
    }
}