using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//요원(agent=enemy)에게 목적지를 알려줘서 목적지로 이동.

// 적과 player가 일정 거리 안에 들어왔을때 추적. 근데 방이 다를때 숨으면 추적 종료.
public class EnemyMove : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;
     
    public Transform target; //player의 위치
    public bool isWander;
    public bool isChase;
    public bool isAttack;
    public bool isDead = false;

    public BoxCollider meleeArea;

    //gpt
    public float chaseRange = 15f;//플레이어를 쫓기 시작할 거리

    public float wanderRadius = 20f;//배회할 반경
    public float wanderTimer; //배회할 시간 간격

    public float minWanderTimer = 3f; // 최소 배회 시간
    public float maxWanderTimer = 8f; // 최대 배회 시간
    private float timer;

    private float deadRange = 8f; // 숨어도 쫓는 거리->같은 방에 있는지 여부로 update
    public int EnemyRoomID = -1; //적이 있는 방의 id. room 스크립트에서 update할거임

    private PlayerHiding playerController; // 스크립트 받아오기
    private PlayerController playerC;
    private PlayerController P;

    public Camera ActivatedCamera;

    void Awake()//시작할때 처음만
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;

        nav = GetComponent<NavMeshAgent>(); //agent
        
        anim = GetComponent<Animator>();

        playerController = target.GetComponent<PlayerHiding>();//다른 스크립트에서 가져오기
        //P = FindObjectOfType<PlayerController>();


        Invoke("WanderStart", 2);//chasestart 2초 후에

        timer = minWanderTimer;//gpt

        
    }

    void FreezeVelocity()//1
    {
        if(isWander || isAttack)
        {
            rigid.angularVelocity = Vector3.zero;//enemy와 player 충돌시 속도 변화 방지

            rigid.velocity = Vector3.zero;
        }
        
    }

    void WanderStart()//첫 실행
    {
        isWander = true;

        anim.SetBool("IsWander", true);//->wander 동작 실행

    }

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        
        
        //쫓는 거리보다 작아질때&&1일때 -> 쫓기. 2면 쫓기 멈춤.. 근데 일정거리보다 가까워진다? 2여도 죽음
        if (distanceToPlayer <= chaseRange )
        {
            
            //경로계산시작
            NavMeshPath path = new NavMeshPath();//새로운 객체 생성

            if (nav.CalculatePath(target.position, path))
            {
                // 경로 길이 계산
                float pathLength = GetPathLength(path);

                // 경로 길이가 추적 범위 이내라면 플레이어를 쫓아감
                if (pathLength <= chaseRange && (playerController.isPlayer1Active || isDead))
                {
                    // 플레이어 추적->chase
                    nav.SetDestination(target.position);
                    isChase = true;
                    anim.SetBool("IsWalk", true);

                    Targerting();//쫓기
                    FreezeVelocity();
                    

                    if (EnemyRoomID == playerController.playerRoomID)//쫓는 동안 가까이에 있는지?
                    {
                        isDead = true;
                        target = ActivatedCamera.transform;
                        //타겟 바꿔주기
                    }
                    else
                    {
                        isDead = false;
                        target = P.transform; 
                    }
                }

                else
                {
                    Debug.Log("Is wander");
                    // 배회 로직
                    timer += Time.deltaTime;

                    isChase = false;
                    anim.SetBool("IsWalk", false);

                    nav.speed = 1.0f;//이동시간 바꿔주기

                    if (timer >= wanderTimer)
                    {
                        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                        nav.SetDestination(newPos);
                        SetRandomWanderTimer();
                        timer = 0;

                    }
                }

            }
           

            

        }

        

    }

    float GetPathLength(NavMeshPath path)
    {
        float length = 0.0f;
        for (int i = 1; i < path.corners.Length; i++)
        {
            length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
        return length;
    }

    void SetRandomWanderTimer()
    {
        wanderTimer = UnityEngine.Random.Range(minWanderTimer, maxWanderTimer);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void Targerting()//1->반복
    {
        float targetRadius = 1.0f;
        float targetRange = 1.5f;

        nav.speed = 6f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        playerController.isBeating = true;
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        //정지
        isChase =false;
        isAttack = true;
        nav.isStopped = true;
        //animation 호출
        anim.SetBool("IsAttack",true);
        //delay 주기
        yield return new WaitForSeconds(0.2f);

        //공격범위활성화
        meleeArea.enabled = true;

        //delay 주기
        yield return new WaitForSeconds(1f);
        //공격범위비활성화
        meleeArea.enabled = false;

        //정지풀기
        isChase = true;
        isAttack = false;
        nav.isStopped = false;

        //animation 호출
        anim.SetBool("IsAttack", false);

    }

    

}


