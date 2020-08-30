namespace ActorsLib
{
    using System;

    public class ActorEventArgs : EventArgs
    {
        public ActorEventArgs(Actor actor)
        {
            this.Actor = actor;
        }

        public Actor Actor { get; private set; }
    }
}