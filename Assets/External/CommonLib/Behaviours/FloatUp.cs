using UnityEngine;

namespace CommonLib.Behaviours
{
    public class FloatUp : MonoBehaviour
    {
        public float Speed;

        void Update()
        {
            this.transform.Translate(0.0f, this.Speed*Time.deltaTime, 0.0f);
        }
    }
}