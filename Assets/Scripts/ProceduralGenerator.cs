using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    public GameObject cleanWallPrefab; 
    private int rows = 3;
    private int cols = 3;
    private int floorsNumber = 1;
    private Floor[] floors;
    private void Awake()
    {
        Generate();
        Place();
    }

    void Generate()
    {
        floors = new Floor[floorsNumber];
        for (int countFloors = 0; countFloors < floorsNumber; countFloors++) 
        {
            Room[,] rooms = new Room[rows, cols];
            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < cols; j++)
                {

                    rooms[i, j] = gameObject.AddComponent<Room>();
                    float wallWidth = rooms[i, j].GetWallWidth();
                    rooms[i, j].SetPosition(new Vector2(i * wallWidth, j * wallWidth));

                }
                floors[countFloors] = gameObject.AddComponent<Floor>();
                floors[countFloors].SetFloorNumber(countFloors);
                floors[countFloors].SetRooms(rooms);
            }
        }
    }

    void Place()
    {
        foreach(Floor floor in floors) { 
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Room room = floor.GetRooms()[i, j];
                    float wallWidth = room.GetWallWidth();
                    float wallHight = room.GetWallHight();
                    GameObject wall1 = Instantiate(cleanWallPrefab, new Vector3(room.GetPosition().x - wallWidth / 2, floor.GetFloorNumber() * wallHight, room.GetPosition().y), Quaternion.Euler(0, 90, 0));
                    wall1.transform.parent = transform;
                    GameObject wall2 = Instantiate(cleanWallPrefab, new Vector3(room.GetPosition().x, floor.GetFloorNumber() * wallHight, room.GetPosition().y - wallWidth / 2), Quaternion.Euler(0, 0, 0));
                    wall2.transform.parent = transform;
                    GameObject wall3 = Instantiate(cleanWallPrefab, new Vector3(room.GetPosition().x, floor.GetFloorNumber() * wallHight, room.GetPosition().y + wallWidth / 2), Quaternion.Euler(0, 0, 0));
                    wall3.transform.parent = transform;
                    GameObject wall4 = Instantiate(cleanWallPrefab, new Vector3(room.GetPosition().x + wallWidth / 2, floor.GetFloorNumber() * wallHight, room.GetPosition().y), Quaternion.Euler(0, 90, 0));
                    wall4.transform.parent = transform;
                }
            }
        }
    }
}
