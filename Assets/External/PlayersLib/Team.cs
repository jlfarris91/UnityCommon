namespace PlayersLib
{
    using System;
    using ActorsLib;
    using CommonLib.Collections;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Serializable]
    public class Team : MonoBehaviour
    {
        public string Id;
    }

    public class TeamAffectedEventArgs : AffectedEventArgs
    {
        public TeamAffectedEventArgs(Team team, Object affector)
            : base(team, affector)
        {
        }

        public new Team Affected
        {
            get { return base.Affected as Team; }
        }
    }

    [Serializable]
    public class TeamUnityEvent : AffectedUnityEvent<TeamAffectedEventArgs>
    {
    }

    [Serializable]
    public class TeamToIntDictionary : SafeDictionary<Team, int>
    {
    }
}