using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralFloorGenerator : MonoBehaviour
{
    public float width = 75;
    public float height = 50;
    public float minX = -500;
    public float minZ = -500;
    public float maxX = 500;
    public float maxZ = 500;
    public float rotation;
    // Start is called before the first frame update
    Mesh NewRect(Vector3 topLeftCorner, float width, float height)
    {
        Mesh ret = new Mesh();
        Vector3 topRightCorner = topLeftCorner + new Vector3(
            width,
            0f,
            0f
        );

        Vector3 bottomLeftCorner = topLeftCorner - new Vector3(
            0f,
            0f,
            height
        );

        Vector3 bottomRightCorner = bottomLeftCorner + new Vector3(
            width,
            0f,
            0f
        );
        Vector3[] vertices = new Vector3[]
        {
            topLeftCorner,
            topRightCorner,
            bottomRightCorner,
            bottomLeftCorner
        };
        int[] triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };

        ret.vertices = vertices;
        ret.triangles = triangles;
        ret.RecalculateNormals();

        return ret;

    }
    Mesh NewRectCenter(Vector3 centerPoint, float width, float height)
    {
        Vector3 topLeft = new Vector3(
            centerPoint.x - width / 2,
            centerPoint.y,
            centerPoint.z + height / 2
        );
        return NewRect(topLeft, width, height);
    }
    void Start()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        List<CombineInstance> combineInstances = new List<CombineInstance>();

        // Start with base rect
        Mesh baseMesh = NewRectCenter(new Vector3(0, 0, 0), width, height);
        CombineInstance baseCombineInstance = new CombineInstance
        {
            mesh = baseMesh,
            subMeshIndex = 0,
            transform = Matrix4x4.Rotate(Quaternion.Euler(0, Random.Range(0f, 360f), 0))
        };
        combineInstances.Add(baseCombineInstance);

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), false);
        filter.mesh = combinedMesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
