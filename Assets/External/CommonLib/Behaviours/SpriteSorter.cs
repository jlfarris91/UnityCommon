using UnityEngine;

namespace CommonLib.Behaviours
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSorter : MonoBehaviour
    {
        public bool Once;
        public int SortOrder;

        void Start()
        {
            this.SetSortOrder();
        }

        void LateUpdate()
        {
            if (this.Once)
            {
                return;
            }

            this.SetSortOrder();
        }

        void SetSortOrder()
        {
            this.SortOrder = (int)(this.transform.position.y * -100.0f);
            this.GetComponent<SpriteRenderer>().sortingOrder = this.SortOrder;
        }
    }
}
