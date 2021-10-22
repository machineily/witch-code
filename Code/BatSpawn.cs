using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawn : MonoBehaviour
{
    public GameObject bat;
    public float startSpawnTime = 1f;
    public float timePerSpawn = 3f;
    public float x = 0;
    public float y = 5;
    public float z = 0;
    void Start()
    {
        InvokeRepeating ("Spawn", startSpawnTime, timePerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn() {
        var newBat = GameObject.Instantiate(bat, new Vector3(x, y, z), Quaternion.identity);
    }
}
