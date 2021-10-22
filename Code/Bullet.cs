using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Player temp;
    void Start()
    {
        temp = FindObjectOfType(typeof(Player)) as Player ;
    }
    
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     // encounter monster (slime)
    //     if (other.gameObject.tag == "Slime")
    //     {
    //         Destroy(other.gameObject);
    //         Destroy(this.gameObject);
    //     }

    // }
}
