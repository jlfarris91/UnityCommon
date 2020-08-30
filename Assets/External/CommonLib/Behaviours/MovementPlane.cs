using UnityEngine;

namespace CommonLib.Behaviours
{
    public class MovementPlane : MonoBehaviour
    {
        public float Width;
        public float Height;

        public Vector3 LastProjectedVector;
        public Vector3 LastProjectedPoint;

        public Vector3 ProjectPointOnPlane(Vector3 worldPosition)
        {
            Vector3 ladderNormal = this.transform.forward;
            Vector3 pointToBottom = worldPosition - this.transform.position;
            Vector3 pointOnPlane = Vector3.ProjectOnPlane(pointToBottom, ladderNormal);
            Vector3 projected = this.transform.position + pointOnPlane;
            this.LastProjectedPoint = projected;
            return projected;
        }

        public Vector3 ProjectVectorOnPlane(Vector3 worldVector)
        {
            Vector3 ladderNormal = this.transform.forward;
            Vector3 projected = Vector3.ProjectOnPlane(worldVector, ladderNormal);
            this.LastProjectedVector = projected;
            return projected;
        }

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void CalculatePlane()
        {
        
        }

        private void OnDrawGizmos()
        {
            GizmosEx.PushMatrix(this.transform.localToWorldMatrix);
            GizmosEx.PushColor(Color.red);
            GizmosEx.DrawXYWirePlane(this.Width, this.Height);
            GizmosEx.PopColor();
            GizmosEx.PopMatrix();
            GizmosEx.PopMatrix();

            GizmosEx.PushColor(Color.cyan);
            GizmosEx.DrawArrow(this.LastProjectedPoint, this.LastProjectedPoint + this.LastProjectedVector);
            GizmosEx.PopColor();
        }
    }
}
