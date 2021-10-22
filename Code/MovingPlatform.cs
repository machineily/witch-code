using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float leftPos = -3.5f;
    public float rightPos = 4.5f;
    
    public bool moveRight = true;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > rightPos){
            moveRight = false;
        }
        if(transform.position.x < leftPos){
            moveRight = true;
        }
        if(moveRight){
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }else{
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
}
