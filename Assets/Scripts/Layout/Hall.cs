using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum = System.Enum;
using Convert = System.Convert;
using Math = System.Math;

public class Hall {
    public Vector2 upperLeftCorner;
    public Vector2 lowerRightCorner;
    private List<Room> rooms;

    public Hall(Vector2 upperLeftCorner, Vector2 lowerRightCorner)
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