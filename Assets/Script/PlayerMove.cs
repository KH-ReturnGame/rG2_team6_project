using UnityEngine;
using System.Collections;
public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
    }
    /*IEnumerator Logonetwo()
    {
        Debug.Log("1초");
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime);

        Debug.Log("2초");
        yield return null;
    } */
}
