namespace CommonLib
{
    using UnityEngine;

    public class CountdownTimer : NotifyPropertyChanged
    {
        public bool StartEnabled;

        public float Duration;

        private float timeRemaining;

        [ReadOnly]
        [ShowInInspector]
        public float TimeRemaining
        {
            get { return this.timeRemaining; }

            set
            {
                if (!Mathf.Approximately(this.timeRemaining, value))
                {
                    this.timeRemaining = value;
                    this.RaisePropertyChanged("TimeRemaining");
                }
            }
        }

        public void RestartTimer()
        {
            this.TimeRemaining = this.Duration;
        }

        private void Start()
        {
            this.enabled = this.StartEnabled;
        }

        private void OnEnable()
        {
            this.RestartTimer();
        }

        private void Update()
        {
            this.UpdateTimeRemaining();
        }

        private void UpdateTimeRemaining()
        {
            this.TimeRemaining -= Time.deltaTime;
        }
    }
}