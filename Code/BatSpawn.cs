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
    public bool allowSpawn = true;
    void Start()
    {
        InvokeRepeating ("Spawn", startSpawnTime, timePerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Player").transform.position.y > y){
            allowSpawn = false;
        }else{
            allowSpawn = true;
        }
    }

    void Spawn() {
        if(allowSpawn && !PublicVars.bossBeaten){
            var newBat = GameObject.Instantiate(bat, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
