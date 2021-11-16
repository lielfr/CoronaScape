using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class ProceduralFloorGenerator : MonoBehaviour
{
    public float width = 7500;
    public float height = 5000;
    public float minX = -50000;
    public float minZ = -50000;
    public float maxX = 50000;
    public float maxZ = 50000;
    public float rotation;
    public int numRects = 3;
    public float roomWidth = 100;
    public float roomBreadth = 50;
    public float roomHeight = 20;
    public float doorWidth = 30;
    public float roomWallThickness = 10;

    object[] NewRoomShape(Vector3 baseTopLeftCorner, float roomWidth, float roomBreadth, float rotationAngle = 0f, bool flipTriangles = false)
    {
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, rotationAngle, 0f));
        Mesh ret = new Mesh();

        Vector3 baseToCeiling = new Vector3(0f, roomHeight, 0f);
        Vector3 leftToRight = new Vector3(roomWidth, 0f, 0f);

        Vector3 baseTopRightCorner = baseTopLeftCorner + leftToRight;
        Vector3 baseBottomLeftCorner = baseTopLeftCorner - new Vector3(
            0f,
            0f,
            roomBreadth
        );
        Vector3 baseBottomRightCorner = baseBottomLeftCorner + leftToRight;

        Vector3 baseBottomMid = (baseBottomLeftCorner + baseBottomRightCorner) / 2;
        Vector3 door = new Vector3(doorWidth / 2, 0f, 0f);
        Vector3 baseBottomDoorLeft = baseBottomMid - door;
        Vector3 baseBottomDoorRight = baseBottomMid + door;

        Vector3 ceilTopLeftCorner = baseTopLeftCorner + baseToCeiling;
        Vector3 ceilTopRightCorner = baseTopRightCorner + baseToCeiling;
        Vector3 ceilBottomLeftCorner = baseBottomLeftCorner + baseToCeiling;
        Vector3 ceilBottomRightCorner = baseBottomRightCorner + baseToCeiling;

        Vector3 ceilBottomDoorLeft = baseBottomDoorLeft + baseToCeiling;
        Vector3 ceilBottomDoorRight = baseBottomDoorRight + baseToCeiling;

        Vector3[] vertices = new Vector3[]
        {
            rotationMatrix.MultiplyPoint3x4(baseTopLeftCorner),
            rotationMatrix.MultiplyPoint3x4(baseTopRightCorner),
            rotationMatrix.MultiplyPoint3x4(ceilTopLeftCorner),
            rotationMatrix.MultiplyPoint3x4(ceilTopRightCorner),
            rotationMatrix.MultiplyPoint3x4(baseBottomLeftCorner),
            rotationMatrix.MultiplyPoint3x4(baseBottomRightCorner),
            rotationMatrix.MultiplyPoint3x4(ceilBottomLeftCorner),
            rotationMatrix.MultiplyPoint3x4(ceilBottomRightCorner),
            rotationMatrix.MultiplyPoint3x4(baseBottomDoorLeft),
            rotationMatrix.MultiplyPoint3x4(ceilBottomDoorLeft),
            rotationMatrix.MultiplyPoint3x4(baseBottomDoorRight),
            rotationMatrix.MultiplyPoint3x4(ceilBottomDoorRight),
        };

        int[] triangles;

        if (flipTriangles)
        {
            triangles = new int[]
             {
                0, 2, 1, 2, 3, 1,
                0, 4, 2, 6, 2, 4,
                5, 1, 7, 3, 7, 1,
                6, 4, 8, 9, 6, 8,
                10, 5, 11, 7, 11, 5
             };
        } else
        {
           triangles = new int[]
            {
                0, 1, 2, 3, 2, 1,
                0, 2, 4, 2, 6, 4,
                5, 7, 1, 7, 3, 1,
                6, 8, 4, 6, 9, 8,
                10, 11, 5, 11, 7, 5
            };
        }

        

        ret.vertices = vertices;
        ret.triangles = triangles;
        ret.RecalculateNormals();

        object[] retArr = new object[]
        {
            ret,
            rotationMatrix.MultiplyPoint3x4(baseBottomDoorLeft),
            rotationMatrix.MultiplyPoint3x4(baseBottomDoorRight),
            rotationMatrix.MultiplyPoint3x4(ceilBottomDoorLeft),
            rotationMatrix.MultiplyPoint3x4(ceilBottomDoorRight)
        };
        return retArr;
    }

    Mesh NewRoom(Vector3 baseTopLeftCorner, float rotationAngle = 0f)
    {
        List<CombineInstance> combineInstances = new List<CombineInstance>();

        object[] innerData = NewRoomShape(baseTopLeftCorner, roomWidth, roomBreadth, rotationAngle);

        Vector3 innerRoomTL = baseTopLeftCorner + new Vector3(roomWallThickness, 0, -roomWallThickness);
        object[] outerData = NewRoomShape(
            innerRoomTL,
            roomWidth - roomWallThickness * 2,
            roomBreadth - roomWallThickness * 2,
            rotationAngle,
            true
        );

        CombineInstance outerRoom = new CombineInstance()
        {
            mesh = (Mesh)innerData[0],
            subMeshIndex = 0,
            transform = Matrix4x4.identity
        };

        
        CombineInstance innerRoom = new CombineInstance()
        {
            mesh = (Mesh)outerData[0],
            subMeshIndex = 0,
            transform = Matrix4x4.identity

        };

        Mesh connector = new Mesh();
        connector.vertices = new Vector3[]
        {
            (Vector3)outerData[1],
            (Vector3)outerData[2],
            (Vector3)outerData[3],
            (Vector3)outerData[4],
            (Vector3)innerData[1],
            (Vector3)innerData[2],
            (Vector3)innerData[3],
            (Vector3)innerData[4],
        };

        connector.triangles = new int[]
        {
            0, 4, 2, 6, 2, 4,
            5, 3, 7, 5, 1, 3
        };
        connector.RecalculateNormals();

        CombineInstance connectorCombiner = new CombineInstance()
        {
            mesh = connector,
            subMeshIndex = 0,
            transform = Matrix4x4.identity
        };



        combineInstances.Add(outerRoom);
        combineInstances.Add(innerRoom);
        combineInstances.Add(connectorCombiner);

        Mesh ret = new Mesh();
        ret.CombineMeshes(combineInstances.ToArray(), true);
        ret.RecalculateNormals();


        return ret;
    }
    // Start is called before the first frame update
    Mesh NewRect(Vector3 topLeftCorner, float width, float height, float rotationAngle = 0f)
    {
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, rotationAngle, 0f));
        Mesh ret = new Mesh();
        Mesh rectMesh = new Mesh();
        List<CombineInstance> combineInstances = new List<CombineInstance>();
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

        // TODO: Add rooms here

        Vector3 topWidth = topRightCorner - topLeftCorner;
        int numRoomsW = Mathf.FloorToInt(topWidth.magnitude / roomWidth);

        for (int i = 0; i < numRoomsW; i++)
        {
            Mesh roomTop = NewRoom(topLeftCorner + new Vector3(i * roomWidth, 0f, 0f), rotationAngle);
            Mesh roomBottom = NewRoom(topRightCorner - new Vector3((i+1) * roomWidth, 0f, 0f), rotationAngle + 180f);
            Vector3 centerOffset = new Vector3((float)((i + 0.5f) * roomWidth), 0f, 0f);
            centerOffset = Matrix4x4.Rotate(Quaternion.Euler(0f, rotationAngle, 0f)).MultiplyPoint3x4(centerOffset);
            centerOffset = -centerOffset;
            Matrix4x4 rotateCenter = Matrix4x4.Translate(centerOffset) * Matrix4x4.Rotate(Quaternion.Euler(0f, 180f, 0f)) * Matrix4x4.Translate(-centerOffset);
            CombineInstance topRoomCombiner = new CombineInstance()
            {
                mesh = roomTop,
                subMeshIndex = 0,
                transform = Matrix4x4.identity
            };
            combineInstances.Add(topRoomCombiner);
            CombineInstance bottomRoomCombiner = new CombineInstance()
            {
                mesh = roomBottom,
                subMeshIndex = 0,
                transform = Matrix4x4.identity
            };
            combineInstances.Add(bottomRoomCombiner);
        }

        //Mesh testRoom = NewRoom(topLeftCorner, rotationAngle);
        //CombineInstance testRoomCombineInstance = new CombineInstance
        //{
        //    mesh = testRoom,
        //    subMeshIndex = 0,
        //    transform = Matrix4x4.identity
        //};
        //combineInstances.Add(testRoomCombineInstance);

        rectMesh.vertices = vertices;
        rectMesh.triangles = triangles;

        CombineInstance rectCombiner = new CombineInstance
        {
            mesh = rectMesh,
            subMeshIndex = 0,
            transform = Matrix4x4.identity
        };
        combineInstances.Add(rectCombiner);

        ret.CombineMeshes(combineInstances.ToArray(), true);
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

        float baseRotation = Random.Range(0f, 360f);
        // Start with base rect
        Mesh baseMesh = NewRectCenter(new Vector3(0, 0, 0), width, height, baseRotation);
        CombineInstance baseCombineInstance = new CombineInstance
        {
            mesh = baseMesh,
            subMeshIndex = 0,
            transform = Matrix4x4.identity
        };
        combineInstances.Add(baseCombineInstance);

        //for (int i = 0; i < numRects - 1; i++)
        //{
        //    CombineInstance combineInstance = new CombineInstance
        //    {
        //        mesh = ExtendBaseRect(new Vector3(0, 0, 0), width, height, width, height),
        //        subMeshIndex = 0,
        //        transform = Matrix4x4.identity
        //    };
        //    combineInstances.Add(combineInstance);
        //}


        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true);
        combinedMesh.RecalculateNormals();
        combinedMesh.RecalculateNormals();
        combinedMesh.RecalculateTangents();
        combinedMesh.Optimize();
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
