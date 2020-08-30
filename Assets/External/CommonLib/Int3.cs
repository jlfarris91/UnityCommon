namespace CommonLib
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct Int3
    {
        // ReSharper disable once InconsistentNaming
        public int x;

        // ReSharper disable once InconsistentNaming
        public int y;
        
        // ReSharper disable once InconsistentNaming
        public int z;

        public Int3(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int Area
        {
            get { return this.x * this.y * this.z; }
        }

        public float Magnitude
        {
            get { return Mathf.Sqrt((float)this.x*this.x + (float)this.y*this.y + (float)this.z*this.z); }
        }

        public float MagnitudeSqr
        {
            get { return (float)this.x * this.x + (float)this.y * this.y + (float)this.z * this.z; }
        }

        public int Sum
        {
            get { return this.x + this.y + this.z; }
        }

        public static Int3 operator +(Int3 lhs, Int3 rhs)
        {
            return new Int3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Int3 operator -(Int3 lhs, Int3 rhs)
        {
            return new Int3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Int3 operator *(Int3 lhs, Int3 rhs)
        {
            return new Int3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        }

        public static Int3 operator *(Int3 lhs, int rhs)
        {
            return new Int3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        }

        public static explicit operator Vector3(Int3 int3)
        {
            return new Vector3(int3.x, int3.y, int3.z);
        }

        public static bool operator ==(Int3 a, Int3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(Int3 a, Int3 b)
        {
            return !(a == b);
        }

        public static Vector3 operator *(Int3 int3, float scale)
        {
            return new Vector3(int3.x*scale, int3.y*scale, int3.z*scale);
        }

        public bool Equals(Int3 other)
        {
            return this.x == other.x && this.y == other.y && this.z == other.z;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj)) return false;
            return obj is Int3 && this.Equals((Int3)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.y * 793) ^ (this.z * 397) ^ this.x;
            }
        }

        public override string ToString()
        {
            return string.Format("(x: {0}, y: {1}, z: {2})", this.x, this.y, this.z);
        }
    }
}
