namespace ActorsLib
{
    using System;
    using UnityEngine.Events;
    using Object = UnityEngine.Object;

    public class ActorKilledEventArgs : ActorAffectedEventArgs
    {
        public ActorKilledEventArgs(Actor affected, Object affector)
            : this(affected, affector, null)
        {
        }

        public ActorKilledEventArgs(Actor affected, Object affector, string message)
            : base(affected, affector)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }

    [Serializable]
    public class ActorKilledUnityEvent : UnityEvent<Object, ActorKilledEventArgs>
    {
    }
}