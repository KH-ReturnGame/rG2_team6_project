using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{


    public float speed = 5f;
    Vector2 move = new Vector2();
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        move.x = Input.GetAxisRaw("Horizontal");

        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxis("Horizontal"));
        
        move.Normalize();
        rb.linearVelocity = move * speed;   
    }
}
