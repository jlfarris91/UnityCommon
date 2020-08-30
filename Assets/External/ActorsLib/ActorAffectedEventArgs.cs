namespace ActorsLib
{
    using UnityEngine;

    public class ActorAffectedEventArgs : AffectedEventArgs
    {
        public ActorAffectedEventArgs(Actor affected, Object affector)
            : base(affected, affector)
        {
        }

        public new Actor Affected
        {
            get { return base.Affected as Actor; }
        }
    }
}