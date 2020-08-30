namespace CommonLib
{
    using System;
    using UnityEngine;

    public class WaitFor : CustomYieldInstruction
    {
        private readonly float duration;
        private readonly Action<float> progress;
        private float time;

        public WaitFor(float seconds)
            : this(seconds, _ => { })
        {
        }

        public WaitFor(float seconds, Action<float> progress)
        {
            this.time = 0.0f;
            this.duration = seconds;
            this.progress = progress;

            if (this.time < this.duration)
            {
                this.progress(0.0f);
            }
        }

        public override bool keepWaiting
        {
            get
            {
                bool continueWaiting = this.time < this.duration;
                if (continueWaiting)
                {
                    float t = this.time / this.duration;
                    this.progress(t);
                    this.time += Time.deltaTime;
                }
                else
                {
                    this.progress(1.0f);
                    this.time = this.duration;
                }
                return continueWaiting;
            }
        }
    }
}