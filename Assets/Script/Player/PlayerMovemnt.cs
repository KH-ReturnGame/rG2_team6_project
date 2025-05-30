using UnityEngine;
using System.Collections;

public class PlayerMovemnt : MonoBehaviour
{
    Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    [Header("이동")]
    public float speed = 10f;

    [Header("점프")]
    public float jumpForce = 10f;
    public bool isOnGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FilpSprite();
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FilpSprite()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = rb.linearVelocity;
        movement.x = moveHorizontal * speed;
        rb.linearVelocity = movement;
    }

    void Jump()
    {
        if (isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
