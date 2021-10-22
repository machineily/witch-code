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
    public Transform feet;
    public bool grounded = false;

    float xSpeed = 0;
    int jumps = 0;
    
    //bullet
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    int bulletForce = 50;
    public Transform spawnPos;
    public int levelToLoad = 1;
    public Camera cam;
    Vector2 mousePos;// mouse position

    //heart
    
    public Image[] hearts;
    public Sprite fHeart;
    public Sprite eHeart;
    public GameObject hurt;
    //potion
    public GameObject usePotion;
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
    bool immune = false;

    // audio source
    public AudioClip magicSnd;
    AudioSource _audioSource;

    // wand
    public Transform wandPos;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        hurt.SetActive(false);
        usePotion.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y); 


    }

    void Update()
    {
        // pausemenu
        if (PublicVars.paused) return;
        // potion use reminder
        if (PublicVars.Herb > 0){
            usePotion.SetActive(true);
        }
        else{
            usePotion.SetActive(false);
        }

        // maximum life = 6
        if (PublicVars.life > 6){
            PublicVars.life = 6;
        }
        grounded = Physics2D.OverlapCircle(feet.position, .3f, groundLayer);
        if (grounded) {
            jumps = 0;
        }

        if (Input.GetButtonDown("Jump") && (jumps > 0 || grounded)) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            jumps--;
        }

        //heart
        if (PublicVars.life > PublicVars.numHearts){
            PublicVars.life = PublicVars.numHearts;
        }
        for (int i = 0; i < hearts.Length; i++){
            if (i < PublicVars.life){
                hearts[i].sprite = fHeart;
            }
            else{
                hearts[i].sprite = eHeart;
            }

            if (i >=  PublicVars.numHearts){
                hearts[i].enabled = false;
            } else {
                hearts[i].enabled = true;
            }
        }

        //potion
        for (int j = 0; j < Potion.Length; j++){
            if (j == PublicVars.Herb){
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
            _audioSource.PlayOneShot(magicSnd);
            if (!PublicVars.doubleDamage){
                GameObject newbullet = Instantiate(bulletPrefab, spawnPos.position, 
                Quaternion.Euler(0, 0, bulletAngle));
                newbullet.GetComponent<Rigidbody2D>().velocity = spawnPos.right * bulletForce; 
            }
            else{
                GameObject newbullet = Instantiate(bulletPrefab2, spawnPos.position, 
                Quaternion.Euler(0, 0, bulletAngle));
                newbullet.GetComponent<Rigidbody2D>().velocity = spawnPos.right * bulletForce; 
            }
            
        }
        //wand moving with mouse position
        Vector2 mDir2 = mousePos - _rigidbody.position;
        float angle = Mathf.Atan2(mDir.y, mDir.x) * Mathf.Rad2Deg;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        wandPos.rotation = Quaternion.Euler(0, 0, angle);
        
    }

    // landing function
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Ground") | (other.gameObject.tag == "Platform"))
        {
            OnLanding();
        }

        // encounter monster (enemy)
        if (other.gameObject.tag == "Enemy")
        {
            loseLife();
        }
    }
    public void OnLanding(){
        animator.SetBool("Jump", false);
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

        if (other.CompareTag("LevelGate"))
        {
            SceneManager.LoadScene("Level" + levelToLoad);
        }
        
        if (other.CompareTag("Laser") && !immune) {
            loseLife();
        }

        if (other.gameObject.tag == "Enemy" && !immune)
        {
            loseLife();
        }

        if (other.CompareTag("Herb")){
            PublicVars.Herb++;
            Destroy (GameObject.FindWithTag("Herb"));
        }

    }
    public void loseLife(){
        PublicVars.life--;
        if (PublicVars.life <= 0){
            StartCoroutine(death());
            reset();
        }
        else{
            StartCoroutine(warning());
        }
        immune = true;
        Invoke("SetImmuneFalse", 1.0f);
    }


    IEnumerator warning() {
        //_audioSource.PlayOneShot(hurtSnd);
        hurt.SetActive(true);
        yield return new WaitForSecondsRealtime(0.3f);
        hurt.SetActive(false);
    }

    IEnumerator Restart() {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        reset();
    }

    IEnumerator death() {
        reset();
        yield return new WaitForSecondsRealtime(5f);
    }

    void SetImmuneFalse()
    {
        immune = false;
    }

    void reset(){
    
    PublicVars. paused = false;
    PublicVars.Herb = 0;

    PublicVars.numHearts = 3;

    PublicVars.life = 3;

    PublicVars.bossBeaten = false;
    PublicVars.doubleDamage = false;
    }

}
