namespace InputLib
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using CommonLib.Behaviours;
    using InControl;
    using PlayersLib;
    using UnityEngine;

    [RequireComponent(typeof(InputController))]
    public abstract class InputManager : SingletonBehaviour<InputManager>
    {
        private readonly InputDevice[] devices = new InputDevice[4];

        public bool ShowPlayerDevices;

        /// <summary>
        /// Gets the last <see cref="InputController"/> that provided input.
        /// </summary>
        public InputController AnyInputController { get; private set; }

        /// <summary>
        /// Called once when the <see cref="InputManager"/> is initialized.
        /// </summary>
        protected virtual void Start()
        {
            this.AnyInputController = this.GetComponent<InputController>();
            this.AddGamePadBindings(this.AnyInputController);
            this.AddKeyboardBindings(this.AnyInputController);
            InControl.InputManager.OnDeviceAttached += this.OnDeviceAttached;
            InControl.InputManager.OnDeviceDetached += this.OnDeviceDetached;
        }

        /// <summary>
        /// Fired when an <see cref="InputDevice"/> is attached.
        /// </summary>
        /// <param name="attachedDevice">The <see cref="InputDevice"/> attached.</param>
        private void OnDeviceAttached(InputDevice attachedDevice)
        {
            int assignedPlayerId = -1;

            // Try to reassign the newly attached device to the same player
            for (var i = 0; i < 4; ++i)
            {
                InputDevice device = this.devices[i];
                if (device != null && !device.IsAttached)
                {
                    if (device.Name == attachedDevice.Name &&
                        device.Meta == attachedDevice.Meta)
                    {
                        this.devices[i] = attachedDevice;
                        assignedPlayerId = i;
                        break;
                    }
                }
            }

            // Determine which player it was before reassigning devices
            if (assignedPlayerId != -1)
            {
                Player connectedPlayer = PlayerManager.Instance.LocalPlayers[assignedPlayerId];

                Debug.LogFormat("Reassigned gamepad to player {0}", connectedPlayer.PlayerName);

                Debug.Log(string.Format("Local player {0} connected gamepad.", connectedPlayer.Id));
            }
            else
            {
                this.AssignInputDevices();
            }
        }

        /// <summary>
        /// Fired when an <see cref="InputDevice"/> is detached.
        /// </summary>
        /// <param name="detachedDevice">The <see cref="InputDevice"/> detached.</param>
        private void OnDeviceDetached(InputDevice detachedDevice)
        {
            int assignedPlayerId = -1;

            for (var i = 0; i < 4; ++i)
            {
                InputDevice device = this.devices[i];
                if (device == detachedDevice)
                {
                    assignedPlayerId = i;
                    break;
                }
            }

            if (assignedPlayerId != -1)
            {
                Player disconnectedPlayer = PlayerManager.Instance.LocalPlayers[assignedPlayerId];
                
                Debug.Log(string.Format("Local player {0} disconnected gamepad.", disconnectedPlayer.Id));
            }
            else
            {
                this.AssignInputDevices();
            }
        }

        /// <summary>
        /// Enables all player input.
        /// </summary>
        public void EnablePlayerInput()
        {
            foreach (Player player in PlayerManager.Instance.LocalPlayers)
            {
                this.EnablePlayerInput(player);
            }
        }

        /// <summary>
        /// Enables a specific player's input.
        /// </summary>
        /// <param name="player">Enables this player's controller.</param>
        public void EnablePlayerInput(Player player)
        {
            InputController controller = player.GetComponent<InputController>();

            if (controller != null)
            {
                controller.UpdateActorControllers = true;
                controller.Flush();
            }
        }

        /// <summary>
        /// Disable all player input.
        /// </summary>
        public void DisablePlayerInput()
        {
            foreach (Player player in PlayerManager.Instance.LocalPlayers)
            {
                this.DisablePlayerInput(player);
            }
        }

        /// <summary>
        /// Disable a specific player's input.
        /// </summary>
        /// <param name="player">Disables this player's controller.</param>
        public void DisablePlayerInput(Player player)
        {
            InputController controller = player.GetComponent<InputController>();

            if (controller != null)
            {
                controller.UpdateActorControllers = false;
                controller.Flush();
            }
        }

        /// <summary>
        /// Returns true if the <see cref="Player"/> is using a connected device.
        /// </summary>
        /// <param name="player">The <see cref="Player"/>.</param>
        /// <returns>True if the <see cref="Player"/> is using a connected device.</returns>
        public bool IsDeviceConnected(Player player)
        {
            var inputController = player.GetComponent<InputController>();
            return inputController != null && inputController.InputDevice != null && inputController.InputDevice.IsAttached;
        }

        public void SetDevice(int index, InputDevice device)
        {
            this.devices[index] = device;
        }

        /// <summary>
        /// Vibrates a gamepad device.
        /// </summary>
        /// <param name="controller">The gamepad <see cref="InputController"/>.</param>
        /// <param name="power">The power of the vibration.</param>
        /// <param name="duration">The amount of time to vibrate in seconds.</param>
        public void VibrateDevice(InputController controller, float power, float duration)
        {
            this.StartCoroutine(this.VibrateDeviceCoroutine(controller, power, duration));
        }

        private IEnumerator VibrateDeviceCoroutine(InputController controller, float power, float duration)
        {
            controller.InputDevice.Vibrate(power);
            yield return new WaitForSeconds(duration);
            controller.InputDevice.StopVibration();
        }

        public IEnumerable<InputDevice> GetGamepadDevices()
        {
            return InControl.InputManager.Devices.Where(d => d.DeviceClass == InputDeviceClass.Controller);
        }

        /// <summary>
        /// Manually assign an <see cref="InputDevice"/> for each player.
        /// Assigns all gamepad devices first, then a keyboard.
        /// </summary>
        public void AssignInputDevices()
        {
            var lastWasGamePad = true;

            List<InputDevice> connectedGamepads = InputManager.Instance.GetGamepadDevices().ToList();
            int deviceCount = connectedGamepads.Count;

            for (var i = 0; i < PlayerManager.Instance.LocalPlayers.Length; i++)
            {
                Player player = PlayerManager.Instance.LocalPlayers[i];
                InputDevice device = i < deviceCount ? connectedGamepads[i] : null;

                // Gamepad
                if (device != null)
                {
                    if (player != null)
                    {
                        this.SetPlayerDevice(player, device);

                        if (player.Controller != null)
                        {
                            player.Controller.UpdateActorControllers = true;
                        }
                    }

                    lastWasGamePad = true;
                }
                // Keyboard
                else if (lastWasGamePad)
                {
                    if (player != null)
                    {
                        this.SetPlayerDevice(player, null);

                        if (player.Controller != null)
                        {
                            player.Controller.UpdateActorControllers = true;
                        }
                    }

                    lastWasGamePad = false;
                }
                // Disconnected
                else
                {
                    if (player != null)
                    {
                        this.SetPlayerDevice(player, InputDevice.Null);

                        if (player.Controller != null)
                        {
                            player.Controller.UpdateActorControllers = false;
                        }
                    }
                }

                InputManager.Instance.SetDevice(i, device);
            }
        }

        /// <summary>
        /// Assign an <see cref="InputDevice"/> to a <see cref="Player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/>.</param>
        /// <param name="device">The <see cref="InputDevice"/>.</param>
        public void SetPlayerDevice(Player player, InputDevice device)
        {
            player.Controller = player.GetComponent<InputController>();

            if (player.Controller == null)
            {
                Debug.LogErrorFormat("Could not find an InputController for player {0}", player.PlayerName);
                return;
            }
            
            player.Controller.InputDevice = device;

            // TODO: remember previous bindings?
            // Always clear bindings
            this.ClearBindings(player.Controller);
            
            // Assign keyboard bindings if the InputDevice is null (keyboard)
            if (device == null)
            {
                this.AddKeyboardBindings(player.Controller);
                Debug.LogFormat("Assigned keyboard device to player {0}", player.PlayerName);
            }
            // Otherwise, assign gamepad bindings if the InputDevice is not InputDevice.Null (no input)
            else if (device != InputDevice.Null)
            {
                this.AddGamePadBindings(player.Controller);
                Debug.LogFormat("Assigned gampad device {0} to player {1}", device.Name, player.PlayerName);
            }
            else
            {
                Debug.LogFormat("Assigned no device for player {0}", player.PlayerName);
            }
        }

        protected abstract void ClearBindings(InputController inputController);
        protected abstract void AddKeyboardBindings(InputController inputController);
        protected abstract void AddGamePadBindings(InputController inputController);

        private void OnGUI()
        {
            if (!this.ShowPlayerDevices)
            {
                return;
            }

            for (var i = 0; i < PlayerManager.Instance.LocalPlayers.Length; ++i)
            {
                if (PlayerManager.Instance.LocalPlayers[i] == null)
                {
                    continue;
                }

                InputDevice device = PlayerManager.Instance.LocalPlayers[i].Controller.InputDevice;

                string stateText = string.Empty;
                string deviceName = "Keyboard";

                if (device == InputDevice.Null)
                {
                    deviceName = "None";
                }
                else if (device != null)
                {
                    stateText = device.IsAttached ? " - Connected" : " - Disconnected";
                    deviceName = device.Name;
                }

                GUILayout.Label(string.Format("Player {0}: {1}{2}", i, deviceName, stateText));
            }
        }
    }
}