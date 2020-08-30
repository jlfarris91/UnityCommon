namespace CommonLib.Collections
{
    using UnityEngine;

    public class WeightedGameObjectSetContainer : MonoBehaviour, IWeightedSetContainer<WeightedGameObjectSet, GameObject>
    {
        public WeightedGameObjectSet Set = new WeightedGameObjectSet();

        WeightedGameObjectSet IWeightedSetContainer<WeightedGameObjectSet, GameObject>.WeightedSet
        {
            get { return this.Set; }
        }
    }
}