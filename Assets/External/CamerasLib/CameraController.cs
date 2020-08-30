using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CamerasLib
{
    using CommonLib;

    public class CameraController : MonoBehaviour
    {
        public Vector3 Forward = Vector3.zero;
        public float MovementSmoothTime;
        public float MinDistance;
        public float MaxDistance;
        public float Offset;
        public Camera FrustumCamera;
        public float LetterboxValue = 0.0f;

        [ReadOnly]
        public float Distance;

        private Vector3 targetPosition;
        private Vector3 positionVelocity;

        private Bounds worldSpacePlayerBounds;

        private void LateUpdate()
        {
            this.worldSpacePlayerBounds = this.CalculateWorldSpacePlayerBoundingBox();

            this.targetPosition = this.FindPosition(10, 10.0f);

            this.Distance = Vector3.Distance(this.transform.position, this.worldSpacePlayerBounds.center);

            this.MoveTowardsTarget();
            this.LookAtTarget();
        }

        private void MoveTowardsTarget()
        {
            Vector3 position = Vector3.SmoothDamp(
                this.transform.position,
                this.targetPosition,
                ref this.positionVelocity,
                this.MovementSmoothTime,
                999.0f,
                Time.deltaTime);

            this.transform.position = position;
        }

        private void LookAtTarget()
        {
            this.transform.forward = this.Forward.normalized;
        }

        private Bounds CalculateWorldSpacePlayerBoundingBox()
        {
            Vector3[] positions = CameraFollowTarget.ActiveTargets.ExceptNull().Select(a => a.transform.position * a.Weight).ToArray();

            if (!positions.Any())
            {
                return new Bounds();
            }

            // Create world-space rect surrounding the actors
            Vector3 min;
            Vector3 max;
            Vector3Ex.GetMinMax(positions, out min, out max);

            return new Bounds {min = min, max = max};
        }

        private float ClampDistance(float distance)
        {
            float actualMinDistance = Mathf.Min(this.MinDistance, this.MaxDistance);
            float actualMaxDistance = Mathf.Max(this.MinDistance, this.MaxDistance);

            return Mathf.Clamp(distance, actualMinDistance, actualMaxDistance);
        }

        private Vector3 FindPosition(int iterations, float moveDist)
        {
            Vector3 pos = this.worldSpacePlayerBounds.center;
            Vector3 forward = this.Forward.normalized;
            float distance = 0.0f;
            bool movingBack = true;

            distance = this.ClampDistance(distance);

            Vector3 origPos = this.transform.position;

            HashSet<CameraFollowTarget> cameraTargets = CameraFollowTarget.ActiveTargets;

            if (!cameraTargets.Any())
            {
                return this.targetPosition;
            }

            Bounds[] bounds = cameraTargets
                .Select(a => new Bounds(a.transform.position + Vector3.up * 0.5f, new Vector3(0.25f, 0.5f, 0.25f)))
                .ToArray();

            do
            {
                this.transform.position = pos - forward * distance;
            
                Plane[] cameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(this.FrustumCamera);

                Plane downPlane = cameraFrustumPlanes[2];
                Plane upPlane = cameraFrustumPlanes[3];

                downPlane.SetNormalAndPosition(downPlane.normal, this.FrustumCamera.transform.position - downPlane.normal * this.LetterboxValue);
                upPlane.SetNormalAndPosition(upPlane.normal, this.FrustumCamera.transform.position - upPlane.normal * this.LetterboxValue);

                cameraFrustumPlanes[2] = downPlane;
                cameraFrustumPlanes[3] = upPlane;

                bool cameraContainsPlayers = bounds.All(b => GeometryUtility.TestPlanesAABB(cameraFrustumPlanes, b));

                // If the camera doesn't contain the player bounds then to move 
                // it back until it does
                if (!cameraContainsPlayers)
                {
                    distance += moveDist;

                    // If we were previously moving forward and have switched
                    // directions then reduce the move speed
                    if (!movingBack)
                    {
                        moveDist *= 0.5f;
                    }

                    movingBack = true;
                }
                else
                {
                    distance -= moveDist;

                    // If we were previously moving backward and have switched
                    // directions then reduce the move speed
                    if (movingBack)
                    {
                        moveDist *= 0.5f;
                    }

                    movingBack = false;
                }

            } while (iterations-- > 0);

            this.transform.position = origPos;

            distance = this.ClampDistance(distance) + this.Offset;

            return pos - forward * distance;
        }

        private void OnDrawGizmos()
        {
            Vector3 pos = this.worldSpacePlayerBounds.center;
            Vector3 size = this.worldSpacePlayerBounds.size;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(pos, size);
            GizmosEx.DrawArrow(pos, pos + this.Forward.normalized, 0.1f);
        }
    }
}