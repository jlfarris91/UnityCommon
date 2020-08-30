namespace CommonLib
{
    using System;
    using UnityEngine;

    public class Timer
    {
        private DateTime startTime;

        public float Elapsed
        {
            get { return (DateTime.UtcNow - this.startTime).Milliseconds; }
        }

        public void ReportElapsed(string messageFormat, params object[] messageArgs)
        {
            TimeSpan elapsed = DateTime.UtcNow - this.startTime;
            Debug.LogFormat(messageFormat, messageArgs);
            Debug.LogFormat("Took {0} seconds.", elapsed.TotalSeconds);
        }

        public void Reset()
        {
            this.startTime = DateTime.UtcNow;
        }

        public static Timer Start()
        {
            return new Timer { startTime = DateTime.UtcNow };
        }
    }
}
