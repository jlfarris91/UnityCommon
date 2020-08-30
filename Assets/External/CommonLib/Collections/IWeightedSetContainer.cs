namespace CommonLib.Collections
{
    public interface IWeightedSetContainer<out TWeightedSet, TWeightedSetItem> where TWeightedSet : WeightedSet<TWeightedSetItem>
    {
        TWeightedSet WeightedSet { get; }
    }
}