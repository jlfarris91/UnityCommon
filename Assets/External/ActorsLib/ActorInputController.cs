namespace ActorsLib
{
    using System.Collections.Generic;
    using ActorsLib.Controllers;
    using CommonLib;
    using InControl;
    using InputLib;
    using UnityEngine;

    public abstract class ActorInputController : InputController
    {
        public List<ActorController> Controllers = new List<ActorController>();

        public override void Flush()
        {
            foreach (ActorController controller in this.Controllers)
            {
                foreach (ActorAction action in controller.Actions.Values)
                {
                    Mapping mapping;
                    if (!this.Mappings.TryGetValue(action.Name, out mapping))
                    {
                        continue;
                    }

                    action.Flush();
                }
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            // Flush all actions on all controllers. If a player was holding down
            // a button when their input is disabled this will clear out any values.
            this.Controllers.ExceptNull().Foreach(controller => controller.Flush());
        }

        protected override void Update()
        {
            base.Update();
            
            foreach (ActorController controller in this.Controllers)
            {
                this.UpdateMappedActions(controller);
            }
        }

        /// <summary>
        /// Update the actions of an ActorController.
        /// </summary>
        /// <param name="controller">The <see cref="ActorController"/> to update.</param>
        private void UpdateMappedActions(ActorController controller)
        {
            if (!this.UpdateActorControllers)
            {
                return;
            }

            if (controller == null)
            {
                return;
            }

            foreach (ActorAction action in controller.Actions.Values)
            {
                Mapping mapping;
                if (!this.Mappings.TryGetValue(action.Name, out mapping))
                {
                    continue;
                }

                var oneAxisMapping = mapping as OneAxisActionMapping;
                var twoAxisMapping = mapping as TwoAxisActionMapping;

                if (oneAxisMapping != null)
                {
                    PlayerAction mappedAction = oneAxisMapping.GetPlayerAction(this.ActiveActionSet);
                    action.Value = new Vector2(mappedAction.Value, 0.0f);
                }

                if (twoAxisMapping != null)
                {
                    PlayerTwoAxisAction mappedAction = twoAxisMapping.GetPlayerAction(this.ActiveActionSet);

                    Vector2 value = mappedAction.Value;

                    if (twoAxisMapping.TransformToCameraLocal && this.Camera != null)
                    {
                        value = CameraEx.TransformWorldToLocalBasedOnCamera(
                            this.Camera,
                            value.ToXZVector3()).ToXZVector2();
                    }

                    action.Value = value;
                }
            }
        }
    }
}
