using UnityEngine;

public class player_move : MonoBehaviour
{
    public float maxspeed;
    private Rigidbody2D rigid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        Vector2 movement = rigid.linearVelocity;
        movement.x = h * 5;
        rigid.linearVelocity = movement;
    }
}
