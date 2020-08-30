namespace CommonLib.GUI
{
    using UnityEngine;

    [RequireComponent(typeof(WorldSpaceTarget))]
    public class LookAtWorldSpaceTarget : MonoBehaviour
    {
        private WorldSpaceTarget worldSpaceTarget;

        private void Start()
        {
            this.worldSpaceTarget = this.GetComponent<WorldSpaceTarget>();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (this.worldSpaceTarget.Target == null)
            {
                return;
            }

            Vector3 forward = this.worldSpaceTarget.ScreenPosition - this.transform.position;
            forward.Normalize();

            float rotZ = MathEx.GetAngle(forward);

            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);
        }
    }
}