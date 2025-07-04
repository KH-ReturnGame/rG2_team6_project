using UnityEngine;
using System.Collections;
public class PlayerMove : MonoBehaviour
{
    public Collider2D groundTrigger; 
    public bool isGrounded;

    public GameObject checker;


    private float speed = 5.0f;
    private float jumpForce = 7.0f;

    public float jumpCooldown = 1.8f;  
    private float lastJumpTime = -10f; 

    private Rigidbody2D rigid2D;

    public int hp = 5;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("에러");
        }
    }
   /* void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1");
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("0");
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
}
*/

    void Update()
    {
        //transform.GetChild(1); //트랜스폼 참조
        //transform.GetComponentInChild;
        //isGrounded = groundTrigger.CompareTag("Grid");
        // Debug.Log(isGrounded);
        float x = Input.GetAxisRaw("Horizontal");
         Move(x);

        float currentTime = Time.time;

        // GameObject.Find("Groundchk").GetComponent<Groundchk.cs>().OnCollisionEnter2D();
        if (Input.GetKeyDown(KeyCode.Space)/*&&checker.GetComponent<Groundchk>().canjump*/  /*&& isGrounded*/  /*&& currentTime - lastJumpTime >= jumpCooldown*/)
        {
            rigid2D.linearVelocity = Vector2.up * jumpForce;

            lastJumpTime = currentTime;
        }
    }


    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    public void Move(float x)
    {
        rigid2D.linearVelocity = new Vector2(x * speed, rigid2D.linearVelocity.y);
    }
    void FixedUpdate()
    {
    
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("thorn")) && !isInvincible)
        {
            hp--;
            Debug.Log("체력감소. 남은 HP: " + hp);
            StartCoroutine(InvincibilityTimer());  
        }
    }

     private void OnColliderEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Enemy")) && !isInvincible)
        {
            hp--;
            Debug.Log("체력감소. 남은 HP: " + hp);
            StartCoroutine(InvincibilityTimer());  
        }
    }
    private IEnumerator InvincibilityTimer()
    {
        isInvincible = true;
        Debug.Log("무적 ON");

        yield return new WaitForSeconds(1.5f);

        isInvincible = false;
        Debug.Log("무적 OFF");

        //yield return null;


    }

    // 충돌 함수
   

    /*IEnumerator Logonetwo()
    {
        Debug.Log("1초");
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime);

        Debug.Log("2초");
        yield return null;
    } */
}

 