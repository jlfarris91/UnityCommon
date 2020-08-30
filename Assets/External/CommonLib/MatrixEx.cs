namespace CommonLib
{
    using UnityEngine;

    internal static class MatrixEx
    {
        public static Matrix4x4 RotateX(float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            Matrix4x4 matrix = Matrix4x4.identity;
            matrix.m00 = Mathf.Cos(radians);
            matrix.m01 = -Mathf.Sin(radians);
            matrix.m10 = Mathf.Sin(radians);
            matrix.m11 = Mathf.Cos(radians);
            return matrix;
        }

        public static Matrix4x4 RotateY(float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            Matrix4x4 matrix = Matrix4x4.identity;
            matrix.m00 = Mathf.Cos(radians);
            matrix.m02 = Mathf.Sin(radians);
            matrix.m20 = -Mathf.Sin(radians);
            matrix.m22 = Mathf.Cos(radians);
            return matrix;
        }

        public static Matrix4x4 Lerp(Matrix4x4 a, Matrix4x4 b, float t)
        {
            var result = new Matrix4x4();
            for (var i = 0; i < 16; ++i)
            {
                result[i] = Mathf.Lerp(a[i], b[i], t);
            }
            return result;
        }
    }
}
