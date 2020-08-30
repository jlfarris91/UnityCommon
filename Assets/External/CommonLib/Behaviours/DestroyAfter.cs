using UnityEngine;

namespace CommonLib.Behaviours
{
    /// <summary>
    /// Destroys the object after a duration.
    /// </summary>
    public class DestroyAfter : MonoBehaviour
    {
        /// <summary>
        /// The amount of time the object lives.
        /// </summary>
        public float Duration = 1.0f;

        private float timer;

        private void OnEnable()
        {
            this.timer = 0.0f;
        }

        private void Update()
        {
            this.timer += Time.deltaTime;
            if (this.timer > this.Duration)
            {
                Object.Destroy(this.gameObject);
            }
        }
    }
}