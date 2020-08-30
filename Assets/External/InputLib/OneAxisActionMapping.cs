namespace InputLib
{
    using System;
    using ActorsLib.Controllers;
    using CommonLib;
    using InControl;

    /// <summary>
    /// Maps a <see cref="PlayerAction"/> to a <see cref="ActorAction"/>.
    /// </summary>
    public class OneAxisActionMapping : Mapping
    {
        private readonly Func<PlayerActionSet, PlayerAction> getter;

        public string ActionName
        {
            get { return this.Key as string; }
            set { this.Key = value; }
        }

        public OneAxisActionMapping(string actionName, Func<PlayerActionSet, PlayerAction> getter)
        {
            ThrowIf.ArgumentIsNull(getter, "getter");
            this.ActionName = actionName;
            this.getter = getter;
        }

        public PlayerAction GetPlayerAction(PlayerActionSet actionSet)
        {
            ThrowIf.ArgumentIsNull(actionSet, "actionSet");
            return this.getter(actionSet);
        }
    }
}