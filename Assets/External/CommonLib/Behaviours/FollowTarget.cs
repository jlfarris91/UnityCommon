using System;
using UnityEngine;

namespace CommonLib.Behaviours
{
    /// <summary>
    /// Makes a transform follow another.
    /// </summary>
    public class FollowTarget : MonoBehaviour
    {
        public enum MovementType
        {
            Instant,
            Step,
            Smooth
        }

        public enum TransformType
        {
            World,
            Local,
            Forward
        }

        public Transform Target;
        public Vector3 Offset;
        public MovementType Movement;
        public float SmoothTime = 0.1f;
        public float MaxSpeed = 10.0f;
        public TransformType Transform = TransformType.World;
        public bool UseFixedUpdate;

        private Vector3 targetPos;
        private Vector3 currentVelocity;

        // Use this for initialization
        private void OnEnable()
        {
            this.UpdatePosition();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (!this.UseFixedUpdate)
            {
                this.UpdatePosition();
            }
        }

        private void FixedUpdate()
        {
            if (this.UseFixedUpdate)
            {
                this.UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            if (this.Target == null)
            {
                return;
            }

            this.targetPos = this.GetTargetPosition(this.Transform);
            this.Move(this.Movement);
        }

        public Vector3 GetTargetPosition(TransformType transformType)
        {
            Vector3 pos;

            switch (transformType)
            {
                case TransformType.World:
                    pos = this.Target.position + this.Offset;
                    break;
                case TransformType.Local:
                    pos = this.Target.TransformPoint(this.targetPos);
                    break;
                case TransformType.Forward:
                    pos = this.Target.position +
                          this.Target.forward * this.Offset.z +
                          Vector3.up * this.Offset.y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return pos;
        }

        public void Move(MovementType movementType)
        {
            switch (movementType)
            {
                case MovementType.Instant:
                    this.transform.position = this.targetPos;
                    break;
                case MovementType.Step:
                    this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPos, this.MaxSpeed * Time.deltaTime);
                    break;
                case MovementType.Smooth:
                    float smoothTime = this.SmoothTime / 100.0f;
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, this.targetPos, ref this.currentVelocity, smoothTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}