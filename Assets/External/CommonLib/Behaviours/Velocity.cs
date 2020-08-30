using System.Linq;
using UnityEngine;

namespace CommonLib.Behaviours
{
    public class Velocity : MonoBehaviour
    {
        public Vector3 Value;
        public Vector3 AverageValue;
        public Vector3 AverageDirection;
        public float Speed;
        public float SpeedDt;
        public float AverageSpeed;
        public float AverageSpeedDt;
        public int SampleCount = 10;

        private Vector3 lastPosition;
        private Vector3[] velocities;
        private Vector3[] velocitiesDt;
        private int count;
        private int index;

        // Use this for initialization
        private void Start()
        {
            this.Restart();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            this.Value = (this.transform.position - this.lastPosition);
            this.Speed = this.Value.magnitude;
            this.SpeedDt = this.Speed / Time.deltaTime;

            this.count = Mathf.Min(this.count + 1, this.SampleCount);

            this.velocities[this.index] = this.Value;
            Vector3 aggregate = this.velocities.Aggregate((a, b) => a + b);
            this.AverageValue = aggregate / this.count;
            this.AverageDirection = aggregate.normalized;
            this.AverageSpeed = this.AverageValue.magnitude;

            this.velocitiesDt[this.index] = this.Value / Time.deltaTime;
            aggregate = this.velocitiesDt.Aggregate((a, b) => a + b);
            this.AverageSpeedDt = aggregate.magnitude / this.count;

            this.index = (this.index + 1) % this.SampleCount;

            this.lastPosition = this.transform.position;
        }

        public void Restart()
        {
            this.velocities = new Vector3[this.SampleCount];
            this.velocitiesDt = new Vector3[this.SampleCount];
            this.lastPosition = this.transform.position;
            this.index = 0;
            this.count = 0;
            this.Speed = 0.0f;
            this.SpeedDt = 0.0f;
            this.AverageSpeed = 0.0f;
            this.AverageSpeedDt = 0.0f;
            this.AverageDirection = Vector3.forward;
            this.AverageValue = Vector3.zero;
        }
    }
}