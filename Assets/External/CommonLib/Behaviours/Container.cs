namespace CommonLib.Behaviours
{
    using UnityEngine;

    public class Container<T> : MonoBehaviour where T : MonoBehaviour
    {
        public T Item
        {
            get { return this.GetComponentInChildren<T>(); }
        }

        public T[] Items
        {
            get { return this.GetComponentsInChildren<T>(); }
        }
    }
}