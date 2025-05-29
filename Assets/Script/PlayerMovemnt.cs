using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float speed = 10f;
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = rb.linearVelocity;
        movement.x = moveHorizontal * speed;
        rb.linearVelocity = movement;
    }

}
