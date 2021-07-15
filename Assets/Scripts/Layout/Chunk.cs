using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum = System.Enum;
using Convert = System.Convert;

public class Chunk
{
    private Vector3 upperLeftCorner;
    private Vector3 lowerRightCorner;
    private ProceduralGenerator generator;

    private const float GAP = 1f;
    private const int HALL_WIDTH = 4;

    public enum SplitDirection {
        HORIZONTAL_SPLIT,
        VERTICAL_SPLIT
    }

    public SplitDirection GetRandomDirection()
    {
        var possibleOptions = Enum.GetValues(typeof(SplitDirection));
        return (SplitDirection) possibleOptions.GetValue(Random.Range(0, possibleOptions.Length));
    }

    // TODO: Add neighbouring halls lists.

    public float GetSize()
    {
        return (lowerRightCorner.x - upperLeftCorner.x) * (lowerRightCorner.z - upperLeftCorner.z);
    }

    public Chunk(Vector3 upperLeftCorner, Vector3 lowerRightCorner, ProceduralGenerator generator)
    {
        this.upperLeftCorner = upperLeftCorner;
        this.lowerRightCorner = lowerRightCorner;
        this.generator = generator;
    }

    public void Split()
    {
        SplitDirection direction = GetRandomDirection();
        Vector3 splitEndA = new Vector3(), wallDirection = new Vector3(), secondWallOffset = new Vector3(0f, 0f, 0f);
        float splitPosition;
        long wallAmount = 0;
        switch (direction)
        {
            case SplitDirection.HORIZONTAL_SPLIT:
                splitPosition = Random.Range(upperLeftCorner.x + GAP, lowerRightCorner.x - GAP * HALL_WIDTH);
                splitEndA = new Vector3(splitPosition, upperLeftCorner.y, upperLeftCorner.z);
                wallDirection = new Vector3(0f, 0f, 1f);
                wallAmount = Convert.ToInt64(lowerRightCorner.z - upperLeftCorner.z);
                secondWallOffset.x = HALL_WIDTH;
                break;
            case SplitDirection.VERTICAL_SPLIT:
                splitPosition = Random.Range(upperLeftCorner.z + GAP, lowerRightCorner.z - GAP * HALL_WIDTH);
                splitEndA = new Vector3(upperLeftCorner.x, upperLeftCorner.y, splitPosition);
                wallDirection = new Vector3(1f, 0f, 0f);
                wallAmount = Convert.ToInt64(lowerRightCorner.x - upperLeftCorner.x);
                secondWallOffset.z = HALL_WIDTH;
                break;
        }
        Utils.StackWalls(generator.wallPrefab, generator.layoutContainer, splitEndA, wallDirection, wallAmount);
        Utils.StackWalls(generator.wallPrefab, generator.layoutContainer, splitEndA + secondWallOffset, wallDirection, wallAmount);
    }
}