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


//agent=enemy���� �������� �˷��༭ �������� �̵�.
// ���� player�� ���� �Ÿ� �ȿ� �������� ����. �ٵ� ���� �ٸ��� ������ ���� ����.
public class EnemyMove : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;
    public Transform player;
    public Transform target; //player�� ��ġ
    public bool isWander;
    public bool isChase = true;
    public bool isAttack;
    public bool isDead = true;

    public Collider meleeArea;
    public GameObject deathCutscene;

    //gpt
    public float chaseRange = 15f;//�÷��̾ �ѱ� ������ �Ÿ�

    public float wanderRadius = 20f;//��ȸ�� �ݰ�

    private float deadRange = 8f; // ��� �Ѵ� �Ÿ�->���� �濡 �ִ��� ���η� update
    public int EnemyRoomID = -1; //���� �ִ� ���� id. room ��ũ��Ʈ���� update�Ұ���

    private PlayerHiding playerController; // ��ũ��Ʈ �޾ƿ���
    private PlayerController playerC;
    private PlayerController P;

    public Camera ActivatedCamera;

    public Vector3 destination;

    public AudioClip soundClip; // ����� �Ҹ� �޾ƿ�
    private AudioSource audioSource;

    public bool playedSound = false;//�Ҹ��� ��Ⱦ�����
    public bool isPlayerDead = false;
    //public PostProcessVolume postProcessVolume;

    public VideoPlayer DeadCutScene;//�״� �ƾ�

    public string[] navMeshAreaNames;//enemy�� ���ƴٴ� �� �ִ� ����
    private int combinedAreaMask;//area�������� ��ġ��..? �׷� int
    void Awake()//�����Ҷ� ó����
    {
        meleeArea.GetComponent<Collider>().isTrigger = true;
        meleeArea.enabled = false;
        deathCutscene.SetActive(false);

        target = player.transform;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;

        nav = GetComponent<NavMeshAgent>(); //agent
        
        anim = GetComponent<Animator>();

        playerController = target.GetComponent<PlayerHiding>();//�ٸ� ��ũ��Ʈ���� ��������

        P = FindObjectOfType<PlayerController>();

        combinedAreaMask = 0;
        //area������ ��ġ��
        foreach (string areaName in navMeshAreaNames)
        {
            combinedAreaMask |= 1 << NavMesh.GetAreaFromName(areaName);
        }

        //Invoke("WanderStart", 2);//chasestart 2�� �Ŀ�
        WanderStart();
        rigid.velocity = Vector3.zero; // �̵��ӵ� ���߱�

        //timer = minWanderTimer;//gpt

        destination = target.position; //�� �ʱⰪ ����

        ActivatedCamera =  player.GetComponentInChildren<Camera>();

        // AudioSource ������Ʈ�� �����ɴϴ�.
        audioSource = GetComponent<AudioSource>();
        // �Ҹ� Ŭ���� �����մϴ�.
        audioSource.clip = soundClip;
        // Play On Awake �ɼ��� ���ϴ� (���� ���� �� �ڵ� ��� ����)
        audioSource.playOnAwake = false;

        DeadCutScene.started += CutSceneStart;
        DeadCutScene.loopPointReached += CutSceneEnd;

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
            rigid.angularVelocity = Vector3.zero;//enemy�� player �浹�� �ӵ� ��ȭ ����

            rigid.velocity = Vector3.zero;
        }
        
    }

    void WanderStart()//ù ����
    {
        isWander = true;

        //FreezeVelocity();
        
        
        anim.SetBool("IsWander", true);//->wander ���� ����

    }

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);//�ܼ� �����Ÿ�

        //isDead=true�� ����

        if (EnemyRoomID != playerController.playerRoomID && !playerController.isPlayer1Active)//enemy�� �ٸ� �濡�� ������ 
        {
            isDead = false; //�� wander
            
            //target = ActivatedCamera.transform; //-> ���� �濡 ������ Ȱ��ȭ�� ī�޶� Ÿ������
            
        }//->wandering�� �Ԥ�..
        else//�Ϲ����� ��� -> �� active�� �� ã�Ƽ� ����
        {
            if (isDead == false)
            {
                if (playerController.isPlayer1Active)//���� ����
                {
                    isDead = true;//isDead ������ case. ��� olayer���� �Ÿ� ���
                }
            }
            else
            {
                isDead = true;
            }
            
            //target = player.transform;
        }

        //Debug.Log("isDead : " + isDead);

        //-----------------------------------------------------------------------------------------------------------------------------------
        //�Ѵ� �Ÿ����� �۾�����&&1�϶� -> �ѱ�. 2��(������) �ѱ� ����.. �ٵ� �������̴�? 2���� ����
        //if (distanceToPlayer <= 16.0f ) //�����Ÿ��� �� �����϶� �Ÿ� ��� ����..�ٵ� �̷��� �ٸ� ���϶� ������..
        //{
            //��ΰ�����

            //Debug.Log("1");

        NavMeshPath path = new NavMeshPath();//���ο� ��ü ����
        nav.CalculatePath(target.position, path);

        if (path.status == NavMeshPathStatus.PathComplete) // �̰� �ȵǸ� ��ȿ�� ���� ���°���. �Ǵ� player�� �ٸ� ���� �ִ� ���
        {

            // ��� ���� ���
            float pathLength = GetPathLength(path);//player�� �ٸ� ���� �ִ� ��� �̰� 0���� ��ȯ��..

            // ��� ���̰� ���� ���� �̳���� �÷��̾ �Ѿư�
            if (pathLength <= chaseRange && isDead)//player1�� �Ѵ� ���
            {
                // �÷��̾� ����->chase

                nav.SetDestination(target.position);
                isChase = true;
                anim.SetBool("IsWalk", true);//�Ѵ� �׼�

                Targerting();//�ѱ�
                FreezeVelocity();
                PlaySound();//�� ���� �︮��

            }
            else//isDead�� false�� ���(���� ���), �������� �̳��� �ƴ� ���
            {
                Wandering();
            }

            if (distanceToPlayer <= 10.0f)//�ٹ濡 enemy������ ����Ҹ�
            {
                //if (playerController.HeartBeatPlaying == false){
                playerController.HeartBeatPlaying = true;


                Debug.Log("distance to player is less then 10.0");
            }
            else
            {
                //if (playerController.HeartBeatPlaying == true)

                playerController.HeartBeatPlaying = false;


                Debug.Log("distance to player is more then 10.0");
            }
        }
        else//player�� �ٸ� ���� �ִ� ���
        {
            Wandering();
        }
        
    }

    void Wandering()
    {
         //�׳� ��ȸ ����. ���� ��ġ ���� -> �̵� �ݺ�
        
         

            //isChase = false; -> �� ������ �� ���� �����Ϸ��� ���

            anim.SetBool("IsWalk", false);
        playedSound = false;

            
            nav.speed = 1.5f;//�ȴ� �ӵ� �ٲ��ֱ�


            float distanceThreshold = 3.0f; //�ٹ濡 �����ϸ� complete

            //if (timer >= wanderTimer)
            if (isChase)//ó�� ���ƴٴϱ� �����ϴ� ���
            {
                isChase = false;
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, combinedAreaMask);
                destination = newPos;
                nav.SetDestination(destination);

                //SetRandomWanderTimer(); // ���ο� Ÿ�̸� ����
                //timer = 0;
            }

            NavMeshPath pathToDestination = new NavMeshPath();
            nav.CalculatePath(target.position, pathToDestination);

            if ((Vector3.Distance(destination, transform.position) <= distanceThreshold) /*|| (pathToDestination.status != NavMeshPathStatus.PathComplete)*/)//�ٹ濡 �����ߴ���? �Ǵ� destination�� bake�� �� ���� �ִ���?
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
    /*
    void SetRandomWanderTimer()
    {
        wanderTimer = UnityEngine.Random.Range(minWanderTimer, maxWanderTimer);
    }
    */

    /*public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    */

    //Ư�� area����� random�� ��ġ�� ��� �Լ�
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int areaMask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;

        if (NavMesh.SamplePosition(randDirection, out navHit, dist, areaMask))
        {
            return navHit.position; // ��ȿ�� ��ġ ��ȯ
        }
        else
        {
            return origin; // ��ȿ�� ��ġ�� ã�� ���ϸ� ���� ��ġ ��ȯ
        }
    }


    void Targerting()//1->�ݺ�
    {
       // float targetRadius = 1.0f;
        //float targetRange = 1.5f;

        nav.speed = 3f; // �Ѵ� �ӵ� 6�̸� player���� ����

        //RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player")); //Player ��ü�� ���ϴ� �ֵ������ �Ÿ� ����
        //��ü�� ��������� []���⿡ �������� 
        target = ActivatedCamera.transform;
      
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        playerController.isBeating = true;
        
        if (distanceToTarget < 2.0f)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {

        Debug.Log("Attack");
        //����
        isChase =false;
        isAttack = true;
        nav.isStopped = true;
        //animation ȣ��
        anim.SetBool("IsAttack",true);
        //delay �ֱ�
        yield return new WaitForSeconds(0.5f); // 1.0����
        //Debug.Log("meleeArea enabled");
        //���ݹ���Ȱ��ȭ
        meleeArea.enabled = true;
        
        //delay �ֱ�
        yield return new WaitForSeconds(2.0f);
        //���ݹ�����Ȱ��ȭ
        meleeArea.enabled = false;
        //Debug.Log("meleeArea unabled");

        //����Ǯ��
        isChase = true;
        isAttack = false;
        nav.isStopped = false;

        //animation ȣ��
        anim.SetBool("IsAttack", false);

        //Debug.Log("end Attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerDead)
        {
            // �÷��̾ ���ݿ� �¾��� �� �״� �ƽ� ���
            TriggerDeathCutscene();
        }
    }

    void TriggerDeathCutscene()
    {
        // �÷��̾� ��� ó��
        isPlayerDead = true;
        //Ʋ������ ī�޶� �ٲ������
        DeadCutScene.targetCamera = ActivatedCamera;//�� �ڵ尡 �´����� �𸣰���
        // �״� �ƽ� ����
        if (ActivatedCamera.isActiveAndEnabled)
        {
            deathCutscene.SetActive(true);
        }
        else
        {
            Debug.LogError("ī�޶� ��Ȱ��ȭ");
        }
        
        // ���� ���� ó�� ���� �߰��� �� ����
    }
    void PlaySound()
    {
        // �Ҹ��� Ʋ���� �� ���ٸ� Ʋ����
        if (!playedSound)
        {
            audioSource.Play();
        }
    }

    void endSound()
    {
        audioSource.Stop();
    }

}


