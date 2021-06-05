using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    public int roomsQuantity = 6;
    public GameObject container;
    public GameObject prefab;
    public GameObject floor;
    private float xPosMin, xPosMax, zPosMin, zPosMax, yPos; // Floor's bounds
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer floorRenderer = floor.GetComponent<MeshRenderer>();
        xPosMin = floorRenderer.bounds.min.x;
        zPosMin = floorRenderer.bounds.min.z;
        xPosMax = floorRenderer.bounds.max.x;
        zPosMax = floorRenderer.bounds.max.z;
        yPos = floorRenderer.bounds.max.y;
        StartCoroutine(GenerateRooms());
    }

    IEnumerator GenerateRooms()
    {
        int currentRoomsQuantity = 0;
        while (currentRoomsQuantity < roomsQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax); // Based on current layout
            float zRandomPos = Random.Range(zPosMin, zPosMax); // Based on current layout
            GameObject generatedRoom = Instantiate(prefab, new Vector3(xRandomPos, yPos, zRandomPos), Quaternion.identity);
            generatedRoom.transform.SetParent(container.transform);
            Renderer renderer = generatedRoom.GetComponent<Renderer>();
            generatedRoom.AddComponent<MeshCollider>();
            yield return new WaitForSeconds(0f);
            currentRoomsQuantity++;
        }
    }
}
