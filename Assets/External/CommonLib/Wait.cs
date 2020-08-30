namespace CommonLib
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using UnityEngine;

    public class Wait : CustomYieldInstruction
    {
        private readonly Stack<IEnumerator> enumerators;
        private readonly long timeSlice;
        private readonly Stopwatch stopwatch;
        private readonly Func<bool> evaluateKeepWaiting;

        public Wait(IEnumerator enumerator)
        {
            this.enumerators = new Stack<IEnumerator>();
            this.enumerators.Push(enumerator);
            this.evaluateKeepWaiting = this.KeepWaiting;
        }

        public Wait(IEnumerator enumerator, long timeSlice)
        {
            this.enumerators = new Stack<IEnumerator>();
            this.enumerators.Push(enumerator);
            this.timeSlice = timeSlice;
            this.stopwatch = new Stopwatch();
            this.evaluateKeepWaiting = this.KeepWaitingForTimeSlice;
        }
    
        public override bool keepWaiting
        {
            get { return this.evaluateKeepWaiting(); }
        }

        private bool KeepWaiting()
        {
            if (!this.enumerators.Any())
            {
                return false;
            }

            IEnumerator currentEnumerator = this.enumerators.Peek();
            bool keepWaitingOnCurrentEnumerator = currentEnumerator.MoveNext();

            if (keepWaitingOnCurrentEnumerator)
            {
                var enumeratorCurrent = currentEnumerator.Current as IEnumerator;
                if (enumeratorCurrent != null)
                {
                    this.enumerators.Push(enumeratorCurrent);
                }
            }
            else
            {
                this.enumerators.Pop();
                keepWaitingOnCurrentEnumerator = this.enumerators.Any();
            }

            return keepWaitingOnCurrentEnumerator;
        }

        private bool KeepWaitingForTimeSlice()
        {
            var result = true;

            this.stopwatch.Start();
            while (this.stopwatch.ElapsedMilliseconds < this.timeSlice)
            {
                result = result & this.KeepWaiting();
            }
            this.stopwatch.Stop();

            return result;
        }
    }
}