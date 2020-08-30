namespace CommonLib.Collections
{
    using System;
    using UnityEngine;

    [Serializable]
    public class WeightedGameObjectSet : WeightedSet<GameObject>
    {
        // Unity doesn't recognize generic classes
    }
}