namespace CommonLib.Behaviours
{
    using UnityEngine;

    public class SetRotation : MonoBehaviour
    {
        public Vector3 Eulers;

        private void LateUpdate()
        {
            this.transform.rotation = Quaternion.Euler(this.Eulers);
        }
    }
}