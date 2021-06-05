using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotion : MonoBehaviour
{
    public GameObject bluePotionPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Teleportation Potion Picked");
            other.GetComponent<PlayerStatsController>().TakeTeleportationPotion();
            Destroy(bluePotionPrefab);
        }
    }
}
