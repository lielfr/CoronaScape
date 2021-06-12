using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject boxPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Box Picked");
            other.GetComponent<PlayerStatsController>().PickBox();
            Destroy(boxPrefab);
        }
    }
}
