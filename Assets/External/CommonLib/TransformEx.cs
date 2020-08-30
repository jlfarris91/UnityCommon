namespace CommonLib
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class TransformEx
    {
        public static void DestroyChildren(this Transform transform)
        {
            Transform[] children = transform.GetChildren().ToArray();
            foreach (Transform child in children)
            {
                ObjectEx.SafeDestroyImmediate(child.gameObject);
            }
        }

        public static void DestroyChildren<TBehaviour>(this Transform transform) where TBehaviour : MonoBehaviour
        {
            Transform[] children = transform.GetChildren().ToArray();
            foreach (Transform child in children)
            {
                if (child.HasComponent<TBehaviour>())
                {
                    ObjectEx.SafeDestroyImmediate(child.gameObject);
                }
            }
        }

        public static IEnumerable<Transform> GetChildren(this Transform transform)
        {
            return transform.Cast<Transform>();
        }

        public static IEnumerable<Transform> GetChildren(this Transform transform, string name)
        {
            return transform.Cast<Transform>().Where(child => child.name == name);
        }

        public static Transform FindRecursive(this Transform transform, string childName)
        {
            Transform child = transform.Find(childName);

            if (child == null)
            {
                foreach (Transform nestedChild in transform)
                {
                    child = nestedChild.FindRecursive(childName);
                    if (child != null)
                    {
                        break;
                    }
                }
            }

            return child;
        }

        public static IEnumerable<Transform> FindChildren(this Transform transform, string childName, bool recursive = false)
        {
            List<Transform> children = new List<Transform>();
            transform.FindChildrenInternal(childName, children, recursive);
            return children;
        }

        private static void FindChildrenInternal(this Transform transform,
                                                  string childName,
                                                  List<Transform> children,
                                                  bool recursive)
        {
            foreach (Transform child in transform)
            {
                if (child.name == childName)
                {
                    children.Add(child);
                }

                if (recursive)
                {
                    child.FindChildrenInternal(childName, children, true);
                }
            }
        }

        public static T FindChildComponent<T>(this Transform transform, string childName, bool recursive = false) where T : Component
        {
            Transform child = transform.FindRecursive(childName);
            return child != null ? child.GetComponent<T>() : null;
        }

        public static IEnumerable<T> FindChildComponents<T>(this Transform transform,
                                                         string childName,
                                                         bool recursive = false) where T : Component
        {
            List<Transform> children = new List<Transform>();
            transform.FindChildrenInternal(childName, children, recursive);
            return children.Select(c => c.GetComponent<T>()).ExceptNull();
        }

        public static string GetUniqueChildObjectName(this Transform parent, string baseName)
        {
            IEnumerable<string> partiallyMatchingNames =
                parent.Cast<Transform>().Select(_ => _.name).Where(_ => _.StartsWith(baseName)).ToArray();

            if (!partiallyMatchingNames.Any())
            {
                return baseName;
            }

            string[][] splitNames = partiallyMatchingNames.Select(_ => _.Split(' ')).ToArray();
            int[] trailingInts = splitNames.Where(_ => _.Length > 1).Select(_ =>
            {
                int integer;
                int.TryParse(_.Last(), out integer);
                return integer;
            }).ToArray();

            var maxValue = 0;

            if (trailingInts.Any())
            {
                maxValue = trailingInts.Max();
            }

            return string.Format("{0} {1}", baseName, maxValue + 1);
        }
    }
}
