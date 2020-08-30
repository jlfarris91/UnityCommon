namespace CommonLib
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class Vector2Ex
    {
        public static Vector2 Abs(this Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }

        public static Vector3 ToXZVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, 0.0f, vector.y);
        }

        public static Vector2 Clamp(Vector2 vector, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y));
        }

        public static void GetMinMax(IEnumerable<Vector2> points, out Vector2 min, out Vector2 max)
        {
            min = Vector2.one * float.MaxValue;
            max = Vector2.one * float.MinValue;

            foreach (Vector2 point in points)
            {
                if (point.x < min.x) min.x = point.x;
                if (point.x > max.x) max.x = point.x;
                if (point.y < min.y) min.y = point.y;
                if (point.y > max.y) max.y = point.y;
            }
        }
    }
}