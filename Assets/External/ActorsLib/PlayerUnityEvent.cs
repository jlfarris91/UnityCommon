namespace ActorsLib
{
    using System;
    using PlayersLib;
    using Object = UnityEngine.Object;

    public class PlayerAffectedEventArgs : AffectedEventArgs
    {
        public PlayerAffectedEventArgs(Player player, Object affector)
            : base(player, affector)
        {
        }

        public new Player Affected
        {
            get { return base.Affected as Player; }
        }
    }

    [Serializable]
    public class PlayerUnityEvent : AffectedUnityEvent<PlayerAffectedEventArgs>
    {
    }
}