using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//���(agent=enemy)���� �������� �˷��༭ �������� �̵�.

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
    public bool isChase;
    public bool isAttack;
    public bool isDead = false;

    public BoxCollider meleeArea;

    //gpt
    public float chaseRange = 15f;//�÷��̾ �ѱ� ������ �Ÿ�

    public float wanderRadius = 20f;//��ȸ�� �ݰ�
    public float wanderTimer; //��ȸ�� �ð� ����

    public float minWanderTimer = 3f; // �ּ� ��ȸ �ð�
    public float maxWanderTimer = 8f; // �ִ� ��ȸ �ð�
    private float timer;

    private float deadRange = 8f; // ��� �Ѵ� �Ÿ�->���� �濡 �ִ��� ���η� update
    public int EnemyRoomID = -1; //���� �ִ� ���� id. room ��ũ��Ʈ���� update�Ұ���

    private PlayerHiding playerController; // ��ũ��Ʈ �޾ƿ���
    private PlayerController playerC;
    private PlayerController P;

    public Camera ActivatedCamera;

    public Vector3 destination;
    void Awake()//�����Ҷ� ó����
    {
        target = player.transform;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;

        nav = GetComponent<NavMeshAgent>(); //agent
        
        anim = GetComponent<Animator>();

        playerController = target.GetComponent<PlayerHiding>();//�ٸ� ��ũ��Ʈ���� ��������
        P = FindObjectOfType<PlayerController>();
        

        Invoke("WanderStart", 2);//chasestart 2�� �Ŀ�

        timer = minWanderTimer;//gpt

        destination = target.position; //�� �ʱⰪ ����

        ActivatedCamera =  player.GetComponentInChildren<Camera>();
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

        FreezeVelocity();

        anim.SetBool("IsWander", true);//->wander ���� ����

    }

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        
        
        //�Ѵ� �Ÿ����� �۾�����&&1�϶� -> �ѱ�. 2�� �ѱ� ����.. �ٵ� �����Ÿ����� ���������? 2���� ����
        if (distanceToPlayer <= 16.0f ) //�����Ÿ��� �� �����϶� �Ÿ� ��� ����
        {
            //��ΰ�����
            NavMeshPath path = new NavMeshPath();//���ο� ��ü ����

            if (nav.CalculatePath(target.position, path)) // �̰� �ȵǸ� ��ȿ�� ���� ���°���
            {

                
                // ��� ���� ���
                float pathLength = GetPathLength(path);

                // ��� ���̰� ���� ���� �̳���� �÷��̾ �Ѿư�
                if (pathLength <= chaseRange && (playerController.isPlayer1Active || isDead))
                {
                    // �÷��̾� ����->chase
                    
                    nav.SetDestination(target.position);
                    isChase = true;
                    anim.SetBool("IsWalk", true);

                    Targerting();//�ѱ�
                    FreezeVelocity();
                    
                    if (EnemyRoomID == playerController.playerRoomID)//�Ѵ� ���� ���� �濡 �ִ���?
                    {
                        isDead = true;
                        target = ActivatedCamera.transform; //-> ���� �濡 ������ Ȱ��ȭ�� ī�޶� Ÿ������
                        //Debug.Log(target);
                        
                        
                    }
                    else//���� �濡 ����? -> �׳� player�� ��ġ�� Ÿ������
                    {
                        isDead = false;
                        target = player.transform;
                    }
                }
                else //�׳� ��ȸ ����. ���� ��ġ ���� -> �̵� �ݺ�
                {
                    //Debug.Log("Is wander");
                    // ��ȸ ����
                    //timer += Time.deltaTime;

                    //isChase = false; -> �� ������ �� ���� �����Ϸ��� ���

                    anim.SetBool("IsWalk", false);

                    nav.speed = 1.5f;//�ȴ� �ӵ� �ٲ��ֱ�

                    

                    //if (timer >= wanderTimer)
                    if (isChase)
                    {
                        isChase = false;
                        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                        destination = newPos;
                        nav.SetDestination(destination);
                        //SetRandomWanderTimer(); // ���ο� Ÿ�̸� ����
                        //timer = 0;
                    }

                    if (destination == transform.position)//����������
                    {
                        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                        destination = newPos;
                        nav.SetDestination(destination);
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
    /*
    void SetRandomWanderTimer()
    {
        wanderTimer = UnityEngine.Random.Range(minWanderTimer, maxWanderTimer);
    }
    */

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void Targerting()//1->�ݺ�
    {
       // float targetRadius = 1.0f;
        //float targetRange = 1.5f;

        nav.speed = 6f; // �Ѵ� �ӵ�

        //RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player")); //Player ��ü�� ���ϴ� �ֵ������ �Ÿ� ����
        //��ü�� ��������� []���⿡ �������� 
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
        yield return new WaitForSeconds(0.2f);
        Debug.Log("meleeArea enabled");
        //���ݹ���Ȱ��ȭ
        meleeArea.enabled = true;

        //delay �ֱ�
        yield return new WaitForSeconds(1f);
        //���ݹ�����Ȱ��ȭ
        meleeArea.enabled = false;
        Debug.Log("meleeArea unabled");

        //����Ǯ��
        isChase = true;
        isAttack = false;
        nav.isStopped = false;

        //animation ȣ��
        anim.SetBool("IsAttack", false);

        Debug.Log("end Attack");
    }

}


