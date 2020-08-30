namespace CamerasLib
{
    using CommonLib;
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class CameraPlayerBounds : MonoBehaviour
    {
        private new Camera camera;

        private void Start()
        {
            this.camera = this.GetComponent<Camera>();
            this.CreateFrustrumPanels();
        }

        private void CreateFrustrumPanels()
        {
            Plane[] cameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(this.camera);

            this.transform.DestroyChildren();

            foreach (Plane frustumPlane in cameraFrustumPlanes)
            {
                this.CreateFrustrumPanelObject(frustumPlane);
            }
        }

        private void CreateFrustrumPanelObject(Plane frustumPlane)
        {
            GameObject planeObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
            planeObject.layer = LayerMask.NameToLayer("CameraBoundary");
            Object.Destroy(planeObject.GetComponent<MeshRenderer>());
            planeObject.transform.SetParent(this.transform);
            planeObject.transform.localPosition = Vector3.zero;
            planeObject.transform.forward = -frustumPlane.normal;
            planeObject.transform.localScale = Vector3.one * this.camera.farClipPlane;
        }
    }
}