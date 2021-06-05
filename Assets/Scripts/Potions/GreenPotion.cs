using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotion : MonoBehaviour
{
    public GameObject greenPotionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(greenPotionPrefab);
            Debug.Log("Green Potion Picked Up");
        }
    }
}
