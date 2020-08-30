namespace CommonLib.Editor.Collections
{
    using CommonLib.Collections;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(WeightedGameObjectSetContainer))]
    public class WeightedGameObjectSetContainerEditor : WeightedSetEditor<WeightedGameObjectSet, GameObject>
    {
    }
}