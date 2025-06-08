using UnityEngine;

public class player_move : MonoBehaviour
{
    public float maxspeed;
    public float jumppower;
    SpriteRenderer spriteRenderer;

    private Rigidbody2D rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector2 movement = rigid.linearVelocity;
        movement.x = h * 5;
        rigid.linearVelocity = movement;
    }




    void Update()
    {
        {//점프 기능
            if (Input.GetButtonDown("Jump"))
                rigid.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);

        }
    }
}