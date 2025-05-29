using UnityEngine;
using System.Collections;

public class PlayerDamaged : MonoBehaviour
{
    [Header("플레이어 데미지")]
    SpriteRenderer spriteRenderer;
    public int PlayerMaxHP = 10;
    public int PlayerCurHP = 10;
    bool isGrace = false;
    public float graceTime = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("thorn"))
        {
            if(!isGrace)
            {
                StartCoroutine(Damage());
            }
        }
    }

    IEnumerator Damage()
    {
        PlayerCurHP -= 1;
        isGrace = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        if (PlayerCurHP <= 0)
        {
            Die();
        }

        yield return new WaitForSeconds(graceTime);

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        isGrace = false;

        yield return null;
    }

    void Die()
    {
        StopAllCoroutines();
        Debug.Log("사망");
    }
}
