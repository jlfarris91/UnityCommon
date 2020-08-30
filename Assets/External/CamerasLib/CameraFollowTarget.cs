using System.Collections.Generic;
using UnityEngine;

namespace CamerasLib
{
    public class CameraFollowTarget : MonoBehaviour
    {
        public static readonly HashSet<CameraFollowTarget> ActiveTargets = new HashSet<CameraFollowTarget>();

        public float Weight = 1.0f;

        private void OnEnable()
        {
            CameraFollowTarget.ActiveTargets.Add(this);
        }

        private void OnDisable()
        {
            CameraFollowTarget.ActiveTargets.Remove(this);
        }

        private void OnDestroy()
        {
            CameraFollowTarget.ActiveTargets.Remove(this);
        }
    }
}