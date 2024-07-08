using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//���(agent=enemy)���� �������� �˷��༭ �������� �̵�.
public class Enemy : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;
     
    public Transform target; //player�� ��ġ
    public bool isWander;
    public bool isChase;
    public bool isAttack;
    public bool isDead = false;

    public BoxCollider meleeArea;

    //gpt
    public float chaseRange = 10f;//�÷��̾ �ѱ� ������ �Ÿ�
    public float wanderRadius = 20f;//��ȸ�� �ݰ�
    public float wanderTimer; //��ȸ�� �ð� ����

    public float minWanderTimer = 3f; // �ּ� ��ȸ �ð�
    public float maxWanderTimer = 8f; // �ִ� ��ȸ �ð�
    private float timer;

    private float deadRange = 5f; // ��� �Ѵ� �Ÿ�

    private PlayerHiding playerController; // ��ũ��Ʈ �޾ƿ���
    void Awake()//�����Ҷ� ó����
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>(); //agent
        anim = GetComponent<Animator>();

        playerController = target.GetComponent<PlayerHiding>();//�ٸ� ��ũ��Ʈ���� ��������
        
        Invoke("WanderStart", 2);//chasestart 2�� �Ŀ�

        timer = minWanderTimer;//gpt
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

        anim.SetBool("IsWander", true);//->wander ���� ����

    }

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        
        
        //�Ѵ� �Ÿ����� �۾�����, 1�϶� -> �ѱ�. 2�� �ѱ� ����.. �ٵ� �����Ÿ����� ���������? 2���� ����
        if (distanceToPlayer <= chaseRange && (playerController.isPlayer1Active||isDead))
        {
            
            // �÷��̾� ����->chase
            nav.SetDestination(target.position);
            isChase = true;
            anim.SetBool("IsWalk", true);
            
            Targerting();//�ѱ�
            FreezeVelocity();

            if (distanceToPlayer<= deadRange)//�Ѵ� ���� �����̿� �ִ���?
            {
                isDead = true;
            }

            else
            {
                isDead=false;
            }

        }

        else
        {
            
            // ��ȸ ����
            timer += Time.deltaTime;

            isChase = false;
            anim.SetBool("IsWalk", false);

            nav.speed = 0.8f;//�̵��ð� �ٲ��ֱ�

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                nav.SetDestination(newPos);
                SetRandomWanderTimer();
                timer = 0;

            }
        }

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

    void Targerting()//1->�ݺ�
    {
        float targetRadius = 0.5f;
        float targetRange = 1.4f;

        nav.speed = 6f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        //����
        isChase =false;
        isAttack = true;
        nav.isStopped = true;
        //animation ȣ��
        anim.SetBool("IsAttack",true);
        //delay �ֱ�
        yield return new WaitForSeconds(0.2f);

        //���ݹ���Ȱ��ȭ
        meleeArea.enabled = true;

        //delay �ֱ�
        yield return new WaitForSeconds(1f);
        //���ݹ�����Ȱ��ȭ
        meleeArea.enabled = false;

        //����Ǯ��
        isChase = true;
        isAttack = false;
        nav.isStopped = false;

        //animation ȣ��
        anim.SetBool("IsAttack", false);

    }
   

    void Dead()
    {

    }
}


