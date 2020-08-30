using UnityEngine;

namespace CommonLib.Behaviours
{
    public class Billboard : MonoBehaviour
    {
        private void OnWillRenderObject()
        {
            this.transform.up = Camera.current.transform.up;
        }
    }
}
