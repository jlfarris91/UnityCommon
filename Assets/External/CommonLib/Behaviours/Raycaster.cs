namespace CommonLib.Behaviours
{
    using UnityEngine;

    public enum Ignore
    {
        Objects,
        Triggers,
        None
    }

    public class Raycaster : MonoBehaviour
    {
        public float RayLength = 1000.0f;
        public LayerMask Mask;
        public Ignore Ignore = Ignore.None;

        public RaycastHit[] RaycastAll()
        {
            return Physics.RaycastAll(this.transform.position, this.transform.forward, this.RayLength, this.Mask);
        }

        public bool RaycastHit(out RaycastHit hit)
        {
            hit = default(RaycastHit);

            RaycastHit[] hits = this.RaycastAll();
            if (hits.Length == 0)
            {
                return false;
            }

            foreach (RaycastHit h in hits)
            {
                switch (this.Ignore)
                {
                    case Ignore.Objects:
                        if (!h.collider.isTrigger)
                        {
                            continue;
                        }
                        break;
                    case Ignore.Triggers:
                        if (h.collider.isTrigger)
                        {
                            continue;
                        }
                        break;
                }
                hit = h;
                return true;
            }

            return false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(this.transform.position, this.transform.forward * this.RayLength);
        }
    }
}