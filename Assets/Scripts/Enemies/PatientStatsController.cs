using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientStatsController : MonoBehaviour
{
    /* Patient health stats */
    public GameObject patientEnemy;
    public float health;
    private float maxHealth = 50;

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
            Debug.Log("Stating Player-Patient Combat");
            patientEnemy.GetComponent<PatientCombat>().Fight();
        }
    }
}
