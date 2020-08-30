using System;
using UnityEngine.Events;

namespace ActorsLib
{
    [Serializable]
    public class ActorUnityEvent : UnityEvent<UnityEngine.Object, ActorEventArgs>
    {
    }
}