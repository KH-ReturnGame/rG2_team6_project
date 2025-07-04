using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float pursuitSpeed = 3f; // 추격 속도
    public float wanderSpeed = 1f; // 배회 속도
    public float currentSpeed; // 현재 속도

    public float directionChangeInterval = 2f; // 배회 시 방향 전환 주기
    public float wanderMoveDistance = 2f; // 배회 시 이동 거리

    public float detectRadius = 8.0f; // 플레이어 감지 반경

    public GameObject plr; // 플레이어 오브젝트 참조

    private bool isPlayerInSight; // 플레이어 감지 여부

    LayerMask playerMask; // 플레이어 레이어
    LayerMask groundMask; // 땅 레이어 (IsGroundAhead에서 사용)

    Coroutine moveCoroutine;
    CircleCollider2D circleCollider2D;
    Rigidbody2D rb;
    Animator animator;

    Transform targetTransform = null; // 추격 대상 (플레이어)
    Vector3 endPosition; // 배회 시 목표 위치

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        
        plr = GameObject.FindWithTag("Player"); // Tag로 플레이어를 찾는 것이 더 안전합니다.
        playerMask = LayerMask.GetMask("Player");
        groundMask = LayerMask.GetMask("Ground"); // Ground Layer를 설정해주세요.

        // 초기 상태는 배회
        currentSpeed = wanderSpeed;
        StartCoroutine(EnemyRoutine()); // 새로운 메인 코루틴 시작
    }

    void Update()
    {
        // Debugging
        Debug.DrawLine(rb.position, endPosition, Color.red);
        Debug.DrawRay(transform.position + Vector3.right * Mathf.Sign(endPosition.x - transform.position.x) * 0.5f, Vector2.down * 1f, Color.green);

        // 매 프레임 플레이어 감지 여부 확인
        isPlayerInSight = Physics2D.OverlapCircle(transform.position, detectRadius, playerMask);
    }

    // --- 새로운 메인 코루틴 ---
    IEnumerator EnemyRoutine()
    {
        while (true)
        {
            if (isPlayerInSight && plr != null)
            {
                // 플레이어가 시야 내에 있으면 추격
                currentSpeed = pursuitSpeed;
                targetTransform = plr.transform; // 플레이어를 추격 대상으로 설정

                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(MoveToTarget(rb, currentSpeed, targetTransform));
                
                yield return null; // 다음 프레임까지 기다림 (플레이어가 시야 내에 있는 동안 계속 추적)
            }
            else
            {
                // 플레이어가 시야 내에 없으면 배회
                currentSpeed = wanderSpeed;
                targetTransform = null; // 추적 대상 초기화

                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(Wander()); // 배회 코루틴 시작

                // 배회 중에는 방향 전환 주기를 기다림
                yield return new WaitForSeconds(directionChangeInterval);
            }
            yield return null; // 다음 루프를 위해 다음 프레임까지 기다림
        }
    }

    // --- 배회 로직 코루틴 ---
    IEnumerator Wander()
    {
        ChooseNewEndPoint(); // 새로운 목표 지점 선택

        // 목표 지점까지 이동
        yield return StartCoroutine(MoveToPosition(rb, currentSpeed, endPosition));
    }

    void ChooseNewEndPoint()
    {
        // 좌우 방향 중 랜덤 선택
        float direction = Random.Range(0, 2) == 0 ? -1f : 1f;
        endPosition = transform.position + new Vector3(direction * wanderMoveDistance, 0f, 0f);
    }

    // --- 특정 위치로 이동하는 코루틴 ---
    IEnumerator MoveToPosition(Rigidbody2D rigidBodyToMove, float speed, Vector3 targetPos)
    {
        float remainingDistance = (transform.position - targetPos).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            // 땅이 없으면 멈춤 (배회 중에만 해당)
            if (targetTransform == null && !IsGroundAhead())
            {
                animator.SetBool("isWalking", false);
                yield break;
            }

            if (rigidBodyToMove != null)
            {
                animator.SetBool("isWalking", true);

                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, targetPos, speed * Time.deltaTime);
                rb.MovePosition(newPosition);

                // 방향에 따라 스프라이트 뒤집기
                if (targetPos.x < transform.position.x)
                    transform.localScale = new Vector3(-1, 1, 1);
                else
                    transform.localScale = new Vector3(1, 1, 1);

                remainingDistance = (transform.position - targetPos).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }

    // --- 특정 Transform을 따라 이동하는 코루틴 (주로 플레이어 추격) ---
    IEnumerator MoveToTarget(Rigidbody2D rigidBodyToMove, float speed, Transform target)
    {
        while (target != null && isPlayerInSight) // 플레이어가 시야 내에 있는 동안 계속 추적
        {
            if (rigidBodyToMove != null)
            {
                animator.SetBool("isWalking", true);

                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, target.position, speed * Time.deltaTime);
                rb.MovePosition(newPosition);

                // 방향에 따라 스프라이트 뒤집기
                if (target.position.x < transform.position.x)
                    transform.localScale = new Vector3(-1, 1, 1);
                else
                    transform.localScale = new Vector3(1, 1, 1);
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }


    bool IsGroundAhead()
    {
        // 현재 바라보는 방향으로 지면이 있는지 확인
        // endPosition.x와 transform.position.x를 비교하여 에너미가 어느 방향으로 가고 있는지 확인
        float currentDirectionX = Mathf.Sign(endPosition.x - transform.position.x);

        // Raycast의 시작점을 에너미의 발 앞부분으로 조정
        Vector2 rayOrigin = new Vector2(transform.position.x + currentDirectionX * 0.5f, transform.position.y - (circleCollider2D.radius * transform.localScale.y)); // 발 아래쪽으로 조정 필요
        
        // Raycast를 아래로 쏴서 Ground 레이어와 충돌하는지 확인
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.2f, groundMask); // 짧은 거리로 확인
        
        Debug.DrawRay(rayOrigin, Vector2.down * 0.2f, Color.blue); // 디버그를 위한 Ray 그리기
        
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        // Collider의 반지름에 맞게 그리기 (만약 CircleCollider2D가 있다면)
        if (circleCollider2D != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, circleCollider2D.radius * transform.localScale.x);
        }
        
        // 감지 반경 그리기
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}