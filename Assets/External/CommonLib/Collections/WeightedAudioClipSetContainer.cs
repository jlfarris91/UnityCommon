namespace CommonLib.Collections
{
    using UnityEngine;

    public class WeightedAudioClipSetContainer : MonoBehaviour, IWeightedSetContainer<WeightedAudioClipSet, AudioClip>
    {
        public WeightedAudioClipSet Set = new WeightedAudioClipSet();

        WeightedAudioClipSet IWeightedSetContainer<WeightedAudioClipSet, AudioClip>.WeightedSet
        {
            get { return this.Set; }
        }
    }
}