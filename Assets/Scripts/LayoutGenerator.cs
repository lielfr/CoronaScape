using System.Collections;
using System.Collections.Generic;
using Convert = System.Convert;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    // These numbers will later be based on difficulty
    private int roomsQuantity = 6;
    private int potionsQuantity = 20;
    private int moneyQuantity = 50;
    /* ---------- Necessary prefabs ---------- */
    public GameObject roomPrefab;
    public GameObject redPotionPrefab;
    public GameObject bluePotionPrefab;
    public GameObject greenPotionPrefab;
    public GameObject coinPrefab;
    public GameObject boxPrefab;
    public GameObject wallPrefab;
    /* ---------- Necessary containers ---------- */
    public GameObject layoutContainer;
    public GameObject floorContainer;
    /* ---------- Other necessary info ---------- */
    private float xPosMin, xPosMax, zPosMin, zPosMax, yPos; // floor bounds
    private enum potionTypes { BLUE = 1, GREEN = 2, RED = 3 };
    private int potionTypesQuantity = 3;
    private enum moneyTypes { COIN = 1, BOX = 2 };
    private int moneyTypesQuantity = 2;

    void Start()
    {
        MeshRenderer floorRenderer = floorContainer.GetComponent<MeshRenderer>();
        // Calculating floor bounds +-10, for some extra spacing
        xPosMin = floorRenderer.bounds.min.x + 10;
        zPosMin = floorRenderer.bounds.min.z + 10;
        xPosMax = floorRenderer.bounds.max.x - 10;
        zPosMax = floorRenderer.bounds.max.z - 10;
        yPos = floorRenderer.bounds.min.y;
        StartCoroutine(GenerateRooms());
        StartCoroutine(GeneratePotions());
        StartCoroutine(GenerateMoney());
        GenerateBounds();
    }

    void StackWalls(Vector3 startPosition, Vector3 direction, long num)
    {
        Vector3 defaultDirection = new Vector3(0f, 0f, 1f).normalized;
        Vector3 currentPosition = startPosition;
        for (long i = 0; i < num; i++)
        {
            GameObject newWall = Instantiate<GameObject>(wallPrefab);

            newWall.transform.Translate(currentPosition);
            newWall.transform.Rotate(Quaternion.FromToRotation(defaultDirection, direction).eulerAngles);
            newWall.transform.parent = layoutContainer.transform;
            currentPosition += direction;
        }
    }

    void GenerateBounds()
    {
        Vector3 floorBounds = floorContainer.GetComponent<Renderer>().bounds.size;
        Vector3 cornerA = floorContainer.transform.position - floorBounds / 2;
        Vector3 cornerB = floorContainer.transform.position + floorBounds / 2;
        Vector3 cornerC = cornerA + new Vector3(floorBounds.x, 0f, 0f);
        long coverAmount = Convert.ToInt64(floorContainer.transform.localScale.x * floorContainer.transform.localScale.z);
        StackWalls(cornerA, new Vector3(0f, 0f, 1f), coverAmount);
        StackWalls(cornerC, new Vector3(0f, 0f, 1f), coverAmount);
        StackWalls(cornerB, new Vector3(-1f, 0f, 0f), coverAmount);
        StackWalls(cornerA, new Vector3(1f, 0f, 0f), coverAmount);
    }

    IEnumerator GenerateRooms()
    {
        int currentRoomsQuantity = 0;
        while (currentRoomsQuantity < roomsQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax);
            float zRandomPos = Random.Range(zPosMin, zPosMax);
            GameObject generatedRoom = Instantiate(roomPrefab, new Vector3(xRandomPos, 0, zRandomPos), Quaternion.identity);
            generatedRoom.transform.SetParent(layoutContainer.transform);
            Renderer renderer = generatedRoom.GetComponent<Renderer>();
            generatedRoom.AddComponent<MeshCollider>();
            yield return new WaitForSeconds(0f);
            currentRoomsQuantity++;
        }

        // TODO: Place two elevators, one in each side (can be one for the first level)

        // TODO: Replace the current logic with one that uses the StackWalls function

        // TODO: Have a data structure for walls placement
    }

    IEnumerator GeneratePotions()
    {
        int currentPotionsQuantity = 0;
        while (currentPotionsQuantity < potionsQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax);
            float zRandomPos = Random.Range(zPosMin, zPosMax);
            potionTypes potionType = (potionTypes)Random.Range(1, potionTypesQuantity + 1);
            switch (potionType)
            {
                case potionTypes.BLUE:
                    {
                        GameObject generatedPotion = Instantiate(bluePotionPrefab, new Vector3(xRandomPos, yPos, zRandomPos), Quaternion.identity);
                        generatedPotion.transform.parent = layoutContainer.transform;
                        break;
                    }
                case potionTypes.RED:
                    {
                        GameObject generatedPotion = Instantiate(redPotionPrefab, new Vector3(xRandomPos, yPos, zRandomPos), Quaternion.identity);
                        generatedPotion.transform.parent = layoutContainer.transform;
                        break;
                    }
                case potionTypes.GREEN:
                    {
                        GameObject generatedPotion = Instantiate(greenPotionPrefab, new Vector3(xRandomPos, yPos, zRandomPos), Quaternion.identity);
                        generatedPotion.transform.parent = layoutContainer.transform;
                        break;
                    }
            }

            yield return new WaitForSeconds(0f);
            currentPotionsQuantity++;
        }
    }

    IEnumerator GenerateMoney()
    {
        int currentMoneyQuantity = 0;
        while (currentMoneyQuantity < moneyQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax); // Based on current layout
            float zRandomPos = Random.Range(zPosMin, zPosMax); // Based on current layout
            moneyTypes potionType = (moneyTypes)Random.Range(1, moneyTypesQuantity + 1);
            switch (potionType)
            {
                case moneyTypes.COIN:
                    {
                        GameObject generatedMoney = Instantiate(coinPrefab, new Vector3(xRandomPos, yPos, zRandomPos), Quaternion.identity);
                        generatedMoney.transform.parent = layoutContainer.transform;
                        break;
                    }
                case moneyTypes.BOX:
                    {
                        GameObject generatedMoney = Instantiate(boxPrefab, new Vector3(xRandomPos, yPos, zRandomPos), Quaternion.identity);
                        generatedMoney.transform.parent = layoutContainer.transform;
                        break;
                    }
            }
            yield return new WaitForSeconds(0f);
            currentMoneyQuantity++;
        }
    }
}
