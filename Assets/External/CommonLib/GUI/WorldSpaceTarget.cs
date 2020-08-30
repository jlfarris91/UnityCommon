namespace CommonLib.GUI
{
    using UnityEngine;

    public class WorldSpaceTarget : NotifyPropertyChanged
    {
        public Camera Camera;

        private bool hasTarget;
        private bool isTargetVisible;
        private Vector3 screenPosition;
        private Vector3 viewportPosition;
        private Transform target;

        [ShowInInspector]
        public Transform Target
        {
            get { return this.target; }

            set
            {
                if (this.target != value)
                {
                    this.target = value;
                    this.RaisePropertyChanged("Target");

                    this.HasTarget = this.target != null;
                }
            }
        }

        [ShowInInspector]
        public bool HasTarget
        {
            get { return this.hasTarget; }
            set
            {
                if (this.hasTarget != value)
                {
                    this.hasTarget = value;
                    this.RaisePropertyChanged("HasTarget");
                }
            }
        }
        
        [ShowInInspector]
        public Vector3 ViewportPosition
        {
            get { return this.viewportPosition; }

            set
            {
                if (!Vector3Ex.IsApproximately(this.viewportPosition, value))
                {
                    this.viewportPosition = value;
                    this.RaisePropertyChanged("ViewportPosition");
                }
            }
        }

        [ShowInInspector]
        public Vector3 ScreenPosition
        {
            get { return this.screenPosition; }
            set
            {
                if (!Vector3Ex.IsApproximately(this.screenPosition, value))
                {
                    this.screenPosition = value;
                    this.RaisePropertyChanged("ScreenPosition");
                }
            }
        }

        [ShowInInspector]
        public bool IsTargetVisible
        {
            get { return this.isTargetVisible; }

            set
            {
                if (this.isTargetVisible != value)
                {
                    this.isTargetVisible = value;
                    this.RaisePropertyChanged("IsTargetVisible");
                }
            }
        }

        private void Start()
        {
            this.SetDefaultCamera();
        }

        private void SetDefaultCamera()
        {
            if (this.Camera != null)
            {
                return;
            }

            var canvas = this.GetComponentInParent<Canvas>();
            if (canvas != null && canvas.worldCamera != null)
            {
                this.Camera = canvas.worldCamera;
            }
        }

        private void LateUpdate()
        {
            if (this.Camera == null || this.Target == null)
            {
                this.IsTargetVisible = false;
                this.HasTarget = false;
                return;
            }

            this.CalculateViewportPosition();
            this.UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            var boundingBox = this.Target.GetComponent<Collider>();
            if (boundingBox == null)
            {
                this.IsTargetVisible = true;
                return;
            }

            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(this.Camera);
            this.IsTargetVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, boundingBox.bounds);
        }

        private void CalculateViewportPosition()
        {
            this.ViewportPosition = this.Camera.WorldToViewportPoint(this.Target.position);
            this.ScreenPosition = new Vector3(this.ViewportPosition.x * Screen.width, this.ViewportPosition.y * Screen.height);
        }
    }
}