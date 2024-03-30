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
        //agent(따라가는 애)정의
        agent = GetComponent<NavMeshAgent>();
        //player 찾기
        target = GameObject.Find("Player").transform;
        //agent에게 따라가라고
        agent.destination = target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
