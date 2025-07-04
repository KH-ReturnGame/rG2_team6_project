using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    [Header("Ground Check Settings")]
    public Transform groundCheck;            
    public float groundCheckDistance = 0.3f;   
    public LayerMask groundLayer;             

    [Header("Player Tracking")]
    public Transform player;            
    public float followRange = 10f;       
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (groundCheck == null) return;

        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position, Vector2.down, groundCheckDistance, groundLayer
        );

        isGrounded = hit.collider != null;


        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < followRange) //플레이어가 범위 안에 있으면 따라가고 아니면 멈추는 코드
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}
