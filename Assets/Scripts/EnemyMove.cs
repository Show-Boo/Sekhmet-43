using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//요원(agent=enemy)에게 목적지를 알려줘서 목적지로 이동.
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

        Invoke("ChaseStart", 2);//chasestart 2초 후에
    }

    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.angularVelocity = Vector3.zero;//enemy와 player 충돌시 속도 변화 방지
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
        if (nav.enabled)//이게 뭔소린지 모르겠음..
        {
            nav.SetDestination(target.position);//추적 게시
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
        //정지
        isChase =false;
        isAttack = true;
        //animation 호출
        anim.SetBool("IsAttack",true);
        //delay 주기
        yield return new WaitForSeconds(0.2f);

        //공격범위활성화
        meleeArea.enabled = true;

        //delay 주기
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        //정지
        isChase = true;
        isAttack = false;
        //animation 호출
        anim.SetBool("IsAttack", false);

    }
    void FixedUpdate()
    {
        Targerting();
        FreezeVelocity();
    }
}


