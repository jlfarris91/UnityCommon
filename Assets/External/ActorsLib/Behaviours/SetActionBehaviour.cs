namespace ActorsLib.Behaviours
{
    using System.Collections;
    using ActorsLib.Controllers;
    using UnityEngine;

    public class SetActionBehaviour : ActorBehaviour
    {
        [ActionTag]
        public string Action;

        public Vector2 Value;

        public float Cooldown = 0.0f;

        private void OnEnable()
        {
            this.StartCoroutine(this.DoActionCoroutine());
        }

        private void OnDisable()
        {
            this.StopAllCoroutines();
        }

        private IEnumerator DoActionCoroutine()
        {
            while (true)
            {
                ActorAction action;
                if (this.Actor.Controller.Actions.TryGetValue(this.Action, out action))
                {
                    action.Value = this.Value;
                }

                yield return new WaitForSeconds(this.Cooldown);
            }
        }
    }
}
