namespace CommonLib.Collections
{
    using System;
    using UnityEngine;

    [Serializable]
    public class FixedVector3Stack : FixedStack<Vector3>
    {
        public FixedVector3Stack()
        {
        }

        public FixedVector3Stack(int capacity)
            : base(capacity)
        {
        }
    }
}