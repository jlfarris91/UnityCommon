using UnityEngine;

namespace CommonLib.Behaviours
{
    public class EffectSpawner : MonoBehaviour
    {
        public GameObject Prefab;
        public Vector3 Offset;

        public GameObject SpawnAt(Vector3 worldPosition)
        {
            GameObject obj = Object.Instantiate(this.Prefab);
            obj.transform.position = worldPosition + this.Offset;
            return obj;
        }
    }
}