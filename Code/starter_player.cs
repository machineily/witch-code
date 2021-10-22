using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starter_player : MonoBehaviour
{

    Rigidbody2D _rigidbody;
    public int speed = 2;

    private int state;

    public GameObject text;
    public GameObject title_box;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "trigger")&&(state==1))
        {
            anim.SetBool("walk", true);
            speed = 3;
            state++;
            title_box.SetActive(false);
        }

        if ((other.gameObject.tag == "Enemy"))
        {
            text.SetActive(true);
        }


    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }

}
