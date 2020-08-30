namespace ActorsLib
{
    using ActorsLib.Behaviours;

    public partial class Actor
    {
        public void StartBehaviour<T>() where T : ActorBehaviour
        {
            T behaviour = this.GetComponent<T>();
            if (behaviour != null)
            {
                behaviour.Begin();
            }
        }

        public void StopBehaviour<T>() where T : ActorBehaviour
        {
            T behaviour = this.GetComponent<T>();
            if (behaviour != null)
            {
                behaviour.End();
            }
        }

        public bool IsBehaviourRunning<T>() where T : ActorBehaviour
        {
            T behaviour = this.GetComponent<T>();
            return behaviour != null && behaviour.IsRunning;
        }
    }
}