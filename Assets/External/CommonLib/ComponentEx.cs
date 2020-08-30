////////////////////////////////////////////////////////////////////////////////
//
// ComponentEx.cs
// 
////////////////////////////////////////////////////////////////////////////////

namespace CommonLib
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Extension methods for the Component class.
    /// </summary>
    public static class ComponentEx
    {
        public static T GetComponentInNeighbors<T>(this Component component) where T : Component
        {
            if (component == null)
            {
                return null;
            }

            if (component.transform != null && component.transform.parent != null)
            {
                return component.transform.parent.GetComponentInChildren<T>();
            }

            return null;
        }

        public static T[] GetComponentsInNeighbors<T>(this Component component) where T : Component
        {
            if (component == null)
            {
                return null;
            }

            if (component.transform != null && component.transform.parent != null)
            {
                return component.transform.parent.GetComponentsInChildren<T>();
            }

            return Enumerable.Empty<T>().ToArray();
        }

        /// <summary>
        /// Returns the first component of type <typeparamref name="TComponent"/> in
        /// any descendent subtree.
        /// </summary>
        /// <typeparam name="TComponent">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="component">The <see cref="Component"/>.</param>
        /// <returns>
        /// The first component of type <typeparamref name="TComponent"/> found in
        /// any descendent subtree.
        /// </returns>
        public static TComponent GetFirstComponentInChildren<TComponent>(this Component component)
            where TComponent : Component
        {
            ThrowIf.ArgumentIsNull(component, "component");
            return component.GetFirstComponentInChildrenInternal<TComponent>().FirstOrDefault();
        }

        /// <summary>
        /// Returns the first component of type <typeparamref name="TComponent"/> in
        /// each descendent subtree.
        /// </summary>
        /// <typeparam name="TComponent">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="component">The <see cref="Component"/>.</param>
        /// <returns>
        /// The first component of type <typeparamref name="TComponent"/> found in
        /// each descendent subtree.
        /// </returns>
        public static TComponent[] GetFirstComponentsInChildren<TComponent>(this Component component)
            where TComponent : Component
        {
            ThrowIf.ArgumentIsNull(component, "component");
            return component.GetFirstComponentInChildrenInternal<TComponent>().ToArray();
        }

        private static IEnumerable<TComponent> GetFirstComponentInChildrenInternal<TComponent>(this Component component)
        {
            foreach (Transform child in component.transform)
            {
                TComponent[] childComps = child.GetComponents<TComponent>();

                if (childComps.Any())
                {
                    foreach (TComponent childComp in childComps)
                    {
                        yield return childComp;
                    }
                }
                else
                {
                    IEnumerable<TComponent> asd = child.GetFirstComponentInChildrenInternal<TComponent>();

                    foreach (TComponent childComp in asd)
                    {
                        yield return childComp;
                    }
                }
            }
        }

        public static bool HasComponent<T>(this Component component) where T : Component
        {
            return component != null && component.GetComponent<T>() != null;
        }

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetComponent<T>() ?? component.gameObject.AddComponent<T>();
        }

        public static void EnableComponent<T>(this Component component) where T : MonoBehaviour
        {
            T otherComp = component.GetComponent<T>();
            if (otherComp != null)
            {
                otherComp.enabled = true;
            }
        }

        public static void DisableComponent<T>(this Component component) where T : MonoBehaviour
        {
            T otherComp = component.GetComponent<T>();
            if (otherComp != null)
            {
                otherComp.enabled = false;
            }
        }

        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }
    }
}