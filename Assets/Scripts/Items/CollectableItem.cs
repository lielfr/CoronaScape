using UnityEngine;
using GameEnums;

public abstract class CollectableItem : MonoBehaviour
{
    public abstract CollectableItems Type { get; }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayManager.instance.Collect(Type);
            Destroy(gameObject);
        }
    }

}
