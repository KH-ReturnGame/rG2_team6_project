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

    [Header("플레이어 사망")]
    public GameObject DieUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("thorn") || other.CompareTag("Bullet"))
        {
            if (!isGrace)
            {
                StartCoroutine(Damage(1));
            }
        }
        if(other.CompareTag("EnemyAttack"))
        {
            if (!isGrace)
            {
                StartCoroutine(Damage(2));
            }
        }
    }

    IEnumerator Damage(int i)
    {
        PlayerCurHP -= i;
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
        DieUI.SetActive(true);
        Time.timeScale = 0;
    }
}
