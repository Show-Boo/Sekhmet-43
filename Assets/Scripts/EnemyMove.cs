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
        if (isChase)
        {
            nav.SetDestination(target.position);//추적 게시
        }
        

    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
}


