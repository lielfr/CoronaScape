using UnityEngine;
using GameEnums;

public abstract class Potion : MonoBehaviour
{
    public abstract PotionType Type { get; }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatsController>().TakePotion(Type);
            Destroy(gameObject);
        }
    }
}
