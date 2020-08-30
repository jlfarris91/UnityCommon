namespace CommonLib
{
    using System;
    using UnityEngine;

    [Serializable]
    public class ValueChangeWatcher<T>
    {
        [SerializeField]
        private T value;

        public ValueChangeWatcher(T initialValue)
        {
            this.LastValue = this.Value = initialValue;
        }

        public T LastValue { get; private set; }

        public T Value
        {
            get { return this.value; }

            set
            {
                this.LastValue = this.value;
                this.value = value;
            }
        }

        public bool HasChanged
        {
            get { return !object.Equals(this.Value, this.LastValue); }
        }

        public void Reset(T value)
        {
            this.LastValue = this.Value = value;
        }
    }

    public class FloatValueChangeWatcher : ValueChangeWatcher<float>
    {
        public FloatValueChangeWatcher(float initialValue)
            : base(initialValue)
        {
        }
    }

    public class ComponentValueChangeWatcher : ValueChangeWatcher<Component>
    {
        public ComponentValueChangeWatcher(Component initialValue)
            : base(initialValue)
        {
        }
    }
}
