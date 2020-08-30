using UnityEngine;

namespace CommonLib.Behaviours
{
    public class ToggleLight : NotifyPropertyChanged
    {
        public Color EmissiveColor;
        public Renderer Renderer;
        public Light Light;
        private bool isOn = true;

        [ShowInInspector]
        public bool IsOn
        {
            get { return this.isOn; }

            set
            {
                if (this.isOn != value)
                {
                    this.isOn = value;
                    this.RaisePropertyChanged("On");
                    this.UpdateLightState(this.isOn);
                }
            }
        }

        private void Start()
        {
            this.UpdateLightState(this.IsOn);
        }

        private void UpdateLightState(bool on)
        {
            if (this.Renderer != null)
            {
                var block = new MaterialPropertyBlock();
                this.Renderer.GetPropertyBlock(block);
                block.SetColor("_EmissionColor", on ? this.EmissiveColor : Color.clear);
                this.Renderer.SetPropertyBlock(block);
            }

            if (this.Light != null)
            {
                this.Light.enabled = on;
            }
        }
    }
}