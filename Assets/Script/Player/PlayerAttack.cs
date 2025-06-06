using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovemnt playerMovement;
    float ATKDelay = 0.5f;
    GameObject attackCollider;
    Transform attackTransform;

    bool canAttack = true;
    bool canShoot = true;
    float SHOOTDelay = 2f;
    public GameObject bulletPrefab;
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

        if(Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            StartCoroutine(ShotBullet());
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

    IEnumerator ShotBullet()
    {
        canShoot = false;

        GameObject instance = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));

        Bullet bullet = instance.GetComponent<Bullet>();
        int x = playerMovement.spriteRenderer.flipX ? -1 : 1;
        bullet.directionVec = new Vector2(x, 0);

        if (x == -1)
        {
            SpriteRenderer sr = instance.GetComponent<SpriteRenderer>();
            sr.flipX = true;    
        }
        

        yield return new WaitForSeconds(SHOOTDelay);

        canShoot = true;

        yield return null;
    }
}
