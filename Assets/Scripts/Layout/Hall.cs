using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum = System.Enum;
using Convert = System.Convert;
using Math = System.Math;

public class Hall {
    private Vector3 upperLeftCorner;
    private Vector3 lowerRightCorner;
    private List<Room> rooms;

    public Hall(Vector3 upperLeftCorner, Vector3 lowerRightCorner)
    {
        this.upperLeftCorner = upperLeftCorner;
        this.lowerRightCorner = lowerRightCorner;
        rooms = new List<Room>();
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public Room GetRoomAt(int i)
    {
        return rooms[i];
    }

    public int NumRooms()
    {
        return rooms.Count;
    }
}