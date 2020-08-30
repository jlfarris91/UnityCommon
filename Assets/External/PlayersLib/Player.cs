namespace PlayersLib
{
    using CommonLib;
    using InputLib;
    using UnityEngine;

    public class Player : NotifyPropertyChanged
    {
        public InputController Controller;

        [SerializeField]
        [HideInInspector]
        private int id;

        [SerializeField]
        [HideInInspector]
        private string playerName;

        [SerializeField]
        [HideInInspector]
        private bool isPlaying;

        [SerializeField]
        [HideInInspector]
        private Sprite portrait;

        [SerializeField]
        [HideInInspector]
        private Color color;

        [ShowInInspector]
        public int Id
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }

        [ShowInInspector]
        public string PlayerName
        {
            get { return this.playerName; }
            set
            {
                if (this.playerName != value)
                {
                    this.playerName = value;
                    this.RaisePropertyChanged("PlayerName");
                }
            }
        }

        [ShowInInspector]
        public bool IsPlaying
        {
            get { return this.isPlaying; }
            set
            {
                if (this.isPlaying != value)
                {
                    this.isPlaying = value;
                    this.RaisePropertyChanged("IsPlaying");
                }
            }
        }

        [ShowInInspector]
        public Sprite Portrait
        {
            get { return this.portrait; }
            set
            {
                if (this.portrait != value)
                {
                    this.portrait = value;
                    this.RaisePropertyChanged("Portrait");
                }
            }
        }

        [ShowInInspector]
        public Color Color
        {
            get { return this.color; }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }

        private void Start()
        {
            if (PlayerManager.Exists)
            {
                this.Color = PlayerManager.Instance.PlayerColors[this.Id];
            }
        }
    }
}