namespace CommonLib.Collections
{
    using System.Collections.Generic;

    public interface IWeightedSet<T> : IEnumerable<T>
    {
        float TotalWeight { get; }

        List<T> Items { get; }

        List<float> Weights { get; }

        IEnumerable<T> RandomItems { get; }

        T Next();

        T Next(float value);

        void Add(T item, float value);

        void Insert(int index, T item, float value);

        void Remove(T item);
    }
}