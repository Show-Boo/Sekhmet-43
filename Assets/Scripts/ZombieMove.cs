using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class ZombieMove : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //agent(���󰡴� ��)����
        agent = GetComponent<NavMeshAgent>();
        //player ã��
        target = GameObject.Find("Player").transform;
        //agent���� ���󰡶��
        agent.destination = target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
