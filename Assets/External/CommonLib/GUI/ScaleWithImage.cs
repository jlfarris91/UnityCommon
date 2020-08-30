namespace CommonLib.GUI
{
    using UnityEngine;
    using UnityEngine.UI;

    [ExecuteInEditMode]
    public class ScaleWithImage : NotifyPropertyChanged
    {
        public Vector2 InitialTargetSize;

        private new RectTransform transform;

        private Vector2 imageSize;

        private RectTransform targetTransform;

        [SerializeField]
        [HideInInspector]
        private Image target;

        private float imageRatio;

        [ShowInInspector]
        public Image Target
        {
            get { return this.target; }
            set
            {
                if (this.target != value)
                {
                    this.target = value;
                    this.Reset();
                    this.RaisePropertyChanged("Image");
                }
            }
        }

        private float TargetWidth
        {
            get { return this.targetTransform.rect.width; }
        }

        private float TargetHeight
        {
            get { return this.targetTransform.rect.height; }
        }

        private void Start()
        {
            this.transform = this.GetComponent<RectTransform>();

            this.Reset();
        }

        private void Update()
        {
            if (this.Target != null && this.Target.transform.hasChanged)
            {
                this.UpdateTransform();
            }
        }

        private void UpdateTransform()
        {
            Vector3 scalar = Vector3.one;

            Vector2 currentImageSize = this.CalculateImageSize();

            if (!Mathf.Approximately(this.InitialTargetSize.x, 0.0f))
            {
                scalar.x = currentImageSize.x / this.InitialTargetSize.x;
            }

            if (!Mathf.Approximately(this.InitialTargetSize.y, 0.0f))
            {
                scalar.y = currentImageSize.y / this.InitialTargetSize.y;
            }

            this.transform.localScale = scalar;
        }

        private Vector2 CalculateImageSize()
        {
            Vector2 size = Vector2.zero;

            float currentAspectRatio = this.CalcualteTargetAspectRatio();

            if (currentAspectRatio > this.imageRatio)
            {
                if (this.imageRatio < 1.0f)
                {
                    size.x = this.TargetHeight * this.imageRatio;
                    size.y = this.TargetHeight;
                }
                else
                {
                    size.x = this.TargetWidth;
                    size.y = this.TargetWidth / this.imageRatio;
                }
            }
            else
            {
                if (this.imageRatio < 1.0f)
                {
                    size.x = this.TargetWidth;
                    size.y = this.TargetWidth / this.imageRatio;
                }
                else
                {
                    size.x = this.TargetHeight * this.imageRatio;
                    size.y = this.TargetHeight;
                }
            }

            return size;
        }

        private void Reset()
        {
            this.imageSize = Vector2.one;
            this.targetTransform = null;

            if (this.Target != null)
            {
                this.targetTransform = this.Target.GetComponent<RectTransform>();

                Texture texture = this.Target.mainTexture;
                if (texture != null)
                {
                    this.imageSize = new Vector2(texture.width, texture.height);
                    this.imageRatio = this.imageSize.x / this.imageSize.y;
                }
            }
        }

        private float CalcualteTargetAspectRatio()
        {
            var ratio = 1.0f;

            if (!Mathf.Approximately(this.TargetHeight, 0.0f))
            {
                ratio = this.TargetWidth / this.TargetHeight;
            }

            return ratio;
        }
    }
}
