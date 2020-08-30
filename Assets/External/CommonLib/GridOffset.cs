namespace CommonLib
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct GridOffset
    {
        public static readonly GridOffset zero = new GridOffset();

        public static readonly GridOffset[] CardinalDirections =
        {
            new GridOffset {z =  0, x =  1}, // E
            new GridOffset {z =  1, x =  0}, // N
            new GridOffset {z =  0, x = -1}, // W
            new GridOffset {z = -1, x =  0}  // S
        };

        public static readonly GridOffset[] AllDirections =
        {
            new GridOffset {z =  0, x =  1}, // E
            new GridOffset {z =  1, x =  1}, // NE
            new GridOffset {z =  1, x =  0}, // N
            new GridOffset {z =  1, x = -1}, // NW
            new GridOffset {z =  0, x = -1}, // W
            new GridOffset {z = -1, x = -1}, // SW
            new GridOffset {z = -1, x =  0}, // S
            new GridOffset {z = -1, x =  1}  // SE
        };

        // ReSharper disable once InconsistentNaming
        public int x;

        // ReSharper disable once InconsistentNaming
        public int y;

        // ReSharper disable once InconsistentNaming
        public int z;

        public GridOffset(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static GridOffset operator +(GridOffset a, GridOffset b)
        {
            return new GridOffset
            {
                x = a.x + b.x,
                y = a.y + b.y,
                z = a.z + b.z
            };
        }

        public static GridOffset operator -(GridOffset a, GridOffset b)
        {
            return new GridOffset
            {
                x = a.x - b.x,
                y = a.y - b.y,
                z = a.z - b.z
            };
        }

        public static explicit operator Vector3(GridOffset offset)
        {
            return new Vector3(offset.x, offset.y, offset.z);
        }

        public static bool operator ==(GridOffset a, GridOffset b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(GridOffset a, GridOffset b)
        {
            return !(a == b);
        }

        public bool Equals(GridOffset other)
        {
            return this.x == other.x
                   && this.y == other.y
                   && this.z == other.z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GridOffset && this.Equals((GridOffset)obj);
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
            return string.Format("(x: {0}, y: {1}, z: {2})", this.x, this.y, this.z);
        }
    }
}