namespace CommonLib
{
    using System;
    using CommonLib;
    using UnityEngine;

    [Serializable]
    public struct GridCell
    {
        public static readonly GridCell Zero = new GridCell();

        // ReSharper disable once InconsistentNaming
        public int x;
    
        // ReSharper disable once InconsistentNaming
        public int y;

        // ReSharper disable once InconsistentNaming
        public int z;

        public GridCell(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Calculates the index of this cell in a given 2D grid.
        /// </summary>
        /// <example>
        /// 
        /// Row major:
        /// [0][1][2]
        /// [3][4][5]
        /// [6][7][8]
        /// Cell(r: 2, c: 1) = 7
        /// 
        /// Column major:
        /// [0][3][6]
        /// [1][4][7]
        /// [2][5][8]
        /// Cell(r: 2, c: 1) = 5
        /// 
        /// </example>
        /// <returns>The index of this cell in a grid.</returns>
        public int ToIndex(Int3 gridSize)
        {
            return GridCell.ToIndex(this.x, this.y, this.z, gridSize);
        }

        /// <summary>
        /// Determines if this cell is in a grid of a given width and height.
        /// </summary>
        /// <returns>True if the cell is within the boundaries of the grid.</returns>
        public bool IsInGrid(Int3 gridSize)
        {
            return MathEx.IsInGrid(this, gridSize);
        }

        /// <summary>
        /// Adds the row and column of two <see cref="GridCell"/>s together and returns the result.
        /// </summary>
        /// <param name="a">The first <see cref="GridCell"/>.</param>
        /// <param name="b">The second <see cref="GridCell"/>.</param>
        /// <returns></returns>
        public static GridCell operator +(GridCell a, GridOffset b)
        {
            return new GridCell(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static GridOffset operator -(GridCell a, GridCell b)
        {
            return new GridOffset
            {
                x = a.x - b.x,
                y = a.y - b.y,
                z = a.z - b.z
            };
        }

        public static GridCell operator -(GridCell a, GridOffset b)
        {
            return new GridCell(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static GridCell operator +(GridCell a, Int3 b)
        {
            return new GridCell(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static GridCell operator -(GridCell a, Int3 b)
        {
            return new GridCell(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static GridCell operator *(GridCell a, Int3 b)
        {
            return new GridCell(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static bool operator==(GridCell a, GridCell b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(GridCell a, GridCell b)
        {
            return !(a == b);
        }

        public static explicit operator Int3(GridCell a)
        {
            return new Int3(a.x, a.y, a.z);
        }

        public static explicit operator Vector3(GridCell a)
        {
            return new Vector3(a.x, a.y, a.z);
        }

        public bool Equals(GridCell other)
        {
            return this.x == other.x && this.y == other.y && this.z == other.z;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj)) return false;
            return obj is GridCell && this.Equals((GridCell)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.x ^ (this.x >> 32);
                result = 31 * result + (this.y ^ (this.y >> 32));
                result = 31 * result + (this.z ^ (this.z >> 32));
                return result;
            }
        }

        public override string ToString()
        {
            return String.Format("(x: {0}, y: {1}, z: {2})", this.x, this.y, this.z);
        }

        public static Vector3 operator *(GridCell cell, float size)
        {
            return new Vector3(cell.x, cell.y, cell.z)*size;
        }

        public static Vector3 operator *(GridCell cell, Vector3 size)
        {
            return new Vector3(cell.x*size.x, cell.y*size.y, cell.z*size.z);
        }

        public static int ToIndex(int x, int y, int z, Int3 extents)
        {
            return y * extents.x * extents.z + z * extents.x + x;
        }

        public static int ToIndex(GridCell cell, Int3 extents)
        {
            return cell.y * extents.x * extents.z + cell.z * extents.x + cell.x;
        }

        public static GridCell FromIndex(int index, Int3 extents)
        {
            int y = index/(extents.x * extents.z);

            index -= y*extents.x*extents.z;
        
            int z = index / extents.x;
            int x = index % extents.x;

            return new GridCell(x, y, z);
        }
    }
}