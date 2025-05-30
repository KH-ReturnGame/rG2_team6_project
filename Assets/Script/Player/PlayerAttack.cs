using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovemnt playerMovement;
    float ATKDelay = 0.5f;
    GameObject attackCollider;
    Transform attackTransform;

    bool canAttack = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovemnt>();
        attackCollider = transform.GetChild(1).gameObject;
        attackTransform = attackCollider.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if (playerMovement.spriteRenderer.flipX)
        {
            attackTransform.localPosition = new Vector3(-1f, 0f, 0f);
        }
        else
        {
            attackTransform.localPosition = new Vector3(1f, 0f, 0f);            
        }
        attackCollider.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        attackCollider.SetActive(false);
        canAttack = false;

        yield return new WaitForSeconds(ATKDelay);

        canAttack = true;
        yield return null;
    }
}
