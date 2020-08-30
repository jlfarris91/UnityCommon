using UnityEngine;

namespace CommonLib.Behaviours
{
    [RequireComponent(typeof(Light))]
    public class BlinkLight : MonoBehaviour
    {
        public AnimationCurve Curve;
        public float CurveDuration;

        private new Light light;
        private float time;
        private float intensity;

        private void Start()
        {
            this.light = this.GetComponent<Light>();
            this.intensity = this.light.intensity;
        }

        // Use this for initialization
        private void OnEnable()
        {
            this.time = 0.0f;
        }

        // Update is called once per frame
        private void Update()
        {
            float t = this.time / this.CurveDuration;
            this.light.intensity = this.intensity * this.Curve.Evaluate(t);
            this.time += Time.deltaTime;
            this.time = MathEx.WrapOverflow(this.time, 0.0f, this.CurveDuration);
        }
    }
}