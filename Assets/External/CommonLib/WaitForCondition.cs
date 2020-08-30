namespace CommonLib
{
    using System;
    using UnityEngine;

    public class WaitForCondition : CustomYieldInstruction
    {
        private readonly Func<bool> predicate;

        public WaitForCondition(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public override bool keepWaiting
        {
            get { return this.predicate != null && !this.predicate(); }
        }
    }
}