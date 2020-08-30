namespace ActorsLib.Controllers
{
    using UnityEngine;

    /// <summary>
    /// An interface for an abstracted controller class using <see cref="ActorAction"/>s.
    /// </summary>
    public interface IActorController
    {
        /// <summary>
        /// Activates an action, giving it a value of <see cref="Vector2.one"/>.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        void Activate(string name);

        /// <summary>
        /// Actives an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="value">The value to activate with.</param>
        void Activate(string name, Vector2 value);

        /// <summary>
        /// Deactives an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        void Deactivate(string name);

        /// <summary>
        /// Determines if an action is active this frame.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>True if the action is active this frame.</returns>
        bool IsActive(string name);

        /// <summary>
        /// Determines if an action was active in the last frame.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>True if the action was active last frame.</returns>
        bool WasActive(string name);

        /// <summary>
        /// Is active this frame but was not active last frame.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsTriggered(string name);

        /// <summary>
        /// Returns true if the action has been active for the given duration.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="duration">A duration in seconds.</param>
        /// <returns>True if the action has been active for the given duration.</returns>
        bool IsHeldFor(string name, float duration);

        /// <summary>
        /// Gets the x value of an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The x value component of the action value.</returns>
        float GetFloat(string name);

        /// <summary>
        /// Gets the non-normalized value of an action. If you are looking for
        /// the normalized value use <see cref="GetDirection"/>.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The value of an action.</returns>
        Vector2 GetVector(string name);

        /// <summary>
        /// Gets the normalized value of an action. If you are looking for
        /// the non-normalized value use <see cref="GetVector"/>.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The normalized value of an action.</returns>
        Vector2 GetDirection(string name);

        /// <summary>
        /// Gets the value an action clamped to a circle.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The value of an action clamped to a circle.</returns>
        Vector2 GetRadial(string name);

        /// <summary>
        /// Gets the angle of an action's value in degrees.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The angle of an action's value in degrees.</returns>
        float GetAngle(string name);

        /// <summary>
        /// Clears an action's hold time and value.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        void Flush(string name);

        /// <summary>
        /// Clears all actions' hold time and value.
        /// </summary>
        void Flush();

        /// <summary>
        /// Gets the icon assigned to an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The icon assigned to an action.</returns>
        Sprite GetIcon(string name);

        /// <summary>
        /// Updates the state of the actions.
        /// </summary>
        void Update();

        /// <summary>
        /// Updates the state of the actions.
        /// </summary>
        void LateUpdate();
    }
}
