using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float speed = 3.5f;
    bool isFall = false;
    float TurnSeconds;
    int direction;

    [Header("떨어짐 방지")]
    public float distance;
    public LayerMask groundLayer;
    public RaycastHit2D hit;
    Vector3 groundCheckPosition;

    [Header("플레이어 감지 및 공격")]
    public bool isDetected;
    Transform playerTransform;
    EnemyAttack enemyAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        distance = 1.5f;
        groundCheckPosition = new Vector3(transform.position.x + 0.75f * direction, transform.position.y, 0);
        StartCoroutine(TrunEnemy());
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyAttack.canAttack)
        {
            if (!isDetected)
            {
                if (isFall)
                {
                    direction *= -1;
                    isFall = false;
                }
                groundCheckPosition = new Vector3(transform.position.x + 0.75f * direction, transform.position.y, 0);
                MoveEnemy();
                CheckGround();
            }
            else
            {
                StopAllCoroutines();
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
        }

        if (!enemyAttack.isAttacking)
        {
            PlayerDistanceCheck();   
        }
    }

    IEnumerator TrunEnemy()
    {
        TurnSeconds = Random.Range(0.5f, 3.5f);
        direction = Random.Range(-1, 2);

        yield return new WaitForSeconds(TurnSeconds);

        StartCoroutine(TrunEnemy());

        yield return null;
    }

    void MoveEnemy()
    {
        Vector2 movement = rb.linearVelocity;
        movement.x = direction * speed;
        rb.linearVelocity = movement;

        if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void CheckGround()
    {
        hit = Physics2D.Raycast(groundCheckPosition, Vector2.down, distance, groundLayer);
        if (hit.collider == null)
        {
            isFall = true;
        }
    }

    void PlayerDistanceCheck()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < 3f)
        {
            enemyAttack.canAttack = true;
        }
        else
        {
            enemyAttack.canAttack = false;
        }

        if (isDetected)
        {
            if (playerTransform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (playerTransform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
