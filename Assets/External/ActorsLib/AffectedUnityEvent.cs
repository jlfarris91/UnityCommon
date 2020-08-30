namespace ActorsLib
{
    using System;
    using UnityEngine.Events;
    using Object = UnityEngine.Object;

    [Serializable]
    public class AffectedUnityEvent<TSender, TArgs> : UnityEvent<TSender, TArgs> where TArgs : AffectedEventArgs
    {
    }

    [Serializable]
    public class AffectedUnityEvent<TArgs> : UnityEvent<Object, TArgs> where TArgs : AffectedEventArgs
    {
    }

    [Serializable]
    public class AffectedUnityEvent : UnityEvent<Object, AffectedEventArgs>
    {
    }

    /// <summary>
    /// Arguments to describe an event where an object affects another object.
    /// For example; if an actor takes damage these event arguments would describe
    /// the actor who is taking damage (<see cref="Affected"/>) and the object that
    /// is causing the damage (<see cref="Affector"/>).
    /// </summary>
    public class AffectedEventArgs : EventArgs
    {
        public AffectedEventArgs(Object affected, Object affector)
        {
            this.Affected = affected;
            this.Affector = affector;
        }

        /// <summary>
        /// The <see cref="Object"/> that is the target of the event.
        /// For example; if an actor takes damage this would be the actor
        /// that is taking damage.
        /// </summary>
        public Object Affected { get; private set; }

        /// <summary>
        /// The <see cref="Object"/> that caused this event to happen.
        /// For example; if an actor takes damage this would be the object
        /// that did the damage.
        /// </summary>
        public Object Affector { get; private set; }
    }
}