using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Convert = System.Convert;

public class ProceduralGenerator : MonoBehaviour
{
    public GameObject wallPrefab; 
    public GameObject layoutContainer;
    public GameObject floorContainer;
    public GameObject lightPrefab;

    public GameObject redPotionPrefab;
    public GameObject bluePotionPrefab;
    public GameObject greenPotionPrefab;
    public GameObject coinPrefab;
    public GameObject boxPrefab;

    public int wallHeight = 30;

    private object matrixLock;

    public int roomAmount = 5;
    
    public enum LayoutCell {
        WALL,
        HALL,
        CHUNK,
        DOOR,
        ELEVATOR
    }

    public LayoutCell[,] layoutMatrix;
    public const int LAYOUT_DIM = 1000;

    public GameObject playerObject;

    private GameManager gameManager;

    List<Hall> halls;

    List<Room> rooms;
    private void Awake()
    {
        rooms = new List<Room>();
        halls = new List<Hall>();
        matrixLock = new object();
        layoutMatrix = new LayoutCell[LAYOUT_DIM, LAYOUT_DIM];
        gameManager = GameManager.Instance;
        ResetLayoutMatrix();
        GenerateLayout();
    }

    void ResetLayoutMatrix()
    {
        for (int i = 0; i < LAYOUT_DIM; i++)
        {
            for (int j = 0; j < LAYOUT_DIM; j++)
            {
                layoutMatrix[i, j] = LayoutCell.CHUNK;
            }
        }
    }

    void GenerateLayout()
    {
        lock(matrixLock)
        {
            Vector3 floorBounds = floorContainer.GetComponent<Renderer>().bounds.size;
            Vector2Int cornerA = new Vector2Int(0, 0);
            Vector2Int cornerB = new Vector2Int(LAYOUT_DIM - 1, LAYOUT_DIM - 1);

            

            for (int i = 0; i < LAYOUT_DIM; i++)
            {
                layoutMatrix[0, i] = LayoutCell.WALL;
                layoutMatrix[i, 0] = LayoutCell.WALL;
                layoutMatrix[LAYOUT_DIM - 1, i] = LayoutCell.WALL;
                layoutMatrix[i, LAYOUT_DIM - 1] = LayoutCell.WALL;
            }

            Chunk.SplitToWalls(cornerA, cornerB, this, roomAmount);

            PlaceElevators();

            MatrixToScreen();
        }
        PopulateRooms();
        PlacePlayer();
        // Chunk.AddLights();
    }

    void MatrixToScreen()
    {
        Vector3 cornerA = transform.position;
        for (int i = 0; i < LAYOUT_DIM; i++)
        {
            for (int j = 0; j < LAYOUT_DIM; j++)
            {
                if (layoutMatrix[i, j] == LayoutCell.WALL)
                {
                    Utils.StackWalls(wallPrefab, layoutContainer, cornerA + new Vector3((float)i, 0f, (float)j), new Vector3(0f, 0f, 1f), 1);
                }
            }
        }
    }

    void Clear()
    {
        lock(matrixLock)
        {
            foreach (Transform child in layoutContainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            ResetLayoutMatrix();
        }
        halls.Clear();
        rooms.Clear();
        gameManager.RestartGame();
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

    public void AddHall(Hall hall) {
        halls.Add(hall);
    }

    public void AddLightAt(Vector2Int pos, int minDim, int roomSize)
    {
        var newLight = Instantiate<GameObject>(lightPrefab);
        Light lightObject = newLight.transform.GetComponentInChildren<Light>();
        lightObject.intensity = roomSize;
        lightObject.range = minDim;
        newLight.transform.parent = layoutContainer.transform;
        newLight.transform.Translate(layoutContainer.transform.position + new Vector3(pos.x, wallHeight - 0.5f, pos.y));
    }

    public void PopulateRooms()
    {
        var offset = new Vector2Int(Convert.ToInt32(transform.position.x), Convert.ToInt32(transform.position.z));
        foreach (var chunk in Chunk.finalChunks)
        {
            //rooms.Add(new Room(offset + chunk.GetUpperLeftCorner(), offset + chunk.GetLowerRightCorner(), this, redPotionPrefab, bluePotionPrefab, greenPotionPrefab, coinPrefab, boxPrefab));
        }
    }

    void PlacePlayer()
    {
        var offset = new Vector2(transform.position.x, transform.position.z);
        if (gameManager.Level == 1)
        {
            var roomIndex = Random.Range(0, rooms.Count);
            var selectedRoom = rooms[roomIndex];
            var playerX = Random.Range(selectedRoom.GetXMin(), selectedRoom.GetXMax());
            var playerZ = Random.Range(selectedRoom.GetZMin(), selectedRoom.GetZMax());
            playerObject.transform.position = new Vector3(playerX, playerObject.transform.position.y, playerZ);
        }
    }

    void PlaceElevators()
    {
        // Select random hall
        // Select edge (make sure it actually exists!)
        // Select random position on the edge
        // Place elevator
    }
}
