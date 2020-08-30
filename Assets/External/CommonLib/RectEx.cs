namespace CommonLib
{
    using System.Collections.Generic;
    using UnityEngine;

    internal static class RectEx
    {
        public static Rect ScaleS(this Rect rect, float scale)
        {
            return rect.Scale(scale, rect.center);
        }

        public static Rect Scale(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }

        public static Rect Scale(this Rect rect, Vector2 scale)
        {
            return rect.Scale(scale, rect.center);
        }

        public static Rect Scale(this Rect rect, Vector2 scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale.x;
            result.xMax *= scale.x;
            result.yMin *= scale.y;
            result.yMax *= scale.y;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }

        public static Rect Expand(this Rect rect, Rect include)
        {
            rect.xMin = Mathf.Min(rect.xMin, include.xMin);
            rect.xMax = Mathf.Max(rect.xMax, include.xMax);
            rect.yMin = Mathf.Min(rect.yMin, include.yMin);
            rect.yMax = Mathf.Min(rect.yMax, include.yMax);
            return rect;
        }
        
        public static Rect Expand(this Rect rect, float x, float y)
        {
            return new Rect(rect.x - x/2, rect.y - y/2, rect.width + x, rect.height + y);
        }
        
        public static Rect FromPoints(IEnumerable<Vector2> points)
        {
            Vector2 min;
            Vector2 max;
            Vector2Ex.GetMinMax(points, out min, out max);

            return new Rect(
                min.x,
                min.y,
                Mathf.Abs(max.x - min.x),
                Mathf.Abs(max.y - min.y));
        }

        public static Rect FromMinMax(Vector3 min, Vector3 max)
        {
            return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
        }
    }
}
