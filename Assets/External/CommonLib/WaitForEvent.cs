namespace CommonLib
{
    using UnityEngine;
    using UnityEngine.Events;

    public class WaitForEvent<TEvent> : CustomYieldInstruction where TEvent : UnityEvent
    {
        private readonly TEvent unityEvent;
        private bool stillWaiting = true;

        public WaitForEvent(TEvent unityEvent)
        {
            this.unityEvent = unityEvent;
            this.unityEvent.AddListener(this.WaitOne);
        }

        private void WaitOne()
        {
            this.stillWaiting = false;
        }

        public override bool keepWaiting
        {
            get
            {
                if (this.unityEvent == null)
                {
                    return false;
                }

                if (!this.stillWaiting)
                {
                    this.unityEvent.RemoveListener(this.WaitOne);
                }

                return this.stillWaiting;
            }
        }
    }
}