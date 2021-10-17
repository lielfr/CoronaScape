using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enum = System.Enum;
using Convert = System.Convert;
using Math = System.Math;

public class Chunk
{
    private Vector2Int upperLeftCorner;
    private Vector2Int lowerRightCorner;
    private static ProceduralGenerator generator;

    private const int GAP = 25;
    private const int HALL_WIDTH = 30;

    private static Queue<Chunk> availableChunks = new Queue<Chunk>();
    public static List<Chunk> finalChunks = new List<Chunk>();

    private List<Hall> hallsLeft, hallsRight, hallsDown, hallsUp;

    public enum SplitDirection {
        HORIZONTAL_SPLIT,
        VERTICAL_SPLIT
    }

    private SplitDirection currentSplitDirection;

    public SplitDirection GetRandomDirection()
    {
        var possibleOptions = Enum.GetValues(typeof(SplitDirection));
        return (SplitDirection) possibleOptions.GetValue(Random.Range(0, possibleOptions.Length));
    }

    // TODO: Add neighbouring halls lists.

    public int GetSize()
    {
        return Math.Abs(lowerRightCorner.x - upperLeftCorner.x) * Math.Abs(lowerRightCorner.y - upperLeftCorner.y);
    }

    public float GetMinDimension()
    {
        var dimensionA = (lowerRightCorner.x - upperLeftCorner.x);
        var dimensionB = (lowerRightCorner.y - upperLeftCorner.y);
        return Math.Min(dimensionA, dimensionB);
    }

    public Chunk(Vector2Int upperLeftCorner, Vector2Int lowerRightCorner, ProceduralGenerator generator)
    {
        this.upperLeftCorner = upperLeftCorner;
        this.lowerRightCorner = lowerRightCorner;
        Chunk.generator = generator;
        this.hallsLeft = new List<Hall>();
        this.hallsRight = new List<Hall>();
        this.hallsDown = new List<Hall>();
        this.hallsUp = new List<Hall>();
        this.currentSplitDirection = GetRandomDirection();
    }

    public Chunk(Vector2Int upperLeftCorner, Vector2Int lowerRightCorner, ProceduralGenerator generator, SplitDirection direction) : 
        this(upperLeftCorner, lowerRightCorner, generator)
    {
        this.currentSplitDirection = direction;
    }

    public void Split()
    {
        var dimensionA = (lowerRightCorner.x - upperLeftCorner.x);
        var dimensionB = (lowerRightCorner.y - upperLeftCorner.y);
        Vector2Int splitEndA = new Vector2Int(), secondWallOffset = new Vector2Int();
        int splitPosition;
        Hall newHall;
        switch (currentSplitDirection)
        {
            case SplitDirection.VERTICAL_SPLIT:
                splitPosition = Convert.ToInt32(Random.Range(upperLeftCorner.x + GAP + HALL_WIDTH, lowerRightCorner.x - GAP - HALL_WIDTH));
                splitEndA = new Vector2Int(splitPosition, upperLeftCorner.y);
                secondWallOffset.x = HALL_WIDTH;
                FillWithAndAddWalls(splitEndA, new Vector2Int(splitPosition + HALL_WIDTH, lowerRightCorner.y), ProceduralGenerator.LayoutCell.HALL);
                newHall = new Hall(splitEndA, new Vector2Int(splitPosition + HALL_WIDTH, lowerRightCorner.y));
                ConnectAdjacentHalls(splitEndA, new Vector2Int(splitPosition + HALL_WIDTH, lowerRightCorner.y), currentSplitDirection);
                Chunk leftChunk = new Chunk(upperLeftCorner, new Vector2Int(splitPosition, lowerRightCorner.y), generator, SplitDirection.HORIZONTAL_SPLIT);
                Chunk rightChunk = new Chunk(new Vector2Int(splitPosition + HALL_WIDTH, upperLeftCorner.y), lowerRightCorner, generator, SplitDirection.HORIZONTAL_SPLIT);
                availableChunks.Enqueue(leftChunk);
                availableChunks.Enqueue(rightChunk);
                leftChunk.hallsLeft.AddRange(hallsLeft);
                leftChunk.hallsRight.Add(newHall);
                rightChunk.hallsRight.AddRange(hallsRight);
                rightChunk.hallsLeft.Add(newHall);
                DistributeHallsVertical(hallsUp, leftChunk.hallsUp, rightChunk.hallsUp, splitPosition, upperLeftCorner, lowerRightCorner);
                DistributeHallsVertical(hallsDown, leftChunk.hallsDown, rightChunk.hallsDown, splitPosition, upperLeftCorner, lowerRightCorner);
                break;
            default:
                splitPosition = Convert.ToInt32(Random.Range(upperLeftCorner.y + GAP + HALL_WIDTH, lowerRightCorner.y - GAP - HALL_WIDTH));
                splitEndA = new Vector2Int(upperLeftCorner.x, splitPosition);
                secondWallOffset.y = HALL_WIDTH;
                FillWithAndAddWalls(splitEndA, new Vector2Int(lowerRightCorner.x, splitPosition + HALL_WIDTH), ProceduralGenerator.LayoutCell.HALL);
                newHall = new Hall(splitEndA, new Vector2Int(lowerRightCorner.x, splitPosition + HALL_WIDTH));
                ConnectAdjacentHalls(splitEndA, new Vector2Int(lowerRightCorner.x, splitPosition + HALL_WIDTH), currentSplitDirection);
                Chunk topChunk = new Chunk(upperLeftCorner, new Vector2Int(lowerRightCorner.x, splitPosition), generator, SplitDirection.VERTICAL_SPLIT);
                Chunk bottomChunk = new Chunk(new Vector2Int(upperLeftCorner.x, splitPosition + HALL_WIDTH), lowerRightCorner, generator, SplitDirection.VERTICAL_SPLIT);
                availableChunks.Enqueue(topChunk);
                availableChunks.Enqueue(bottomChunk);
                topChunk.hallsUp.AddRange(hallsUp);
                topChunk.hallsDown.Add(newHall);
                bottomChunk.hallsDown.AddRange(hallsDown);
                bottomChunk.hallsUp.Add(newHall);
                DistributeHallsHorizontal(hallsLeft, topChunk.hallsLeft, bottomChunk.hallsLeft, splitPosition, upperLeftCorner, lowerRightCorner);
                DistributeHallsHorizontal(hallsRight, topChunk.hallsRight, bottomChunk.hallsRight, splitPosition, upperLeftCorner, lowerRightCorner);
                break;
        }
        generator.AddHall(newHall);
    }

    private void DistributeHallsVertical(List<Hall> sourceList, List<Hall> leftTarget, List<Hall> rightTarget, int splitPosition, Vector2Int upperLeftCorner, Vector2Int lowerRightCorner)
    {
        foreach (var item in sourceList)
        {
            if (item.upperLeftCorner.x >= upperLeftCorner.x || item.lowerRightCorner.x <= splitPosition)
                leftTarget.Add(item);
            if (item.upperLeftCorner.x > splitPosition + HALL_WIDTH || item.lowerRightCorner.x <= lowerRightCorner.x)
                rightTarget.Add(item);
        } 
    }
    private void DistributeHallsHorizontal(List<Hall> sourceList, List<Hall> topTarget, List<Hall> bottomTarget, int splitPosition, Vector2Int upperLeftCorner, Vector2Int lowerRightCorner)
    {
        foreach (var item in sourceList)
        {
            if (item.upperLeftCorner.y >= upperLeftCorner.y || item.lowerRightCorner.y <= splitPosition)
                topTarget.Add(item);
            if (item.upperLeftCorner.y > splitPosition + HALL_WIDTH || item.lowerRightCorner.y <= lowerRightCorner.y)
                bottomTarget.Add(item);
        } 
    }

    public static void SplitToWalls(Vector2Int upperLeftCorner, Vector2Int lowerRightCorner, ProceduralGenerator generator, int roomAmount)
    {
        finalChunks.Clear();
        availableChunks.Clear();
        Chunk initialChunk = new Chunk(upperLeftCorner, lowerRightCorner, generator);
        availableChunks.Enqueue(initialChunk);
        int numSplits = 0;
        while (availableChunks.Count > 0 && numSplits < roomAmount - 1)
        {
            Chunk currentChunk = availableChunks.Dequeue();
            if (currentChunk.GetSize() > 70 + HALL_WIDTH + GAP * 2)
            {
                currentChunk.Split();
                numSplits++;
            } else
            {
                finalChunks.Add(currentChunk);
            }
        }
        while (availableChunks.Count > 0)
        {
            finalChunks.Add(availableChunks.Dequeue());
        }
    }

    private void FillWithAndAddWalls(Vector2Int topLeft, Vector2Int bottomRight, ProceduralGenerator.LayoutCell cellType)
    {
        for (int i = topLeft.x + 1; i < bottomRight.x; i++)
        {
            for (int j = topLeft.y + 1; j < bottomRight.y; j++)
            {
                try {
                    generator.layoutMatrix[i, j] = cellType;
                } catch (System.Exception e)
                {
                    Debug.LogErrorFormat("Exception: {0}, i = {1}, j = {2}", e.ToString(), i, j);
                }
            }
        }

        for (int i = topLeft.x; i <= bottomRight.x; i++)
        {
            generator.layoutMatrix[i, topLeft.y] = ProceduralGenerator.LayoutCell.WALL;
            generator.layoutMatrix[i, bottomRight.y] = ProceduralGenerator.LayoutCell.WALL;
        }

        for (int j = topLeft.y; j <= bottomRight.y; j++)
        {
            generator.layoutMatrix[topLeft.x, j] = ProceduralGenerator.LayoutCell.WALL;
            generator.layoutMatrix[bottomRight.x, j] = ProceduralGenerator.LayoutCell.WALL;
        }
    }

    private void ConnectAdjacentHalls(Vector2Int topLeft, Vector2Int bottomRight, SplitDirection wallDirection)
    {
        if (wallDirection == SplitDirection.VERTICAL_SPLIT)
        {
            for (int i = topLeft.x; i < bottomRight.x; i++)
            {
                if (topLeft.y > 0)
                {
                    var topWallCell = generator.layoutMatrix[i, topLeft.y];
                    var aboveTopWallCell = generator.layoutMatrix[i, topLeft.y - 1];
                    if (aboveTopWallCell == ProceduralGenerator.LayoutCell.HALL)
                        generator.layoutMatrix[i, topLeft.y] = ProceduralGenerator.LayoutCell.HALL;
                }
                if (bottomRight.y < ProceduralGenerator.LAYOUT_DIM - 1)
                {
                    var bottomWallCell = generator.layoutMatrix[i, bottomRight.y];
                    var belowBottomWallCell = generator.layoutMatrix[i, bottomRight.y + 1];
                    if (belowBottomWallCell == ProceduralGenerator.LayoutCell.HALL)
                        generator.layoutMatrix[i, bottomRight.y] = ProceduralGenerator.LayoutCell.HALL;
                }
            }
        } else
        {
            for (int j = topLeft.y; j < bottomRight.y; j++)
            {
                if (topLeft.x > 0)
                {
                    var leftWallCell = generator.layoutMatrix[topLeft.x, j];
                    var beforeLeftWallCell = generator.layoutMatrix[topLeft.x - 1, j];
                    if (beforeLeftWallCell == ProceduralGenerator.LayoutCell.HALL)
                        generator.layoutMatrix[topLeft.x, j] = ProceduralGenerator.LayoutCell.HALL;
                }
                if (bottomRight.x < ProceduralGenerator.LAYOUT_DIM - 1)
                {
                    var rightWallCell = generator.layoutMatrix[bottomRight.x, j];
                    var afterRightWallCell = generator.layoutMatrix[bottomRight.x + 1, j];
                    if (afterRightWallCell == ProceduralGenerator.LayoutCell.HALL)
                        generator.layoutMatrix[bottomRight.x, j] = ProceduralGenerator.LayoutCell.HALL;
                }
            }
        }
    }

    public static void AddLights()
    {
        // Debug.Log("Trying to add lights, got: " + finalChunks.Count + " chunks");
        // foreach (Chunk chunk in finalChunks)
        // {
        //     int dimensionA = (chunk.lowerRightCorner.x - chunk.upperLeftCorner.x);
        //     int dimensionB = (chunk.lowerRightCorner.y - chunk.upperLeftCorner.y);
        //     int lightX = Convert.ToInt32(Math.Round(Convert.ToDecimal(dimensionA) / 2));
        //     int lightY = Convert.ToInt32(Math.Round(Convert.ToDecimal(dimensionB) / 2));
        //     var lightPosition = chunk.upperLeftCorner + new Vector2Int(lightX, lightY);
        //     generator.AddLightAt(lightPosition, Math.Max(dimensionA, dimensionB), chunk.GetSize());
        // }
    }

    public Vector2Int GetUpperLeftCorner()
    {
        return upperLeftCorner;
    }

    public Vector2Int GetLowerRightCorner()
    {
        return lowerRightCorner;
    }
}