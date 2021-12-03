using UnityEngine;

public struct RoomBaseCoordinates
{

    public RoomBaseCoordinates(Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft, Vector3 bottomRight)
    {
        this.topLeft = new Vector2(topLeft.x, topLeft.z);
        this.topRight = new Vector2(topRight.x, topRight.z);
        this.bottomLeft = new Vector2(bottomLeft.x, bottomLeft.z);
        this.bottomRight = new Vector2(bottomRight.x, bottomRight.z);
        topLeftOrig = topLeft;
        topRightOrig = topRight;
        bottomLeftOrig = bottomLeft;
        bottomRightOrig = bottomRight;
    }

    public Vector2 topLeft { get; }
    public Vector2 topRight { get; }
    public Vector2 bottomLeft { get; }
    public Vector2 bottomRight { get; }

    public Vector3 topLeftOrig { get; }
    public Vector3 topRightOrig { get; }
    public Vector3 bottomLeftOrig { get; }
    public Vector3 bottomRightOrig { get; }

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
        return new RoomBaseCoordinates(m.MultiplyPoint3x4(coords.topLeftOrig),
            m.MultiplyPoint3x4(coords.topRightOrig),
            m.MultiplyPoint3x4(coords.bottomLeftOrig),
            m.MultiplyPoint3x4(coords.bottomRightOrig)
        );
    }
}
