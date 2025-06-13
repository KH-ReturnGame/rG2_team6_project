using UnityEngine;


public class Enemy_move : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    private bool isGrounded;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("Think", 2);
    }

    void FixedUpdate()
    {
        //움직익
        rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocity.y);

        //밑에 레이저 쏴서 플랫폼 체크
        Vector2 forntVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(forntVec, Vector3.down * 5, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayhit.collider == null)
        {
            nextMove *= -1;
            Debug.Log("lost");

        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 2);
    }
}
