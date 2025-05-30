using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    EnemyMovement enemyMovement;
    float ATKDelay = 2f;
    GameObject attackCollider;
    Transform attackTransform;

    public bool canAttack = false;
    public bool isAttacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        attackCollider = transform.GetChild(1).gameObject;
        attackTransform = attackCollider.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if (enemyMovement.spriteRenderer.flipX)
        {
            attackTransform.localPosition = new Vector3(-1.25f, 0f, 0f);
        }
        else
        {
            attackTransform.localPosition = new Vector3(1.25f, 0f, 0f);            
        }
        attackCollider.SetActive(true);

        canAttack = false;
        isAttacking = true;

        yield return new WaitForSeconds(0.05f);

        attackCollider.SetActive(false);

        yield return new WaitForSeconds(ATKDelay);

        canAttack = true;
        isAttacking = false;
        yield return null;
    }
}
