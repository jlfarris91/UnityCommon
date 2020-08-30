using UnityEngine;

namespace ActorsLib
{
    using CommonLib;

    [RequireComponent(typeof(CapsuleCollider))]
    public class GroundingState : MonoBehaviour
    {
        // This will be a component on each player that is accessible to all other Actor behaviors
        // Constantly keep track of Grounding state and make Distance to Ground public.  Make RaycastHit publicly available too.

        public bool IsGrounded;
        public float DistanceToGround;
        public LayerMask FloorLayerMask;
        public float GroundCheckDistance = 0.2f;
        public float OriginalGroundCheckDistance { get; protected set; }
        public Vector3 GroundNormal;

        public bool GroundActor = true;
        private new CapsuleCollider collider;

        // Use this for initialization
        void Start()
        {
            this.collider = this.GetComponent<CapsuleCollider>();

            this.DistanceToGround = 0f;
            this.OriginalGroundCheckDistance = 0.2f;
        }

        void FixedUpdate()
        {
            RaycastHit raycastInfo;

            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character

            if (PhysicsEx.CapsuleCast(
                this.collider, 
                this.transform.position + (Vector3.up * 0.1f),
                Vector3.down,
                out raycastInfo, 
                this.GroundCheckDistance, 
                this.FloorLayerMask))
            {
                this.GroundNormal = raycastInfo.normal;
                this.IsGrounded = true;
            }
            else
            {
                this.IsGrounded = false;
                this.GroundNormal = Vector3.up;
            }

            this.DistanceToGround = raycastInfo.distance;
        }

        private void OnDrawGizmos()
        {
            GizmosEx.PushColor(Color.green);
            Gizmos.DrawRay(this.transform.position + Vector3.up * 0.1f, Vector3.down * this.GroundCheckDistance);
            Gizmos.DrawRay(this.transform.position, this.GroundNormal.normalized);
            GizmosEx.PopColor();
        }
    }
}
