using UnityEngine;

namespace CommonLib
{
    public static class PhysicsEx
    {
        public static bool CapsuleCast(CapsuleCollider collider, Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, LayerMask mask)
        {
            float colliderHalfHeight = (collider.height * 0.5f) - collider.radius;

            origin += collider.center;

            Vector3 colliderPoint1 = origin + collider.transform.up * colliderHalfHeight;
            Vector3 colliderPoint2 = origin - collider.transform.up * colliderHalfHeight;

            return Physics.CapsuleCast(
                colliderPoint1,
                colliderPoint2,
                collider.radius,
                direction,
                out hitInfo,
                maxDistance,
                mask);
        }

        public static RaycastHit[] CapsuleCastAll(CapsuleCollider collider, Vector3 start, Vector3 end, LayerMask mask)
        {
            float colliderHalfHeight = (collider.height * 0.5f) - collider.radius;

            Vector3 verticalOffset = collider.center;

            start += verticalOffset;
            end += verticalOffset;

            Vector3 colliderPoint1 = start + collider.transform.up * colliderHalfHeight;
            Vector3 colliderPoint2 = start - collider.transform.up * colliderHalfHeight;

            Vector3 castDirection = end - start;
            float castLength = castDirection.magnitude;

            return Physics.CapsuleCastAll(
                colliderPoint1,
                colliderPoint2,
                collider.radius,
                castDirection.normalized,
                castLength,
                mask);
        }
    }
}