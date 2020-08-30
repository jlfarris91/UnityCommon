namespace CommonLib.GUI
{
    using UnityEngine;

    [RequireComponent(typeof (RectTransform))]
    public class ViewportPanel : MonoBehaviour
    {
        public Camera Camera;

        public void UpdateViewport()
        {
            if (this.Camera == null)
            {
                return;
            }

            Rect viewportRect = this.Camera.rect;
            var rectTrans = this.GetComponent<RectTransform>();
            rectTrans.anchorMin = viewportRect.min;
            rectTrans.anchorMax = viewportRect.max;
        }

        private void Start()
        {
            this.UpdateViewport();
        }

        private void OnViewportsUpdated()
        {
            this.UpdateViewport();
        }
    }
}