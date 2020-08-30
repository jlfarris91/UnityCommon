using UnityEngine;

namespace ActorsLib.Behaviours
{
    using CommonLib;

    [RequireComponent(typeof(Actor))]
    public class ActorBehaviour : NotifyPropertyChanged
    {
        [ReadOnly]
        public Actor Actor;

        public bool StartEnabled;

        public bool LogEvents;

        public bool IsRunning
        {
            get { return this.enabled; }
        }
    
        public virtual bool CanBegin()
        {
            return true;
        }
    
        public void Begin()
        {
            if (this.IsRunning)
            {
                return;
            }

            this.BeginInternal();
        }

        public void End()
        {
            if (!this.IsRunning)
            {
                return;
            }

            this.enabled = false;
            this.OnEnd();
        }

        private void BeginInternal()
        {
            if (!this.CanBegin())
            {
                return;
            }

            this.enabled = true;
            this.OnBegin();
        }

        private void Update()
        {
            this.OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnBegin()
        {
            if (this.LogEvents)
            {
                Debug.Log(string.Format("{0}.OnBegin()", this.GetType()));
            }
        }

        protected virtual void OnEnd()
        {
            if (this.LogEvents)
            {
                Debug.Log(string.Format("{0}.OnEnd()", this.GetType()));
            }
        }

        protected virtual void Start()
        {
            if (this.StartEnabled)
            {
                this.BeginInternal();
            }
            else
            {
                this.enabled = false;
            }
        }

        protected virtual void Awake()
        {
            this.enabled = true;
            this.Actor = this.GetComponent<Actor>();
        }
    }
}