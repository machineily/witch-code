using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_move : MonoBehaviour
{

    Rigidbody2D _rigidbody;
    public float speed = 2.3F;
    public Animator anim;

    private int state;

    public GameObject box;
    public GameObject self;
    public GameObject boss_box;


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
        if ((other.gameObject.tag == "trigger"))
        {
            transform.Rotate(0f, 180f, 0f);
            speed = -1 * speed;
            state++;
        }else if(other.gameObject.tag == "Enemy")
        {
            self.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (state != 4)
        {
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        }
        else if(state==4)
        {
            //anim.SetBool("cat", true);
            box.SetActive(false);
            //speed = 0;
            state++;
        }
    }

}
