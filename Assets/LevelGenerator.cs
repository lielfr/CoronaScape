using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject levelObj = new GameObject("Level");
        Rigidbody levelRigidbody = levelObj.AddComponent<Rigidbody>();
        levelRigidbody.useGravity = false;
        levelObj.transform.position.Set(0, 0, 0);
        Material material = new Material(Shader.Find("Standard"));
        GameObject floor = GameObjectGenerator.generatePlane(material, new Vector3(-500, 0, -500), PlaneDirection.XZ, 1000, 1000, "Floor");
        floor.transform.parent = levelObj.transform;

        GameObject floorA = GameObjectGenerator.generatePlane(material, new Vector3(-500, 0, -500), PlaneDirection.YZ, 1000, 1000, "Wall");
        floorA.transform.parent = levelObj.transform;

        levelObj.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
