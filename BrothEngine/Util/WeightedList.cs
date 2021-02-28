using System;
using System.Collections.Generic;
using System.Text;

namespace Broth.Util
{
    /// <summary>
    /// Weighted Lists contain a collection of item+weight pairs, where the weight represents the likeliness
    /// that the item will be selected, relative to the cumulative weight of all other items in the list.
    /// </summary>
    /// <typeparam name="T"> The lowest common Type of objects in the list </typeparam>
    public class WeightedList<T>
    {
        private class WeightedItem<TItem>
        {
            public int Weight { get; set; } = 1;
            public TItem Item { get; set; }

            public WeightedItem(TItem item, int weight)
            {
                Weight = weight;
                Item = item;
            }
        }

        private int _totalWeight = 0;
        private readonly List<WeightedItem<T>> _list = new List<WeightedItem<T>>();

        public void Add(T item, int weight)
        {
            if (weight < 1) return;

            _list.Add(new WeightedItem<T>(item, weight));
            _totalWeight += weight;
        }

        public T GetRandomItem()
        {
            if (_list.Count < 1 || _totalWeight < 1) return default;

            int randomNumber = BrothMath.Random.Next(_totalWeight);
            for (int i = 0; i < _list.Count; i++)
            {
                if (randomNumber < _list[i].Weight)
                {
                    return _list[i].Item;
                }
                randomNumber -= _list[i].Weight;
            }

            return default;
        }
    }
}
