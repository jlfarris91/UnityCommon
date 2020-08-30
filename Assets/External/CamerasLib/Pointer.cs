namespace CamerasLib
{
    using System;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public class RaycastHitEvent : UnityEvent<RaycastHit>
    {
    }

    [RequireComponent(typeof(Camera))]
    public class Pointer : MonoBehaviour
    {
        public int Button;
        public LayerMask CollisionMask;
        public GameObject[] GameObjectsHit;

        public UnityEvent ButtonClicked;
        public UnityEvent LeftButtonClicked;
        public UnityEvent RightButtonClicked;
        public UnityEvent MiddleButtonClicked;
        public RaycastHitEvent Hovered;

        public Ray Ray
        {
            get; private set;
        }

        public Vector3 CollisionPoint
        {
            get; private set;
        }

        public Vector3 CollisionNormal
        {
            get; private set;
        }

        public bool IsColliding
        {
            get; private set;
        }

        public RaycastHit[] Hits
        {
            get; private set;
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            var camera = this.GetComponent<Camera>();

            this.Ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] colliding = Physics.RaycastAll(
                this.Ray, 
                9999.0f, 
                this.CollisionMask, 
                QueryTriggerInteraction.Ignore);

            this.IsColliding = colliding != null && colliding.Any();

            if (this.IsColliding)
            {
                this.Hits = colliding.OrderBy(h => h.distance).ToArray();
                this.GameObjectsHit = this.Hits.Select(h => h.collider.gameObject).ToArray();

                RaycastHit target = this.Hits.FirstOrDefault();

                this.CollisionPoint = target.point;
                this.CollisionNormal = target.normal;

                this.Hovered.Invoke(target);
            }
            else
            {
                this.CollisionPoint = Vector3.zero;
            }

            if (Input.GetMouseButtonDown(this.Button))
            {
                this.ButtonClicked.Invoke();
            }

            if (Input.GetMouseButtonDown(0))
            {
                this.LeftButtonClicked.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                this.RightButtonClicked.Invoke();
            }

            if (Input.GetMouseButtonDown(2))
            {
                this.MiddleButtonClicked.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            if (this.IsColliding)
            {
                Gizmos.DrawWireSphere(this.CollisionPoint, 0.1f);
                Gizmos.DrawLine(this.CollisionPoint, this.CollisionPoint + this.CollisionNormal);
            }
        }
    }
}