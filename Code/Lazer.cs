using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject lazer;
    public float lazerUptime = 1f;
    public float startSpawnTime = 1f;
    public float timePerSpawn = 2f;
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
        var newLazer = GameObject.Instantiate(lazer, new Vector3(x, y, z), Quaternion.identity);
        Object.Destroy(newLazer, lazerUptime);
    }
}
