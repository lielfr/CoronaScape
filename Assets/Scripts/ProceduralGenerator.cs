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

    List<Hall> halls;
    private void Awake()
    {
        GenerateLayout();
    }

    void GenerateLayout()
    {
        halls = new List<Hall>();
        Vector3 floorBounds = floorContainer.GetComponent<Renderer>().bounds.size;
        Vector3 cornerA = transform.position;
        Vector3 cornerB = transform.position + floorBounds;
        Vector3 cornerC = cornerA + new Vector3(floorBounds.x, 0f, 0f);
        long coverAmount = Convert.ToInt64(floorContainer.transform.localScale.x * floorContainer.transform.localScale.z);
        Utils.StackWalls(wallPrefab, layoutContainer, cornerA, new Vector3(0f, 0f, 1f), coverAmount);
        Utils.StackWalls(wallPrefab, layoutContainer, cornerC, new Vector3(0f, 0f, 1f), coverAmount);
        Utils.StackWalls(wallPrefab, layoutContainer, cornerB, new Vector3(-1f, 0f, 0f), coverAmount);
        Utils.StackWalls(wallPrefab, layoutContainer, cornerA, new Vector3(1f, 0f, 0f), coverAmount);

        Chunk.SplitToWalls(cornerA, cornerB, this);
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
