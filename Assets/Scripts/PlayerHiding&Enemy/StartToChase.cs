using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StartToChase : MonoBehaviour
{

    EnemyMove EnemyMove;
    NavMeshAgent NavMeshAgent;
    Animator anim;

    public bool isNearBy = false;
    // Start is called before the first frame update
    void Start()
    {
        EnemyMove = GetComponent<EnemyMove>();
        EnemyMove.enabled = false;

        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.enabled = false;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearBy)
        {
            anim.SetBool("IsNearBy",true);//일어서기 시작
            anim.SetBool("StartToWalk", true);

        }
    }

}
