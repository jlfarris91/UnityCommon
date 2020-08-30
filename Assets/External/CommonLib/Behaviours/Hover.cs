using UnityEngine;

namespace CommonLib.Behaviours
{
    public class Hover : MonoBehaviour
    {
        public float Amplitude;
        public float Speed;

        private float angle;
        private Vector3 initialLocalPosition;

        void Start()
        {
            this.initialLocalPosition = this.transform.localPosition;
        }

        // Update is called once per frame
        void Update ()
        {
            this.transform.localPosition = this.initialLocalPosition + Vector3.up * Mathf.Sin(this.angle) * this.Amplitude;
            this.angle += this.Speed*Time.deltaTime;
        }
    }
}
