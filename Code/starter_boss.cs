using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starter_boss : MonoBehaviour
{

    Rigidbody2D _rigidbody;
    public Animator anim;
    public int speed = 0;

    private int state;

    public GameObject player_box;


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
            //text.SetActive(true);
            anim.SetBool("cat", true);
            speed = 3;
            player_box.SetActive(true);
            state++; 
        }


    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }

}
