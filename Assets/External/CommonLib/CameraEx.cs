using UnityEngine;

namespace CommonLib
{
    public static class CameraEx
    {
        public static Vector3 TransformWorldToLocalBasedOnCamera(Camera camera, Vector3 vector)
        {
            if (camera == null)
            {
                return vector;
            }

            Vector3 cameraForwardXZ = camera.transform.forward;
            cameraForwardXZ.y = 0.0f;
            cameraForwardXZ.Normalize();

            float angle = MathEx.GetAngle(cameraForwardXZ.ToXZVector2()) - 90.0f;

            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.up);
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);

            return matrix * vector;
        }
    }
}