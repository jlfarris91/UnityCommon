using UnityEngine;

namespace CamerasLib
{
    public class CameraBlender : MonoBehaviour
    {
        public bool Smooth = true;
        public Camera Target;

        [Header("Position")]
        public bool UpdatePosition;
        public float PositionSmoothTime = 0.1f;
        public float PositionMaxSpeed = float.MaxValue;

        [Header("Rotation")]
        public bool UpdateRotation;
        public float RotationSmoothTime = 0.1f;

        [Header("Field of View")]
        public bool UpdateFieldOfView;
        public float FieldOfViewSmoothTime = 0.1f;
        public float FieldOfViewMaxSpeed = float.MaxValue;

        [Header("Orthographic Size")]
        public bool UpdateOrthographicSize;
        public float OrthographicSizeSmoothTime = 0.1f;
        public float OrthographicSizeMaxSpeed = float.MaxValue;

        private new Camera camera;

        private Vector3 positionVelocity;
        private float fieldOfViewVelocity;
        private float orthographicSizeVelocity;

        public void MoveInstantly()
        {
            if (this.Target == null)
            {
                return;
            }

            // Position
            if (this.UpdatePosition)
            {
                this.transform.position = this.Target.transform.position;
            }

            // Rotation
            if (this.UpdateRotation)
            {
                this.transform.rotation = this.Target.transform.rotation;
            }

            // Fov
            if (this.UpdateFieldOfView)
            {
                this.camera.fieldOfView = this.Target.fieldOfView;
            }

            // Orthographic size
            if (this.UpdateOrthographicSize)
            {
                this.camera.orthographicSize = this.Target.orthographicSize;
            }
        }

        private void Awake()
        {
            this.camera = this.GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (this.Smooth)
            {
                this.Step();
            }
            else
            {
                this.MoveInstantly();
            }
        }

        private void Step()
        {
            if (this.Target == null)
            {
                return;
            }

            // Position
            if (this.UpdatePosition)
            {
                this.transform.position = Vector3.SmoothDamp(
                    this.transform.position,
                    this.Target.transform.position,
                    ref this.positionVelocity,
                    this.PositionSmoothTime,
                    this.PositionMaxSpeed);
            }

            // Rotation
            if (this.UpdateRotation)
            {
                this.transform.rotation = Quaternion.Slerp(
                    this.transform.rotation,
                    this.Target.transform.rotation,
                    this.RotationSmoothTime * Time.deltaTime);
            }

            // Fov
            if (this.UpdateFieldOfView)
            {
                this.camera.fieldOfView = Mathf.SmoothDamp(this.camera.fieldOfView,
                    this.Target.fieldOfView,
                    ref this.fieldOfViewVelocity,
                    this.FieldOfViewSmoothTime,
                    this.FieldOfViewMaxSpeed);
            }

            // Orthographic size
            if (this.UpdateOrthographicSize)
            {
                this.camera.orthographicSize = Mathf.SmoothDamp(this.camera.orthographicSize,
                    this.Target.orthographicSize,
                    ref this.orthographicSizeVelocity,
                    this.OrthographicSizeSmoothTime,
                    this.OrthographicSizeMaxSpeed);
            }
        }
    }
}