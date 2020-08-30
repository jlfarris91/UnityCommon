using UnityEngine;

namespace CommonLib.Behaviours
{
    public class LookAt : MonoBehaviour
    {
        [Header("The object to look at.")]
        public GameObject Target;

        public Vector3 Offset;

        public bool Fixed;

        private void Start()
        {
            this.LateUpdate();
        }

        private void LateUpdate()
        {
            if (this.Target == null)
            {
                return;
            }

            if (!this.Fixed)
            {
                this.transform.LookAt(this.Target.transform.position + this.Offset, Vector3.up);
            }
        }

        private void FixedUpdate()
        {
            if (this.Target == null)
            {
                return;
            }

            if (this.Fixed)
            {
                this.transform.LookAt(this.Target.transform.position + this.Offset, Vector3.up);
            }
        }
    }
}