namespace CommonLib.Behaviours
{
    using System.Collections.Generic;
    using System.Linq;
    using CommonLib;
    using UnityEngine;

    public class Grid : MonoBehaviour
    {
        public Vector3 GridCellSize;
        public Vector3 Padding;

        private Dictionary<GridCell, HashSet<GridTransform>> cells;
        private bool initialized;
        private Transform cellsRoot;

        public IEnumerable<GridTransform> Cells
        {
            get { return this.cells.Values.SelectMany(_ => _); }
        }

        public Vector3 CellSizeWithPadding
        {
            get { return this.GridCellSize + this.Padding * 2.0f; }
        }

        private Dictionary<GridCell, HashSet<GridTransform>> CellMap
        {
            get
            {
                if (this.cells == null)
                {
                    this.cells = new Dictionary<GridCell, HashSet<GridTransform>>();
                }

                return this.cells;
            }
        }

        private Transform CellsRoot
        {
            get
            {
                if (this.cellsRoot == null)
                {
                    this.cellsRoot = this.transform.Find("Cells");
                }

                return this.cellsRoot;
            }
        }

        public Vector3 GetLocalPosition(GridCell gridCell)
        {
            // 
            //    +-------------+
            //    |   Padding   |
            //    |  +-------+  |
            //    |  |       |  |
            //    |  | Cell  |  |
            //    |  |       |  |
            //    |  +-------+  |
            //    |             |
            //    +-------------+
            // 

            return new Vector3
            {
                x = gridCell.x * this.CellSizeWithPadding.x + this.Padding.x,
                y = gridCell.y * this.CellSizeWithPadding.y + this.Padding.y,
                z = gridCell.z * this.CellSizeWithPadding.z + this.Padding.z
            };
        }

        public Vector3 GetWorldPosition(GridCell gridCell)
        {
            Vector3 localPosition = this.GetLocalPosition(gridCell);
            return this.transform.localToWorldMatrix * localPosition;
        }

        public GridCell GetCell(Vector3 worldPosition)
        {
            Vector3 pos = worldPosition - this.transform.position;

            var cell = new GridCell
            {
                x = Mathf.FloorToInt(pos.x / this.CellSizeWithPadding.x),
                y = Mathf.FloorToInt(pos.y / this.CellSizeWithPadding.y),
                z = Mathf.FloorToInt(pos.z / this.CellSizeWithPadding.z)
            };
            return cell;
        }

        public IEnumerable<GridTransform> GetElementsAt(int x, int y, int z)
        {
            IEnumerable<GridTransform> elements;

            if (Application.isPlaying)
            {
                HashSet<GridTransform> hashSet;
                elements = this.CellMap.TryGetValue(new GridCell(x, y, z), out hashSet)
                    ? hashSet
                    : Enumerable.Empty<GridTransform>();
            }
            else
            {
                elements = this.GetComponentsInChildren<GridTransform>().Where(_ => _.X == x && _.Y == y && _.Z == z);
            }

            return elements;
        }

        public void Initialize()
        {
            if (this.initialized)
            {
                return;
            }

            List<GridTransform> elements = this.GetComponentsInChildren<GridTransform>().ToList();

            this.CellMap.Clear();

            foreach (GridTransform gridElement in elements)
            {
                HashSet<GridTransform> list;
                if (!this.CellMap.TryGetValue(gridElement.Cell, out list))
                {
                    list = new HashSet<GridTransform>();
                    this.CellMap.Add(gridElement.Cell, list);
                }

                list.Add(gridElement);
            }

            this.initialized = true;
        }

        private void Awake()
        {
            this.Initialize();
        }

        ///// <summary>
        ///// When a GridElement changes grid cells reparent it to the correct
        ///// GridCellContainer, create a new one if one does not exist.
        ///// </summary>
        ///// <param name="cell">GridElement that changed cells.</param>
        //internal void OnGridCellChanged(GridElement cell)
        //{
        //    return;

        //    // We don't care if GridCellContainers move around
        //    if (cell is GridCellContainer)
        //    {
        //        return;
        //    }

        //    GridCellContainer container;
        //    this.CellMap.TryGetValue(cell.GridCell, out container);

        //    if (container == null)
        //    {
        //        container = this.CreateGridCellContainer(cell.GridCell);
        //    }

        //    cell.transform.SetParent(container.transform);
        //    container.transform.hasChanged = false;
        //    cell.transform.hasChanged = false;
        //}

        //private GridCellContainer CreateGridCellContainer(GridCell cell)
        //{
        //    string containerObjectName = Grid.BuildGridCellContainerName(cell);
        //    GameObject containerObject = new GameObject(containerObjectName);
        //    containerObject.transform.SetParent(this.CellsRoot);

        //    GridCellContainer container = containerObject.AddComponent<GridCellContainer>();
        //    container.GridCell = cell;

        //    this.CellMap.Add(cell, container);

        //    return container;
        //}

        public static string BuildGridCellContainerName(GridCell cell)
        {
            return string.Format("Cell{0:D3}{1:D3}{2:D3}", cell.x, cell.y, cell.z);
        }

        //private void OnDrawGizmos()
        //{
        //    for (int r = -5; r < 5; ++r)
        //    {
        //        for (int c = -5; c < 5; ++c)
        //        {
        //            GizmosEx.DrawWireBox(this.GetLocalPosition(new GridCell(c, 0, r)) + this.GridCellSize * 0.5f,
        //                this.GridCellSize);
        //        }
        //    }
        //}
    }
}