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
        if (isChase)
        {
            nav.SetDestination(target.position);//���� �Խ�
        }
        

    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
}


