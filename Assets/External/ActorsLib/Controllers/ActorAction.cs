namespace ActorsLib.Controllers
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Represents an abstract action that can be activated. This is used mostly
    /// by <see cref="ActorController"/> as a layer of abstraction allowing for
    /// generic input.
    /// </summary>
    [Serializable]
    public class ActorAction
    {
        /// <summary>
        /// The name of the action.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The value of the action.
        /// </summary>
        public Vector2 Value = Vector2.zero;

        /// <summary>
        /// The value of the action in the last frame.
        /// </summary>
        public Vector2 LastValue = Vector2.zero;

        /// <summary>
        /// The tolerance to determine whether or not the action is active.
        /// </summary>
        public float Tolerance = 0.05f;

        /// <summary>
        /// The duration this action has been active.
        /// </summary>
        public float HeldTime;

        /// <summary>
        /// The icon assigned to this action.
        /// </summary>
        public Sprite Icon;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        public ActorAction(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets whether or not the action is active this frame.
        /// </summary>
        public bool IsActive
        {
            get { return Math.Abs(this.Value.sqrMagnitude) > this.Tolerance * this.Tolerance; }
        }

        /// <summary>
        /// Gets whether or not the action was active last frame.
        /// </summary>
        public bool WasActive
        {
            get { return Math.Abs(this.LastValue.sqrMagnitude) > this.Tolerance * this.Tolerance; }
        }

        /// <summary>
        /// Copies the state from another <see cref="ActorAction"/>.
        /// </summary>
        /// <param name="other">An <see cref="ActorAction"/> to copy state from.</param>
        public void CopyFrom(ActorAction other)
        {
            this.Value = other.Value;
            this.LastValue = other.LastValue;
            this.Tolerance = other.Tolerance;
            this.HeldTime = other.HeldTime;
            this.Icon = other.Icon;
        }

        /// <summary>
        /// Flushes the action by setting <see cref="HeldTime"/> to 0, <see cref="LastValue"/> to
        /// <see cref="Value"/> and <see cref="Value"/> to <see cref="Vector2.zero"/>. 
        /// </summary>
        public void Flush()
        {
            this.HeldTime = 0.0f;
            this.LastValue = this.Value;
            this.Value = Vector2.zero;
        }

        /// <summary>
        /// Converts an action's value to a <see cref="float"/>.
        /// </summary>
        /// <param name="action"></param>
        public static explicit operator float(ActorAction action)
        {
            return action.Value.x;
        }

        /// <summary>
        /// Converts an action's value to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="action"></param>
        public static explicit operator Vector2(ActorAction action)
        {
            return action.Value;
        }

        /// <summary>
        /// Returns whether or not the action is active this frame.
        /// </summary>
        /// <param name="action"></param>
        public static explicit operator bool(ActorAction action)
        {
            return action.IsActive;
        }

        /// <summary>
        /// Converts the action to a string.
        /// </summary>
        /// <returns>The action in the form of a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}", this.Name, this.Value);
        }
    }
}