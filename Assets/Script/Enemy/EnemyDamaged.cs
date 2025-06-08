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
            Damage(1);
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            Damage(2);
        }
    }

    void Damage(int i)
    {
        EnemyCurHP -= i * PlayerPrefs.GetInt("PlayerAttackRate");
        if (EnemyCurHP <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        // 사망 애니메이션 재생
        int randomGold = Random.Range(15, 55);
        BankManager.PlusGold(randomGold);

        Destroy(gameObject);

        StopAllCoroutines();

        yield return null;
    }
}
