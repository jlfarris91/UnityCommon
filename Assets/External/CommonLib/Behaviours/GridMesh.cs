using System.Collections.Generic;
using UnityEngine;

namespace CommonLib.Behaviours
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    public class GridMesh : MonoBehaviour
    {
        public float CellSize = 1.0f;
        public int Height = 10;
        public int Width = 10;
        private Mesh mesh;

        // Use this for initialization
        void Start()
        {
            this.mesh = new Mesh();
            this.UpdateMesh();
        }

        public void UpdateMesh()
        {
            this.mesh.Clear();

            var indices = new List<int>();
            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();

            // Rows
            for (var j = 0; j <= this.Height; ++j)
            {
                var vert = Vector3.up * this.CellSize * j;
                vertices.Add(vert); // from
                vert.x = this.Width * this.CellSize;
                vertices.Add(vert); // to

                normals.Add(Vector3.forward);
                normals.Add(Vector3.forward);
                indices.Add(j * 2);
                indices.Add(j * 2 + 1);
            }

            var offset = indices.Count;

            // Columns
            for (var j = 0; j <= this.Width; ++j)
            {
                var vert = Vector3.right * this.CellSize * j;
                vertices.Add(vert); // from
                vert.y = this.Height * this.CellSize;
                vertices.Add(vert); // to

                normals.Add(Vector3.forward);
                normals.Add(Vector3.forward);
                indices.Add(offset + j * 2);
                indices.Add(offset + j * 2 + 1);
            }

            this.mesh.name = "Generated Grid";
            this.mesh.SetVertices(vertices);
            this.mesh.SetNormals(normals);
            this.mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
            this.mesh.RecalculateBounds();

            this.GetComponent<MeshFilter>().mesh = this.mesh;
        }
    }
}
