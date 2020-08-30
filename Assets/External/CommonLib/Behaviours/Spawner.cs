namespace CommonLib.Behaviours
{
    using System;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Flags]
    public enum TransformClearFlags
    {
        LocalPosition = 1 << 1,
        LocalRotation = 1 << 2,
        LocalScale = 1 << 3,
        Local = LocalPosition | LocalRotation | LocalScale
    }

    public class Spawner<TPrefab> : MonoBehaviour, ISpawner where TPrefab : Component
    {
        public TPrefab Prefab;
        public Transform Parent;
        public bool SpawnOnStart;
        public bool DestroyAfterSpawning;
        public bool InstantiateInWorldSpace;
        public TransformClearFlags TransformClearFlags = TransformClearFlags.Local;

        public virtual TPrefab Spawn()
        {
            Transform parent = this.Parent == null ? this.transform : this.Parent;
            TPrefab spawnedObject =  Object.Instantiate(this.Prefab, parent, this.InstantiateInWorldSpace);

            if (this.TransformClearFlags.HasFlag(TransformClearFlags.LocalPosition))
            {
                spawnedObject.transform.localPosition = Vector3.zero;
            }

            if (this.TransformClearFlags.HasFlag(TransformClearFlags.LocalRotation))
            {
                spawnedObject.transform.localRotation = Quaternion.identity;
            }

            if (this.TransformClearFlags.HasFlag(TransformClearFlags.LocalScale))
            {
                spawnedObject.transform.localScale = Vector3.one;
            }

            return spawnedObject;
        }

        protected virtual void Start()
        {
            if (this.SpawnOnStart)
            {
                this.Spawn();
            }
        }

        GameObject ISpawner.Spawn()
        {
            return this.Spawn().gameObject;
        }
    }
}