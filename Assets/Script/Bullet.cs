using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int direction;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    int speed = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        FlipSprite();
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(direction * speed, 0f);
    }

    void Parry()
    {
        direction -= 1;
        gameObject.tag = "PlayerBullet";
        FlipSprite();
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

    void FlipSprite()
    {
        if (direction < 0)
        {
            spriteRenderer.flipY = false;
        }
        else
        {
            spriteRenderer.flipY = true;
        }
    }
}
