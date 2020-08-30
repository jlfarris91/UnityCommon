namespace ActorsLib
{
    using System.Collections.Generic;
    using CommonLib;
    using UnityEngine;
    using Object = UnityEngine.Object;
    using Controllers;

    /// <summary>
    /// The actor class.
    /// </summary>
    [RequireComponent(typeof(ActorController))]
    public abstract partial class Actor : NotifyPropertyChanged
    {
        public static HashSet<Actor> ActiveActors = new HashSet<Actor>();

        /// <summary>
        /// Gets this actor's controller.
        /// </summary>
        [ReadOnly]
        [HideInInspector]
        public ActorController Controller;

        [HideInInspector]
        public ActorKilledUnityEvent Killed;

        public bool IsTargetable = true;

        public bool IsAlive { get; protected set; }

        public void Kill(Object killer)
        {
            this.OnKilled(killer, null);
        }

        public void Kill(Object killer, string message)
        {
            this.OnKilled(killer, message);
        }

        protected virtual void OnKilled(Object killer, string message)
        {
            this.IsAlive = false;

            this.RaiseKilledEvent(killer, message);
        }

        protected virtual void Awake()
        {
            this.Controller = this.GetComponent<ActorController>();
        }

        protected virtual void Start()
        {
        }

        protected virtual void OnEnable()
        {
            ActiveActors.Add(this);

            this.Controller.enabled = true;
            this.IsAlive = true;
        }

        protected virtual void OnDisable()
        {
            this.Controller.enabled = false;

            ActiveActors.Remove(this);
        }

        protected void RaiseKilledEvent(Object killer, string message)
        {
            this.Killed.Invoke(this, new ActorKilledEventArgs(this, killer, message));
        }
    }
}