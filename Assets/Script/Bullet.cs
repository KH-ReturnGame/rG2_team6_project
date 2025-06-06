using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Vector2 directionVec;
    public int direction;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    int speed = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = directionVec * speed;
    }

    void Parry()
    {
        direction -= 1;
        gameObject.tag = "PlayerBullet";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("platform"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            StartCoroutine(DestroyFaster());
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }

    IEnumerator DestroyFaster()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
