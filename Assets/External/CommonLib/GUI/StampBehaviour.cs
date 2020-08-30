namespace CommonLib.GUI
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class StampBehaviour : MonoBehaviour
    {
        public float Duration;
        public float Size;
        public Vector3 Direction;
        public float Distance;

        public string IncomingSound;
        public string StampSound;

        public bool DoStamp = false;
        private Vector3 initialSize;
        private Vector3 initialPos;

        public void Stamp()
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.StampCoroutine());
        }

        private void Awake()
        {
            this.initialSize = this.transform.localScale;
            this.initialPos = this.transform.localPosition;
        }

        private void Update()
        {
            if (this.DoStamp)
            {
                this.DoStamp = false;
                this.Stamp();
            }
        }
        
        private IEnumerator StampCoroutine()
        {
            float time = this.Duration;

            var canvasGroup = this.GetComponent<CanvasGroup>();

            var startAlpha = canvasGroup.alpha;

            var startSize = new Vector3(this.Size, this.Size, this.initialSize.z);

            var startPos = this.initialPos + new Vector3(
                this.Direction.x * this.Distance, 
                this.Direction.y * this.Distance,
                this.initialPos.z);

            //SoundSystem.Instance.StopSound(this.IncomingSound);
            //SoundSystem.Instance.PlaySound(this.IncomingSound);

            while (time > 0.0f)
            {
                float t = 1.0f - time/this.Duration;

                // Scale
                this.transform.localScale = Vector3.Lerp(startSize, this.initialSize, t);
                this.transform.localPosition = Vector3.Lerp(startPos, this.initialPos, t);

                // Fade in
                canvasGroup.alpha = Mathf.Lerp(0.0f, startAlpha, t);

                time -= Time.deltaTime;
                yield return null;
            }

            //SoundSystem.Instance.StopSound(this.IncomingSound);
            //SoundSystem.Instance.PlaySound(this.StampSound);

            canvasGroup.alpha = startAlpha;

            this.transform.localScale = this.initialSize;
            this.transform.localPosition = this.initialPos;
        }
    }
}