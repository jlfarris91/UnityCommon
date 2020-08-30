namespace CommonLib
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class WaitParallel : CustomYieldInstruction
    {
        private readonly List<Wait> waits;

        public WaitParallel(IEnumerable<IEnumerator> enumerators)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            ThrowIf.ArgumentIsEmpty(enumerators, "enumerators");
            // ReSharper disable once PossibleMultipleEnumeration
            this.waits = enumerators.Select(_ => new Wait(_)).ToList();
        }

        public WaitParallel(IEnumerable<IEnumerator> enumerators, long timeSlice)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            ThrowIf.ArgumentIsEmpty(enumerators, "enumerators");
            // ReSharper disable once PossibleMultipleEnumeration
            this.waits = enumerators.Select(_ => new Wait(_, timeSlice)).ToList();
        }

        public override bool keepWaiting
        {
            get { return this.waits.Any(_ => _.keepWaiting); }
        }
    }
}