using UnityEngine;

namespace ActorsLib
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(GroundingState))]
    public class GravityModifier : MonoBehaviour
    {
        //[Range(1f, 4f)]
        public float gravityMultiplier = 2f;
        public float terminalVelocity = 8f;

        private new Rigidbody rigidbody;
        private GroundingState groundingState;

        private void Start()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
            this.groundingState = this.GetComponent<GroundingState>();
        }

        private void FixedUpdate()
        {
            if (!this.groundingState.IsGrounded && this.rigidbody.velocity.y > -this.terminalVelocity)
            {
                // apply extra gravity from multiplier:
                Vector3 extraGravityForce = (Physics.gravity * this.gravityMultiplier) - Physics.gravity;
                this.rigidbody.AddForce(extraGravityForce, ForceMode.Acceleration);
            }

            this.groundingState.GroundCheckDistance = this.rigidbody.velocity.y < 0 ? this.groundingState.OriginalGroundCheckDistance : this.groundingState.GroundCheckDistance; //0.2f;// 0.01f;
        }
    }
}
