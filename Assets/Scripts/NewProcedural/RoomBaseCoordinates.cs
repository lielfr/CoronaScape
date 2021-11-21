using UnityEngine;

public struct RoomBaseCoordinates
{

    public RoomBaseCoordinates(Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft, Vector3 bottomRight)
    {
        this.topLeft = new Vector2(topLeft.x, topLeft.z);
        this.topRight = new Vector2(topRight.x, topRight.z);
        this.bottomLeft = new Vector2(bottomLeft.x, bottomLeft.z);
        this.bottomRight = new Vector2(bottomRight.x, bottomRight.z);
    }

    public Vector2 topLeft { get; }
    public Vector2 topRight { get; }
    public Vector2 bottomLeft { get; }
    public Vector2 bottomRight { get; }

    public Vector2Pair[] getPairs()
    {
        Vector2Pair[] pairs = new Vector2Pair[] {
            new Vector2Pair(topLeft, topRight),
            new Vector2Pair(topLeft, bottomLeft),
            new Vector2Pair(topRight, bottomRight),
            new Vector2Pair(bottomLeft, bottomRight)
        };

        return pairs;
    }

    public static RoomBaseCoordinates operator *(Matrix4x4 m, RoomBaseCoordinates coords)
    {
        Vector3 newTopLeft = new Vector3(coords.topLeft.x, 0, coords.topLeft.y);
        Vector3 newTopRight = new Vector3(coords.topRight.x, 0, coords.topRight.y);
        Vector3 newBottomLeft = new Vector3(coords.bottomLeft.x, 0, coords.bottomLeft.y);
        Vector3 newBottomRight = new Vector3(coords.bottomRight.x, 0, coords.bottomRight.y);

        newTopLeft = m.MultiplyPoint3x4(newTopLeft);
        newTopRight = m.MultiplyPoint3x4(newTopRight);
        newBottomLeft = m.MultiplyPoint3x4(newBottomLeft);
        newBottomRight = m.MultiplyPoint3x4(newBottomRight);

        return new RoomBaseCoordinates(newTopLeft, newTopRight, newBottomLeft, newBottomRight);
    }
}
