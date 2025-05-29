using UnityEngine;
using System.Collections;

public class EnemyDamaged : MonoBehaviour
{
    public int EnemyMaxHP = 5;
    public int EnemyCurHP = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyCurHP = EnemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Damage();
        }
    }

    void Damage()
    {
        EnemyCurHP--;
        if (EnemyCurHP <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        // 사망 애니메이션 재생

        gameObject.SetActive(false);

        yield return null;

        StopAllCoroutines();

        yield return null;
    }
}
