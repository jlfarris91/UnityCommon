using UnityEngine;

namespace CommonLib.Behaviours
{
    public class Description : MonoBehaviour
    {
        public string Text = string.Empty;

        private void OnEnable()
        {
            this.enabled = false;
        }
    }
}
