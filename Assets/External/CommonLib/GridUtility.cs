namespace CommonLib
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class GridUtility
    {
        private static Stack<GridData> gridDataStack = new Stack<GridData>();

        public static void SetColumn(int column, float width, bool isFlex)
        {
            GridData data = GridUtility.gridDataStack.Peek();
            GridColumn c = data.Columns[column];
            c.Width = width;
            c.IsFlex = isFlex;
            data.Columns[column] = c;
        }

        public static void SetRow(int row, float height, bool isFlex)
        {
            GridData data = GridUtility.gridDataStack.Peek();
            GridRow r = data.Rows[row];
            r.Height = height;
            r.IsFlex = isFlex;
            data.Rows[row] = r;
        }

        public static Rect GetCell(int row, int column)
        {
            return GetCell(row, column, 1, 1);
        }

        public static Rect GetCell(int row, int column, int rowSpan, int columnSpan)
        {
            Rect cellRect = CalculateCellRect(row, column);

            if (rowSpan > 1 || columnSpan > 1)
            {
                row += Mathf.Max(rowSpan - 1, 0);
                column += Mathf.Max(columnSpan - 1, 0);
                cellRect = cellRect.Expand(CalculateCellRect(row, column));
            }

            return cellRect;
        }

        private static Rect CalculateCellRect(int row, int column)
        {
            GridData data = GridUtility.gridDataStack.Peek();

            float totalFlexHeight = data.Rows.Sum(r => r.IsFlex ? r.Height : 0.0f);
            float totalFlexWidth = data.Columns.Sum(c => c.IsFlex ? c.Width : 0.0f);

            var cellRect = new Rect();

            cellRect.y = data.Area.y + data.Rows.Select(r => CalculateHeight(r, totalFlexHeight, data.Area.height)).Take(row).Sum();
            cellRect.height = CalculateHeight(data.Rows[row], totalFlexHeight, data.Area.height);

            cellRect.x = data.Area.x + data.Columns.Select(c => CalculateWidth(c, totalFlexWidth, data.Area.width)).Take(column).Sum();
            cellRect.width = CalculateWidth(data.Columns[column], totalFlexWidth, data.Area.width);

            return cellRect;
        }

        private static float CalculateWidth(GridColumn column, float totalFlexSize, float totalWidth)
        {
            return CalculateSize(column.Width, totalWidth, totalFlexSize, column.IsFlex);
        }

        private static float CalculateHeight(GridRow row, float totalFlexSize, float totalHeight)
        {
            return CalculateSize(row.Height, totalHeight, totalFlexSize, row.IsFlex);
        }

        private static float CalculateSize(float value, float totalSize, float totalFlexSize, bool isFlex)
        {
            if (isFlex && totalFlexSize != 0.0f)
            {
                value /= totalFlexSize;
                value *= totalSize;
            }

            return value;
        }

        public static void BeginGrid(Rect area, int rows, int columns)
        {
            var data = new GridData
            {
                Area = area,
                Rows = new GridRow[rows],
                Columns = new GridColumn[columns]
            };

            for (var i = 0; i < rows; ++i)
            {
                data.Rows[i].Height = 1.0f;
                data.Rows[i].IsFlex = true;
            }

            for (var i = 0; i < columns; ++i)
            {
                data.Columns[i].Width = 1.0f;
                data.Columns[i].IsFlex = true;
            }

            GridUtility.gridDataStack.Push(data);

            //GUI.BeginClip(area);
        }

        public static void EndGrid()
        {
            //GUI.EndClip();

            if (GridUtility.gridDataStack.Any())
            {
                GridUtility.gridDataStack.Pop();
            }
        }

        private struct GridData
        {
            public Rect Area;
            public GridRow[] Rows;
            public GridColumn[] Columns;
        }

        private struct GridRow
        {
            public float Height;
            public bool IsFlex;
        }

        private struct GridColumn
        {
            public float Width;
            public bool IsFlex;
        }
    }
}
