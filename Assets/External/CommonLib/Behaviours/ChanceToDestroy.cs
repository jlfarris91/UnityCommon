using UnityEngine;

namespace CommonLib.Behaviours
{
    public class ChanceToDestroy : MonoBehaviour
    {
        public bool EvaluateOnStart = true;

        [Range(0, 100)]
        public float Chance = 100.0f;

        public float Normalized
        {
            get { return this.Chance/100.0f; }
        }

        public bool Evaluate()
        {
            if (Random.Range(0.0f, 1.0f) <= this.Normalized)
            {
                GameObject.Destroy(this.gameObject);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called once when the object is created.
        /// </summary>
        private void Start ()
        {
            if (this.EvaluateOnStart)
            {
                this.Evaluate();
            }
        }
    }
}
