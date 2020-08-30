namespace CommonLib
{
    using System;
    using UnityEngine;

    [Serializable]
    public class LazyReference<T>
    {
        private readonly Func<T> getter;
        private T value;

        public LazyReference(Func<T> getter)
        {
            this.getter = getter;
        }

        public T Value
        {
            get
            {
                if (this.value == null)
                {
                    this.value = this.getter();
                }

                return this.value;
            }
        }
    }

    [Serializable]
    public class LazyTransform : LazyReference<Transform>
    {
        public LazyTransform(Func<Transform> getter) : base(getter)
        {
        }
    }
}
