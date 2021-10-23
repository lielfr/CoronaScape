using UnityEngine;
using GameEnums;

public abstract class Item : MonoBehaviour
{
    public abstract ItemType Type { get; }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatsController>().PickItem(Type);
            Destroy(gameObject);
        }
    }

}
