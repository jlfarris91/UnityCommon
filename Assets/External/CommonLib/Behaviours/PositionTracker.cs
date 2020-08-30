namespace CommonLib.Behaviours
{
    using System.Linq;
    using CommonLib.Collections;
    using UnityEngine;

    public abstract class PositionTracker : MonoBehaviour
    {
        public float TrackingTime = 1.0f;
        public FixedVector3Stack Positions = new FixedVector3Stack(3);
        public FixedQuaternionStack Rotations = new FixedQuaternionStack(3);
        private float time;

        public Vector3 LastKnownGoodPosition
        {
            get
            {
                Vector3 position = this.transform.position;

                if (this.Positions.Any())
                {
                    position = this.Positions.Last();
                }

                return position;
            }
        }

        public Quaternion LastKnownGoodRotation
        {
            get
            {
                Quaternion rotation = this.transform.rotation;

                if (this.Rotations.Any())
                {
                    rotation = this.Rotations.Last();
                }

                return rotation;
            }
        }

        private void Start()
        {
            this.RecordPosition();
        }

        private void Update()
        {
            if (this.time > this.TrackingTime)
            {
                this.RecordPosition();
                this.time -= this.TrackingTime;
            }

            this.time += Time.deltaTime;
        }

        protected abstract bool ShouldRecordPosition();

        private void OnDrawGizmosSelected()
        {
            foreach (Vector3 position in this.Positions)
            {
                GizmosEx.DrawWireArc(position, 1.0f);
                GizmosEx.DrawArrow(position, position + Vector3.up);
            }
        }

        private void RecordPosition()
        {
            if (!this.ShouldRecordPosition())
            {
                return;
            }

            this.Positions.Push(this.transform.position);
            this.Rotations.Push(this.transform.rotation);
        }
    }
}