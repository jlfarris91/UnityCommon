namespace CommonLib.GUI
{
    using UnityEngine;

    public class TextToast : Toast
    {
        [SerializeField]
        [HideInInspector]
        private string message;

        [ShowInInspector]
        public string Message
        {
            get { return this.message; }

            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
    }
}