namespace CommonLib.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using CommonLib;
    using UnityEngine;

    public class GridTransform : NotifyPropertyChanged
    {
        [NonSerialized]
        [HideInInspector]
        public GridElementEvent CellChanged = new GridElementEvent();

        private Grid grid;

        [HideInInspector]
        [SerializeField]
        private int x;

        [HideInInspector]
        [SerializeField]
        private int y;

        [HideInInspector]
        [SerializeField]
        private int z;

        [HideInInspector]
        [SerializeField]
        private int width = 1;

        [HideInInspector]
        [SerializeField]
        private int height = 1;

        [HideInInspector]
        [SerializeField]
        private int depth = 1;

        [ShowInInspector]
        public int X
        {
            get { return this.x; }
            set
            {
                if (this.x != value)
                {
                    this.x = value;
                    this.UpdateTransformPosition();
                    this.RaisePropertyChanged("X");
                }
            }
        }

        [ShowInInspector]
        public int Y
        {
            get { return this.y; }
            set
            {
                if (this.y != value)
                {
                    this.y = value;
                    this.UpdateTransformPosition();
                    this.RaisePropertyChanged("Y");
                }
            }
        }

        [ShowInInspector]
        public int Z
        {
            get { return this.z; }
            set
            {
                if (this.z != value)
                {
                    this.z = value;
                    this.UpdateTransformPosition();
                    this.RaisePropertyChanged("Z");
                }
            }
        }

        [ShowInInspector]
        public int Width
        {
            get { return this.width; }
            set
            {
                if (this.width != value)
                {
                    this.width = value;
                    this.RaisePropertyChanged("Width");
                }
            }
        }

        [ShowInInspector]
        public int Height
        {
            get { return this.height; }
            set
            {
                if (this.height != value)
                {
                    this.height = value;
                    this.RaisePropertyChanged("Height");
                }
            }
        }

        [ShowInInspector]
        public int Depth
        {
            get { return this.depth; }
            set
            {
                if (this.depth != value)
                {
                    this.depth = value;
                    this.RaisePropertyChanged("Depth");
                }
            }
        }

        public GridCell Cell
        {
            get { return new GridCell(this.X, this.Y, this.Z); }

            set
            {
                this.X = value.x;
                this.Y = value.y;
                this.Z = value.z;
            }
        }

        public Grid Grid
        {
            get
            {
                if (this.grid == null)
                {
                    this.grid = this.GetComponentInParent<Grid>();
                }

                return this.grid;
            }
        }

        [ShowInInspector]
        public Vector3 BottomCenter
        {
            get
            {
                if (this.Grid == null)
                {
                    return this.transform.position;
                }

                return this.transform.position + this.LocalBottomCenter;
            }
        }

        [ShowInInspector]
        public Vector3 LocalBottomCenter
        {
            get
            {
                if (this.Grid == null)
                {
                    return this.transform.position;
                }

                return new Vector3(
                    this.Grid.GridCellSize.x * 0.5f,
                    0.0f,
                    this.Grid.GridCellSize.z * 0.5f);
            }
        }

        [ShowInInspector]
        public Vector3 TopCenter
        {
            get
            {
                if (this.Grid == null)
                {
                    return this.transform.position;
                }

                return new Vector3(
                    this.transform.position.x + this.Grid.GridCellSize.x * 0.5f,
                    this.transform.position.y + this.Grid.GridCellSize.y,
                    this.transform.position.z + this.Grid.GridCellSize.z * 0.5f);
            }
        }

        public GridTransform GetNeighbor(int dirX, int dirY, int dirZ)
        {
            if (this.Grid == null)
            {
                return null;
            }

            return this.Grid.GetElementsAt(
                this.X + dirX,
                this.Y + dirY,
                this.Z + dirZ).FirstOrDefault();
        }

        public GridTransform[] GetNeighbors(int dirX, int dirY, int dirZ)
        {
            if (this.Grid == null)
            {
                return null;
            }

            return this.Grid.GetElementsAt(
                this.X + dirX,
                this.Y + dirY,
                this.Z + dirZ).ToArray();
        }

        public T[] GetComponentsInNeighborCell<T>(int dirX, int dirY, int dirZ, bool includeInactive = false) where T : UnityEngine.Component
        {
            var components = new List<T>();

            foreach (GridTransform cell in this.GetNeighbors(dirX, dirY, dirZ))
            {
                components.AddRange(cell.GetComponentsInChildren<T>(includeInactive).ExceptNull());
            }

            return components.ToArray();
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
        }

        protected virtual void OnGridCellChanged()
        {
        }

        protected void RaiseGridCellChanged()
        {
            this.transform.hasChanged = false;

            this.OnGridCellChanged();

            this.CellChanged.Invoke(this);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            GridTransform transform = sender as GridTransform;
            if (transform == null)
            {
                return;
            }

            if (args.PropertyName == "X" ||
                args.PropertyName == "Y" ||
                args.PropertyName == "Z")
            {
                this.RaiseGridCellChanged();
            }
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (this.Grid == null)
            {
                return;
            }

            Vector3 gridCellSize = this.Grid.GridCellSize;
            var extents = new Vector3(this.Width * gridCellSize.x, this.Height * gridCellSize.y,
                this.Depth * gridCellSize.z);

            GizmosEx.DrawWireBox(this.transform.position + extents * 0.5f, extents);
        }
        
        private void UpdateTransformPosition()
        {
            this.transform.localPosition = this.Grid.GetLocalPosition(this.Cell);
        }
    }
}