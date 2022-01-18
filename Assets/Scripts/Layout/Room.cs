using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private const int DIV_HORIZONTAL = 15;
    private const int DIV_VERTICAL = 15;

    // These numbers will later be based on difficulty
    private static int potionsQuantity = 40;
    private static int moneyQuantity = 20;
    /* ---------- Necessary prefabs ---------- */
    public GameObject redPotionPrefab;
    public GameObject bluePotionPrefab;
    public GameObject greenPotionPrefab;
    public GameObject coinPrefab;
    public GameObject boxPrefab;
    public GameObject keyPrefab;
    /* ---------- Other necessary info ---------- */
    private float xPosMin, xPosMax, zPosMin, zPosMax;
    private enum potionTypes { BLUE = 1, GREEN = 2, RED = 3 };
    private int potionTypesQuantity = 3;
    private enum moneyTypes { COIN = 1, BOX = 2 };
    private int moneyTypesQuantity = 2;

    /* ---------- Current room quantities ------------ */
    private int currentPotions = 0;
    private float spawnInterval;

    

    private GameObject parent;

    public void Init(Vector2 topLeftCorner, Vector2 bottomRightCorner, GameObject parent, GameObject redPotionPrefab, GameObject bluePotionPrefab, GameObject greenPotionPrefab, GameObject coinPrefab, GameObject boxPrefab, GameObject keyPrefab)
    {
        var minWithGaps = Vector2.Lerp(topLeftCorner, bottomRightCorner, 0.15f);
        var maxWithGaps = Vector2.Lerp(topLeftCorner, bottomRightCorner, 0.85f);
        xPosMin = minWithGaps.x;
        zPosMin = minWithGaps.y;
        xPosMax = maxWithGaps.x;
        zPosMax = maxWithGaps.y;
        this.parent = parent;
        this.redPotionPrefab = redPotionPrefab;
        this.bluePotionPrefab = bluePotionPrefab;
        this.greenPotionPrefab = greenPotionPrefab;
        this.coinPrefab = coinPrefab;
        this.boxPrefab = boxPrefab;
        this.keyPrefab = keyPrefab;
        switch (GameManager.Instance.Difficulty)
        {
            case GameEnums.Difficulty.EASY:
                potionsQuantity = 40;
                moneyQuantity = 20;
                break;
            case GameEnums.Difficulty.MEDIUM:
                potionsQuantity = 30;
                moneyQuantity = 15;
                break;
            case GameEnums.Difficulty.HARD:
                potionsQuantity = 20;
                moneyQuantity = 10;
                break;
            case GameEnums.Difficulty.EXTREME:
                potionsQuantity = 10;
                moneyQuantity = 5;
                break;
        }
        
        
        GenerateMoney();
        GenerateKey();
        StartCoroutine(GeneratePotions());
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 topLeft = new Vector2(xPosMin, zPosMin);
        Vector2 topRight = new Vector2(xPosMax, zPosMin);
        Vector2 bottomLeft = new Vector2(xPosMin, zPosMax);
        Vector2 ret = Vector2.Lerp(topLeft, topRight, Random.Range(0f, 1f));
        ret.y = Vector2.Lerp(topLeft, bottomLeft, Random.Range(0.3f, 1f)).y;
        return ret;

    }

    private IEnumerator GeneratePotions()
    {
        yield return new WaitWhile(() => GameplayManager.levelTime == 0);
        spawnInterval = GameplayManager.levelTime / potionsQuantity;
        while (currentPotions < potionsQuantity)
        {
            var randomPos2D = GetRandomPosition();
            Vector3 randomPos = new Vector3(randomPos2D.x, 20, randomPos2D.y);
            potionTypes potionType = (potionTypes)Random.Range(1, potionTypesQuantity + 1);
            GameObject generatedPotion;
            switch (potionType)
            {
                case potionTypes.BLUE:
                    {
                        generatedPotion = Object.Instantiate(bluePotionPrefab, randomPos, Quaternion.identity);
                        break;
                    }
                case potionTypes.RED:
                    {
                        generatedPotion = Object.Instantiate(redPotionPrefab, randomPos, Quaternion.identity);
                        break;
                    }
                default:
                    {
                        // Used default here just to suppress the annoying uninitialized local variable error.
                        generatedPotion = Object.Instantiate(greenPotionPrefab, randomPos, Quaternion.identity);
                        break;
                    }
            }
            generatedPotion.transform.parent = parent.transform;
            currentPotions++;
            yield return new WaitForSeconds(Random.Range(0f, spawnInterval));
        }
    }

    void GenerateKey()
    {
        
        Vector2 randomPos = GetRandomPosition();
        

        GameObject generatedKey = Object.Instantiate(keyPrefab, new Vector3(randomPos.x, 60, randomPos.y), Quaternion.identity);
        generatedKey.transform.parent = parent.transform;
    }

    void GenerateMoney()
    {
        int currentMoneyQuantity = 0;
        while (currentMoneyQuantity < moneyQuantity)
        {
            Vector3 randomPos = new Vector3(xPosMin, 0, zPosMin) + new Vector3(Random.Range(0.2f, 1f) * (xPosMax - xPosMin), 0, Random.Range(0.2f, 1f) * (zPosMax - zPosMin));
            moneyTypes potionType = (moneyTypes)Random.Range(1, moneyTypesQuantity + 1);
            switch (potionType)
            {
                case moneyTypes.COIN:
                    {
                        GameObject generatedMoney = GameObject.Instantiate(coinPrefab, randomPos, Quaternion.identity);
                        generatedMoney.transform.parent = parent.transform;
                        break;
                    }
                case moneyTypes.BOX:
                    {
                        GameObject generatedMoney = GameObject.Instantiate(boxPrefab, randomPos, Quaternion.identity);
                        generatedMoney.transform.parent = parent.transform;
                        break;
                    }
            }
            currentMoneyQuantity++;
        }
    }

    public float GetXMin()
    {
        return xPosMin;
    }
    public float GetXMax()
    {
        return xPosMax;
    }
    public float GetZMin()
    {
        return zPosMin;
    }
    public float GetZMax()
    {
        return zPosMax;
    }
}
