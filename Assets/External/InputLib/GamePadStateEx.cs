namespace InputLib
{
    using UnityEngine;
    using XInputDotNetPure;

    public enum GamePadButton
    {
        A,
        B,
        X,
        Y,
        Start,
        Back,
        Guide,
        LeftShoulder,
        RightShoulder,
        LeftStick,
        RightStick,
        DpadLeft,
        DpadRight,
        DpadUp,
        DpadDown
    }

    public enum GamePadTrigger
    {
        Right,
        Left
    }

    public enum GamePadThumbStick
    {
        Right,
        Left
    }

    internal static class GamePadStateEx
    {

        public static float GetTrigger(this GamePadState gamePad, GamePadTrigger gamePadTrigger)
        {
            switch (gamePadTrigger)
            {
                case GamePadTrigger.Left:
                    return gamePad.Triggers.Left;
                case GamePadTrigger.Right:
                    return gamePad.Triggers.Right;
                default:
                    return 0.0f;
            }
        }

        public static Vector2 GetThumbstick(this GamePadState gamePad, GamePadThumbStick stick)
        {
            switch (stick)
            {
                case GamePadThumbStick.Left:
                    return new Vector2(gamePad.ThumbSticks.Left.X, gamePad.ThumbSticks.Left.Y);
                case GamePadThumbStick.Right:
                    return new Vector2(gamePad.ThumbSticks.Right.X, gamePad.ThumbSticks.Right.Y);
                default:
                    return Vector2.zero;
            }
        }

        public static bool IsButtonPressed(this GamePadState gamePad, GamePadButton gamePadButton)
        {
            return gamePad.IsButton(gamePadButton, ButtonState.Pressed);
        }

        public static bool WasButtonPressed(this GamePadState gamePad, GamePadButton gamePadButton)
        {
            return false;
        }

        public static bool IsButtonReleased(this GamePadState gamePad, GamePadButton gamePadButton)
        {
            return gamePad.IsButton(gamePadButton, ButtonState.Released);
        }

        public static bool IsButtonTriggered(this GamePadState gamePad, GamePadButton gamePadButton)
        {
            return gamePad.IsButtonPressed(gamePadButton) && !gamePad.WasButtonPressed(gamePadButton);
        }

        public static bool IsButton(this GamePadState gamePad, GamePadButton gamePadButton, ButtonState state)
        {
            switch (gamePadButton)
            {
                case GamePadButton.A:
                    return gamePad.Buttons.A == state;
                case GamePadButton.B:
                    return gamePad.Buttons.B == state;
                case GamePadButton.X:
                    return gamePad.Buttons.X == state;
                case GamePadButton.Y:
                    return gamePad.Buttons.Y == state;
                case GamePadButton.Start:
                    return gamePad.Buttons.Start == state;
                case GamePadButton.Back:
                    return gamePad.Buttons.Back == state;
                case GamePadButton.LeftShoulder:
                    return gamePad.Buttons.LeftShoulder == state;
                case GamePadButton.RightShoulder:
                    return gamePad.Buttons.RightShoulder == state;
                case GamePadButton.LeftStick:
                    return gamePad.Buttons.LeftStick == state;
                case GamePadButton.RightStick:
                    return gamePad.Buttons.RightStick == state;
                case GamePadButton.DpadLeft:
                    return gamePad.DPad.Left == state;
                case GamePadButton.DpadRight:
                    return gamePad.DPad.Right == state;
                case GamePadButton.DpadDown:
                    return gamePad.DPad.Down == state;
                case GamePadButton.DpadUp:
                    return gamePad.DPad.Up == state;
                default:
                    return false;
            }
        }
    }
}