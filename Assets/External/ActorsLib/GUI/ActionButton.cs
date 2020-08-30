namespace ActorsLib.GUI
{
    using InputLib;
    using UnityEngine;
    using UnityEngine.UI;

    public class ActionButton : MonoBehaviour
    {
        public string Action;
        public InputController Controller;
    
        public void UpdateImage()
        {
            var image = this.GetComponentInChildren<Image>();
            InputController controller = this.Controller;
            //image.sprite = controller.GetIcon(this.Action);
        }
    
        // Update is called once per frame
        private void Start()
        {
            this.UpdateImage();
        }
    }
}
