using System.Collections;
using UnityEngine;

namespace CommonLib.Behaviours
{
    public class Shake : MonoBehaviour
    {
        public float Magnitude = 1.0f;
        public float Duration = 3.0f;
        public bool ShakeNow = false;
        public bool RemoveOnFinish = true;
        private Coroutine shakeRoutine;

        void Update()
        {
            if (this.ShakeNow)
            {
                this.ShakeNow = false;
                this.ShakeFor(this.Magnitude, this.Duration);
            }
        }

        public void ShakeFor(float magnitude, float duration)
        {
            if (this.shakeRoutine != null)
            {
                this.StopCoroutine(this.shakeRoutine);
            }

            this.shakeRoutine = this.StartCoroutine(this.ShakeForImpl(magnitude, duration));
        }

        private IEnumerator ShakeForImpl(float magnitude, float duration)
        {
            float m = magnitude;
            float t = 0.0f;
            Vector3 startPos = this.transform.position;
            Vector3 point = startPos + Random.onUnitSphere * m;
            Vector3 currVel = Vector3.zero;
            float smoothTime = 0.005f;

            while (t < duration)
            {
                float dist = (point - this.transform.position).magnitude;
                if (dist < 0.001f)
                {
                    point = startPos + Random.onUnitSphere * m;
                }

                m = Mathf.Lerp(magnitude, 0.0f, t / duration);
                t += Time.deltaTime;

                this.transform.position = Vector3.SmoothDamp(this.transform.position, point, ref currVel, smoothTime);

                yield return null;
            }

            this.transform.position = startPos;

            if (this.RemoveOnFinish)
            {
                Destroy(this);
            }
        }
    }
}