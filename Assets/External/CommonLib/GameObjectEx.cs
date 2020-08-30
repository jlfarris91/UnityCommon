namespace CommonLib
{
    using CommonLib.Behaviours;
    using UnityEngine;

    internal static class GameObjectEx
    {
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject != null && gameObject.GetComponent<T>() != null;
        }

        public static bool ParentHasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject != null && gameObject.GetComponentInParent<T>() != null;
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        public static GameObject GetRoot(this GameObject gameObject)
        {
            if (gameObject.transform.parent != null)
            {
                return gameObject.transform.parent.gameObject.GetRoot();
            }
            return gameObject;
        }

        public static FollowTarget FollowTarget(this GameObject gameObject, GameObject target, bool moveToTarget = false)
        {
            var followTarget = gameObject.GetComponent<FollowTarget>();

            if (followTarget == null)
            {
                followTarget = gameObject.gameObject.AddComponent<FollowTarget>();
            }

            followTarget.Target = target.transform;

            if (moveToTarget)
            {
                followTarget.Move(global::CommonLib.Behaviours.FollowTarget.MovementType.Instant);
            }

            return followTarget;
        }

        public static FollowTarget FollowTarget(this GameObject gameObject,
                                                GameObject target,
                                                Vector3 offset,
                                                bool moveToTarget = false)
        {
            var followTarget = gameObject.GetComponent<FollowTarget>();

            if (followTarget == null)
            {
                followTarget = gameObject.gameObject.AddComponent<FollowTarget>();
            }

            followTarget.Target = target.transform;
            followTarget.Offset = offset;

            if (moveToTarget)
            {
                followTarget.Move(global::CommonLib.Behaviours.FollowTarget.MovementType.Instant);
            }

            return followTarget;
        }

        public static FollowTarget FollowTarget(this GameObject gameObject,
                                                Transform target,
                                                Vector3 offset,
                                                FollowTarget.MovementType movementType,
                                                float smoothTime = 0.0f,
                                                bool moveToTarget = false)
        {
            var followTarget = gameObject.GetComponent<FollowTarget>();

            if (followTarget == null)
            {
                followTarget = gameObject.gameObject.AddComponent<FollowTarget>();
            }

            followTarget.Target = target.transform;
            followTarget.Movement = movementType;
            followTarget.SmoothTime = smoothTime;
            followTarget.Offset = offset;

            if (moveToTarget)
            {
                followTarget.Move(global::CommonLib.Behaviours.FollowTarget.MovementType.Instant);
            }

            return followTarget;
        }

        public static LookAt LookAt(this GameObject gameObject, GameObject target)
        {
            var lookAt = gameObject.GetComponent<LookAt>();
            if (lookAt == null)
            {
                lookAt = gameObject.gameObject.AddComponent<LookAt>();
            }
            lookAt.Target = target;
            return lookAt;
        }

        public static void DestroyAfter(this GameObject gameObject, float seconds)
        {
            var destroyAfter = gameObject.GetComponent<DestroyAfter>();
            // 0.0 seconds destroys the gameObject
            if (Mathf.Approximately(seconds, 0.0f))
            {
                Object.Destroy(destroyAfter.gameObject);
                return;
            }
            // Otherwise add the gameObject
            if (destroyAfter == null)
            {
                destroyAfter = gameObject.gameObject.AddComponent<DestroyAfter>();
            }
            destroyAfter.Duration = seconds;
        }

        public static void ShakeFor(this GameObject gameObject, float magnitude, float duration)
        {
            var shakeComponent = gameObject.GetComponent<Shake>();
            if (shakeComponent == null)
            {
                shakeComponent = gameObject.AddComponent<Shake>();
            }
            shakeComponent.ShakeFor(magnitude, duration);
        }

        public static void SetActiveAfter(this GameObject gameObject, float seconds, bool active)
        {
            var setActiveAfter = gameObject.GetOrAddComponent<SetActiveAfter>();
            setActiveAfter.SetActive(seconds, active);
        }
    }
}