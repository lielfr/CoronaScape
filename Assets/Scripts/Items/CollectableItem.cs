using UnityEngine;
using GameEnums;

public abstract class CollectableItem : MonoBehaviour
{
    public abstract CollectableItems Type { get; }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SendMessageUpwards("Collect", Type);
            Destroy(gameObject);
        }
    }

}
