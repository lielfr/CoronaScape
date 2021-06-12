using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin Picked");
            other.GetComponent<PlayerStatsController>().PickCoin();
            Destroy(coinPrefab);
        }
    }
}
