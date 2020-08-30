namespace CommonLib
{
    using System.Collections.Generic;
    using CommonLib.Behaviours;
    using UnityEngine;

    public static class Vector3Ex
    {
        public static Vector3 xz = new Vector3(1.0f, 0.0f, 1.0f);

        public static bool IsApproximately(this Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) &&
                Mathf.Approximately(a.y, b.y) &&
                Mathf.Approximately(a.z, b.z);
        }

        public static Vector2 ToXZVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        public static Vector3 ToXZPlane(this Vector3 vector)
        {
            vector.y = 0.0f;
            return vector;
        }

        public static Vector3 Clamp(Vector3 vector, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z));
        }

        public static float MaxDistanceBetweenPoints(Vector3[] points)
        {
            return Vector3Ex.MaxVectorBetweenPoints(points).magnitude;
        }

        public static Vector3 MaxVectorBetweenPoints(Vector3[] points)
        {
            Vector3 maxVector = Vector3.zero;
            var maxDistance = 0.0f;

            foreach (Vector3 pointA in points)
            {
                foreach (Vector3 pointB in points)
                {
                    Vector3 vector = pointA - pointB;
                    float distance = vector.sqrMagnitude;

                    if (distance > maxDistance)
                    {
                        maxVector = vector;
                        maxDistance = distance;
                    }
                }
            }

            return maxVector;
        }

        public static Vector3 GetClosestPoint(Vector3 point, Vector3[] points)
        {
            Vector3 closestPoint = Vector3.zero;
            float minDistance = float.MaxValue;

            foreach (Vector3 pointA in points)
            {
                Vector3 vector = pointA - point;
                float distance = vector.sqrMagnitude;

                if (distance < minDistance)
                {
                    closestPoint = pointA;
                    minDistance = distance;
                }
            }

            return closestPoint;
        }

        public static void GetMinMax(IEnumerable<Vector3> points, out Vector3 min, out Vector3 max)
        {
            min = Vector3.one * float.MaxValue;
            max = Vector3.one * float.MinValue;

            foreach (Vector3 point in points)
            {
                if (point.x < min.x) min.x = point.x;
                if (point.x > max.x) max.x = point.x;
                if (point.y < min.y) min.y = point.y;
                if (point.y > max.y) max.y = point.y;
                if (point.z < min.z) min.z = point.z;
                if (point.z > max.z) max.z = point.z;
            }
        }

        public static void FindFurthestPoints(Vector3[] points, out Vector3 furthestPointA, out Vector3 furthestPointB)
        {
            var maxDistance = 0.0f;

            furthestPointA = Vector3.zero;
            furthestPointB = Vector3.zero;

            foreach (Vector3 pointA in points)
            {
                foreach (Vector3 pointB in points)
                {
                    float distance = (pointA - pointB).sqrMagnitude;

                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        furthestPointA = pointA;
                        furthestPointB = pointB;
                    }
                }
            }
        }

        public static Vector3 Midpoint(Vector3 pointA, Vector3 pointB)
        {
            return (pointA + pointB)*0.5f;
        }

        public static Vector3 Abs(this Vector3 vector3)
        {
            return new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
        }

        public static Vector3 Normal(this Vector3 vector, bool flip = false)
        {
            return flip
                ? new Vector3(vector.z, vector.y, -vector.x).normalized
                : new Vector3(-vector.z, vector.y, vector.x).normalized;
        }

        public static float Sum(this Vector3 vector)
        {
            return vector.x + vector.y + vector.z;
        }

        public static Vector3 SmoothTravel(this Vector3[] points, float t)
        {
            return CatmullRomSpline.GetPoint(points, t, false);
        }

        public static Vector3 Multiply(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 Divide(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector3 Average(this IEnumerable<Vector3> vectors)
        {
            Vector3 total = Vector3.zero;
            var count = 0;
            foreach (Vector3 vector in vectors)
            {
                total += vector;
                count++;
            }
            return total / (float)count;
        }

        /// <summary>
        /// Returns a scalar [-PI, PI] where a value of PI means <paramref name="vector"/>
        /// is pointing the exact same direction as <paramref name="facingVector"/> and a
        /// value of -PI means <paramref name="vector"/> is pointing the exact opposite
        /// direction of <paramref name="facingVector"/>.
        /// </summary>
        public static float GetFacingValue(Vector3 facingVector, Vector3 vector)
        {
            return Vector3.Dot(facingVector.normalized, vector.normalized);
        }

        /// <summary>
        /// Returns a scalar [-1.0, 1.0] where a value of 1.0 means <paramref name="vector"/>
        /// is pointing the exact same direction as <paramref name="facingVector"/> and a
        /// value of -1.0 means <paramref name="vector"/> is pointing the exact opposite
        /// direction of <paramref name="facingVector"/>.
        /// </summary>
        public static float GetFacingScalar(Vector3 facingVector, Vector3 vector)
        {
            return Mathf.Acos(Vector3Ex.GetFacingValue(facingVector, vector)) / Mathf.PI;
        }
    }
}
