using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


//agent=enemy에게 목적지를 알려줘서 목적지로 이동.
// 적과 player가 일정 거리 안에 들어왔을때 추적. 근데 방이 다를때 숨으면 추적 종료.


//문제 1. 숨으면 다닐 수 없는 길 위에 있다고 생각함. -> 일단 옮겨둠
//문제 2. 카메라 렌더링 문제 -> 부성쓰 말대로 하니까 성공
//문제 3. 숨고나서 돌아가기
public class EnemyMove : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;
    public Transform player;
    public Transform target; //player의 위치
    public bool isWander;
    public bool isChase = true;
    public bool isAttack;
    public bool isDead = true;

    public Collider meleeArea;
    public GameObject deathCutscene;

    //gpt
    public float chaseRange = 15f;//플레이어를 쫓기 시작할 거리

    public float wanderRadius = 20f;//배회할 반경

    //private float deadRange = 8f; // 숨어도 쫓는 거리->같은 방에 있는지 여부로 update
    public int EnemyRoomID = -1; //적이 있는 방의 id. room 스크립트에서 update할거임

    private PlayerHiding playerController; // 스크립트 받아오기

    private PlayerController playerC;
    private PlayerController P;

    public Camera ActivatedCamera;

    public Vector3 destination;

    public bool playedSound = false;//소리를 울렸었는지
    public bool PlayerDead = false;
    
    public VideoPlayer DeadCutScene;//죽는 컷씻

    public string[] navMeshAreaNames;//enemy가 돌아다닐 수 있는 범위
    private int combinedAreaMask;//area여러개를 합치는..? 그런 int

    private NavMeshPath path;

    public bool retry = false;
    void Awake()//시작할때 처음만
    {
       // meleeArea.GetComponent<Collider>().isTrigger = true;
        meleeArea.enabled = false;

        deathCutscene.SetActive(false);

        target = player.GetComponentInChildren<Camera>().transform;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;

        nav = GetComponent<NavMeshAgent>(); //agent
        
        anim = GetComponent<Animator>();

        playerController = player.GetComponent<PlayerHiding>();//다른 스크립트에서 가져오기

        P = FindObjectOfType<PlayerController>();

        combinedAreaMask = 0;
        //area여러개 합치기
        foreach (string areaName in navMeshAreaNames)
        {
            combinedAreaMask |= 1 << NavMesh.GetAreaFromName(areaName);
        }
        //Invoke("WanderStart", 2);//chasestart 2초 후에
        WanderStart();
        rigid.velocity = Vector3.zero; // 이동속도 멈추기


        destination = target.position; //걍 초기값 설정

        ActivatedCamera =  player.GetComponentInChildren<Camera>();
        
        DeadCutScene.started += CutSceneStart;
        DeadCutScene.loopPointReached += CutSceneEnd;

        path = new NavMeshPath();

    }
    void CutSceneStart(VideoPlayer vp)
    {
        
    }

    void CutSceneEnd(VideoPlayer vp)
    {
        deathCutscene.SetActive(false);
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

        //FreezeVelocity();
        
        
        anim.SetBool("IsWander", true);//->wander 동작 실행

    }

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);//단순 직선거리

        //isDead=true로 시작

        if (EnemyRoomID != playerController.playerRoomID && !playerController.isPlayer1Active)//enemy랑 다른 방에서 숨으면 
        {
            isDead = false; //걍 wander
            
            //target = ActivatedCamera.transform; //-> 같은 방에 있으면 활성화된 카메라를 타깃으로
            
        }//->wandering만 함ㅁ..
        else//일반적인 경우 -> 걍 active된 애 찾아서 죽임
        {
            
            
            if (isDead == false)//숨어있는 상황
            {
                if (playerController.isPlayer1Active)//숨지 않음. 같은 방에 있게 되더라도 isDead false 유지
                {
                    isDead = true;//isDead 돌리는 case. 계속 olayer와의 거리 계산
                }
            }
            else
            {
                isDead = true;
            }
            
            
            //target = player.transform;
        }

        //Debug.Log("isDead : " + isDead);

       

        //NavMeshPath path = new NavMeshPath();//새로운 객체 생성->전역변수로 설정해줌
        nav.CalculatePath(target.position, path);

        if (!retry)
        {
            if (path.status == NavMeshPathStatus.PathComplete) // 이게 안되면 유효한 길이 없는거임. 또는 player가 다른 층에 있는 경우
            {
                // 경로 길이 계산
                float pathLength = GetPathLength(path);//player가 다른 층에 있는 경우 이게 0으로 반환됨..

                // 경로 길이가 추적 범위 이내라면 플레이어를 쫓아감
                if (pathLength <= chaseRange && isDead && !PlayerDead)//player1을 쫓는 경우. isDead = 같은 방에 있는지 여부/isPlayerDead  죽엇는지
                {
                    // 플레이어 추적->chase
                    //target = ActivatedCamera.transform;
                    nav.SetDestination(target.position);
                    isChase = true;
                    anim.SetBool("IsWalk", true);//쫓는 액션

                    Targerting();//쫓기
                    FreezeVelocity();
                    //PlaySound();//한 번만 울리게

                }
                else//isDead가 false인 경우(숨은 경우), 추적범위 이내가 아닌 경우
                {
                    Wandering();
                }

                if (distanceToPlayer <= 10.0f)//근방에 enemy있을때 심장소리
                {
                    //if (playerController.HeartBeatPlaying == false){
                    playerController.HeartBeatPlaying = true;


                    //Debug.Log("distance to player is less then 10.0");
                }
                else
                {
                    //if (playerController.HeartBeatPlaying == true)

                    playerController.HeartBeatPlaying = false;


                    //Debug.Log("distance to player is more then 10.0");
                }
            }
            else//player가 다른 층에 있는 경우
            {
                Wandering();
            }
        }
        else
        {
            //가만히 있어야 함. 이제 움직이지 않음
        }

        
        
    }

    void Wandering()
    {
         //그냥 배회 로직. 랜덤 위치 설정 -> 이동 반복
        
         

            //isChase = false; -> 밑 로직을 한 번만 수행하려는 노력

            anim.SetBool("IsWalk", false);
        playedSound = false;

            
            nav.speed = 1.5f;//걷는 속도 바꿔주기


            float distanceThreshold = 3.0f; //근방에 도달하면 complete

            //if (timer >= wanderTimer)
            if (isChase)//처음 돌아다니기 시작하는 경우
            {
                isChase = false;
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, combinedAreaMask);
                destination = newPos;
                nav.SetDestination(destination);

                //SetRandomWanderTimer(); // 새로운 타이머 설정
                //timer = 0;
            }

            NavMeshPath pathToDestination = new NavMeshPath();
            nav.CalculatePath(target.position, pathToDestination);

            if ((Vector3.Distance(destination, transform.position) <= distanceThreshold) /*|| (pathToDestination.status != NavMeshPathStatus.PathComplete)*/)//근방에 도달했는지? 또는 destination이 bake된 길 위에 있는지?
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, combinedAreaMask);
                destination = newPos;
                nav.SetDestination(destination);
            }

            //Debug.Log(Vector3.Distance(destination, transform.position));
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
    
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int areaMask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;

        if (NavMesh.SamplePosition(randDirection, out navHit, dist, areaMask))
        {
            return navHit.position; // 유효한 위치 반환
        }
        else
        {
            return origin; // 유효한 위치를 찾지 못하면 원래 위치 반환
        }
    }


    void Targerting()//1->반복
    {
       // float targetRadius = 1.0f;
        //float targetRange = 1.5f;

        nav.speed = 3f; // 쫓는 속도 6이면 player보다 빠름

        //RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player")); //Player 객체에 속하는 애들까지의 거리 측정
        //객체가 여러개라면 []여기에 정보저장 
        //target = ActivatedCamera.transform;
      
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);//원래 target이엇음

        playerController.isBeating = true;
        
        if (distanceToTarget < 2.0f)
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
        yield return new WaitForSeconds(0.5f); // 1.0엿음
        //Debug.Log("meleeArea enabled");
        //공격범위활성화
        meleeArea.enabled = true;
        
        //delay 주기
        yield return new WaitForSeconds(2.0f);
        //공격범위비활성화
        meleeArea.enabled = false;
        //Debug.Log("meleeArea unabled");

        //정지풀기
        isChase = true;
        isAttack = false;
        nav.isStopped = false;

        //animation 호출
        anim.SetBool("IsAttack", false);

        //Debug.Log("end Attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.GetComponentInChildren<Camera>()==target);//여기서 false찍힘 -> 타겟은 transform이니까!!!!!!!!!!!!!!

        if ((other.GetComponentInChildren<Camera>() == target.GetComponentInChildren<Camera>()) && !PlayerDead)//원래는 compare tag엿음
        {
            // 플레이어가 공격에 맞았을 때 죽는 컷신 재생
            PlayerDead = true;
            TriggerDeathCutscene();
        }
    }

    void TriggerDeathCutscene()
    {
        
        // 플레이어 사망 처리
        
        //틀어지는 카메라 바꿔줘야함
        DeadCutScene.targetCamera = target.GetComponentInChildren<Camera>();//player로 고정해주기..
        
        
        // 플레이어1죽는 컷신 실행
        if (DeadCutScene.targetCamera.isActiveAndEnabled)
        {
            deathCutscene.SetActive(true);
            //isPlayerDead = false;//추가
        }
        else
        {
            Debug.LogError("카메라 비활성화");
        }
        
        // 게임 오버 처리 등을 추가할 수 있음
    }
    /*
    void PlaySound()
    {
        // 소리가 틀어진 적 없다면 틀어짐
        if (!playedSound)
        {
            audioSource.Play();
        }
    }

    void endSound()
    {
        audioSource.Stop();
    }
    */

}


