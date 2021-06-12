using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum WallTypes { CLEAN, DOOR, WINDOW }
    private Vector2 wallPosition;
    private WallTypes wallType = WallTypes.CLEAN;


    public Vector2 GetWallPosition()
    {
        return wallPosition;
    }
    public void SetWallPosition(Vector2 wallPosition)
    {
        this.wallPosition = wallPosition;
    }
    public WallTypes GetWallType()
    {
        return wallType;
    }
    public void SetWallType(WallTypes wallType)
    {
        this.wallType = wallType;
    }
}
