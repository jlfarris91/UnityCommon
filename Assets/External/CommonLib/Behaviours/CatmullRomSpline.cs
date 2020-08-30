namespace CommonLib.Behaviours
{
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Interpolation between points with a Catmull-Rom spline
    /// From http://www.habrador.com/tutorials/interpolation/1-catmull-rom-splines/
    /// </summary>
    public class CatmullRomSpline : MonoBehaviour
    {
        /// <summary>
        /// Has to be at least 4 points
        /// </summary>
        public Transform[] ControlPointsList;

        /// <summary>
        /// Are we making a line or a loop?
        /// </summary>
        public bool IsLooping = true;

        /// <summary>
        /// Gets a point along the spline.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3 GetPoint(float t)
        {
            t = Mathf.Clamp01(t);

            Vector3[] points = this.ControlPointsList.Select(p => p.position).ToArray();

            int index = Mathf.FloorToInt(points.Length * t);
            index = ClampListPos(this.ControlPointsList.Length, index, this.IsLooping);

            float subTSize = 1.0f / points.Length;
            float subT = (t - index * subTSize) / subTSize;
            subT = Mathf.Clamp01(subT);

            Vector3 p0 = points[ClampListPos(this.ControlPointsList.Length, index - 1, this.IsLooping)];
            Vector3 p1 = points[ClampListPos(this.ControlPointsList.Length, index + 0, this.IsLooping)];
            Vector3 p2 = points[ClampListPos(this.ControlPointsList.Length, index + 1, this.IsLooping)];
            Vector3 p3 = points[ClampListPos(this.ControlPointsList.Length, index + 2, this.IsLooping)];

            return GetCatmullRomPosition(subT, p0, p1, p2, p3);
        }

        /// <summary>
        /// Display without having to press play
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            if (this.ControlPointsList == null)
            {
                return;
            }

            // Draw the Catmull-Rom spline between the points
            for (int i = 0; i < this.ControlPointsList.Length; i++)
            {
                // Cant draw between the endpoints
                // Neither do we need to draw from the second to the last endpoint
                // ...if we are not making a looping line
                if ((i == 0 || i == this.ControlPointsList.Length - 2 || i == this.ControlPointsList.Length - 1) && !this.IsLooping)
                {
                    continue;
                }

                this.DisplayCatmullRomSpline(i);
            }
        }

        /// <summary>
        /// Display a spline between 2 points derived with the Catmull-Rom spline algorithm
        /// </summary>
        private void DisplayCatmullRomSpline(int pos)
        {
            // The 4 points we need to form a spline between p1 and p2
            Vector3 p0 = this.ControlPointsList[this.ClampListPos(pos - 1)].position;
            Vector3 p1 = this.ControlPointsList[this.ClampListPos(pos + 0)].position;
            Vector3 p2 = this.ControlPointsList[this.ClampListPos(pos + 1)].position;
            Vector3 p3 = this.ControlPointsList[this.ClampListPos(pos + 2)].position;

            // The start position of the line
            Vector3 lastPos = p1;

            // The spline's resolution
            // Make sure it's is adding up to 1, so 0.3 will give a gap, but 0.2 will work
            float resolution = 0.2f;

            // How many times should we loop?
            int loops = Mathf.FloorToInt(1f / resolution);

            for (int i = 1; i <= loops; i++)
            {
                // Which t position are we at?
                float t = i * resolution;

                // Find the coordinate between the end points with a Catmull-Rom spline
                Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);

                // Draw this line segment
                Gizmos.DrawLine(lastPos, newPos);

                // Save this pos so we can draw the next line segment
                lastPos = newPos;
            }
        }

        /// <summary>
        /// Clamp the list positions to allow looping
        /// </summary>
        private int ClampListPos(int pos)
        {
            return this.IsLooping
                ? MathEx.WrapOverflow(pos, 0, this.ControlPointsList.Length - 1)
                : Mathf.Clamp(pos, 0, this.ControlPointsList.Length - 1);
        }

        /// <summary>
        /// Gets a point along the spline.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 GetPoint(Vector3[] points, float t, bool isLooping)
        {
            t = Mathf.Clamp01(t);

            int index = Mathf.FloorToInt(points.Length * t);
            index = ClampListPos(points.Length, index, isLooping);

            float subTSize = 1.0f / points.Length;
            float subT = (t - index * subTSize) / subTSize;
            subT = Mathf.Clamp01(subT);

            Vector3 p0 = points[ClampListPos(points.Length, index - 1, isLooping)];
            Vector3 p1 = points[ClampListPos(points.Length, index + 0, isLooping)];
            Vector3 p2 = points[ClampListPos(points.Length, index + 1, isLooping)];
            Vector3 p3 = points[ClampListPos(points.Length, index + 2, isLooping)];

            return GetCatmullRomPosition(subT, p0, p1, p2, p3);
        }

        private static int ClampListPos(int numOfPoints, int index, bool isLooping)
        {
            return isLooping
                ? MathEx.WrapOverflow(index, 0, numOfPoints - 1)
                : Mathf.Clamp(index, 0, numOfPoints - 1);
        }

        /// <summary>
        /// Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
        /// </summary>
        /// <remarks>
        /// http://www.iquilezles.org/www/articles/minispline/minispline.htm
        /// </remarks>
        private static Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            // The coefficients of the cubic polynomial (except the 0.5f * which is added later for performance)
            Vector3 a = 2f * p1;
            Vector3 b = p2 - p0;
            Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
            Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

            // The cubic polynomial: a + b * t + c * t^2 + d * t^3
            Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

            return pos;
        }
    }
}