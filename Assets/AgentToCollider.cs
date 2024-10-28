using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentToCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform targetTransform; // 이동할 목표 위치가 있는 Collider의 Transform
    private EnemyMove EnemyMove;

    private void Start()
    {
        EnemyMove = agent.GetComponent<EnemyMove>();
    }

    void Update()
    {
        
            if (EnemyMove.PlayerDead && !EnemyMove.restart)
            {
                agent.Warp(targetTransform.position); // 목표 위치로 즉시 한 번 이동
                EnemyMove.restart = true;
            }
        
        
    }
}
