namespace CommonLib.GUI
{
    using UnityEngine;

    public class FollowWorldSpaceTarget : MonoBehaviour
    {
        public bool Clamp;
        public Vector2 ClampOffset;
        public float Radius;

        private WorldSpaceTarget worldSpaceTarget;

        private void Start()
        {
            this.worldSpaceTarget = this.GetComponent<WorldSpaceTarget>();
            this.LateUpdate();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (this.worldSpaceTarget.Target == null)
            {
                return;
            }
            
            Vector3 targetViewportPos = this.worldSpaceTarget.ScreenPosition;

            if (this.Clamp)
            {
                var screenSize = new Vector2(Screen.width, Screen.height);

                // Clamp to screen rect
                if (Mathf.Approximately(this.Radius, 0.0f))
                {
                    targetViewportPos = Vector3Ex.Clamp(targetViewportPos, this.ClampOffset, screenSize - this.ClampOffset);
                }
                // Clamp to screen circle
                else
                {
                    targetViewportPos = MathEx.ClampToCircle(Vector2.one * 0.5f, this.Radius, this.worldSpaceTarget.ViewportPosition);
                    targetViewportPos = new Vector3(targetViewportPos.x * Screen.width, targetViewportPos.y * Screen.height);
                }
            }

            this.transform.position = targetViewportPos;
        }
    }
}