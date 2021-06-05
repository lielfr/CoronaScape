using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotion : MonoBehaviour
{
    public GameObject greenPotionPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Heal Potion Picked");
            other.GetComponent<PlayerStatsController>().TakeHealingPotion();
            Destroy(greenPotionPrefab);
        }
    }
}
