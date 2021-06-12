using System.Collections;
using System.Collections.Generic;
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
