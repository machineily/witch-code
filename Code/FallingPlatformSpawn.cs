using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformSpawn : MonoBehaviour
{
    public GameObject platform;
    public float startSpawnTime = 1f;
    public float timePerSpawn = 3f;
    public float x = 0;
    public float y = 5;
    public float z = 0;
    void Start()
    {
        InvokeRepeating ("Spawn", startSpawnTime, timePerSpawn);
    }

    void Spawn() {
            var newPlatform = GameObject.Instantiate(platform, new Vector3(x, y, z), Quaternion.identity);
    }
}
