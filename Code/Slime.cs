using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    public Animator animator;
    // face right or face left
    bool fRight = false;
    float speed  = 4.0f;
    public SpriteRenderer spRenderer;
    private float slimeLife;
    private float MaxSlimeLife = 2;
    //public HealthBar hBar;// slime's health bar
    // Start is called before the first frame update
    void Start()
    {
        //spRenderer = GetComponent<SpriteRenderer>();
        slimeLife = MaxSlimeLife;
        //hBar.SetLife(slimeLife, MaxSlimeLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (fRight){
            moveRight();
        }
        else{
            moveLeft();
        }

        if (transform.position.x <= 3.05f){
            fRight = true;
            spRenderer.flipX = true;
        }
        else if (transform.position.x >= 8.22f) {
            fRight = false;
            spRenderer.flipX = false;
        }
    }

    private void flip(){
        transform.Rotate(0f,180f,0f);
    }

    private void takeShot(){
        slimeLife --;
        //hBar.gameObject.SetActive(true);
        //hBar.SetLife(slimeLife, MaxSlimeLife);
        if (slimeLife == 0)
        {
            Destroy(this.gameObject);
        }
    }
    void moveRight(){
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void moveLeft(){
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Bullet"){
            Destroy(other.gameObject);
            takeShot();
        }
    }
}
