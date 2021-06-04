using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{
    private enum potionTypes { BLUE = 1, GREEN = 2, RED = 3 };
    private int potionTypesQuantity = 3;
    private int potionsQuantity = 20; // This number will be later based on difficulty
    public GameObject redPotionPrefab;
    public GameObject bluePotionPrefab;
    public GameObject greenPotionPrefab;

    public GameObject floor;
    public GameObject layoutContainer;
    private float xPosMin, xPosMax, zPosMin, zPosMax, yPos; // Container's bounds

    void Start()
    {
        MeshRenderer floorRenderer = floor.GetComponent<MeshRenderer>();
        xPosMin = floorRenderer.bounds.min.x;
        zPosMin = floorRenderer.bounds.min.z;
        xPosMax = floorRenderer.bounds.max.x;
        zPosMax = floorRenderer.bounds.max.z;
        yPos = floorRenderer.bounds.center.y;
        StartCoroutine(GeneratePotions());
    }

    IEnumerator GeneratePotions()
    {
        int currentPotionsQuantity = 0;
        while (currentPotionsQuantity < potionsQuantity)
        {
            float xRandomPos = Random.Range(xPosMin, xPosMax); // Based on current layout
            float zRandomPos = Random.Range(zPosMin, zPosMax); // Based on current layout
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
    void Update()
    {
        
    }
}
