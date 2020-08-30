namespace CommonLib.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [Serializable]
    public class WeightedSet<T> : IWeightedSet<T>
    {
        [SerializeField]
        private List<T> items = new List<T>();
        
        [SerializeField]
        private List<float> weights = new List<float>();

        public WeightedSet()
        {
        }

        public WeightedSet(IEnumerable<T> other)
        {
            this.items.AddRange(other);
            this.weights.AddRange(Enumerable.Repeat(1.0f, this.items.Count));
        }

        public WeightedSet(WeightedSet<T> other)
        {
            this.items.Clear();
            this.items.AddRange(other.Items);
            this.weights.Clear();
            this.weights.AddRange(other.weights);
        }

        public List<T> Items
        {
            get { return this.items; }
        }

        public List<float> Weights
        {
            get { return this.weights; }
        }

        /// <summary>
        /// Gets the total combined weight of all of the items in the set.
        /// </summary>
        public float TotalWeight
        {
            get { return this.weights.Sum(); }
        }

        /// <summary>
        /// Gets the number of items in the set.
        /// </summary>
        public int Count
        {
            get { return Mathf.Min(this.items.Count, this.weights.Count); }
        }

        /// <summary>
        /// Returns a random item, removes that item from the set, and repeats until
        /// there are no items remaining. This does not affect the base set.
        /// </summary>
        public IEnumerable<T> RandomItems
        {
            get
            {
                var clone = new WeightedSet<T>(this);

                while (clone.Any())
                {
                    T value = clone.Next();
                    yield return value;
                    clone.Remove(value);
                }
            }
        }

        /// <summary>
        /// Returns a weighted item chosen at random.
        /// </summary>
        /// <returns></returns>
        public T Next()
        {
            float value = Random.Range(0.0f, this.TotalWeight);
            return this.Next(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Next(float value)
        {
            var sumWeight = 0.0f;

            for (var i = 0; i < this.Count; ++i)
            {
                T item = this.items[i];
                float weight = this.weights[i];

                if (value >= sumWeight && value < sumWeight + weight)
                {
                    return item;
                }

                sumWeight += weight;
            }

            return this.items.LastOrDefault();
        }

        /// <summary>
        /// Adds a new item/weight pair to the set.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="weight"></param>
        /// <returns>Returns false if the item already exists in the set.</returns>
        public void Add(T item, float weight)
        {
            this.items.Add(item);
            this.weights.Add(weight);
        }

        public void Insert(int index, T item, float weight)
        {
            this.items.Insert(index, item);
            this.weights.Insert(index, weight);
        }

        /// <summary>
        /// Removes a item/weight pair from the set.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            int index = this.items.IndexOf(item);

            if (!index.IsBetweenInclusive(0, this.Count - 1))
            {
                return;
            }

            float weight = this.weights[index];

            this.items.RemoveAt(index);
            this.weights.RemoveAt(index);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }
    }
}