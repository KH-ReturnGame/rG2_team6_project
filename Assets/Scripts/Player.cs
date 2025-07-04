using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{


    public float speed = 5f;
    float JumpPower = 10f;
    Vector2 move = new Vector2();
    Rigidbody2D rb;
    Collider2D col;
    public LayerMask groundLayer;


    public int HP = 100;
    private bool isInvincible = false; // 무적 여부
    private float invincibleDuration = 1.5f; // 무적 시간








    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        Debug.Log(HP);
    }


    void Update()
    {
        bool isGrounded = col.IsTouchingLayers(groundLayer);
        //Debug.Log("IsGrounded" + isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Debug.Log("스페이스바 누름");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Thorn"))
        {
            TakeDamage(10); // 함정에 닿으면 10 데미지
        }
    }

    void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log("플레이어가 데미지를 입었습니다. 현재 HP: " + HP);

        if (HP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }
    System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Debug.Log("무적 상태 시작");

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        Debug.Log("무적 상태 해제");
    }

    void Die()
    {
        Debug.Log("플레이어 사망");
        //플레이어 죽음
    }


}
