using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : MonoBehaviour
{
    public GameObject redPotionPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Time Potion Picked");
            other.GetComponent<PlayerStatsController>().TakeTimePotion();
            Destroy(redPotionPrefab);
        }
    }
}
