namespace CommonLib
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class GizmosEx
    {
        public static int detail = 32;
        private static Stack<Color> colorStack = new Stack<Color>();
        private static Stack<Matrix4x4> matrixStack = new Stack<Matrix4x4>(); 

        public static void DrawWireArc(Vector3 position, float startAngle, float endAngle, float radius)
        {
            startAngle *= Mathf.Deg2Rad;
            endAngle *= Mathf.Deg2Rad;

            Vector3 lastPos = new Vector3(Mathf.Cos(startAngle), Mathf.Sin(startAngle), 0.0f) * radius;

            float delta = 1.0f / detail;
            for (var t = 0.0f; t <= 1.0f; t += delta)
            {
                float angle = Mathf.LerpAngle(startAngle, endAngle, t);
                Vector3 startPos = lastPos;
                Vector3 endPos = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle)) * radius;
                Gizmos.DrawLine(position + startPos, position + endPos);
                lastPos = endPos;
            }
        }

        public static void DrawWireArc(Vector3 position, float radius)
        {
            DrawWireArc(position, 0.0f, 360.0f, radius);
        }

        public static void DrawArrow(Vector3 start, Vector3 end, float arrowSize = 0.1f)
        {
            Vector3 dir = (end - start).normalized;
            Vector3 normal = dir;
            normal = new Vector3(-normal.z, 0.0f, normal.x);
            Gizmos.DrawLine(start, end);
            Gizmos.DrawLine(end, (end - dir * arrowSize) + (normal * arrowSize));
            Gizmos.DrawLine(end, (end - dir * arrowSize) - (normal * arrowSize));
        }

        public static void DrawArrowRay(Vector3 start, Vector3 dir, float arrowSize = 0.1f)
        {
            Vector3 end = start + dir;
            Vector3 normal = dir.normalized;
            normal = new Vector3(-normal.z, 0.0f, normal.x);
            Gizmos.DrawLine(start, end);
            Gizmos.DrawLine(end, (end - dir * arrowSize) + (normal * arrowSize));
            Gizmos.DrawLine(end, (end - dir * arrowSize) - (normal * arrowSize));
        }

        public static void DrawXYWirePlane(float width, float height)
        {
            Vector3 a = new Vector3(-width, -height) * 0.5f;
            Vector3 b = new Vector3( width, -height) * 0.5f;
            Vector3 c = new Vector3( width,  height) * 0.5f;
            Vector3 d = new Vector3(-width,  height) * 0.5f;
            
            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, d);
            Gizmos.DrawLine(d, a);

            Color color = Gizmos.color;
            color.a = 0.4f;

            GizmosEx.PushColor(color);
            Gizmos.DrawCube(Vector3.zero, new Vector3(width, height, 0.0f));
            GizmosEx.PopColor();
        }

        public static void DrawWireBox(Vector3 pos, Vector3 extents)
        {
            var points = new[]
            {
                // Top
                pos + new Vector3(extents.x, extents.y, extents.z) * 0.5f,
                pos + new Vector3(-extents.x, extents.y, extents.z) * 0.5f,
                pos + new Vector3(-extents.x, extents.y, -extents.z) * 0.5f,
                pos + new Vector3(extents.x, extents.y, -extents.z) * 0.5f,

                // Bottom
                pos + new Vector3(extents.x, -extents.y, extents.z) * 0.5f,
                pos + new Vector3(-extents.x, -extents.y, extents.z) * 0.5f,
                pos + new Vector3(-extents.x, -extents.y, -extents.z) * 0.5f,
                pos + new Vector3(extents.x, -extents.y, -extents.z) * 0.5f,
            };

            var directions = new[]
            {
                // Top
                new[] {Vector3.down, Vector3.left, Vector3.back},
                new[] {Vector3.down, Vector3.right, Vector3.back},
                new[] {Vector3.down, Vector3.right, Vector3.forward},
                new[] {Vector3.down, Vector3.left, Vector3.forward},

                // Bottom
                new[] {Vector3.up, Vector3.left, Vector3.back},
                new[] {Vector3.up, Vector3.right, Vector3.back},
                new[] {Vector3.up, Vector3.right, Vector3.forward},
                new[] {Vector3.up, Vector3.left, Vector3.forward},
            };

            float extent = extents.magnitude * 0.1f;

            for (int i = 0; i < 8; ++i)
            {
                GizmosEx.DrawCorner(points[i], directions[i][0], directions[i][1], directions[i][2], extent);
            }
        }

        public static void DrawCorner(Vector3 pos, float extent)
        {
            DrawCorner(pos, Vector3.down, Vector3.right, Vector3.forward, extent);
        }

        public static void DrawCorner(Vector3 pos, Vector3 a, Vector3 b, Vector3 c, float extent)
        {
            Vector3 p1 = pos + a * 0.5f;
            Vector3 p2 = pos + b * 0.5f;
            Vector3 p3 = pos + c * 0.5f;

            Gizmos.DrawLine(pos, p1);
            Gizmos.DrawLine(pos, p2);
            Gizmos.DrawLine(pos, p3);
        }

        public static void PushColor(Color color)
        {
            if (GizmosEx.colorStack.Count == 0)
            {
                GizmosEx.colorStack.Push(Gizmos.color);
            }

            GizmosEx.colorStack.Push(color);
            Gizmos.color = color;
        }

        public static void PopColor()
        {
            if (GizmosEx.colorStack.Count > 1)
            {
                GizmosEx.colorStack.Pop();
            }

            Color color = Color.red;

            if (GizmosEx.colorStack.Count > 0)
            {
                color = GizmosEx.colorStack.Peek();
            }

            Gizmos.color = color;
        }

        public static void PushMatrix(Matrix4x4 matrix)
        {
            if (GizmosEx.matrixStack.Count == 0)
            {
                GizmosEx.matrixStack.Push(Matrix4x4.identity);
            }

            GizmosEx.matrixStack.Push(GizmosEx.matrixStack.Peek() * matrix);
            Gizmos.matrix = matrix;
        }

        public static void PopMatrix()
        {
            if (GizmosEx.matrixStack.Count > 1)
            {
                GizmosEx.matrixStack.Pop();
            }

            Matrix4x4 matrix = Matrix4x4.identity;

            if (GizmosEx.matrixStack.Count > 0)
            {
                matrix = GizmosEx.matrixStack.Peek();
            }

            Gizmos.matrix = matrix;
        }
    }
}
