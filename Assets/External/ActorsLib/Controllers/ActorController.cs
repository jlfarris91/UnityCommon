namespace ActorsLib.Controllers
{
    using System;
    using System.Collections.Generic;
    using CommonLib;
    using UnityEngine;

    [Serializable]
    public class ActorController : MonoBehaviour, IActorController
    {
        public bool ShowValues = false;

        /// <summary>
        /// The dictionary of actions for quick access by name.
        /// </summary>
        private readonly Dictionary<string, ActorAction> actions = new Dictionary<string, ActorAction>();

        /// <summary>
        /// Gets the dictionary of actions.
        /// </summary>
        public virtual IDictionary<string, ActorAction> Actions
        {
            get { return this.actions; }
        }

        /// <summary>
        /// Activates an action, giving it a value of <see cref="Vector2.one"/>.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        public void Activate(string name)
        {
            this.Activate(name, Vector2.one);
        }

        /// <summary>
        /// Actives an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="value">The value to activate with.</param>
        public void Activate(string name, Vector2 value)
        {
            ActorAction action = this.GetOrCreateAction(name);
            if (action != null)
            {
                action.Value = value;
            }
        }

        /// <summary>
        /// Activates an action by copying values from another.
        /// </summary>
        /// <param name="name">The name of the action to activate.</param>
        /// <param name="other">The source action.</param>
        public void Activate(string name, ActorAction other)
        {
            ActorAction action = this.GetOrCreateAction(name);
            if (action != null)
            {
                action.CopyFrom(other);
            }
        }

        /// <summary>
        /// Deactives an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        public void Deactivate(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            if (action != null)
            {
                action.Value = Vector2.zero;
            }
        }

        /// <summary>
        /// Determines if an action is active this frame.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>True if the action is active this frame.</returns>
        public bool IsActive(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            return action != null && action.IsActive;
        }

        /// <summary>
        /// Determines if an action was active in the last frame.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>True if the action was active last frame.</returns>
        public bool WasActive(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            return action != null && action.WasActive;
        }

        /// <summary>
        /// Is active this frame but was not active last frame.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsTriggered(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            return action != null && action.IsActive && !action.WasActive;
        }

        /// <summary>
        /// Is not active this frame but was active last frame.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsReleased(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            return action != null && !action.IsActive && action.WasActive;
        }

        /// <summary>
        /// Returns true if the action has been active for the given duration.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="duration">A duration in seconds.</param>
        /// <returns>True if the action has been active for the given duration.</returns>
        public bool IsHeldFor(string name, float duration)
        {
            ActorAction action = this.GetOrCreateAction(name);
            return action != null && action.HeldTime > duration;
        }

        /// <summary>
        /// Gets the x value of an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The x value component of the action value.</returns>
        public float GetFloat(string name)
        {
            return this.GetVector(name).magnitude;
        }

        /// <summary>
        /// Gets the non-normalized value of an action. If you are looking for
        /// the normalized value use <see cref="IActorController.GetDirection"/>.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The value of an action.</returns>
        public Vector2 GetVector(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            return action != null ? action.Value : Vector2.zero;
        }

        /// <summary>
        /// Gets the normalized value of an action. If you are looking for
        /// the non-normalized value use <see cref="IActorController.GetVector"/>.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The normalized value of an action.</returns>
        public Vector2 GetDirection(string name)
        {
            return this.GetVector(name).normalized;
        }

        /// <summary>
        /// Gets the value an action clamped to a circle.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The value of an action clamped to a circle.</returns>
        public Vector2 GetRadial(string name)
        {
            return this.GetVector(name).AsRadial();
        }

        /// <summary>
        /// Gets the angle of an action's value in degrees.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The angle of an action's value in degrees.</returns>
        public float GetAngle(string name)
        {
            return MathEx.GetAngle(this.GetVector(name).normalized);
        }

        /// <summary>
        /// Clears an action's hold time and value.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        public void Flush(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            action.Flush();
        }

        /// <summary>
        /// Clears all actions' hold time and value.
        /// </summary>
        public void Flush()
        {
            this.Actions.Values.Foreach(action => action.Flush());
        }

        /// <summary>
        /// Gets the icon assigned to an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The icon assigned to an action.</returns>
        public Sprite GetIcon(string name)
        {
            ActorAction action = this.GetOrCreateAction(name);
            return action != null ? action.Icon : null;
        }

        /// <summary>
        /// Gets a registered action by name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns></returns>
        protected ActorAction GetOrCreateAction(string name)
        {
            if (!this.actions.ContainsKey(name))
            {
                this.actions.Add(name, new ActorAction(name));
            }
            return this.actions[name];
        }

        /// <summary>
        /// Updates the state of the actions.
        /// </summary>
        public void Update()
        {
            foreach (ActorAction action in this.actions.Values)
            {
                if (action.IsActive)
                {
                    action.HeldTime += Time.deltaTime;
                }
                else
                {
                    action.HeldTime = 0.0f;
                }
            }
        }

        /// <summary>
        /// Updates the state of the actions.
        /// </summary>
        public void LateUpdate()
        {
            foreach (ActorAction action in this.actions.Values)
            {
                action.LastValue = action.Value;
            }
        }

        private void OnGUI()
        {
            if (this.ShowValues)
            {
                GUILayout.BeginVertical();

                foreach (var action in this.actions)
                {
                    GUILayout.Label(string.Format("{0}: {1}", action.Key, action.Value.LastValue));
                }

                GUILayout.EndVertical();
            }
        }
    }
}