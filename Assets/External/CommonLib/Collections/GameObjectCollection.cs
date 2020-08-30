namespace CommonLib.Collections
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class GameObjectCollection : ObservableCollection<GameObject>
    {
        public GameObjectCollection()
        {
        }

        public GameObjectCollection(IEnumerable<GameObject> initial)
            : base(initial)
        {
        }
    }
}
