using UnityEngine;
public struct Vector2Pair
{
    public Vector2Pair(Vector2 first, Vector2 second)
    {
        this.first = first;
        this.second = second;
    }

    public Vector2 first { get; }
    public Vector2 second { get; }
}
