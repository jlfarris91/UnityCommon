namespace InputLib
{
    using System;
    using ActorsLib.Controllers;
    using CommonLib;
    using InControl;

    /// <summary>
    /// Maps a <see cref="PlayerTwoAxisAction"/> to a <see cref="ActorAction"/>.
    /// </summary>
    public class TwoAxisActionMapping : Mapping
    {
        private readonly Func<PlayerActionSet, PlayerTwoAxisAction> getter;

        public bool TransformToCameraLocal = false;

        public string ActionName
        {
            get { return this.Key as string; }
            set { this.Key = value; }
        }

        public TwoAxisActionMapping(string actionName, Func<PlayerActionSet, PlayerTwoAxisAction> getter)
        {
            ThrowIf.ArgumentIsNull(getter, "getter");
            this.ActionName = actionName;
            this.getter = getter;
        }

        public PlayerTwoAxisAction GetPlayerAction(PlayerActionSet actionSet)
        {
            ThrowIf.ArgumentIsNull(actionSet, "actionSet");
            return this.getter(actionSet);
        }
    }
}