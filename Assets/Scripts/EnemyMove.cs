using System;
using System.Collections;
using System.Collections.Generic;
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
     
    public Transform target;
    public bool isChase;
    public bool isAttack;
    public BoxCollider meleeArea;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        Invoke("ChaseStart", 2);//chasestart 2�� �Ŀ�
    }

    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.angularVelocity = Vector3.zero;//enemy�� player �浹�� �ӵ� ��ȭ ����
            rigid.velocity = Vector3.zero;
        }
        
    }

    void ChaseStart()
    {
        isChase = true;

        anim.SetBool("IsWalk", true);
    }
    void Update()
    {
        if (nav.enabled)//�̰� ���Ҹ��� �𸣰���..
        {
            nav.SetDestination(target.position);//���� �Խ�
            nav.isStopped = !isChase;
        }

    }

    void Targerting()
    {
        float targetRadius = 0.7f;
        float targetRange = 1.4f;
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
        //animation ȣ��
        anim.SetBool("IsAttack",true);
        //delay �ֱ�
        yield return new WaitForSeconds(0.2f);

        //���ݹ���Ȱ��ȭ
        meleeArea.enabled = true;

        //delay �ֱ�
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        //����
        isChase = true;
        isAttack = false;
        //animation ȣ��
        anim.SetBool("IsAttack", false);

    }
    void FixedUpdate()
    {
        Targerting();
        FreezeVelocity();
    }
}


