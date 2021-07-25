using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum = System.Enum;
using Convert = System.Convert;
using Math = System.Math;

public class Chunk
{
    private Vector3 upperLeftCorner;
    private Vector3 lowerRightCorner;
    private ProceduralGenerator generator;

    private const float GAP = 1f;
    private const int HALL_WIDTH = 4;

    private static Queue<Chunk> availableChunks = new Queue<Chunk>();
    private static List<Chunk> finalChunks = new List<Chunk>();

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
        return Math.Abs(lowerRightCorner.x - upperLeftCorner.x) * Math.Abs(lowerRightCorner.z - upperLeftCorner.z);
    }

    public float GetMinDimension()
    {
        var dimensionA = (lowerRightCorner.x - upperLeftCorner.x);
        var dimensionB = (lowerRightCorner.z - upperLeftCorner.z);
        return Math.Min(dimensionA, dimensionB);
    }

    public Chunk(Vector3 upperLeftCorner, Vector3 lowerRightCorner, ProceduralGenerator generator)
    {
        this.upperLeftCorner = upperLeftCorner;
        this.lowerRightCorner = lowerRightCorner;
        this.generator = generator;
    }

    public void Split()
    {
        SplitDirection direction;
        var dimensionA = (lowerRightCorner.x - upperLeftCorner.x);
        var dimensionB = (lowerRightCorner.z - upperLeftCorner.z);
        var threshold = 6 + GAP + HALL_WIDTH;
        if (dimensionA < threshold) {
            direction = SplitDirection.HORIZONTAL_SPLIT;
        } else if (dimensionB < threshold) {
            direction = SplitDirection.VERTICAL_SPLIT;
        } else {
            direction = GetRandomDirection();
        }
        Vector3 splitEndA = new Vector3(), wallDirection = new Vector3(), secondWallOffset = new Vector3(0f, 0f, 0f);
        float splitPosition;
        long wallAmount = 0;
        switch (direction)
        {
            case SplitDirection.VERTICAL_SPLIT:
                splitPosition = Random.Range(upperLeftCorner.x + GAP * HALL_WIDTH * 2, lowerRightCorner.x - GAP * HALL_WIDTH * 2);
                splitPosition = (float) Math.Ceiling(Convert.ToDecimal(splitPosition));
                splitEndA = new Vector3(splitPosition, upperLeftCorner.y, upperLeftCorner.z);
                wallDirection = new Vector3(0f, 0f, 1f);
                wallAmount = Convert.ToInt64(lowerRightCorner.z - upperLeftCorner.z);
                secondWallOffset.x = HALL_WIDTH;
                Chunk leftChunk = new Chunk(upperLeftCorner, new Vector3(splitPosition, upperLeftCorner.y, lowerRightCorner.z), generator);
                Chunk rightChunk = new Chunk(new Vector3(splitPosition + HALL_WIDTH, upperLeftCorner.y, upperLeftCorner.z), lowerRightCorner, generator);
                availableChunks.Enqueue(leftChunk);
                availableChunks.Enqueue(rightChunk);
                break;
            case SplitDirection.HORIZONTAL_SPLIT:
                splitPosition = Random.Range(upperLeftCorner.z + GAP * HALL_WIDTH * 2, lowerRightCorner.z - GAP * HALL_WIDTH * 2);
                splitPosition = (float) Math.Ceiling(Convert.ToDecimal(splitPosition));
                splitEndA = new Vector3(upperLeftCorner.x, upperLeftCorner.y, splitPosition);
                wallDirection = new Vector3(1f, 0f, 0f);
                wallAmount = Convert.ToInt64(lowerRightCorner.x - upperLeftCorner.x);
                secondWallOffset.z = HALL_WIDTH;
                Chunk topChunk = new Chunk(upperLeftCorner, new Vector3(lowerRightCorner.x, upperLeftCorner.y, splitPosition), generator);
                Chunk bottomChunk = new Chunk(new Vector3(upperLeftCorner.x, lowerRightCorner.y, splitPosition + HALL_WIDTH), lowerRightCorner, generator);
                availableChunks.Enqueue(topChunk);
                availableChunks.Enqueue(bottomChunk);
                break;
        }
        Utils.StackWalls(generator.wallPrefab, generator.layoutContainer, splitEndA, wallDirection, wallAmount);
        Utils.StackWalls(generator.wallPrefab, generator.layoutContainer, splitEndA + secondWallOffset, wallDirection, wallAmount);

        generator.AddHall(new Hall(splitEndA, splitEndA + secondWallOffset + wallDirection * wallAmount));
    }

    public static void SplitToWalls(Vector3 upperLeftCorner, Vector3 lowerRightCorner, ProceduralGenerator generator)
    {
        Chunk initialChunk = new Chunk(upperLeftCorner, lowerRightCorner, generator);
        availableChunks.Enqueue(initialChunk);
        while (availableChunks.Count > 0)
        {
            Chunk currentChunk = availableChunks.Dequeue();
            if (currentChunk.GetSize() > Convert.ToInt32(60 + GAP + HALL_WIDTH) && currentChunk.GetMinDimension() > Convert.ToInt32(10 + GAP + HALL_WIDTH))
            {
                currentChunk.Split();
            } else
            {
                finalChunks.Add(currentChunk);
            }
        }
    }
}