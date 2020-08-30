namespace CamerasLib
{
    using CommonLib;
    using UnityEngine;

    public static class CameraEx
    {
        public static CameraBlender BlendTo(this Camera camera, Camera target, bool smooth)
        {
            ThrowIf.ArgumentIsNull(camera, "camera");

            var cameraBlender = camera.gameObject.GetOrAddComponent<CameraBlender>();
            cameraBlender.UpdatePosition = true;
            cameraBlender.UpdateRotation = true;
            cameraBlender.UpdateFieldOfView = true;
            cameraBlender.UpdateOrthographicSize = camera.orthographic;
            cameraBlender.Target = target;
            cameraBlender.Smooth = smooth;

            return cameraBlender;
        }

        public static void StopBlending(this Camera camera)
        {
            var cameraBlender = camera.gameObject.GetComponent<CameraBlender>();
            if (cameraBlender != null)
            {
                Object.Destroy(cameraBlender);
            }
        }
    }
}
