using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public int speed = 4;
    public int jumpForce = 400;
    public Animator animator;
    Rigidbody2D _rigidbody;
    public LayerMask groundLayer;
    //public Transform feet;
    public bool grounded = false;

    float xSpeed = 0;

    public Text Death;
    public Text warn;
    public Text Guide;
    
    //bullet
    public GameObject bulletPrefab;
    int bulletForce = 50;
    public Transform spawnPos;
    public int levelToLoad = 1;
    public Camera cam;
    Vector2 mousePos;// mouse position

    //heart
    public int life = 3;
    public int numHearts = 3;
    public Image[] hearts;
    public Sprite fHeart;
    public Sprite eHeart;

    //potion
    public int Herb = 0;
    public Image[] Potion;
    public Sprite Zero;
    public Sprite Twenty;
    public Sprite Forty;
    public Sprite Sixty;
    public Sprite Eighty;    
    public Sprite Full;

    //change animation due to speed
    float hMove = 0f;
    bool fright = true;
    private SpriteRenderer spRenderer ;

    // change animation to jump
    bool jump  = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //StartCoroutine(show());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y); 


    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(this.transform.position, 1f, groundLayer);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }

        // border
        if (_rigidbody.transform.position.x >= 8.22f) {
            // change!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (transform.position.x <= -9){
            RePosition();
        } 
        //heart
        if (life > numHearts){
            life = numHearts;
        }
        for (int i = 0; i < hearts.Length; i++){
            if (i < life){
                hearts[i].sprite = fHeart;
            }
            else{
                hearts[i].sprite = eHeart;
            }

            if (i >  numHearts){
                hearts[i].enabled = false;
            } else {
                hearts[i].enabled = true;
            }
        }

        //potion
        for (int j = 0; j < Potion.Length; j++){
            if (j == Herb){
                Potion[j].enabled = true;
            }
            else{
                Potion[j].enabled = false;
            }
        }

        // change animation due to speed change
        hMove = Input.GetAxisRaw("Horizontal") * speed;
       
        if (hMove <0 && fright){
            flip();
        }
        else if (hMove > 0 && !fright)
        {
            flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(hMove));

        
        // jump animation   
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }

        //bullet
        // locate mouse position
        // allow bullet to shoot the direction of mouse
        // mDir is mouse Direction
        Vector2 mDir = mousePos - _rigidbody.position;
        float bulletAngle = Mathf.Atan2(mDir.y, mDir.x) * Mathf.Rad2Deg;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.rotation = Quaternion.Euler(0, 0, bulletAngle);
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newbullet = Instantiate(bulletPrefab, spawnPos.position, 
                Quaternion.Euler(0, 0, bulletAngle));
            newbullet.GetComponent<Rigidbody2D>().velocity = spawnPos.right * bulletForce; 
        }
        
    }

    // landing function
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnLanding();
        }
        // encounter monster (slime)
        else if (other.gameObject.tag == "Slime")
        {
            loseLife();
        }

    }
    public void OnLanding(){
        animator.SetBool("Jump", false);
    }



    IEnumerator show() {
        //run instructions
        if (levelToLoad == 1){
            Guide.text = "FIGHT THE BAT!";
            yield return new WaitForSecondsRealtime(2f);
            Guide.text = "FEAR THE LAVA!!";
            yield return new WaitForSecondsRealtime(2f);
            Guide.text = "EXIT ON THE RIGHT!!!";
            yield return new WaitForSecondsRealtime(2f);
            Guide.enabled = false;
        }
        // etc...
    }
    private void flip(){
        fright = !fright;
        transform.Rotate(0f,180f,0f);
    }
    public void RePosition(){
        this.transform.position = new Vector2(-7.73f,1.25f); 
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Lava"))
        {
            loseLife();
            RePosition();
        }
        
    }

    public void loseLife(){
        life--;
        Guide.enabled = true;
        if (life == 0){
            StartCoroutine(death());
            StartCoroutine(Restart());
        }
        else{
            StartCoroutine(warning());
        }
    }

    IEnumerator warning() {
        //_audioSource.PlayOneShot(hurtSnd);
        warn.enabled = true;
        warn.text = "IT HURTS!";
        yield return new WaitForSecondsRealtime(2f);
        warn.enabled = false;
        
    }

    IEnumerator Restart() {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator death() {
        Death.enabled = true;
        Death.text = "DIED!!!";
        yield return new WaitForSecondsRealtime(5f);
        Death.enabled = false;
    }
}
