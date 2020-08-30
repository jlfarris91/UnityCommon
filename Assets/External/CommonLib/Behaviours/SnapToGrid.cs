namespace CommonLib.Behaviours
{
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    [ExecuteInEditMode]
    public class SnapToGrid : MonoBehaviour
    {
        public Vector3 GridSize = Vector3.one;
        public float Scale = 1.0f;
        public Vector3 Offset = Vector3.zero;
        public bool EditorOnly = true;

        private Vector3 preRenderPosition;

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            this.Snap();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void LateUpdate()
        {
            if (Application.isPlaying && this.EditorOnly)
            {
                Destroy(this);
                return;
            }

            if (this.transform.hasChanged)
            {
                this.Snap();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Snap()
        {
            Vector3 scalar = this.GridSize * this.Scale;

            this.transform.position -= this.Offset;
            this.transform.position = MathEx.SnapToGrid(this.transform.position, scalar);
            this.transform.position += this.Offset;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.transform.position - this.Offset, this.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            GizmosEx.DrawWireArc(this.transform.position - this.Offset, 0.3f);
        }
    }
}