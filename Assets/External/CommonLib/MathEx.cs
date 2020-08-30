namespace CommonLib
{
    using CommonLib;
    using UnityEngine;

    public static class MathEx
    {
        public static float ClampAngle(float angle, float from, float to)
        {
            if (angle > 180) angle = 360 - angle;
            angle = Mathf.Clamp(angle, from, to);
            if (angle < 0) angle = 360 + angle;

            return angle;
        }

        /// <summary>
        /// Returns the angle of a vector in degrees.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static float GetAngle(Vector2 dir)
        {
            var angle = Mathf.Atan2(dir.y, dir.x);
            if (angle < 0.0f)
            {
                angle = 2 * Mathf.PI + angle;
            }
            return angle * Mathf.Rad2Deg;
        }

        public static float WrapAngle(float angle)
        {
            while (angle < 0.0f || angle > 360.0f) angle %= 360.0f;
            return angle;
        }

        public static float GetPositiveDeltaAngle(float from, float to)
        {
            var delta = to - from;
            if (delta < 0.0f)
            {
                delta += 360.0f;
            }
            return delta;
        }

        public static Vector3 SnapToGrid(Vector3 pos, float gridSize)
        {
            pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
            pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
            pos.z = Mathf.Round(pos.z / gridSize) * gridSize;
            return pos;
        }

        public static Vector3 SnapToGrid(Vector3 pos, Vector3 gridSize)
        {
            if (gridSize.x != 0.0f)
            {
                pos.x = Mathf.Round(pos.x/gridSize.x)*gridSize.x;
            }
            if (gridSize.y != 0.0f)
            {
                pos.y = Mathf.Round(pos.y/gridSize.y)*gridSize.y;
            }
            if (gridSize.z != 0.0f)
            {
                pos.z = Mathf.Round(pos.z/gridSize.z)*gridSize.z;
            }
            return pos;
        }

        public static float Wrap(float value, float min, float max)
        {
            return (value < min) ? max : (value > max) ? min : value;
        }

        public static float WrapOverflow(float value, float min, float max)
        {
            while (value > max)
            {
                value -= max - min;
            }

            while (value < min)
            {
                value += max - min;
            }

            return value;
        }

        /// <summary>
        /// Wraps a value to max if it is less than min and min if it is greater than max.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min">The minimum value. Inclusive.</param>
        /// <param name="max">The maximum value. Inclusive.</param>
        public static int Wrap(int value, int min, int max)
        {
            return (value < min) ? max : (value > max) ? min : value;
        }

        public static int WrapOverflow(int value, int min, int max)
        {
            return (value < min) ? max - (min - value - 1) : (value > max) ? min + (value - max - 1) : value;
        }

        public static Vector2 AsRadial(this Vector2 vector)
        {
            return AsRadial(vector, 1.0f);
        }

        public static Vector2 AsRadial(this Vector2 vector, float radius)
        {
            float mag = Mathf.Min(radius, vector.sqrMagnitude);
            float angle = MathEx.GetAngle(vector) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * mag;
        }

        public static float Furthest(float a, float b, float from = 0.0f)
        {
            float distA = Mathf.Abs(a - from);
            float distB = Mathf.Abs(b - from);
            return distA >= distB ? distA : distB;
        }

        /// <summary>
        /// Returns true if the <paramref name="value"/> is between <paramref name="min"/>
        /// and <paramref name="max"/>, exclusively.
        /// Use <see cref="IsBetweenInclusive(float, float, float)"/> to compare inclusively.
        /// </summary>
        public static bool IsBetween(this float value, float min, float max)
        {
            return value > min && value < max;
        }

        /// <summary>
        /// Returns true if the <paramref name="value"/> is between <paramref name="min"/>
        /// and <paramref name="max"/>, exclusively.
        /// Use <see cref="IsBetweenInclusive(int, int, int)"/> to compare inclusively.
        /// </summary>
        public static bool IsBetween(this int value, int min, int max)
        {
            return value > min && value < max;
        }

        /// <summary>
        /// Returns true if the <paramref name="value"/> is between <paramref name="min"/>
        /// and <paramref name="max"/>, inclusively.
        /// </summary>
        public static bool IsBetweenInclusive(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Returns true if the <paramref name="value"/> is between <paramref name="min"/>
        /// and <paramref name="max"/>, inclusively.
        /// </summary>
        public static bool IsBetweenInclusive(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static Vector2 ClampToCircle(Vector2 center, float radius, Vector2 point)
        {
            Vector2 vector = point - center;

            if (vector.sqrMagnitude > radius*radius)
            {
                point = center + vector.normalized * radius;
            }

            return point;
        }
        
        public static float Mirror(float value, float midpoint)
        {
            return midpoint - (value - midpoint);
        }
        
        public static int Mirror(int value, int midpoint)
        {
            return midpoint - (value - midpoint);
        }

        public static bool IsInGrid(GridCell cell, Int3 extents)
        {
            return cell.x.IsBetweenInclusive(0, extents.x - 1)
                && cell.y.IsBetweenInclusive(0, extents.y - 1)
                && cell.z.IsBetweenInclusive(0, extents.z - 1);
        }

        public static bool IsInGrid(int index, Int3 extents)
        {
            GridCell cell = GridCell.FromIndex(index, extents);
            return cell.IsInGrid(extents);
        }

        public static Vector3 GetClosestPointOnLineSegment(Vector3 a, Vector3 b, Vector3 p)
        {
            Vector3 v1 = p - a;
            Vector3 v2 = (b - a).normalized;

            float d = Vector3.Distance(a, b);
            float t = Vector3.Dot(v2, v1);

            if (t <= 0)
            {
                return a;
            }

            if (t >= d)
            {
                return b;
            }

            return a + v2 * t;
        }

        public static float DistanceFromPointToRay(Ray ray, Vector3 point)
        {
            return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
        }
    }
}