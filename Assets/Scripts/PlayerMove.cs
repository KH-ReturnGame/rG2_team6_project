using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{


    public float speed = 5f;
    float JumpPower = 10f;
    Vector2 move = new Vector2();
    Rigidbody2D rb;
    Collider2D col;
    public LayerMask groundLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }


    void Update()
    {
        bool isGrounded = col.IsTouchingLayers(groundLayer);
            Debug.Log("IsGrounded" + isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("스페이스바 누름");
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            //플레이어 점프
        }
    }

    private void FixedUpdate()
    {
        move.x = Input.GetAxisRaw("Horizontal");

        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxis("Horizontal"));
        //이동할때 속도변화 보여줌

        move.Normalize();
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
    }

    /*IEnumerator logonetwo()
    {
        Debug.Log("One");
        float waitTime = 1f;

        yield return new WaitForSeconds(waitTime);

        Debug.Log("Two");

        yield return null;
    }*/
}
