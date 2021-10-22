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
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveDistance);
    }
}
