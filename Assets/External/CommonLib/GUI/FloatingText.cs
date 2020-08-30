namespace CommonLib.GUI
{
    using System;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public class FloatingText : MonoBehaviour
    {
        public Vector3 WorldPosition;
        public Camera Camera;
        public Vector2 Offset;

        public Animation AnimationSettings;

        private RectTransform rectTransform;

        private void Awake()
        {
            this.rectTransform = this.GetComponent<RectTransform>();
        }

        private void Start()
        {
            if (this.Camera == null)
            {
                this.SetDefaultCamera();
            }

            if (this.AnimationSettings.Animate)
            {
                this.StartCoroutine(this.Animate());
            }
        }

        private void LateUpdate()
        {
            this.rectTransform.position = this.CalculateCanvasPosition();
        }

        private void SetDefaultCamera()
        {
            var canvas = this.GetComponentInParent<Canvas>();
            if (canvas != null && canvas.worldCamera != null)
            {
                this.Camera = canvas.worldCamera;
            }
        }

        private Vector3 CalculateCanvasPosition()
        {
            return this.Camera.WorldToScreenPoint(this.WorldPosition) + (Vector3)this.Offset;
        }

        private IEnumerator Animate()
        {
            Vector3 end = this.Offset + this.AnimationSettings.DistanceOverTime;
            Vector3 velocity = Vector3.zero;

            while (true)
            {
                this.Offset = Vector3.SmoothDamp(this.Offset, end, ref velocity, this.AnimationSettings.SmoothTime,
                    this.AnimationSettings.MaxSpeed);
                yield return null;
            }
        }

        [Serializable]
        public struct Animation
        {
            public bool Animate;
            public float SmoothTime;
            public float MaxSpeed;
            public Vector2 DistanceOverTime;
        }
    }
}