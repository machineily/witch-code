using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            PublicVars.score += 1;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
