namespace CommonLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommonLib.Collections;
    using UnityEngine;

    public static class EnumerableEx
    {
        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            foreach (T item in enumerable)
            {
                action.Invoke(item);
            }
        }

        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            if (action == null)
            {
                return;
            }

            var index = 0;
            foreach (T item in enumerable)
            {
                action.Invoke(item, index++);
            }
        }

        public static Vector3 Sum(this IEnumerable<Vector3> enumerable)
        {
            return enumerable.Aggregate(Vector3.zero, (current, item) => current + item);
        }

        public static float MaxAbs<T>(this IEnumerable<T> enumerable, Func<T, float> selector)
        {
            ThrowIf.ArgumentIsNull(selector, "selector");

            var max = 0.0f;

            foreach (float item in enumerable.Select(selector))
            {
                if (Mathf.Abs(item) > Mathf.Abs(max))
                {
                    max = item;
                }
            }

            return max;
        }

        public static Vector2 MaxAbs<T>(this IEnumerable<T> enumerable, Func<T, Vector2> selector)
        {
            ThrowIf.ArgumentIsNull(selector, "selector");

            Vector2 max = Vector2.zero;

            foreach (Vector2 item in enumerable.Select(selector))
            {
                if (Mathf.Abs(item.x) > Mathf.Abs(max.x))
                {
                    max.x = item.x;
                }
                if (Mathf.Abs(item.y) > Mathf.Abs(max.y))
                {
                    max.y = item.y;
                }
            }

            return max;
        }

        public static Vector3 Center(this IEnumerable<Vector3> enumerable)
        {
            int length = enumerable.Count();
            return length == 0
                ? Vector3.zero
                : enumerable.Sum() * (1.0f/length);
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            return new WeightedSet<T>(enumerable).Next();
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> enumerable, int count)
        {
            IEnumerable<T> weightedSet = new WeightedSet<T>(enumerable).RandomItems;
            return weightedSet.Take(count);
        }

        public static IEnumerable<int> Range(int min, int max, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return UnityEngine.Random.Range(min, max);
            }
        }

        public static T FindClosestToPointOrDefault<T>(this IEnumerable<T> points, Vector3 point, Func<T, Vector3> pointSelector)
        {
            ThrowIf.ArgumentIsNull(points, "points");
            ThrowIf.ArgumentIsNull(pointSelector, "pointSelector");

            T closestItem = default(T);
            float minDistance = float.MaxValue;

            foreach (T item in points)
            {
                Vector3 pointA = pointSelector(item);

                Vector3 vector = pointA - point;
                float distance = vector.sqrMagnitude;

                if (distance < minDistance)
                {
                    closestItem = item;
                    minDistance = distance;
                }
            }

            return closestItem;
        }

        public static IEnumerable<T> Trim<T>(this T[] enumerable, int trim)
        {
            return enumerable.Skip(trim).Take(enumerable.Length - trim * 2);
        }

        public static int IndexOf<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; ++i)
            {
                if (object.Equals(array[i], value))
                {
                    return i;
                }
            }

            return -1;
        }

        public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(item => item != null);
        }

        public static IEnumerable<TResult> Zip<T1, T2, TResult>(IEnumerable<T1> enum1, IEnumerable<T2> enum2, Func<T1, T2, TResult> transform)
        {
            ThrowIf.ArgumentIsNull(enum1, "enum1");
            ThrowIf.ArgumentIsNull(enum2, "enum2");

            IEnumerator<T1> e1 = enum1.GetEnumerator();
            IEnumerator<T2> e2 = enum2.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return transform(e1.Current, e2.Current);
            }
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable,
                                          Func<T, bool> predicate,
                                          T defaultValue = default(T))
        {
            T foundItem = defaultValue;
            foreach (T item in enumerable)
            {
                if (predicate(item))
                {
                    foundItem = item;
                    break;
                }
            }
            return foundItem;
        }

        public static IEnumerable<T> WhereMax<T, TValue>(this IEnumerable<T> enumerable, Func<T, TValue> valueSelector)
            where TValue : IComparable<TValue>
        {
            var maxItems = new List<T>();
            TValue max = default(TValue);

            foreach (T item in enumerable)
            {
                int comparator = valueSelector(item).CompareTo(max);
                if (comparator > 0)
                {
                    maxItems.Clear();
                    maxItems.Add(item);
                }
                else if (comparator == 0)
                {
                    maxItems.Add(item);
                }
            }

            return maxItems;
        }
    }
}
