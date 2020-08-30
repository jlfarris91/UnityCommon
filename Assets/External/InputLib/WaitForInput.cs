namespace InputLib
{
    using InControl;
    using UnityEngine;

    public class WaitForInput : CustomYieldInstruction
    {
        private readonly PlayerAction action;

        public WaitForInput(PlayerAction action)
        {
            this.action = action;
        }

        public override bool keepWaiting
        {
            get { return !this.action.WasPressed; }
        }
    }
}