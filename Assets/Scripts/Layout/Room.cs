using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{

    // These numbers will later be based on difficulty
    private int potionsQuantity = 40;
    private int moneyQuantity = 20;
    /* ---------- Necessary prefabs ---------- */
    public GameObject redPotionPrefab;
    public GameObject bluePotionPrefab;
    public GameObject greenPotionPrefab;
    public GameObject coinPrefab;
    public GameObject boxPrefab;
    /* ---------- Other necessary info ---------- */
    private float xPosMin, xPosMax, zPosMin, zPosMax;
    private enum potionTypes { BLUE = 1, GREEN = 2, RED = 3 };
    private int potionTypesQuantity = 3;
    private enum moneyTypes { COIN = 1, BOX = 2 };
    private int moneyTypesQuantity = 2;

    private const int GAP = 5;

    private ProceduralGenerator generator;

    public  Room(Vector2Int topLeftCorner, Vector2Int bottomRightCorner, ProceduralGenerator generator, GameObject redPotionPrefab, GameObject bluePotionPrefab, GameObject greenPotionPrefab, GameObject coinPrefab, GameObject boxPrefab)
    {
        xPosMin = topLeftCorner.x - GAP;
        zPosMin = topLeftCorner.y - GAP;
        xPosMax = bottomRightCorner.x - GAP;
        zPosMax = bottomRightCorner.y - GAP;
        this.generator = generator;
        this.redPotionPrefab = redPotionPrefab;
        this.bluePotionPrefab = bluePotionPrefab;
        this.greenPotionPrefab = greenPotionPrefab;
        this.coinPrefab = coinPrefab;
        this.boxPrefab = boxPrefab;
        GeneratePotions();
        GenerateMoney();
    }

    void GeneratePotions()
    {
        int currentPotionsQuantity = 0;
        while (currentPotionsQuantity < potionsQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax);
            float zRandomPos = Random.Range(zPosMin, zPosMax);
            potionTypes potionType = (potionTypes)Random.Range(1, potionTypesQuantity + 1);
            GameObject generatedPotion;
            switch (potionType)
            {
                case potionTypes.BLUE:
                    {
                        generatedPotion = GameObject.Instantiate(bluePotionPrefab, new Vector3(xRandomPos, 0, zRandomPos), Quaternion.identity);
                        break;
                    }
                case potionTypes.RED:
                    {
                        generatedPotion = GameObject.Instantiate(redPotionPrefab, new Vector3(xRandomPos, 0, zRandomPos), Quaternion.identity);
                        break;
                    }
                default:
                    {
                        // Used default here just to suppress the annoying uninitialized local variable error.
                        generatedPotion = GameObject.Instantiate(greenPotionPrefab, new Vector3(xRandomPos, 0, zRandomPos), Quaternion.identity);
                        break;
                    }
            }
            generatedPotion.transform.parent = generator.transform;
            currentPotionsQuantity++;
        }
    }

    void GenerateMoney()
    {
        int currentMoneyQuantity = 0;
        while (currentMoneyQuantity < moneyQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax); 
            float zRandomPos = Random.Range(zPosMin, zPosMax); 
            moneyTypes potionType = (moneyTypes)Random.Range(1, moneyTypesQuantity + 1);
            switch (potionType)
            {
                case moneyTypes.COIN:
                    {
                        GameObject generatedMoney = GameObject.Instantiate(coinPrefab, new Vector3(xRandomPos, 0, zRandomPos), Quaternion.identity);
                        generatedMoney.transform.parent = generator.transform;
                        break;
                    }
                case moneyTypes.BOX:
                    {
                        GameObject generatedMoney = GameObject.Instantiate(boxPrefab, new Vector3(xRandomPos, 0, zRandomPos), Quaternion.identity);
                        generatedMoney.transform.parent = generator.transform;
                        break;
                    }
            }
            currentMoneyQuantity++;
        }
    }
}
