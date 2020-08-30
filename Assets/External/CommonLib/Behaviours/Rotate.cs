using UnityEngine;

namespace CommonLib.Behaviours
{
    public class Rotate : MonoBehaviour
    {
        public Vector3 Axis = Vector3.up;
        public float DegreesPerSecond = 10.0f;
	
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update ()
        {
            this.transform.Rotate(this.Axis, this.DegreesPerSecond*Time.deltaTime);
        }
    }
}
