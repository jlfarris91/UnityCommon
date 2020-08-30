namespace CommonLib
{
    using System;

    public class Ref<T>
    {
        private Func<T> getter;
        private Action<T> setter;

        public T Value
        {
            get
            {
                if (this.getter != null)
                    return this.getter();
                return default(T);
            }
            set
            {
                if (this.setter != null)
                    this.setter(value);
            }
        }

        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }
}
