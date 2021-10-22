using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private GameObject target;
    public float moveDistance = .03f;

    void Start() {
        target = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        // pausemenu
        if (PublicVars.paused) return;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveDistance);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Bullet"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
