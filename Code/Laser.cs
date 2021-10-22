using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laser;
    public float laserUptime = 1f;
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
        // pausemenu
        if (PublicVars.paused) return;
    }

    void Spawn() {
        var newLaser = GameObject.Instantiate(laser, new Vector3(x, y, z), Quaternion.identity);
        Object.Destroy(newLaser, laserUptime);
    }
}
