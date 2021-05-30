using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Helpers
{
    public enum PlaneDirection
    {
        XZ,
        ZX,
        YZ,
        ZY,
        XY,
        YX
    }
    public class GameObjectGenerator
    {
        public static Mesh generateMesh(Vector3[] vertices, int[] triangles, Vector3[] normals, Vector2[] uv)
        {
            Mesh ret = new Mesh();
            ret.vertices = vertices;
            ret.triangles = triangles;
            ret.normals = normals;
            ret.uv = uv;
            return ret;
        }

        public static GameObject generatePlane(Material material, Vector3 bottomLeftCorner, PlaneDirection direction, float width, float height, string name = "plane")
        {
            GameObject ret = new GameObject(name);
            MeshRenderer renderer = ret.AddComponent<MeshRenderer>();
            MeshFilter filter = ret.AddComponent<MeshFilter>();

            float minusHeight = height * -1.0f;
            float minusWidth = width * -1.0f;

            List<Vector3> vertices = new List<Vector3>();

            vertices.Add(bottomLeftCorner);
            vertices.Add(new Vector3(width, 0, 0) + bottomLeftCorner);
            vertices.Add(new Vector3(0, 0, height) + bottomLeftCorner);
            vertices.Add(new Vector3(width, 0, height) + bottomLeftCorner);

            int[] triangles = new int[]
            {
                0, 2, 1,
                2, 3, 1
            };

            Vector3[] normals = new Vector3[]
            {
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward
            };

            Vector2[] uv = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            filter.mesh = generateMesh(vertices.ToArray(), triangles, normals, uv);

            switch (direction)
            {
                case PlaneDirection.YZ:
                    ret.transform.Rotate(new Vector3(180, 0, 0));
                    break;
            }

            renderer.sharedMaterial = material;

            return ret;
        }
    }
}