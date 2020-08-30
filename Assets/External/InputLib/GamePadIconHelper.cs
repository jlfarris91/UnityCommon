namespace InputLib
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Responsible for easily getting game pad icons.
    /// </summary>
    public static class GamePadIconHelper
    {
        private const string ControllerSpritePath = "Sprites/UI/controller";

        /// <summary>
        /// Gets a button sprite frame from "Sprites\UI\controller.png"
        /// </summary>
        /// <param name="button">The button mapped to the sprite.</param>
        public static Sprite GetIcon(GamePadButton button)
        {
            int iconPath;
            switch (button)
            {
                case GamePadButton.A:
                    iconPath = 62;
                    break;
                case GamePadButton.B:
                    iconPath = 63;
                    break;
                case GamePadButton.X:
                    iconPath = 65;
                    break;
                case GamePadButton.Y:
                    iconPath = 64;
                    break;
                case GamePadButton.Start:
                    iconPath = 67;
                    break;
                case GamePadButton.Back:
                    iconPath = 66;
                    break;
                case GamePadButton.Guide:
                    iconPath = 62;
                    break;
                case GamePadButton.LeftShoulder:
                    iconPath = 68;
                    break;
                case GamePadButton.RightShoulder:
                    iconPath = 69;
                    break;
                case GamePadButton.LeftStick:
                    iconPath = 60;
                    break;
                case GamePadButton.RightStick:
                    iconPath = 61;
                    break;
                case GamePadButton.DpadLeft:
                    iconPath = 56;
                    break;
                case GamePadButton.DpadRight:
                    iconPath = 57;
                    break;
                case GamePadButton.DpadUp:
                    iconPath = 54;
                    break;
                case GamePadButton.DpadDown:
                    iconPath = 55;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("button", button, null);
            }

            return Resources.LoadAll<Sprite>(GamePadIconHelper.ControllerSpritePath)[iconPath];
        }

        /// <summary>
        /// Gets a trigger sprite frame from "Sprites\UI\controller.png"
        /// </summary>
        /// <param name="trigger">The trigger mapped to the sprite.</param>
        public static Sprite GetIcon(GamePadTrigger trigger)
        {
            int iconPath;
            switch (trigger)
            {
                case GamePadTrigger.Right:
                    iconPath = 71;
                    break;
                case GamePadTrigger.Left:
                    iconPath = 70;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("trigger", trigger, null);
            }

            return Resources.LoadAll<Sprite>(GamePadIconHelper.ControllerSpritePath)[iconPath];
        }

        /// <summary>
        /// Gets a thumbstick sprite frame from "Sprites\UI\controller.png"
        /// </summary>
        /// <param name="thumbstick">The thumbstick mapped to the sprite.</param>
        public static Sprite GetIcon(GamePadThumbStick thumbstick)
        {
            int iconPath;
            switch (thumbstick)
            {
                case GamePadThumbStick.Right:
                    iconPath = 61;
                    break;
                case GamePadThumbStick.Left:
                    iconPath = 60;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("thumbstick", thumbstick, null);
            }

            return Resources.LoadAll<Sprite>(GamePadIconHelper.ControllerSpritePath)[iconPath];
        }
    }
}