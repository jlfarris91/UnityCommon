namespace InputLib
{
    using System.Collections.Generic;
    using System.Linq;
    using InControl;
    using UnityEngine;

    public class InputController : MonoBehaviour
    {
        public bool UpdateActorControllers = true;

        public Camera Camera;

        public List<PlayerActionSet> ActionSets = new List<PlayerActionSet>();

        public PlayerActionSet ActiveActionSet;

        /// <summary>
        /// Maps action names to <see cref="Mapping"/>s.
        /// </summary>
        public Dictionary<string, Mapping> Mappings = new Dictionary<string, Mapping>();

        /// <summary>
        /// Gets or sets the <see cref="InputDevice"/> assigned to the <see cref="InputController"/>.
        /// </summary>
        public InputDevice InputDevice
        {
            get
            {
                var firstActionSet = this.ActionSets.FirstOrDefault();
                return firstActionSet != null ? firstActionSet.Device : null;
            }

            set
            {
                foreach (PlayerActionSet actionSet in this.ActionSets)
                {
                    actionSet.Device = value;
                }
            }
        }

        /// <summary>
        /// Gets whether or not the input device is a keyboard
        /// </summary>
        public bool IsKeyboard
        {
            get { return this.InputDevice == null; }
        }

        /// <summary>
        /// Gets whether or not the input device is a keyboard
        /// </summary>
        public bool IsGamePad
        {
            get { return this.InputDevice != null && this.InputDevice.DeviceClass == InputDeviceClass.Controller; }
        }

        public virtual void Flush()
        {
        }

        /// <summary>
        /// Called once when the <see cref="InputController"/> is initialized.
        /// </summary>
        protected virtual void Awake()
        {
            this.RegisterDefaultMappings();
        }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        protected virtual void Update()
        {
        }

        /// <summary>
        /// Called when the InputController is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// Register the default mappings that map an ActionSet to an ActorController.
        /// </summary>
        protected virtual void RegisterDefaultMappings()
        {
        }
    }
}
