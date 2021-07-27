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

    public int[,] layoutMatrix;

    public const int WALL = 1;
    public const int LAYOUT_DIM = 1000;

    List<Hall> halls;
    private void Awake()
    {
        layoutMatrix = new int[LAYOUT_DIM, LAYOUT_DIM];
        GenerateLayout();
    }

    void GenerateLayout()
    {
        halls = new List<Hall>();
        Vector3 floorBounds = floorContainer.GetComponent<Renderer>().bounds.size;
        Vector3 cornerA = transform.position;
        Vector3 cornerB = transform.position + floorBounds;
        Vector3 cornerC = cornerA + new Vector3(floorBounds.x, 0f, 0f);

        // Chunk.SplitToWalls(cornerA, cornerB, this);

        for (int i = 0; i < LAYOUT_DIM; i++)
        {
            layoutMatrix[0, i] = WALL;
            layoutMatrix[i, 0] = WALL;
            layoutMatrix[LAYOUT_DIM - 1, i] = WALL;
            layoutMatrix[i, LAYOUT_DIM - 1] = WALL;
        }

        MatrixToScreen();
    }

    void MatrixToScreen()
    {
        Vector3 cornerA = transform.position;
        for (int i = 0; i < LAYOUT_DIM; i++)
        {
            for (int j = 0; j < LAYOUT_DIM; j++)
            {
                if (layoutMatrix[i, j] == WALL)
                {
                    Utils.StackWalls(wallPrefab, layoutContainer, cornerA + new Vector3((float)i, 0f, (float)j), new Vector3(0f, 0f, 1f), 1);
                }
            }
        }
    }

    void Clear()
    {
        foreach (Transform child in layoutContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
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
}
