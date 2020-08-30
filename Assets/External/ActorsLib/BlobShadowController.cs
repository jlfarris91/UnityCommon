namespace ActorsLib
{
    using CommonLib;
    using UnityEngine;

    [RequireComponent(typeof(Projector))]
    public class BlobShadowController : MonoBehaviour
    {
        private Projector projector;
        private readonly RaycastHit[] hits = new RaycastHit[3];

        // Use this for initialization
        private void Start()
        {
            this.projector = this.GetComponent<Projector>();
        }

        // Update is called once per frame
        private void Update()
        {
            var ray = new Ray(this.transform.position, Vector3.down);

            int hitCount = Physics.RaycastNonAlloc(
                ray,
                this.hits,
                1000,
                LayerMaskEx.Invert(this.projector.ignoreLayers));

            this.projector.enabled = hitCount > 0;

            if (hitCount > 0)
            {
                this.projector.farClipPlane = this.hits[hitCount-1].distance;
            }
        }
    }
}
