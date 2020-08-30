using System;
using UnityEngine;

namespace ActorsLib
{
    using CommonLib.Collections;

    [Serializable]
    public class ActorCoroutineDictionary : SafeDictionary<Actor, Coroutine>
    {
    }
}