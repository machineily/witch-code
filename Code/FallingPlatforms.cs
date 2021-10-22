using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    public float speed = 2f;
    // Update is called once per frame
    void Update()
    {
        // pausemenu
        if (PublicVars.paused) return;
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y <= 16) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // if(other.gameObject.CompareTag("Player")){
        //     other.transform.SetParent(transform);
        // }
        if (other.gameObject.CompareTag("Lava"))
        {
            Destroy(gameObject);
        }
    }

    // private void OnCollisionExit2D(Collision2D other) {
    //     if(other.gameObject.CompareTag("Player")){
    //         other.transform.SetParent(null);
    //     }
    // }
}
