namespace CamerasLib
{
    using UnityEngine;

    public class EditorOnlyCamera : MonoBehaviour
    {
        // Use this for initialization
        private void OnEnable()
        {
            this.gameObject.SetActive(Application.isEditor);
        }
    }
}