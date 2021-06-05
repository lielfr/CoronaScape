using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    public int roomsQuantity = 6;
    public GameObject container;
    public GameObject prefab;
    public GameObject floor;
    private float xPosMin, xPosMax, zPosMin, zPosMax; // Floor's bounds
    // Start is called before the first frame update
    void Start()
    {
        
        MeshRenderer floorRenderer = floor.GetComponent<MeshRenderer>();
        xPosMin = floorRenderer.bounds.min.x + 10;
        zPosMin = floorRenderer.bounds.min.z + 10;
        xPosMax = floorRenderer.bounds.max.x - 10;
        zPosMax = floorRenderer.bounds.max.z - 10;
        StartCoroutine(GenerateRooms());
    }

    IEnumerator GenerateRooms()
    {
        int currentRoomsQuantity = 0;
        while (currentRoomsQuantity < roomsQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax); // Based on current layout
            float zRandomPos = Random.Range(zPosMin, zPosMax); // Based on current layout
            GameObject generatedRoom = Instantiate(prefab, new Vector3(xRandomPos, 0, zRandomPos), Quaternion.identity);
            generatedRoom.transform.SetParent(container.transform);
            Renderer renderer = generatedRoom.GetComponent<Renderer>();
            generatedRoom.AddComponent<MeshCollider>();
            yield return new WaitForSeconds(0f);
            currentRoomsQuantity++;
        }
    }
}
