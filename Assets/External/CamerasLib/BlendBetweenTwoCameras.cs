namespace CamerasLib
{
    using CommonLib;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof (Camera))]
    public class BlendBetweenTwoCameras : MonoBehaviour
    {
        public bool BlendPosition;
        public bool BlendRotation;
        public bool BlendFov;
        public bool BlendPerspective;
        public bool BlendOrthographicSize;

        private new Camera camera;
        private Camera target;

        private void Awake()
        {
            this.camera = this.GetComponent<Camera>();
        }
    
        public void Blend(Camera a, Camera b, float duration)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.BlendCoroutine(a, b, duration));
        }

        private IEnumerator BlendCoroutine(Camera a, Camera b, float duration)
        {
            float time = duration;
            while (time > 0.0f)
            {
                time -= Time.deltaTime;

                float t = Mathf.Clamp01(1.0f - time/duration);

                this.BlendAll(a, b, t);

                yield return null;
            }
        }

        public void BlendAll(Camera a, Camera b, float t)
        {
            if (this.BlendPosition) this.SlerpPosition(a, b, t);
            if (this.BlendRotation) this.SlerpRotation(a, b, t);
            if (this.BlendFov) this.LerpFov(a, b, t);
            if (this.BlendPerspective) this.LerpPerspective(a, b, t);
            if (this.BlendOrthographicSize) this.LerpOrthographicSize(a, b, t);

            if (Mathf.Approximately(t, 0.0f))
            {
                this.camera.orthographic = a.orthographic;
                this.camera.ResetProjectionMatrix();
            }
            else if (Mathf.Approximately(t, 1.0f))
            {
                this.camera.orthographic = b.orthographic;
                this.camera.ResetProjectionMatrix();
            }
        }

        private void LerpPerspective(Camera a, Camera b, float t)
        {
            Matrix4x4 matA = a.projectionMatrix;
            Matrix4x4 matB = b.projectionMatrix;
            this.camera.projectionMatrix = MatrixEx.Lerp(matA, matB, t);
        }

        private void LerpOrthographicSize(Camera a, Camera b, float t)
        {
            float sizeA = a.orthographicSize;
            float sizeB = b.orthographicSize;
            this.camera.orthographicSize = Mathf.Lerp(sizeA, sizeB, t);
        }

        private void LerpPosition(Camera a, Camera b, float t)
        {
            Vector3 posA = a.transform.position;
            Vector3 posB = b.transform.position;
            this.camera.transform.position = Vector3.Lerp(posA, posB, t);
        }

        private void SlerpPosition(Camera a, Camera b, float t)
        {
            Vector3 posA = a.transform.position;
            Vector3 posB = b.transform.position;
            this.camera.transform.position = Vector3.Slerp(posA, posB, t);
        }

        private void SlerpRotation(Camera a, Camera b, float t)
        {
            Quaternion rotA = a.transform.rotation;
            Quaternion rotB = b.transform.rotation;
            this.camera.transform.rotation = Quaternion.Slerp(rotA, rotB, t);
        }

        private void LerpFov(Camera a, Camera b, float t)
        {
            float fovA = a.fieldOfView;
            float fovB = b.fieldOfView;
            this.camera.fieldOfView = Mathf.Lerp(fovA, fovB, t);
        }
    }
}