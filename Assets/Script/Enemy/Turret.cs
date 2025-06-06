using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    public GameObject FireBullet;
    public Transform PlayerTransform;
    public bool isDetected = false;
    bool isAttacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDetected && !isAttacking)
        {
            StartCoroutine(Fire());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        yield return null;
    }

    IEnumerator Fire()
    {
        GameObject instance = Instantiate(FireBullet, transform.position, Quaternion.Euler(0, 0, AngleCalculate(PlayerTransform)));
        Bullet bullet = instance.GetComponent<Bullet>();
        bullet.directionVec = (PlayerTransform.position - transform.position).normalized;
        isAttacking = true;

        yield return new WaitForSeconds(1.25f);

        isAttacking = false;

        yield return null;
    }

    float AngleCalculate(Transform tr)
    {
        Vector2 dir = (tr.position - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return angle;
    }
}
