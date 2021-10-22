using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject levelGate;
    public float x = 0;
    public float y = 5;
    public float z = 0;
    public int bossHealth = 30;
    void Update()
    {
        if (bossHealth <= 0) {
            var newLevelGate = GameObject.Instantiate(levelGate, new Vector3(x, y, z), Quaternion.identity);
            PublicVars.bossBeaten = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            Invoke("BackToWhite", 1f);
            if (PublicVars.doubleDamage){
                bossHealth -= 2;
            }
            else{
                bossHealth -= 1;
            }
            Destroy(other.gameObject);
            //Destroy(gameObject); -- this will bypass the enemy object's health bar. (see Slime code)
        }
    }
}
