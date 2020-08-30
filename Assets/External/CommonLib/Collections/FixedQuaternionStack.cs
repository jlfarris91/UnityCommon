namespace CommonLib.Collections
{
    using System;
    using UnityEngine;

    [Serializable]
    public class FixedQuaternionStack : FixedStack<Quaternion>
    {
        public FixedQuaternionStack()
        {
        }

        public FixedQuaternionStack(int capacity)
            : base(capacity)
        {
        }
    }
}