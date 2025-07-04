using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    private bool isMoving = true;
    
    
    //행동 상태를 나타내는 변수
    //왼쪽 이동은 -1, 정지는 0, 오른쪽 이동은 1로 나타낼 것이다.

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Think();    //오브젝트가 생성될 때, 함수가 실행되면서 랜덤값 결정
        StartCoroutine(ThinkCor());
    }

    void FixedUpdate()
    {
        //Move
        rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + 1f * nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
        }
    }   

    IEnumerator ThinkCor()
    {
        nextMove = Random.Range(-1, 2);
        //Random : 랜덤 수를 생성하는 로직 관련 클래스
        //Range() : 최소~최대 범위의 랜덤 수 생성 (최대 제외)

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);

        yield return new WaitForSeconds(3.0f);

        StartCoroutine(ThinkCor());

        yield return null;
        //업데이트체크용11
    }
}