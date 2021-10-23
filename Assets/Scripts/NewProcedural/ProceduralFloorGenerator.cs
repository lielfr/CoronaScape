using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProceduralFloorGenerator : MonoBehaviour
{
    public float width = 75;
    public float height = 50;
    public float minX = -500;
    public float minZ = -500;
    public float maxX = 500;
    public float maxZ = 500;
    public float rotation;
    public int numRects = 3;
    // Start is called before the first frame update
    Mesh NewRect(Vector3 topLeftCorner, float width, float height, float rotationAngle = 0f)
    {
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, rotationAngle, 0f));
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
            rotationMatrix * topLeftCorner,
            rotationMatrix * topRightCorner,
            rotationMatrix * bottomRightCorner,
            rotationMatrix * bottomLeftCorner
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
    Mesh NewRectCenter(Vector3 centerPoint, float width, float height, float rotationAngle = 0f)
    {
        Vector3 topLeft = new Vector3(
            centerPoint.x - width / 2,
            centerPoint.y,
            centerPoint.z + height / 2
        );
        return NewRect(topLeft, width, height, rotationAngle);
    }

    Mesh ExtendBaseRect(Vector3 baseCenterPoint, float baseWidth, float baseHeight, float width, float height)
    {
        // Randomly select the line among which the new center will be
        Vector3 baseTopLeft = new Vector3(
            baseCenterPoint.x - baseWidth / 2,
            baseCenterPoint.y,
            baseCenterPoint.z + baseHeight / 2
        );
        Vector3 baseTopRight = baseTopLeft + new Vector3(
            baseWidth,
            0f,
            0f
        );

        Vector3 baseBottomLeft = baseTopLeft - new Vector3(
            0f,
            0f,
            baseHeight
        );

        Vector3 baseBottomRight = baseBottomLeft + new Vector3(
            baseWidth,
            0f,
            0f
        );

        Vector3 diag1 = baseBottomRight - baseTopLeft;
        Vector3 diag2 = baseBottomLeft - baseTopRight;


        bool direction = Random.Range(0, 2) == 1;
        float offset = Random.Range(0f, 1f);

        Vector3 newCenter = direction ? (baseTopLeft + offset * diag1) : (baseTopRight + offset * diag2);

        Mesh ret = NewRectCenter(newCenter, width, height, Random.Range(0f, 90f));

        newCenter = ret.vertices[0] + (ret.vertices[2] - ret.vertices[0]);

        float translationMagnitude = Random.Range(0f, 1f);

        Matrix4x4 translationMatrix = Matrix4x4.Translate(translationMagnitude * (newCenter - baseCenterPoint));

        List<Vector3> afterTranslation = new List<Vector3>();

        foreach (Vector3 v in ret.vertices)
        {
            afterTranslation.Add(translationMatrix * v);
        }

        ret.vertices = afterTranslation.ToArray();

        return ret;
    }

    public void Clear()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh.Clear();
    }

    public void GenerateLayout()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        List<CombineInstance> combineInstances = new List<CombineInstance>();

        // Start with base rect
        Mesh baseMesh = NewRectCenter(new Vector3(0, 0, 0), width, height, Random.Range(0f, 360f));
        CombineInstance baseCombineInstance = new CombineInstance
        {
            mesh = baseMesh,
            subMeshIndex = 0,
            transform = Matrix4x4.identity
        };
        combineInstances.Add(baseCombineInstance);

        for (int i = 0; i < numRects - 1; i++)
        {
            CombineInstance combineInstance = new CombineInstance
            {
                mesh = ExtendBaseRect(new Vector3(0, 0, 0), width, height, width, height),
                subMeshIndex = 0,
                transform = Matrix4x4.identity
            };
            combineInstances.Add(combineInstance);
        }


        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true);
        filter.mesh = combinedMesh;
    }

    public void OnRegenerateAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Regenerating layout");
            Clear();
            GenerateLayout();
        }
    }

    void Start()
    {
        GenerateLayout();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
