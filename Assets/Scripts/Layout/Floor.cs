using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private int floorNumber;
    private Room[,] rooms;

    public int GetFloorNumber()
    {
        return floorNumber;
    }

    public void SetFloorNumber(int floorNumber)
    {
        this.floorNumber = floorNumber;
    }

    public Room[,] GetRooms()
    {
        return rooms;
    }

    public void SetRooms(Room[,] rooms)
    {
        this.rooms = rooms;
    }
}
