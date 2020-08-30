namespace CommonLib.Behaviours
{
    using System.Linq;
    using CommonLib.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class AverageRigidbodyVelocity : MonoBehaviour
    {
        public Vector3 AverageVelocity;
        public int Samples = 60;

        private Rigidbody rb;
        private FixedStack<Vector3> recordedVelocities;

        private void OnEnable()
        {
            this.rb = this.GetComponent<Rigidbody>();
            this.recordedVelocities = new FixedStack<Vector3>(this.Samples);
        }

        private void LateUpdate()
        {
            if (!this.enabled)
            {
                return;
            }

            this.recordedVelocities.Push(this.rb.velocity);
            this.AverageVelocity = this.recordedVelocities.Aggregate((a, b) => a + b) / this.recordedVelocities.Count;
        }
    }
}