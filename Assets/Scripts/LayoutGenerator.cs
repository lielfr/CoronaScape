using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    public GameObject container;
    public GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject firstRoom = Instantiate(prefab, new Vector3(0, 1.22f, 0), Quaternion.identity);
        firstRoom.transform.SetParent(container.transform);
        Renderer renderer = firstRoom.GetComponent<Renderer>();
        firstRoom.AddComponent<MeshCollider>();
        Debug.Log(renderer.bounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
